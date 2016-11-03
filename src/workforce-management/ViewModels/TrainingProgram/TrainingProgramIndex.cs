using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Models;

namespace workforce_management.ViewModels
{
    public class TrainingProgramIndex
    {
        public IEnumerable<TrainingProgram> TrainingPrograms { get; set; }
        public Dictionary<int, int> AttendeeCount { get; set; } = new Dictionary<int, int>();
    }
}
