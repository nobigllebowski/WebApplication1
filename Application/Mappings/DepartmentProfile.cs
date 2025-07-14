using Application.DTOs.Department;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile() 
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<UpdateDepartmentDto, Department>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
