using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Models;
using workforce_management.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace workforce_management.Controllers
{
    /**
         * CLASS: EmployeeController
         * PURPOSE: Creates routes for main index view 
         * AUTHOR: Garrett Vangilder/Matt Kraatz
         * METHODS:
         *   Index() - View list of employees
         *   Detail() - View details for a single employee
         *   Add() - View form for adding a new employee
         *   Add(EmployeeForm) - Process form input for adding a new employee
         *   Edit(int) - View form for editing an existing employee
         *   Edit(EmployeeForm) - Process form input for editing an existing employee
         **/
    public class EmployeeController : Controller
    {
        private BangazonContext context;

        //Currently this controller attaches the database context to the employee controller
        public EmployeeController(BangazonContext ctx)
        {
            context = ctx;
        }

        /**
              * Purpose: Provides the index view
              * Arguments:
              *     N/A
              * Return:
              *     Returns the view that includes the employee list as well as the department name for the employee
              */
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new EmployeeList();
            model.Employees = await context.Employee.Where(e => e.EndDate == null).OrderBy(e => e.LastName).ToListAsync();

            foreach (Employee employee in model.Employees)
            {
                employee.Department = await context.Department.Where(e => e.DepartmentId == employee.DepartmentId).SingleAsync();
            }

            return View(model);
        }

        /**
              * Purpose: Provides the detail view for each individual employee
              * Arguments:
              *     N/A
              * Return:
              *     At this point the function will redirect the user to the detail view through the return statement.
              */
         [HttpGet]
        public async Task<IActionResult> Detail([FromRoute]int id)
        {
            var model = new EmployeeDetail(context);
            model.Employee = await context.Employee.Include(e => e.Computer).Include(e => e.Department).SingleOrDefaultAsync(e => e.EmployeeId == id);

            model.TrainingPrograms = await (
                from program in context.TrainingProgram
                from attendee in context.Attendee
                where attendee.EmployeeId == model.Employee.EmployeeId && program.TrainingProgramId == attendee.ProgramId
                select program).ToListAsync();

            if (model.Employee == null)
            {
                return NotFound();
            }
            return View(model);
        }


        /**
         * Purpose: Provide form view for users to add new employees
         * Arguments:
         *     Void
         * Return:
         *     Blank form view
         */
         [HttpGet]
        public IActionResult Add()
        {
            var model = new EmployeeForm(context);
            return View(model);
        }

        /**
         * Purpose: Validates Employee Form input and adds a new Employee record and Attendee records to the database
         * Arguments:
         *     form - complete viewmodel from Employee/Add form
         * Return:
         *     If model is valid, redirects user to Employee/Detail view for the newly created Employee
         *     If model is invalid, returns user to Employee/Add form with validation messages
         */
         [HttpPost]
         [ValidateAntiForgeryToken]
        public IActionResult Add(EmployeeForm form)
        {
            if (ModelState.IsValid)
            {
                context.Employee.Add(form.Employee);
                if (form.EnrolledTraining.Count() > 0)
                {
                    foreach (int program in form.EnrolledTraining)
                    {
                        context.Attendee.Add(new Bangazon.Models.Attendee { EmployeeId = form.Employee.EmployeeId, ProgramId = program });
                    }
                }
                context.SaveChanges();
                return RedirectToAction("Detail", new { id = form.Employee.EmployeeId });
            }
            var model = new EmployeeForm(context);
            model.Employee = form.Employee;
            model.EnrolledTraining = form.EnrolledTraining;
            return View(model);
        }

        /**
         * Purpose: Retrieve an Employee record and provide a form view for the user to update
         * Arguments:
         *     id - EmployeeId for the Employee record being edited
         * Return:
         *     View with Employee Form prepopulated with existing Employee properties
         */
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var model = new EmployeeForm(context);

            model.Employee = await context.Employee.SingleAsync(e => e.EmployeeId == id);
            model.Employee.Computer = await context.Computer.SingleOrDefaultAsync(c => c.ComputerId == model.Employee.ComputerId);
            model.EnrolledTraining = await context.Attendee.Where(a => a.EmployeeId == model.Employee.EmployeeId).Select(a => a.ProgramId).ToArrayAsync();
            return View(model);
        }


        /**
         * Purpose: Process form input to update an existing Employee and any necessary Attendee records
         * Arguments:
         *     form - User input from form
         * Return:
         *     If model is valid, return Detail view for that Employee record
         *     If model is invalid, return Form view to continue editing the Employee record
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeForm form)
        {
            if (ModelState.IsValid)
            {
                Employee originalEmployee = await context.Employee.SingleOrDefaultAsync(e => e.EmployeeId == form.Employee.EmployeeId);
                if (originalEmployee == null)
                {
                    return NotFound();
                }
                if (form.NewComputerId != null)
                {
                    originalEmployee.ComputerId = (int)form.NewComputerId;
                }
                originalEmployee.DepartmentId = form.Employee.DepartmentId;
                originalEmployee.FirstName = form.Employee.FirstName;
                originalEmployee.LastName = form.Employee.LastName;
                originalEmployee.StartDate = form.Employee.StartDate;
                originalEmployee.EndDate = form.Employee.EndDate;
                context.Employee.Update(originalEmployee);

                // Request all Training Programs
                List<TrainingProgram> programs = context.TrainingProgram.ToList();
                foreach (TrainingProgram program in programs)
                {
                    // Try to find an Attendee record that matches the EmployeeId and current ProgramId (from loop)
                    Attendee attendee = await context.Attendee.Where(a => a.EmployeeId == form.Employee.EmployeeId).SingleOrDefaultAsync(a => a.ProgramId == program.TrainingProgramId);
                    if (attendee == null && form.EnrolledTraining != null && form.EnrolledTraining.Contains(program.TrainingProgramId))
                    {
                        // If a program was selected but no attendee record exists, add one
                        context.Attendee.Add(new Bangazon.Models.Attendee { EmployeeId = form.Employee.EmployeeId, ProgramId = program.TrainingProgramId });
                    } else if (attendee != null && form.EnrolledTraining != null && !form.EnrolledTraining.Contains(program.TrainingProgramId))
                    {
                        // If a program was not selected, but an attendee record exists, remove it
                        context.Attendee.Remove(attendee);
                    } else if (attendee != null && form.EnrolledTraining == null)
                    {
                        // If a program was not selected, but an attendee record exists, remove it
                        context.Attendee.Remove(attendee);
                    }
                }

                await context.SaveChangesAsync();
                return RedirectToAction("Detail", new { id = originalEmployee.EmployeeId });
            }
            var model = new EmployeeForm(context);
            model.Employee = form.Employee;
            model.EnrolledTraining = form.EnrolledTraining;
            return View(model);
        }
    }
}
