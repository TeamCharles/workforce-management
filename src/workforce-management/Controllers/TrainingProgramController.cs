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

namespace workforce_management.Controllers
{
    public class TrainingProgramController : Controller
    {
        private BangazonContext context;

        public TrainingProgramController(BangazonContext ctx)
        {
            context = ctx;
        }

        private bool TrainingProgramExists (int id)
        {
            return context.TrainingProgram.Count(e => e.TrainingProgramId == id) > 0;
        }

        public  IActionResult Add()
        {
            var model = new TrainingProgramAdd(context);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(TrainingProgramAdd trainingProgram)
        {
            if (!ModelState.IsValid)
            {
                var model = new TrainingProgramAdd(context);
                model.NewTrainingProgram = trainingProgram.NewTrainingProgram;
                return View(model);
            }
            context.TrainingProgram.Add(trainingProgram.NewTrainingProgram);
            try
            {
                context.SaveChanges();
            }            
            catch (DbUpdateException)
            {
                if (TrainingProgramExists(trainingProgram.NewTrainingProgram.TrainingProgramId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Detail", "TrainingProgram");
        }
    }
}
                //context.Add(trainingProgram.NewTrainingProgram);
                //context.SaveChanges();
                //return RedirectToAction("Index", new RouteValueDictionary(
                //    new { controller = "TrainingProgram", action = "Index" }));
