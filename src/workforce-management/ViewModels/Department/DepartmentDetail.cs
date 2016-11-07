using System.Collections.Generic;
using Bangazon.Models;

namespace workforce_management.ViewModels
{
    public class DepartmentDetail
    {
        public Department Department { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
