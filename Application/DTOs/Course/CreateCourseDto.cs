﻿using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Course
{
    public class CreateCourseDto
    {
        [Required]
        public string Name { get; set; }
    }
}
