using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using workforce_management.ViewModels;

namespace workforce_management.Controllers
{
    public class DepartmentController : Controller
    {
        private BangazonContext context;

        public DepartmentController(BangazonContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new DepartmentIndex();
            model.DepartmentList = from department in context.Department select department; 

            return View(model);
        }
    }
}
