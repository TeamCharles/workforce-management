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
using Microsoft.ApplicationInsights.DataContracts;

namespace workforce_management.Controllers
{

    /**
     * Class: ComputerController
     * Purpose: Manages Controller actions and views
     * Author: Matt Hamil
     * Methods:
     *     IActionResult Index() - Computer list view
     *     IActionResult Add() - Computer add view
     *     Task<IActionResult> Add(Computer computerAdd) - Add new computer to database
     *     Task<IActionResult> Delete(int computerId) - Removes a computer from DB and unassigns its employee
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
            // Check for redirects from Delete()
            if (TempData["ComputerDeleteError"] != null)
            {
                ViewBag.ComputerDeleteError = TempData["ComputerDeleteError"].ToString();
                TempData.Remove("ComputerDeleteError");
            }

            if (TempData["ComputerDeleteSuccess"] != null)
            {
                ViewBag.ComputerDeleteSuccess = TempData["ComputerDeleteSuccess"].ToString();
                TempData.Remove("ComputerDeleteSuccess");
            }

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
         * Purpose: Deletes a computer from the database
         * Arguments:
         *     id - The computer id to delete
         * Return:
         *     Computer list view with success or error message
         */

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var computerToDelete = await (
                from computer in context.Computer
                where id == computer.ComputerId
                select computer).SingleOrDefaultAsync();

            var assignedEmployee = await (
                from employee in context.Employee
                where employee.ComputerId == id
                select employee).SingleOrDefaultAsync();

            if (assignedEmployee != null)
            {
                TempData["ComputerDeleteError"] = $@"Must unassign Computer {computerToDelete.SerialNumber} {computerToDelete.Make} {computerToDelete.Model} from employee {assignedEmployee.FirstName} {assignedEmployee.LastName}.";
                return RedirectToAction("Index");
            }

            context.Remove(computerToDelete);
            await context.SaveChangesAsync();

            TempData["ComputerDeleteSuccess"] = $"Success! Deleted Computer {computerToDelete.SerialNumber} {computerToDelete.Make} {computerToDelete.Model}.";

            return RedirectToAction("Index");

        }
    }
}