using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
  public class Computer
  {
    [Key]
    public int ComputerId { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated { get; set; }

    [Required]
    public string Make { get; set; }

    [Required]
    public string Model { get; set; }

    [Required]
    [Display (Name = "Serial Number")]
    public string SerialNumber { get; set; }

    // Foreign Key Dependencies
    public ICollection<Employee> Employees;
  }
}