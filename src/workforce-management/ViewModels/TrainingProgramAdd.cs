using System.Collections.Generic;
using System.Linq;
using BangazonWeb.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace workforce_management.ViewModels
{
    public class TrainingProgramAdd
    {
        private BangazonContext _context { get; set; }

        public IEnumerable<SelectListItem> Employees { get; set; }

        public TrainingProgram NewTrainingProgram { get; set; }

        public TrainingProgramAdd(BangazonContext ctx)
        {
            _context = ctx;

            this.Employees = _context.Employee
                .OrderBy(l => l.LastName)
                .AsEnumerable()
                .Select(li => new SelectListItem
                {
                    Text = li.LastName,
                    Value = li.EmployeeId.ToString()
                });
        }
        public TrainingProgramAdd() { }
    }
}
