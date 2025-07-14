using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Оценки
    /// </summary>
    public class Grade : BaseEntity
    {
        protected Grade() { }

        public Grade(decimal grade, string? comment) 
        {
            SetValue(grade);
            SetComment(comment);
        }

        [Required]
        [Range(0.0, 5.0, ErrorMessage = "Оценка должна быть от 0 до 5")]
        [Column(TypeName = "decimal(3, 2)")]
        public decimal Value { get; private set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateReceived { get; private set; } = DateTime.Now;

        [Required]
        [StringLength(500)]
        public string? Comment { get; private set; }

        /// <summary>
        /// Кому выставлена оценка
        /// </summary>
        [ForeignKey("Student")]
        public Guid StudentId { get; private set; }
        public Student Student { get; private set; }

        /// <summary>
        /// По какому предмету оценка
        /// </summary>
        [ForeignKey("Course")]
        public Guid CourseId { get; private set; }
        public Course Course { get; private set; }

        /// <summary>
        /// Кем выставлена оценка
        /// </summary>
        [ForeignKey("Teacher")]
        public Guid TeacherId { get; private set; }
        public Teacher Teacher { get; private set; }


        public void SetValue(decimal grade)
        {
            Value = grade;
        }

        public void SetComment(string? comment)
        {
            Comment = comment;
        }

        public void SetStudent(Student student)
        {
            Student = student ?? throw new ArgumentNullException(nameof(student));
            StudentId = student.Id;
        }

        public void SetCource(Course course)
        {
            Course = course ?? throw new ArgumentNullException(nameof(course));
            CourseId = course.Id;
        }

        public void SetTeacher(Teacher teacher)
        {
            Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
            TeacherId = teacher.Id;
        }
    }
}
