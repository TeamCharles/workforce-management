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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace workforce_management.Controllers
{
    /**
         * CLASS: EmployeeController
         * PURPOSE: Creates routes for main index view 
         * AUTHOR: Garrett Vangilder
         * METHODS:
         *   Index() - Constructor the list view of employees
         **/
    public class EmployeeController : Controller
    {
        private BangazonContext context;

        public EmployeeController(BangazonContext ctx)
        {
            context = ctx;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var model = new EmployeeList(context);
            model.Employees = await context.Employee.Where(e => e.EndDate == null).OrderBy(e => e.LastName).ToListAsync();
            return View(model);
        }
        public async Task<IActionResult> Detail()
        {
            var model = new EmployeeList(context);
            model.Employees = await context.Employee.Where(e => e.EndDate == null).OrderBy(e => e.LastName).ToListAsync();
            return View(model);
        }
    }
}
