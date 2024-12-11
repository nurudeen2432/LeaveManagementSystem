using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using AutoMapper;
using LeaveManagementSystem.Web.Services;

namespace LeaveManagementSystem.Web.Controllers
{
    public class LeaveTypesController(ILeaveTypeService leaveTypeService) : Controller
    {
       
        private readonly ILeaveTypeService _leaveTypeService = leaveTypeService;
        private const string NameExistValidationMessage = "This leave type already exist in the database";

        //DepencyInjection
     
        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {



            var viewData = await  _leaveTypeService.GetAll();
      
            return View(viewData);
        }



        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeService.Get<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }
            return View(leaveType);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeCreateVM leaveTypeCreate)
        {
          
            if (await _leaveTypeService.CheckIfLeaveTypeNameExist(leaveTypeCreate.Name))
            {
                ModelState.AddModelError(nameof(leaveTypeCreate.Name), NameExistValidationMessage);
            }

            if (ModelState.IsValid)
            {

               
                await _leaveTypeService.Create(leaveTypeCreate);

                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeCreate);
        }

     
        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeService.Get<LeaveTypeEditVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }

           
            return View(leaveType);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, LeaveTypeEditVM leaveTypeEdit)
        {


            if (id != leaveTypeEdit.Id)
            {
                return NotFound();
            }

            if (await _leaveTypeService.CheckIfLeaveTypeNameExistForEdit(leaveTypeEdit))
            {
                ModelState.AddModelError(nameof(leaveTypeEdit.Name), NameExistValidationMessage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _leaveTypeService.Edit(leaveTypeEdit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_leaveTypeService.LeaveTypeExists(leaveTypeEdit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeEdit);
        }

   

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeService.Get<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
           await _leaveTypeService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

      

    }
}
