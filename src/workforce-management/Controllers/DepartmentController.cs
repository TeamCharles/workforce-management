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

       /**
        * Purpose: Provides Edit Form
        * Arguments:
        *    This method takes the department id to populate the form.
        * Return:
        *     Redirect to the Department form
        **/
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var model = new EditDepartment();
            model.editDepartment = await context.Department.SingleAsync(d => d.DepartmentId == id);

            model.Employees = context.Employee.OrderBy(e => e.FirstName).AsEnumerable().Where(e => e.EndDate == null).ToList();

            foreach (Employee employee in model.Employees)
            {
                string fullName = employee.FirstName + " " + employee.LastName;
                model.EmployeesFullName.Add(employee.EmployeeId, fullName);
            }

            model.selectedEmployees = context.Attendee.Where(e => e.ProgramId == model.editDepartment.DepartmentId).Select(e => e.EmployeeId).ToArray();



            if (model.editDepartment != null)
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }


        /**
         * Purpose: Actually updates the database to reflect changes  
         * Arguments:
         *    Completed editdepartment form
         * Return:
         *     Redirect user to detail view for the newly changed department.
         **/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDepartment form)
        {
            Department originalDepartment = context.Department.Single(p => p.DepartmentId == form.editDepartment.DepartmentId);
            Attendee[] attendeeList = context.Attendee.Where(a => a.ProgramId == originalDepartment.DepartmentId).ToArray();

            if (ModelState.IsValid)
            {


                if (form.selectedEmployees != null)
                {
                    Employee[] employees = context.Employee.Where(e => !form.selectedEmployees.Contains(e.EmployeeId)).ToArray();

                    foreach (Employee employee in employees)
                    {
                        Attendee isListed = attendeeList.SingleOrDefault(a => a.EmployeeId == employee.EmployeeId);
                        if (isListed != null)
                        {
                            context.Attendee.Remove(isListed);
                        }
                    }


                    foreach (int attendeeId in form.selectedEmployees)
                    {
                        Attendee employeeSelected = context.Attendee.Where(e => e.EmployeeId == attendeeId).SingleOrDefault(e => e.ProgramId == form.editDepartment.DepartmentId);
                        if (employeeSelected == null)
                        {
                            context.Attendee.Add(new Bangazon.Models.Attendee { EmployeeId = attendeeId, ProgramId = originalDepartment.DepartmentId });
                        }
                    }
                }
                else
                {
                    foreach (Attendee attendee in attendeeList)
                    {
                        context.Attendee.Remove(attendee);
                    }
                }

                originalDepartment.Name = form.editDepartment.Name;
                originalDepartment.Description = form.editDepartment.Description;
                context.Department.Update(originalDepartment);

                await context.SaveChangesAsync();

                return RedirectToAction("Detail", new { id = form.editDepartment.DepartmentId });

            }
            form.Employees = context.Employee.OrderBy(e => e.FirstName).AsEnumerable().Where(e => e.EndDate == null).ToList();

            foreach (Employee employee in form.Employees)
            {
                string fullName = employee.FirstName + " " + employee.LastName;
                form.EmployeesFullName.Add(employee.EmployeeId, fullName);
            }

            return View(form);
        } 
    }
}
