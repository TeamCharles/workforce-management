using Bangazon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace workforce_management.ViewModels
{

    /**
     * Class: ComputerIndex
     * Purpose: Stores a list of all computers
     * Author: Matt Hamil
     */
    public class ComputerIndex
    {
        public IEnumerable<Computer> ComputerList { get; set; }
    }
}
