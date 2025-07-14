using Application.DTOs.Teacher;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, TeacherDto>()
                .ForMember(dest => dest.FullName,
                          opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<UpdateTeacherDto, Teacher>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
