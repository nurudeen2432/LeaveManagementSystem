using System.ComponentModel;

namespace LeaveManagementSystem.Web.Models.LeaveRequests
{
    public class EmployeeLeaveRequestListVM
    {
        [DisplayName("Total Number of Request")]

        public int TotalRequests { get; set; }

        [DisplayName("Total Number of Approved Requests")]

        public int ApprovedRequests { get; set; }

        [DisplayName("Pending Requests")]
        public int PendingRequests { get; set; }

        [DisplayName("Rejected Requests")]

        public int DeclinedRequests { get; set; }

        public List<LeaveRequestReadOnlyVM> LeaveRequests { get; set; } = new List<LeaveRequestReadOnlyVM>();


    }
}