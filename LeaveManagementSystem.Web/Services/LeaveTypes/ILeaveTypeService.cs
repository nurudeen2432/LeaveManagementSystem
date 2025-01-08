using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.Services.LeaveTypes
{
    public interface ILeaveTypeService
    {
        Task<bool> CheckIfLeaveTypeNameExist(string name);
        Task<bool> CheckIfLeaveTypeNameExistForEdit(LeaveTypeEditVM leaveTypeEdit);
        Task Create(LeaveTypeCreateVM model);
        Task Edit(LeaveTypeEditVM model);
        Task<T?> Get<T>(Guid id) where T : class;
        Task<List<LeaveTypeReadOnlyVM>> GetAll();
        bool LeaveTypeExists(Guid id);
        Task Remove(Guid? id);
        Task<bool> DaysExceedMaximum(Guid leaveTypeId, int days);
    }
}