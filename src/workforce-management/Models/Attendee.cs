using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    /**
     * Class: Attendee
     * Purpose: Represents the Attendee table in the database
     * Author: Matt Kraatz
     */
    public class Attendee
    {
        [Key]
        public int AttendeeId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public int ProgramId { get; set; }
        public Program Program { get; set; }

    }
}