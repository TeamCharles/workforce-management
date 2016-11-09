using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using workforce_management.ViewModels;
using Bangazon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;


namespace workforce_management.Controllers
{
    public class TrainingProgramController: Controller
    {
        /**
         * CLASS: ProductTypes
         * PURPOSE: Creates routes for main index view (buy method) and seller view (sell method)
         * AUTHOR: Matt Kraatz/Anulfo Ordaz/Garrett Vangilder
         * METHODS:
         *   TrainingProgramController(BangazonContext) - Constructor that saves the database context to a private variable.
         *   IActionResult Index() - Returns a View listing all Training Programs.
         *   IActionResult Detail() - Returns a View showing the detail of a particular Training Program.
         **/
        private BangazonContext context;

        /**
         * Purpose: Initializes the TrainingProgramController with a reference to the database context
         * Arguments:
         *      ctx - Reference to the database context
         */
        public TrainingProgramController(BangazonContext ctx)
        {
            context = ctx;
        }

        /**
         * Purpose: Creates a View of the Training Program Form and populates the employees list
         * Return:
         *      Create Training Program Form View
         */

        [HttpGet]
        public IActionResult Add()
        {
            var model = new TrainingProgramAdd();
            model.Employees = context.Employee.OrderBy(e => e.FirstName).AsEnumerable().Where(e => e.EndDate == null).ToList();

            foreach (Employee employee in model.Employees)
            {
                string fullName = employee.FirstName + " " + employee.LastName;
                model.EmployeesFullName.Add(employee.EmployeeId, fullName);
            }

            return View(model);
        }
        /**
         * Purpose: Check if the form is valid, add a new training program to database, and add attendees to the database
         * Arguments:
         *      TrainingProgramAdd trainingProgramAdd - TrainingProgramAdd View Model containing fields to populate the dropdownlist and receive the array of EmployeeIds. 
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TrainingProgramAdd trainingProgramAdd)
        {
            var model = new TrainingProgramAdd();

            if (ModelState.IsValid)
            {

                context.Add(trainingProgramAdd.NewTrainingProgram);
                await context.SaveChangesAsync();

                if(trainingProgramAdd.EmployeeIds != null && trainingProgramAdd.EmployeeIds.Count() >= 0)
                {
                    foreach (int employee in trainingProgramAdd.EmployeeIds)
                    {
                        context.Attendee.Add(new Bangazon.Models.Attendee { EmployeeId = employee,  ProgramId = trainingProgramAdd.NewTrainingProgram.TrainingProgramId });
                    }
                }

                await context.SaveChangesAsync();
                return RedirectToAction("Detail", new RouteValueDictionary(new { controller = "TrainingProgram", action = "Detail", Id = trainingProgramAdd.NewTrainingProgram.TrainingProgramId}) );
            }
            return RedirectToAction("Add", new RouteValueDictionary(new { controller = "TrainingProgram", action = "Add"}));
        }

        /**
         * Purpose: Creates a View of every training program available and every attendee that it holds
         * Return:
         *      Training Program Index View
         */
        [HttpGet]
        public IActionResult Index()
        {
            var model = new TrainingProgramIndex();
            model.TrainingPrograms = from program in context.TrainingProgram select program;
            foreach (TrainingProgram program in model.TrainingPrograms)
            {
                int count = context.Attendee.Count(a => a.ProgramId == program.TrainingProgramId);
                model.AttendeeCount.Add(program.TrainingProgramId, count);
            }
            return View(model);
        }

        /**
         * Purpose: Creates a Detail View of a specific training program
         * Arguments:
         *      int id - TrainingProgramId for the detail being requested
         * Return:
         *      A Detail View for the Trainig program Selected
         */
        public IActionResult Detail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var model = new TrainingProgramDetail();
            model.TrainingProgram = context.TrainingProgram.Single(p => p.TrainingProgramId == id);
            model.Attendees = from attendee in context.Attendee from employee in context.Employee where attendee.ProgramId == id where employee.EmployeeId == attendee.EmployeeId select employee;
            return View(model);
        }
    }
}
