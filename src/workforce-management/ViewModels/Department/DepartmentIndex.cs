using System.Collections.Generic;
using Bangazon.Models;

namespace workforce_management.ViewModels
{
    /**
     * Class: DepartmentIndex
     * Purpose: Creates viewmodel for department index route
     * Author: Dayne Wright
     * Methods:
     *     constructor DepartmentIndex() - returns list of Departments
     */

    public class DepartmentIndex
    {
        public IEnumerable<Department> DepartmentList { get; set; }
    }
}
