using LeaveManagementSystem.Web.Common;
using LeaveManagementSystem.Web.Services.LeaveAllocations;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveAllocationController(ILeaveAllocationService _leaveAllocationService) : Controller
    {

        [Authorize(Roles= Roles.Administrator)]
        public async Task<IActionResult> Index()
        {

            var employees = await _leaveAllocationService.GetEmployees();
            return View(employees);
        }
        public async Task<IActionResult> Details(string? userid)
        {


            var employeeVm = await _leaveAllocationService.GetEmployeeAllocations(userid);
            return View(employeeVm);
        }

   
    }
}
