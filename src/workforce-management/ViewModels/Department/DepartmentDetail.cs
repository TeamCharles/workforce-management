using System.Collections.Generic;
using Bangazon.Models;

namespace workforce_management.ViewModels
{
    /**
     * Class: DepartmentDetail
     * Purpose: ViewModel for the Department detail view
     * Author: Dayne Wright
     */

    public class DepartmentDetail
    {
        public Department Department { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
