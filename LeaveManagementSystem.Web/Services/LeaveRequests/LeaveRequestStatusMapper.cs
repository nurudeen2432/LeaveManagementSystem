using static LeaveManagementSystem.Web.Services.LeaveRequests.LeaveRequestsService;

namespace LeaveManagementSystem.Web.Services.LeaveRequests;

public class LeaveRequestStatusMapper
{
    private static readonly Dictionary<LeaveRequestStatuses, Guid> StatusToGuidMap = new()
    {
        { LeaveRequestStatuses.Pending, Guid.Parse("2caff07e-1485-4603-813d-839098f2cc62") },
        { LeaveRequestStatuses.Approved, Guid.Parse("A07FCF48-E449-4638-94EA-07BE6C232F55") },
        { LeaveRequestStatuses.Declined, Guid.Parse("388d0617-2de5-49a2-9408-f9f5819b0d5a") },
        { LeaveRequestStatuses.Canceled, Guid.Parse("c565c527-ff11-41ef-af5d-6f1d66ba3418") }
    };

    public static readonly Dictionary<Guid, LeaveRequestStatuses> GuidToStatusMap =
        StatusToGuidMap.ToDictionary(kv => kv.Value, kv => kv.Key);

    public static Guid ToGuid(LeaveRequestStatuses status) =>
        StatusToGuidMap.TryGetValue(status, out var guid) ? guid : throw new ArgumentOutOfRangeException(nameof(status));

    public static LeaveRequestStatuses ToStatus(Guid guid) =>
        GuidToStatusMap.TryGetValue(guid, out var status) ? status : throw new ArgumentOutOfRangeException(nameof(guid));
}

