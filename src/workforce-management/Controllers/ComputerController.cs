using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonWeb.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using workforce_management.ViewModels;
using Microsoft.AspNetCore.Routing;

namespace workforce_management.Controllers
{

    /**
     * Class: ComputerController
     * Purpose: Manages Controller actions and views
     * Author: Matt Hamil
     * Methods:
     *     IActionResult Index() - Computer list view
     */
    public class ComputerController : Controller
    {

        private BangazonContext context;

        /**
         * Purpose: Initializes the controller with a reference to the DB context
         * Arguments:
         *     ctx - Database context
         * Return:
         *     Constructed ComputerController with DB reference
         */
        public ComputerController(BangazonContext ctx)
        {
            context = ctx;
        }


        /**
         * Purpose: Serves up the Computer List page with all computers listed
         * Return:
         *     Computer list view
         */
        [HttpGet]
        public IActionResult Index()
        {
            // Select all computers that are not assigned to employees
            var computers = (
                from employee in context.Employee.Include(e => e.Computer)
                from computer in context.Computer
                where context.Employee.All(e => e.ComputerId != computer.ComputerId)
                select computer).ToList();
            return View(computers);
        }


        /**
         * Purpose: Adds a new computer to the database and can update employee if assigned
         * Arguments:
         *     computerAdd - ComputerAdd view model containing form data and assigned employee ID
         * Return:
         *     Computer list view
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ComputerAdd computerAdd)
        {

            if (ModelState.IsValid)
            {
                Computer newComputer = computerAdd.NewComputer;
                Employee assignedEmployee = await (
                    from employee in context.Employee
                    where employee.EmployeeId == computerAdd.AssignedEmployeeId
                    select employee).SingleOrDefaultAsync();

                // Add the new computer to the database
                context.Add(newComputer);
                await context.SaveChangesAsync();

                // If employee assigned to computer, update employee
                if (assignedEmployee != null)
                {
                    // Assign the new computer to the employee
                    assignedEmployee.Computer = newComputer;
                    context.Update(assignedEmployee);
                    await context.SaveChangesAsync();
                }

                // Return to Computer list view
                return RedirectToAction("Index", new RouteValueDictionary(
                        new { controller = "Computer", action = "Index" }));
            }

            return View(computerAdd);
        }
    }
}