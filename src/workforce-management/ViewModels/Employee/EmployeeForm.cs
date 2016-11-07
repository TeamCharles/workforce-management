using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Models;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace workforce_management.ViewModels
{

    /**
     * Class: EmployeeForm
     * Purpose: ViewModel for the Employee Add and Employee Edit views 
     * Author: Matt Kraatz
     * Methods:
     *     DocMe() - description
     */
    public class EmployeeForm
    {
        public Employee Employee { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
        public IEnumerable<SelectListItem> Computers { get; set; }
        public IEnumerable<SelectListItem> TrainingPrograms { get; set; }

        public EmployeeForm(BangazonContext ctx)
        {
            this.Departments = ctx.Department.AsEnumerable().Select(li => new SelectListItem { Value = li.DepartmentId.ToString(), Text = li.Name });
            this.TrainingPrograms = ctx.TrainingProgram.AsEnumerable().Select(li => new SelectListItem { Value = li.TrainingProgramId.ToString(), Text = li.Name });
        }
        public EmployeeForm() { }
    }
}
