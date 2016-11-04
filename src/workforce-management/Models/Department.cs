using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    /**
     * Class: Department
     * Purpose: Represents the Department table in the database
     * Author: Matt Kraatz
     */
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
   
        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        // Foreign Key Dependencies
        public ICollection<Employee> Employees;
    }
}