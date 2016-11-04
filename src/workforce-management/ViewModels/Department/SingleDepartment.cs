using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Models;
using Microsoft.AspNetCore.Mvc;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace workforce_management.ViewModels
{
    public class SingleDepartment
    {
        public IEnumerable<SelectListItem> Employees { get; set; }

        public Department department { get; set; }

    }
}
