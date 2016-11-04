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
         *   Index() - View list of employees
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
        public IActionResult Detail()
        {
            return View();
        }
    }
}
