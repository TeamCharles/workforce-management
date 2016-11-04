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
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult Add()
        {
            var model = new SingleDepartment();
            model.Employees = context.Employee
                    .OrderBy(e => e.LastName)
                    .AsEnumerable()
                    .Where(e => e.EndDate == null)
                    .Select(li => new SelectListItem
                    {
                        Text = li.LastName + " " + li.FirstName,
                        Value = li.EmployeeId.ToString()
                    });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SingleDepartment NewDepartment)
        {
            if (ModelState.IsValid)
            {
                foreach (SelectListItem employee in NewDepartment.Employees)
                {
                    int chechkedValue = Convert.ToInt16(employee.Value);
                    var Employee = await context.Employee.Where(e => e.EmployeeId == chechkedValue).OrderBy(e => e.LastName).ToListAsync();

                };

                context.Add(NewDepartment);
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index", new RouteValueDictionary(
                 new { controller = "Department", action = "Index" }));
        }

    }
}
