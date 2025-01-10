using LeaveManagementSystem.Application.Models.LeaveAllocations;

namespace LeaveManagementSystem.Application.Services.LeaveAllocations;

public interface ILeaveAllocationService
{
    Task allocationLeave(string employeeId);

    Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userid);
    Task<List<EmployeeListVM>> GetEmployees();
    Task<LeaveAllocationEditVM> GetEmployeeAllocation(Guid allocationId);
    Task EditAllocation(LeaveAllocationEditVM allocationEditVM);
    Task<LeaveAllocation> GetCurrentAllocation(Guid leaveTypeId, string employeeId);
}