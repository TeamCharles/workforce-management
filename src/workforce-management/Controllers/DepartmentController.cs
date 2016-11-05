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
using System.Diagnostics;

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


       /**
         * Purpose: Provides the Add view
         * Arguments:
         *    N/A
         * Return:
         *     Redirect to the Department Index if the model is valid, will redirect the user back to the form if it is invalid. 
         **/
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

        /**
         * Purpose: Provides the Add view
         * Arguments:
         *    This method takes a newdepartment as a paramenter, the new department is the one you would like to post into the database.
         * Return:
         *     Redirect to the Department Index if the model is valid, will redirect the user back to the form if it is invalid. 
         **/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SingleDepartment singleDepartment)
        {
            if (ModelState.IsValid)
            {
                Department newDepartment = singleDepartment.NewDepartment;

                context.Add(newDepartment);
                await context.SaveChangesAsync();

                if (newDepartment.Employees == null)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    //Change Employee Department logic goes here. 
                }
            }


            var model = new SingleDepartment();

            return RedirectToAction("Index", new RouteValueDictionary(
                new { controller = "Department", action = "Index" }));
        }

    }
}
