using Application.DTOs.Grade;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GradeProfile : Profile
    {
        public GradeProfile()
        {
            CreateMap<Grade, GradeDto>();

            CreateMap<UpdateGradeDto, Grade>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
