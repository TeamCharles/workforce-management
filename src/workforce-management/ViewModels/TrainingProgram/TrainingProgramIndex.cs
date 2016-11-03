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
    public class TrainingProgramIndex
    {
        public IEnumerable<TrainingProgram> TrainingPrograms { get; set; }
        // Key: TrainingProgramId, Value: Count of Attendees
        public Dictionary<int, int> AttendeeCount { get; set; } = new Dictionary<int, int>();
    }
}
