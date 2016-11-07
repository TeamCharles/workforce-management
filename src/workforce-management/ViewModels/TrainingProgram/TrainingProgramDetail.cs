using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Models;

namespace workforce_management.ViewModels
{
    /**
     * Class: TrainingProgramIndex
     * Purpose: ViewModel for the TrainingProgram/Index view
     * Author: Matt Kraatz
     */
    public class TrainingProgramDetail
    {
        public TrainingProgram TrainingProgram { get; set; }
        public IEnumerable<Employee> Attendees { get; set; }
    }
}
