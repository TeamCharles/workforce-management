using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonWeb.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using workforce_management.ViewModels;
using Microsoft.AspNetCore.Routing;

namespace workforce_management.Controllers
{

    /**
     * Class: ComputerController
     * Purpose: Manages Controller actions and views
     * Author: Matt Hamil
     * Methods:
     *     IActionResult Index() - Computer list view
     *     IActionResult Add() - Computer add view
     *     Task<IActionResult> Add(ComputerAdd computerAdd) - Add new computer to database
     *     Task<IActionResult> Edit(ComputerEdit computerAdd) - Edit a computer in the database
     */
    public class ComputerController : Controller
    {

        private BangazonContext context;

        /**
         * Purpose: Initializes the controller with a reference to the DB context
         * Arguments:
         *     ctx - Database context
         * Return:
         *     Constructed ComputerController with DB reference
         */
        public ComputerController(BangazonContext ctx)
        {
            context = ctx;
        }


        /**
         * Purpose: Serves up the Computer List page with all computers listed
         * Return:
         *     Computer list view
         */
        [HttpGet]
        public IActionResult Index()
        {
            ComputerIndex viewModel = new ComputerIndex();

            // Select all computers that are not assigned to employees
            var unassignedComputerList = (
                from computer in context.Computer
                where context.Employee.All(e => e.ComputerId != computer.ComputerId)
                select computer).ToList();

            // Select all computers that are assigned to employees
            var assignedComputerList = (
                from computer in context.Computer
                where unassignedComputerList.All(c => c.ComputerId != computer.ComputerId)
                select computer).Distinct().ToList();

            foreach (Computer computer in assignedComputerList)
            {
                var assignedEmployee =  (
                    from employee in context.Employee
                    where employee.ComputerId == computer.ComputerId
                    select employee).FirstOrDefault();

                viewModel.ComputerDictionary.Add(computer, assignedEmployee);
            }

            foreach (Computer computer in unassignedComputerList)
            {
                viewModel.ComputerDictionary.Add(computer, null);
            }

            return View(viewModel);
        }


        /**
         * Purpose: Add new computer form
         * Return:
         *     Add computer view
         */
        [HttpGet]
        public IActionResult Add()
        {
            ComputerAdd model = new ComputerAdd();
            return View(model);
        }

        /**
         * Purpose: Adds a new computer to the database
         * Arguments:
         *     computerAdd - ComputerAdd view model containing form data
         * Return:
         *     Computer list view
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ComputerAdd computerAdd)
        {

            if (ModelState.IsValid)
            {
                Computer newComputer = computerAdd.NewComputer;

                // Add the new computer to the database
                context.Add(newComputer);
                await context.SaveChangesAsync();

                // Return to Computer list view
                return RedirectToAction("Index");
            }

            return View(computerAdd);
        }


        /**
         * Purpose: Loads view model with computer to edit
         * Arguments:
         *     id - Computer id to edit
         * Return:
         *     Edit view
         */
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var model = new ComputerEdit();
            model.ComputerToEdit = await context.Computer.SingleAsync(c => c.ComputerId == id);
            return View(model);
        }

        /**
         * Purpose: Updates a computer in the database
         * Arguments:
         *     computerEdit - Computer View Model with edited computer
         * Return:
         *     Computer list view if success or computer edit view if invalid model
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ComputerEdit computerEdit)
        {
            if (ModelState.IsValid)
            {
                Computer editedComputer = computerEdit.ComputerToEdit;

                Computer computerToEdit = await (
                    from computer in context.Computer
                    where editedComputer.ComputerId == computer.ComputerId
                    select computer).SingleOrDefaultAsync();

                computerToEdit.SerialNumber = editedComputer.SerialNumber;
                computerToEdit.Make = editedComputer.Make;
                computerToEdit.Model = editedComputer.Model;

                context.Computer.Update(computerToEdit);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(computerEdit);
        }
    }
}