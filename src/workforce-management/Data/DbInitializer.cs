
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
                // Look for any products.
                if (context.Employee.Any())
                {
                    return;   // DB has been seeded
                }

                var employees = new Employee[]
                {
                  new Employee {
                      FirstName = "Carson",
                      LastName = "Daily",
                      DepartmentId = 1,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0),
                      EndDate = null
                  },
                  new Employee {
                      FirstName = "Steve",
                      LastName = "Brownlee",
                      DepartmentId = 1,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0),
                      EndDate = null
                  },
                  new Employee {
                      FirstName = "Grace",
                      LastName = "Nichols",
                      DepartmentId = 1,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0),
                      EndDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  },
                  new Employee {
                      FirstName = "Tony",
                      LastName = "Nichols",
                      DepartmentId = 1,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0),
                      EndDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  },
                  new Employee {
                      FirstName = "Steve",
                      LastName = "Brownlee",
                      DepartmentId = 2,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0),
                      EndDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  },
                  new Employee {
                      FirstName = "Dejan",
                      LastName = "Stjephanovic",
                      DepartmentId = 2,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0),
                      EndDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  },
                  new Employee {
                      FirstName = "Cody",
                      LastName = "Szkalarski",
                      DepartmentId = 2,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0),
                      EndDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  },
                  new Employee {
                      FirstName = "Brandon",
                      LastName = "Yorks",
                      DepartmentId = 3,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0),
                      EndDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  },
                  new Employee {
                      FirstName = "Mike",
                      LastName = "Morris",
                      DepartmentId = 3,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0),
                      EndDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  },
                  new Employee {
                      FirstName = "Stephanie",
                      LastName = "Best",
                      DepartmentId = 3,
                      StartDate = new DateTime(2008, 3, 1, 7, 0, 0),
                      EndDate = new DateTime(2008, 3, 1, 7, 0, 0)
                  }
                };

                foreach (Employee e in employees)
                {
                    context.Employee.Add(e);
                }

                context.SaveChanges();

                var computer = new Computer[]
                    {
                        new Computer {
                           EmployeeId = 1,
                           Make = "Apple",
                           Model = "MacBook Pro",
                           SerialNumber = new Random().ToString()
                        },
                        new Computer {
                           EmployeeId = 2,
                           Make = "Apple",
                           Model = "MacBook Pro",
                           SerialNumber = new Random().ToString()
                        },
                        new Computer {
                           EmployeeId = 3,
                           Make = "Apple",
                           Model = "MacBook Pro",
                           SerialNumber = new Random().ToString()
                        },
                        new Computer {
                           EmployeeId = 4,
                           Make = "Apple",
                           Model = "MacBook Pro",
                           SerialNumber = new Random().ToString()
                        },
                        new Computer {
                           EmployeeId = 5,
                           Make = "Apple",
                           Model = "MacBook Pro",
                           SerialNumber = new Random().ToString()
                        },
                        new Computer {
                           EmployeeId = 6,
                           Make = "Apple",
                           Model = "MacBook Pro",
                           SerialNumber = new Random().ToString()
                        },
                        new Computer {
                           EmployeeId = 7,
                           Make = "Apple",
                           Model = "MacBook Pro",
                           SerialNumber = new Random().ToString()
                        },
                        new Computer {
                           EmployeeId = 8,
                           Make = "Dell",
                           Model = "Inspiron Pro",
                           SerialNumber = new Random().ToString()
                        },
                        new Computer {
                           EmployeeId = 9,
                           Make = "Dell",
                           Model = "Inspiron Pro",
                           SerialNumber = new Random().ToString()
                        },
                        new Computer {
                           EmployeeId = 10,
                           Make = "Dell",
                           Model = "Inspiron Pro",
                           SerialNumber = new Random().ToString()
                        },
                        new Computer {
                           EmployeeId = null,
                           Make = "Dell",
                           Model = "Inspiron Pro",
                           SerialNumber = new Random().ToString()
                        },
                        new Computer {
                           EmployeeId = null,
                           Make = "Dell",
                           Model = "Inspiron Pro",
                           SerialNumber = new Random().ToString()
                        }
                    };

                foreach (Computer c in computer)
                {
                    context.Computer.Add(c);
                }

                context.SaveChanges();

                var department = new Department[]
                    {
                        new Department {
                            Name = "Human Resources",
                            Description = "Human Resources is located in Muskogee, Oklaholma at our Corporate Headquarters"
                        },
                        new Department {
                            Name = "Web Development",
                            Description = ""
                        },
                        new Department {
                            Name = "Warehouse and Shipping",
                            Description = ""
                        }
                    };



            };
        }
    }
}
