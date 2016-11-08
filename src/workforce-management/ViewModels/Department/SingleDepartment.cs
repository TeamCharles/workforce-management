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
    public class SingleDepartment
    {
        public IEnumerable<SelectListItem> Employees { get; set; }

        public Department NewDepartment { get; set; }

        public int[] EmployeeIds { get; set; }

    }
}
