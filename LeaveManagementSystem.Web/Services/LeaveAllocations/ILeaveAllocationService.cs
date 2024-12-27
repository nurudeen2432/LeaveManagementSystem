using LeaveManagementSystem.Web.Models.LeaveAllocations;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations;

public interface ILeaveAllocationService
{
    Task allocationLeave(string employeeId);

    Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userid);
    Task<List<EmployeeListVM>> GetEmployees();
}