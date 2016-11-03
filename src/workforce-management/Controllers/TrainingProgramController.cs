using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using workforce_management.ViewModels;

namespace workforce_management.Controllers
{
    public class TrainingProgramController: Controller
    {

        private BangazonContext context;

        public TrainingProgramController(BangazonContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new TrainingProgramIndex();
            model.TrainingPrograms = from program in context.TrainingProgram select program;
            return View(model);
        }
    }
}
