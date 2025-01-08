using System.ComponentModel.DataAnnotations.Schema;
using static LeaveManagementSystem.Web.Data.LeaveType;

namespace LeaveManagementSystem.Web.Data;

public partial class LeaveType 
{
    //Based on the Name of the property Id Efcore determines the Primary Key of the Leavetype Entity.

    public Guid Id { get; set; }

    [Column(TypeName ="nvarchar(150)")]
    public string Name { get; set; } = default!;

    public int NumberOfDays { get; set; }

    public List<LeaveAllocation> LeaveAllocations { get; set; }


}
