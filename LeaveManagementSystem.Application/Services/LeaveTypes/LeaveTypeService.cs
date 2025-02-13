﻿using AutoMapper;
using LeaveManagementSystem.Application.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LeaveManagementSystem.Application.Services.LeaveTypes;

public class LeaveTypeService(ApplicationDbContext _context, IMapper _mapper, ILogger<LeaveTypeService> _logger) : ILeaveTypeService
{


    public async Task<List<LeaveTypeReadOnlyVM>> GetAll()
    {
        var data = await _context.LeaveTypes.ToListAsync();


        var viewData = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);

        return viewData;

    }

    public async Task<T?> Get<T>(Guid id) where T : class
    {
        var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);

        if (data == null)
        {
            return null;
        }
        var viewData = _mapper.Map<T>(data);

        return viewData;
    }


    public async Task Remove(Guid? id)
    {
        var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);

        if (data != null)
        {
            _context.Remove(data);

            await _context.SaveChangesAsync();
        }




    }

    public async Task Edit(LeaveTypeEditVM model)
    {
      
            var leaveType = _mapper.Map<LeaveType>(model);

            _context.Update(leaveType);

            await _context.SaveChangesAsync();
        
       
    }

    public async Task Create(LeaveTypeCreateVM model)
    {
        _logger.LogInformation("Creating Leave Type: {LeaveTypeName} - {days}", model.Name, model.NumberOfDays);
        var leaveType = _mapper.Map<LeaveType>(model);

        _context.Add(leaveType);

        await _context.SaveChangesAsync();

    }

    public bool LeaveTypeExists(Guid id)
    {
        return _context.LeaveTypes.Any(e => e.Id == id);
    }

    public async Task<bool> CheckIfLeaveTypeNameExist(string name)
    {
        var lowerCaseName = name.ToLower();
        return await _context.LeaveTypes.AnyAsync(m => m.Name.ToLower().Equals(lowerCaseName));
    }

    public async Task<bool> CheckIfLeaveTypeNameExistForEdit(LeaveTypeEditVM leaveTypeEdit)
    {
        var lowerCaseName = leaveTypeEdit.Name.ToLower();
        return await _context.LeaveTypes.AnyAsync(m => m.Name.ToLower().Equals(lowerCaseName)
        && m.Id != leaveTypeEdit.Id
        );
    }

    public async Task<bool> DaysExceedMaximum(Guid leaveTypeId, int days)
    {
        var leaveType = await _context.LeaveTypes.FindAsync(leaveTypeId);
        return leaveType.NumberOfDays < days;

    }



}
