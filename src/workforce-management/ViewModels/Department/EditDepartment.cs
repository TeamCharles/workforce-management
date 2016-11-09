using System.Collections.Generic;

using Bangazon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace workforce_management.ViewModels
{
    /**
   * Class: SingleDepartment
   * Purpose: Model View for Actions that take a single department
   * Author:Garrett Vangilder
   * Methods:
   *    IEnumerable<SelectListItem> get and set
   */
    public class EditDepartment
    {
        public List<Employee> Employees { get; set; }

        public Department editDepartment { get; set; }
        public int[] selectedEmployees { get; set; }


        public Dictionary<int, string> EmployeesFullName { get; set; } = new Dictionary<int, string>();

    }
}
