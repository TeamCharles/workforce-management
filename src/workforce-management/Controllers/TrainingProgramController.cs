using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Models;
using BangazonWeb.Data;
using workforce_management.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace workforce_management.Controllers
{
    public class TrainingProgramController : Controller
    {
        private BangazonContext context;

        public TrainingProgramController(BangazonContext ctx)
        {
            context = ctx;
        }

        private bool TrainingProgramExists(int id)
        {
            return context.TrainingProgram.Count(e => e.TrainingProgramId == id) > 0;
        }


        [HttpGet]
        public IActionResult Add()
        {
            var model = new TrainingProgramAdd();
            model.Employees = context.Employee
                .OrderBy(l => l.LastName)
                .AsEnumerable()
                .Select(li => new SelectListItem
                {
                    Text = li.LastName,
                    Value = li.EmployeeId.ToString()
                });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TrainingProgramAdd trainingProgramAdd)
         {
            if (ModelState.IsValid)
            {
                TrainingProgram newTrainingProgram = trainingProgramAdd.NewTrainingProgram;

                context.Add(newTrainingProgram);


                if (trainingProgramAdd.Employees != null)
                {
                    foreach (SelectListItem employee in trainingProgramAdd.Employees)
                    {
                        Attendee newattendee = new Attendee();
                        newattendee.EmployeeId = Int32.Parse(employee.Value);
                        newattendee.TrainingProgramId = newTrainingProgram.TrainingProgramId;
                    }
                }

                await context.SaveChangesAsync();
                return RedirectToAction("Detail", "TrainingProgram");
            }
            return RedirectToAction("Index", "TrainingProgram");
        }

    }
}
                //context.Add(trainingProgram.NewTrainingProgram);
                //context.SaveChanges();
                //return RedirectToAction("Index", new RouteValueDictionary(
                //    new { controller = "TrainingProgram", action = "Index" }));
