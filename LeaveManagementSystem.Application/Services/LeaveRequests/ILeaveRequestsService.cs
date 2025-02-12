﻿using LeaveManagementSystem.Application.Models.LeaveRequests;

namespace LeaveManagementSystem.Application.Services.LeaveRequests;

public interface ILeaveRequestsService
{
    Task CreateLeaveRequest(LeaveRequestCreateVM model);

    Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequests();

    Task<LeaveRequestReadOnlyVM> GetAllLeaveRequests();

    Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequest();
    Task CancelLeaveRequest(Guid leaveRequestId);

    Task ReviewLeaveRequest(Guid leaveRequestId, bool approved);

    Task<bool> RequestLeaveExceedAllocation(LeaveRequestCreateVM model);
    Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(Guid id);
}