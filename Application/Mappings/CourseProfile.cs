﻿using Application.DTOs.Course;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => !src.IsDeleted));

            CreateMap<UpdateCourseDto, Course>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
