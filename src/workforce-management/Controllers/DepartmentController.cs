using Bangazon.Models;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using workforce_management.ViewModels;

namespace workforce_management.Controllers
{

    /**
     * Class: DepartmentController
     * Purpose: Route all department views
     * Author: Dayne Wright
     * Methods:
     *     constructor DepartmentController() - returns instance of DepartmentController
     *     view Index() - Queries for departments and returns model to razor view
     *     view Index(int id) - Quires for specific department details and employees and returns model to razor view
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
         *     view model for razor template to index route
         */

        [HttpGet]
        public IActionResult Index()
        {
            var model = new DepartmentIndex();
            model.DepartmentList = from department in context.Department select department; 

            return View(model);
        }


        /**
         * Purpose: Routes http get to detail view for department id passed in
         * Arguments:
         *     id - Department id for detail view
         * Return:
         *     View model for razor template to detail route
         */

        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var model = new DepartmentDetail();
            model.Department = context.Department.Single(p => p.DepartmentId == id);
            model.Employees = context.Employee.Where(e => e.DepartmentId == id).OrderBy(e => e.FirstName).ToList();


            if (model.Employees.Count > 0)
            {
                foreach (Employee employee in model.Employees)
                {
                    employee.Computer = context.Computer.Single(e => e.ComputerId == employee.ComputerId);
                }
            }

            return View(model);
        }
    }
}
