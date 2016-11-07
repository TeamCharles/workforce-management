using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using workforce_management.ViewModels;

namespace workforce_management.Controllers
{

    /**
     * Class: DepartmentController
     * Purpose: Route all department views
     * Author: Dayne Wright
     * Methods:
     *     constructor DepartmentController() - returns instance of DepartmentController
     *     view Index() - Queries for departments and returns model to razor view
     */

    public class DepartmentController : Controller
    {
        private BangazonContext context;

        /**
         * Purpose: Creates instance of DepartmentController
         * Arguments:
         *     ctx - database context based to contructor 
         * Return:
         *     instance with database context
         */

        public DepartmentController(BangazonContext ctx)
        {
            context = ctx;
        }

        /**
         * Purpose: Routes http get to index
         * Arguments:
         *     none
         * Return:
         *     view for razor template to index route
         */

        [HttpGet]
        public IActionResult Index()
        {
            var model = new DepartmentIndex();
            model.DepartmentList = from department in context.Department select department; 

            return View(model);
        }
    }
}
