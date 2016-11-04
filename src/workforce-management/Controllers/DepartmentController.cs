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

namespace workforce_management.Controllers
{
    public class DepartmentController : Controller
    {
        private BangazonContext context;

        //Currently this controller attaches the database context to the employee controller
        public DepartmentController(BangazonContext ctx)
        {
            context = ctx;
        }
        //This method provides the index view of the Employee controller. This diplays the employee list
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new DepartmentList();
            model.Departments = await context.Department.ToListAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new EmployeeList();
            model.Employees = await context.Employee.Where(e => e.EndDate == null).OrderBy(e => e.LastName).ToListAsync();
            return View(model);
        }
    }
}
