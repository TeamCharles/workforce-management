
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bangazon.Models;
using System.Collections.Generic;

namespace BangazonWeb.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BangazonContext(serviceProvider.GetRequiredService<DbContextOptions<BangazonContext>>()))
            {
                // Look for any employee.
                if (context.Employee.Any())
                {
                    return;   // DB has been seeded
                }

                var department = new Department[]
                    {
                        new Department {
                            Name = "Human Resources",
                            Description = "Human Resources is located in Muskogee, Oklaholma at our Corporate Headquarters"
                        },
                        new Department {
                            Name = "Web Development",
                            Description = "The Web Development Department is located in San Fransisco at our second largest office."
                        },
                        new Department {
                            Name = "Warehouse and Shipping",
                            Description = "The Bangazon warehouse is located in Mexico."
                        }
                    };

                foreach (Department d in department)
                {
                    context.Department.Add(d);
                }

                context.SaveChanges();

                var rand = new Random();

                var computer = new Computer[]
                     {
                        new Computer {
                           Make = "Apple",
                           Model = "MacBook Pro",
                           SerialNumber = rand.Next(10000).ToString()
                        },
                        new Computer {
                           Make = "Apple",
                           Model = "MacBook Pro",
                           SerialNumber = rand.Next(10000).ToString()
                        },
                        new Computer {
                           Make = "Apple",
                           Model = "MacBook Pro",
                           SerialNumber = rand.Next(10000).ToString()
                        },
                        new Computer {
                           Make = "Apple",
                           Model = "MacBook Pro",
                           SerialNumber = rand.Next(10000).ToString()
                        },
                        new Computer {
                           Make = "Apple",
                           Model = "MacBook Pro",
                           SerialNumber = rand.Next(10000).ToString()
                        },
                        new Computer {
                           Make = "Apple",
                           Model = "MacBook Pro",
                           SerialNumber = rand.Next(10000).ToString()
                        },
                        new Computer {
                           Make = "Apple",
                           Model = "MacBook Pro",
                           SerialNumber = rand.Next(10000).ToString()
                        },
                        new Computer {
                           Make = "Dell",
                           Model = "Inspiron Pro",
                           SerialNumber = rand.Next(10000).ToString()
                        },
                        new Computer {
                           Make = "Dell",
                           Model = "Inspiron Pro",
                           SerialNumber = rand.Next(10000).ToString()
                        },
                        new Computer {
                           Make = "Dell",
                           Model = "Inspiron Pro",
                           SerialNumber = rand.Next(10000).ToString()
                        },
                        new Computer {
                           Make = "Dell",
                           Model = "Inspiron Pro",
                           SerialNumber = rand.Next(10000).ToString()
                        },
                        new Computer {
                           Make = "Dell",
                           Model = "Inspiron Pro",
                           SerialNumber = rand.Next(10000).ToString()
                        }
                     };

                foreach (Computer c in computer)
                {
                    context.Computer.Add(c);
                }

                context.SaveChanges();

                var employees = new Employee[]
                {
                  new Employee {
                      FirstName = "Carson",
                      LastName = "Daily",
                      DepartmentId = 1,
                      ComputerId = 1,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  },
                  new Employee {
                      FirstName = "Steve",
                      LastName = "Brownlee",
                      DepartmentId = 1,
                      ComputerId = 2,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  },
                  new Employee {
                      FirstName = "Grace",
                      LastName = "Nichols",
                      DepartmentId = 1,
                      ComputerId = 3,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  },
                  new Employee {
                      FirstName = "Tony",
                      LastName = "Nichols",
                      DepartmentId = 1,
                      ComputerId = 4,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  },
                  new Employee {
                      FirstName = "Steve",
                      LastName = "Erwin",
                      DepartmentId = 2,
                      ComputerId = 5,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  },
                  new Employee {
                      FirstName = "Dejan",
                      LastName = "Stjephanovic",
                      DepartmentId = 2,
                      ComputerId = 6,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  },
                  new Employee {
                      FirstName = "Cody",
                      LastName = "Szkalarski",
                      DepartmentId = 2,
                      ComputerId = 7,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  },
                  new Employee {
                      FirstName = "Brandon",
                      LastName = "Yorks",
                      DepartmentId = 3,
                      ComputerId = 8,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0),
                  },
                  new Employee {
                      FirstName = "Mike",
                      LastName = "Morris",
                      DepartmentId = 3,
                      ComputerId = 9,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  },
                  new Employee {
                      FirstName = "Stephanie",
                      LastName = "Best",
                      DepartmentId = 3,
                      ComputerId = 10,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  }
                };

                foreach (Employee e in employees)
                {
                    context.Employee.Add(e);
                }

                context.SaveChanges();

                var trainingProgram = new TrainingProgram[]
                    {
                        new TrainingProgram {
                            Name = "SQL for Squares",
                            Description = "Surely the title tells you enough"
                        },
                        new TrainingProgram {
                            Name = "PowerPoint for Developers",
                            Description = "Who knew PowerPoint was so powerful!"
                        },
                        new TrainingProgram {
                            Name = "Visual Studio for JavaScript Developers",
                            Description = "Your computer is probably broken!"
                        }
                    };

                foreach(TrainingProgram p in trainingProgram)
                    {
                    context.TrainingProgram.Add(p);
                    }

                context.SaveChanges();

                var attendees = new Attendee[]
                    {
                        new Attendee {
                            EmployeeId = 1,
                            ProgramId = 1,
                        },
                        new Attendee {
                            EmployeeId = 2,
                            ProgramId = 2
                        },
                        new Attendee {
                            EmployeeId = 2,
                            ProgramId = 3
                        }
                    };

                foreach (Attendee p in attendees)
                {
                    context.Attendee.Add(p);
                }

                context.SaveChanges();
            };
        }
    }
}
