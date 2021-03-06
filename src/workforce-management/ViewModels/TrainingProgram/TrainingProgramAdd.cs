﻿using System.Collections.Generic;
using System.Linq;
using BangazonWeb.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

 
namespace workforce_management.ViewModels
{

    /**
     * Class: TrainingProgramAdd
     * Purpose: ViewModel for the TrainingProgram/Add view. Employees get the employees for the dropdownlist. EmployeeIds retrieve an array of integers.
     * Author: Anulfo Ordaz
     * 
     */
    public class TrainingProgramAdd
    {
        public IEnumerable<Employee> Employees { get; set; }

        public TrainingProgram NewTrainingProgram { get; set; }

        public int[] EmployeeIds { get; set; }

        public Dictionary<int, string> EmployeesFullName { get; set; } = new Dictionary<int, string>();
    }
}
