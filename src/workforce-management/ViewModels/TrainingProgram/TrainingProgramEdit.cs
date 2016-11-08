using System.Collections.Generic;
using Bangazon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace workforce_management.ViewModels
{
    /**
     * Class: TrainingProgramEdit
     * Purpose: ViewModel for the TrainingProgram/Edit view
     * Author: Dayne Wright
     */
    public class TrainingProgramEdit
    {
        public TrainingProgram TrainingProgram { get; set; }
        public int[] selectedAttendees { get; set; }
        public List<Employee> Employees { get; set; }
    }
}