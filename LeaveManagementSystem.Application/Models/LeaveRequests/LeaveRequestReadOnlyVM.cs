using System.ComponentModel;
using static LeaveManagementSystem.Application.Services.LeaveRequests.LeaveRequestsService;

namespace LeaveManagementSystem.Application.Models.LeaveRequests
{
    public class LeaveRequestReadOnlyVM
    {
        public Guid Id { get; set; }

        [DisplayName("Start Date")]
        public DateOnly StartDate { get; set; }

        [DisplayName("End Date")]

        public DateOnly EndDate { get; set; }

        [DisplayName("Total Days")]
        public int NumberOfDays { get; set; }

        [DisplayName("Leave Type")]

        public string LeaveTypes { get; set; } = string.Empty;


        public LeaveRequestStatuses LeaveRequestStatus;

    }
}