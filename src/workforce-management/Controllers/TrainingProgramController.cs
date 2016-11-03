using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using workforce_management.ViewModels;
using Bangazon.Models;

namespace workforce_management.Controllers
{
    public class TrainingProgramController: Controller
    {
        /**
         * CLASS: ProductTypes
         * PURPOSE: Creates routes for main index view (buy method) and seller view (sell method)
         * AUTHOR: Matt Kraatz
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
         * Purpose: Creates a View list all Training Programs currently in the database
         * Return:
         *      View containing a list of all Training Programs
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
         * Purpose: Creates a View list all Training Programs currently in the database
         * Arguments:
         *      int id - TrainingProgramId for the detail being requested
         * Return:
         *      View containing a list of all Training Programs
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
