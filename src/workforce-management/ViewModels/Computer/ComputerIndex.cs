using Bangazon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace workforce_management.ViewModels
{

    /**
     * Class: ComputerIndex
     * Purpose: Stores a list of all computers with the assigned employee (or null)
     * Author: Matt Hamil
     */
    public class ComputerIndex
    {
        public Dictionary<Computer, Employee> ComputerDictionary = new Dictionary<Computer, Employee>();
    }
}
