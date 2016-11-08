using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Models;
using BangazonWeb.Data;

namespace workforce_management.ViewModels
{

    /**
     * Class: EmployeeDetail
     * Purpose: ViewModel for the Employee/Detail view
     * Author: Matt Kraatz
     * Methods:
     *     EmployeeDetail(ctx) - overloaded constructor accepting the Bangazon context
     *     EmployeeDetail() - basic constructor required for overload
     */

    public class EmployeeDetail
    {
        public Employee Employee { get; set; }
        public List<TrainingProgram> TrainingPrograms { get; set; }
        public EmployeeDetail(BangazonContext ctx) { }
        public EmployeeDetail() { }
    }
}
