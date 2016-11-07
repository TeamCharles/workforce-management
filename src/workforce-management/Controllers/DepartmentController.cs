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
    /**
     * Class: DepartmentController
     * Purpose: Route all department views
     * Author: Dayne Wright
     * Methods:
     *     constructor DepartmentController() - returns instance of DepartmentController
     *     view Index() - Queries for departments and returns model to razor view
     */
    public class DepartmentController : Controller
    {
        private BangazonContext context;

        /**
         * Purpose: Creates instance of DepartmentController
         * Arguments:
         *     ctx - database context based to contructor 
         * Return:
         *     instance with database context
         */

        public DepartmentController(BangazonContext ctx)
        {
            context = ctx;
        }

        /**
         * Purpose: Routes http get to index
         * Arguments:
         *     none
         * Return:
         *     view for razor template to index route
         */

        [HttpGet]
        public IActionResult Index()
        {
            var model = new DepartmentIndex();
            model.DepartmentList = from department in context.Department select department;

            return View(model);
        }


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

                if (newDepartment.Employees != null)
                {
                    foreach (Employee employee in newDepartment.Employees)
                    {
                        //DO SOMETHING
                        employee.DepartmentId = newDepartment.DepartmentId;
                        context.Update(employee);
                    }

                }
                await context.SaveChangesAsync();
                return RedirectToAction("Index");

            }


            var model = new SingleDepartment();

            return RedirectToAction("Index", new RouteValueDictionary(
                new { controller = "Department", action = "Index" }));
        }

    }
}
