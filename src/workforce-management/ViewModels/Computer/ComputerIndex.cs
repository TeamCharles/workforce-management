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
        public SortedDictionary<Computer, Employee> ComputerDictionary = new SortedDictionary<Computer, Employee>(new ComputerComparer());
    }


    /**
     * Class: ComputerComparer
     * Purpose: Used to sort the SortedDictionary by Serial Numbers in the ComputerIndex class
     * Author: Matt Hamil
     * Methods:
     *     int Compare(Computer x, Computer y) - Determines which serial number is larger to sort the dictionary
     */
    public class ComputerComparer : IComparer<Computer>
    {
        /**
         * Purpose: Implements the IComparer method Compare to be used to compare computers in the SortedDictionary in the ComputerIndex class
         * Arguments:
         *     Computer x - Computer in the SortedDictionary
         *     Computer y - New computer to be added
         * Return:
         *     -1 if y's SN is larger than x's, 1 if y's SN is smaller, 0 if they are equal
         */
        public int Compare(Computer x, Computer y)
        {
            if (x != null && y != null)
            {
                return x.SerialNumber.CompareTo(y.SerialNumber);
            }
            else
            {
                throw new System.ArgumentException($"Can't compare computer with serial number {x.SerialNumber} to computer with serial number {y.SerialNumber}!");
            }
        }
    }


}
