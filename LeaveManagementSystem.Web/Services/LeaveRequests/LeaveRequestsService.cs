using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using LeaveManagementSystem.Web.Models.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveAllocations;
using LeaveManagementSystem.Web.Services.Periods;
using LeaveManagementSystem.Web.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveRequests
{
    public partial class LeaveRequestsService(
        IMapper _mapper,
        IUserService _userService,
        ApplicationDbContext _context,
        ILogger<LeaveRequestsService> _logger,
        IPeriodsServices _periodsServices,
        ILeaveAllocationService _leaveAllocationService
        
        ) : ILeaveRequestsService
    {
        public async Task CancelLeaveRequest(Guid leaveRequestId)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync( leaveRequestId );

            leaveRequest.LeaveRequestStatusId = LeaveRequestStatusMapper.ToGuid(LeaveRequestStatuses.Canceled );

            //restore allocation days based on cancel request

            await UpdateAllocationDays(leaveRequest, false);

            await _context.SaveChangesAsync();


        }

        public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            var leaveRequest = _mapper.Map<LeaveRequest>(model);

            var user = await _userService.GetLoggedInUser();

            leaveRequest.EmployeeId = user.Id;

            //set leaveRequestId to pending
            leaveRequest.LeaveRequestStatusId = LeaveRequestStatusMapper.ToGuid(LeaveRequestStatuses.Pending);

            //save leave request

            _context.LeaveRequests.Add(leaveRequest);



            // deduct allocation date based on Leave request
            await UpdateAllocationDays(leaveRequest, true);


            await _context.SaveChangesAsync();



        }

        public Task<LeaveRequestReadOnlyVM> GetAllLeaveRequests()
        {
            throw new NotImplementedException();
        }

        
        public async Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequest()
        {
            var leaveRequests = await _context.LeaveRequests.Include(q => q.LeaveType)
                .ToListAsync();
            var approvedLeaveRequestCount = leaveRequests.Count(q => q.LeaveRequestStatusId
            == LeaveRequestStatusMapper.ToGuid(LeaveRequestStatuses.Approved)
            );
            var pendingLeaveRequestCount = leaveRequests.Count(q => q.LeaveRequestStatusId
          == LeaveRequestStatusMapper.ToGuid(LeaveRequestStatuses.Pending)
          );
            var declinedLeaveRequestCount = leaveRequests.Count(q => q.LeaveRequestStatusId
          == LeaveRequestStatusMapper.ToGuid(LeaveRequestStatuses.Declined)
          );
            var leaveRequestModel = leaveRequests.Select(q => new LeaveRequestReadOnlyVM
            {
                StartDate = q.StartDate,
                EndDate = q.EndDate,
                Id = q.Id,
                LeaveTypes = q.LeaveType.Name,
                LeaveRequestStatus = LeaveRequestStatusMapper.GuidToStatusMap.TryGetValue(q.LeaveRequestStatusId, out var status)
   ? status
   : throw new InvalidOperationException($"Unknown LeaveRequestStatusId: {q.LeaveRequestStatusId}"),
                NumberOfDays = q.EndDate.DayNumber - q.StartDate.DayNumber


            }).ToList();
            var model = new EmployeeLeaveRequestListVM
            {
                ApprovedRequests = approvedLeaveRequestCount,
                DeclinedRequests = declinedLeaveRequestCount,
                PendingRequests = pendingLeaveRequestCount,
                TotalRequests = leaveRequests.Count,
                LeaveRequests = leaveRequestModel

            };

            return model;

        }

        public async Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequests()
        {
            var user = await _userService.GetLoggedInUser();

            var leaveRequest = await _context.LeaveRequests
                .Include(q=> q.LeaveType)
                .Where(q => q.EmployeeId == user.Id)
                .ToListAsync();

            var model = leaveRequest.Select(q => new LeaveRequestReadOnlyVM
            {
                StartDate = q.StartDate,
                EndDate = q.EndDate,
                Id = q.Id,
                LeaveTypes = q.LeaveType.Name,
                LeaveRequestStatus = LeaveRequestStatusMapper.GuidToStatusMap.TryGetValue(q.LeaveRequestStatusId, out var status)
                ? status
                  : throw new InvalidOperationException($"Unknown LeaveRequestStatusId: {q.LeaveRequestStatusId}"),
                NumberOfDays = q.EndDate.DayNumber - q.StartDate.DayNumber
            }).ToList();

            return model;

        }

        public async Task<bool> RequestLeaveExceedAllocation(LeaveRequestCreateVM model)
        {
            var user = await _userService.GetLoggedInUser();
            var period = await _periodsServices.GetCurrentPeriod();
            var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            var allocation = await _context.LeaveAllocations.FirstAsync(q => q.LeaveTypeId == model.LeaveTypeId
            && q.EmployeeId == user.Id
            && q.PeriodId == period.Id
            );

            var result = allocation.Days < numberOfDays;
            _logger.LogInformation(">>>>>>>Days Allocated: {Days}, Days Requested: {Requested}, Exceeds Allocation: {Result}",
                        allocation.Days, numberOfDays, result);
            return result;
         

        }

        public async Task ReviewLeaveRequest(Guid leaveRequestId, bool approved)
        {
            var user = await _userService.GetLoggedInUser();
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            leaveRequest.LeaveRequestStatusId = approved
            ? LeaveRequestStatusMapper.ToGuid(LeaveRequestStatuses.Approved)
            : LeaveRequestStatusMapper.ToGuid(LeaveRequestStatuses.Declined);

            leaveRequest.ReviewerId = user.Id;
            
            if(!approved)
            {
                await UpdateAllocationDays(leaveRequest, false);
            }

            await _context.SaveChangesAsync();
            
            
        }

       
        public async Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(Guid id)
        {
            var leaveRequest = await _context.LeaveRequests.
                Include(q => q.LeaveType)
                .FirstAsync(q => q.Id == id);
                
            var user = await _userService.GetUserById(leaveRequest.EmployeeId);

            var model = new ReviewLeaveRequestVM
            {
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                NumberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber,
                LeaveRequestStatus =
                LeaveRequestStatusMapper.GuidToStatusMap.TryGetValue(leaveRequest.LeaveRequestStatusId, out var status)
                                        ? status
                                        : throw new InvalidOperationException($"Unknown LeaveRequestStatusId: {leaveRequest.LeaveRequestStatusId}"),
                Id = leaveRequest.Id,
                RequestComments = leaveRequest.RequestComment,
                LeaveTypes = leaveRequest.LeaveType.Name,
                Employee = new EmployeeListVM
                {
                    Id = leaveRequest.EmployeeId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                }

            };
            return model;

        }

        private async Task UpdateAllocationDays (LeaveRequest leaveRequest, bool deductDays)
        {
            var allocation = await _leaveAllocationService.GetCurrentAllocation(leaveRequest.LeaveTypeId, leaveRequest.EmployeeId);

            var numberOfDays = CalculateDays(leaveRequest.StartDate , leaveRequest.EndDate);

            if (deductDays)
            {
                allocation.Days -= numberOfDays;
            }
            else
            {
                allocation.Days += numberOfDays;
            }

            _context.Entry(allocation).State = EntityState.Modified;

        }

        private int CalculateDays(DateOnly start, DateOnly end)
        {
            return end.DayNumber - start.DayNumber;
        }
    }
}
