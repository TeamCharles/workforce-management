using System.Collections.Generic;
using Bangazon.Models;

namespace workforce_management.ViewModels
{
    /**
     * Class: DepartmentIndex
     * Purpose: Creates viewmodel for department index route
     * Author: Dayne Wright
     */

    public class DepartmentIndex
    {
        public IEnumerable<Department> DepartmentList { get; set; }
    }
}
