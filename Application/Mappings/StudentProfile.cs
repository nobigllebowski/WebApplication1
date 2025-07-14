using Application.DTOs.Student;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.FullName,
                          opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<UpdateStudentDto, Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Student, StudentLiteDto>()
                .ForMember(dest => dest.FullName,
                          opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
