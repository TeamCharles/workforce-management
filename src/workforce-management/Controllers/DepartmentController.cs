using System.Linq;
using System.Threading.Tasks;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Models;
using workforce_management.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            model.DepartmentList = from department in context.Department orderby department.Name select department;

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
                        Text = li.FirstName + " " + li.LastName,
                        Value = li.EmployeeId.ToString()
                    });
            return View(model);
        }

        /**
         * Purpose: Provides the Add view and updates the database
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

                if (singleDepartment.EmployeeIds != null && singleDepartment.EmployeeIds.Count() > 0)
                {
                    foreach (int employee in singleDepartment.EmployeeIds)
                    {
                        Employee employeeToChange = await context.Employee.SingleAsync(e => e.EmployeeId == employee);
                        employeeToChange.DepartmentId = newDepartment.DepartmentId;
                        context.Employee.Update(employeeToChange);
                    }
                }

                await context.SaveChangesAsync();
                return RedirectToAction("Detail", new { id = singleDepartment.NewDepartment.DepartmentId });
            }

            var model = new SingleDepartment();
            model.NewDepartment = singleDepartment.NewDepartment;
            return View(model);
        }

    }
}
