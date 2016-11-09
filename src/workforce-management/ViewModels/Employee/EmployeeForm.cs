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
     *     EmployeeForm(BangazonContext) - overloaded constructor that queries the database to populate select inputs for Departments, Training Programs and Available Computers
     *     EmployeeForm() - constructor required to write overloaded signature
     */
    public class EmployeeForm
    {
        public Employee Employee { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
        public IEnumerable<SelectListItem> Computers { get; set; }
        public IEnumerable<TrainingProgram> TrainingPrograms { get; set; }
        public int[] EnrolledTraining { get; set; }
        public int? NewComputerId { get; set; }

        /**
         * Purpose: Constructor that populates select inputs for Departments, Unassigned Computers, TrainingPrograms
         * Arguments:
         *     ctx - Database context provided during construction
         * Return:
         *     Void
         */
        public EmployeeForm(BangazonContext ctx)
        {
            this.Departments = ctx.Department.AsEnumerable().Select(li => new SelectListItem { Value = li.DepartmentId.ToString(), Text = li.Name });
            this.Computers = from computer in ctx.Computer where ctx.Employee.All(e => e.ComputerId != computer.ComputerId)
                                select new SelectListItem { Value = computer.ComputerId.ToString(), Text = computer.Model };
            this.TrainingPrograms = ctx.TrainingProgram.AsEnumerable();
        }

        /**
         * Purpose: Blank constructor
         * Arguments:
         *     Void
         * Return:
         *     Void
         */
        public EmployeeForm() { }
    }
}
