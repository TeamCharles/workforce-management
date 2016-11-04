using Bangazon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace workforce_management.ViewModels
{

    /**
     * Class: ComputerIndex
     * Purpose: Stores new computer and a reference to the employee id assigned a new computer (if created)
     * Author: Matt Hamil
     */
    public class ComputerAdd
    {
        public Computer NewComputer { get; set; }
        public int? AssignedEmployeeId { get; set; }
    }
}
