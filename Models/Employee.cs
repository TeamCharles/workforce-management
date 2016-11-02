using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
  /**
   * Class: Employee
   * Purpose: Represents the Employee table in the database
   * Author: Matt Kraatz
   */
  public class Employee
  {
    [Key]
    public int EmployeeId { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated { get; set; }

    [Required]
    public int DepartmentId { get; set; }
    public Department Department { get; set; }

    [Required]
    public int ComputerId { get; set; }
    public Computer Computer { get; set; }

    [Required]
    [Display (Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [Display (Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [Display (Name = "Start Date")]
    public DateTime StartDate { get; set; }

    [Display (Name = "End Date")]
    public DateTime EndDate { get; set; }

    // Foreign Key Dependencies
    public ICollection<Attendee> Attendees;
  }
}