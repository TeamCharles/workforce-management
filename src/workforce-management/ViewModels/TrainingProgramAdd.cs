using System.Collections.Generic;
using System.Linq;
using BangazonWeb.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace workforce_management.ViewModels
{
    public class TrainingProgramAdd
    {
        public IEnumerable<SelectListItem> Employees { get; set; }

        public TrainingProgram NewTrainingProgram { get; set; }

        public int[] EmployeeIds { get; set; }
    }
}
