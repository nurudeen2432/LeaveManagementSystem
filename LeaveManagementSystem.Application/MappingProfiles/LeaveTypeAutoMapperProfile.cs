﻿using AutoMapper;
using LeaveManagementSystem.Application.Models.LeaveTypes;

namespace LeaveManagementSystem.Application.MappingProfiles;

public class LeaveTypeAutoMapperProfile : Profile
{
    public LeaveTypeAutoMapperProfile()
    {
        CreateMap<LeaveType, LeaveTypeReadOnlyVM>();

        CreateMap<LeaveTypeCreateVM, LeaveType>();

        CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();
        //.ForMember(dest => dest.NumberOfDays, opt => opt.MapFrom(src => src.NumberOfDays));
    }



}
