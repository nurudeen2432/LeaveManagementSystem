using AutoMapper;
using LeaveManagementSystem.Application.Models.LeaveAllocations;
using LeaveManagementSystem.Application.Services.Periods;
using LeaveManagementSystem.Application.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Application.Services.LeaveAllocations;

public class LeaveAllocationService(
    ApplicationDbContext _context,
    IUserService _userService,
    IPeriodsServices _periodsServices,
    IMapper _mapper
    ) : ILeaveAllocationService
{




    public async Task allocationLeave(string employeeId)
    {
        //get all leave types
        var leaveTypes = await _context.LeaveTypes
            .Where(q => !q.LeaveAllocations.Any(
                x => x.EmployeeId == employeeId
                )).
            ToListAsync();

        //get current period based on the year



        var period = await _periodsServices.GetCurrentPeriod();
        var monthsRemaining = period.EndDate.Month - DateTime.Now.Month;

        //for each leavetype create an allocation entry

        foreach (var leaveType in leaveTypes)
        {
            //works but not best practice
            //var allocationExist = await IsAllocationExist(employeeId, period.Id, leaveType.Id);

            //if (allocationExist)
            //{
            //    continue;
            //}

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
            ? await _userService.GetLoggedInUser()
             : await _userService.GetUserById(userid);

        var allocations = await GetAllocation(user.Id);



        //we are mapping from list of List leave allocation object into a list of Leave allocation VM object


        var allocationVMList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);

        var leaveTypesCount = await _context.LeaveTypes.CountAsync();



        var employeeVm = new EmployeeAllocationVM
        {
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            LeaveAllocations = allocationVMList,
            IsCompletedAllocation = leaveTypesCount == allocationVMList.Count()
        };

        return employeeVm;





    }
    public async Task<List<EmployeeListVM>> GetEmployees()
    {
        var users = await _userService.GetEmployees();

        var employee = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList());

        return employee;
    }

    public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(Guid allocationId)
    {
        var allocation = await _context.LeaveAllocations.
                                Include(q => q.LeaveType)
                                .Include(q => q.Employee)
                                .FirstOrDefaultAsync(q => q.Id == allocationId);

        var model = _mapper.Map<LeaveAllocationEditVM>(allocation);

        return model;
    }

    public async Task EditAllocation(LeaveAllocationEditVM allocationEditVM)
    {
        //null coalescing expression
        var leaveAllocation = await GetEmployeeAllocation(allocationEditVM.Id) ?? throw new Exception("Leave allocation does not exist");
        leaveAllocation.Days = allocationEditVM.Days;



        //option1 _context.Update(leaveAllocation); will run a new update query to our DB

        //_context.Entry(leaveAllocation).State = EntityState.Modified; // will modify on the part changed

        //await _context.SaveChangesAsync();

        await _context.LeaveAllocations
              .Where(q => q.Id == allocationEditVM.Id)
              .ExecuteUpdateAsync(s => s.SetProperty(e => e.Days, allocationEditVM.Days));



    }

    public async Task<LeaveAllocation> GetCurrentAllocation(Guid leaveTypeId, string employeeId)
    {
        var period = await _periodsServices.GetCurrentPeriod();

        var allocation = await _context.LeaveAllocations.FirstAsync(q => q.LeaveTypeId == leaveTypeId && q.EmployeeId == employeeId
        && q.PeriodId == period.Id
        );
        return allocation;
    }

    private async Task<List<LeaveAllocation>> GetAllocation(string? userid)
    {

        var currentDate = DateTime.Now;

        var leaveAllocations = await _context.LeaveAllocations
            .Include(q => q.LeaveType)
            .Include(q => q.Period)
            .Where(q => q.EmployeeId == userid && q.Period.EndDate.Year == currentDate.Year)
            .ToListAsync();

        return leaveAllocations;
    }



    private async Task<bool> IsAllocationExist(string userid, Guid periodId, Guid leaveTypeId)
    {
        var exists = await _context.LeaveAllocations.AnyAsync(
            q => q.EmployeeId == userid && q.PeriodId == periodId && q.LeaveTypeId == leaveTypeId
            );

        return exists;
    }

    public async Task<bool> DaysExceedMaximum(Guid leaveTypeId, int days)
    {
        var leaveType = await _context.LeaveTypes.FindAsync(leaveTypeId);
        return leaveType.NumberOfDays < days;

    }

}
