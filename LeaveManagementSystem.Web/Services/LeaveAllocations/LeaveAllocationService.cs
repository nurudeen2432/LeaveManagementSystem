
using AutoMapper;
using LeaveManagementSystem.Web.Common;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations;

public class LeaveAllocationService(
    ApplicationDbContext _context,
    IHttpContextAccessor _httpContextAccessor,
    UserManager<ApplicationUser> _userManager,

    IMapper _mapper
    ) : ILeaveAllocationService
{
    

   

    public async Task allocationLeave(string employeeId)
    {
        //get all leave types
        var leaveTypes = await _context.LeaveTypes.ToListAsync();

        //get current period based on the year

        var currentDate = DateTime.Now;

        var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
        var monthsRemaining = period.EndDate.Month - currentDate.Month;

        //for each leavetype create an allocation entry

        foreach (var leaveType in leaveTypes)
        {
            var accuralRate = decimal.Divide(leaveType.NumberOfDays, 12);
            var leaveAllocation = new LeaveAllocation
            {
                EmployeeId = employeeId,
                PeriodId = period.Id,
                LeaveTypeId = leaveType.Id,
                Days = (int)Math.Ceiling(accuralRate * monthsRemaining)
            };

            _context.Add(leaveAllocation);

     
        }
        await _context.SaveChangesAsync();
    }

 

    public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userid)
    {


        var user = string.IsNullOrEmpty(userid)
            ? await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User)
             : await _userManager.FindByIdAsync(userid);

        var allocations = await GetAllocation(user.Id);

        //we are mapping from list of List leave allocation object into a list of Leave allocation VM object


        var allocationVMList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);



        var employeeVm = new EmployeeAllocationVM
        {
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            LeaveAllocations = allocationVMList
        };

        return employeeVm;




      
    }
    public async Task<List<EmployeeListVM>> GetEmployees()
    {
        var users = await _userManager.GetUsersInRoleAsync(Roles.Employee);

        var employee = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList());

        return employee;
    }


    private async Task<List<LeaveAllocation>> GetAllocation(string? userid)
    {


        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        var currentDate = DateTime.Now;

        var leaveAllocations = await _context.LeaveAllocations
            .Include(q => q.LeaveType)
            .Include(q => q.Period)
            .Where(q => q.EmployeeId == user.Id && q.Period.EndDate.Year == currentDate.Year)
            .ToListAsync();

        return leaveAllocations;
    }


}
