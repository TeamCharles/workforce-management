using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Models;
using Microsoft.AspNetCore.Mvc;
using BangazonWeb.Data;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace workforce_management.ViewModels
{
    /**
     * Class: EmployeeList
     * Purpose: ViewModel for the EmployeeList
     * Author: Garrett Vangilder
     */
    public class EmployeeList
    {
        public IEnumerable<Employee> Employees { get; set; }

    }
}
