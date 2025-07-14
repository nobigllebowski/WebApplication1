using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Преподаватель
    /// </summary>
    public class Teacher : Person
    {
        public Teacher() { }
        public Teacher(string firstName, string lastName)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
        }

        /// <summary>
        /// Факультет
        /// </summary>
        public Guid DepartmentId { get; private set; }
        public Department Department { get; private set; }

        /// <summary>
        /// Какой предмет ведет
        /// </summary>
        [ForeignKey("Course")]
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        /// <summary>
        /// Выставленные оценки
        /// </summary>
        public IList<Grade> Grades { get; set; } = new List<Grade>();

        public void SetDepartment(Department department)
        {
            Department = department ?? throw new ArgumentNullException(nameof(department));
            DepartmentId = department.Id;
        }

        public void SetCource(Course course)
        {
            Course = course ?? throw new ArgumentNullException(nameof(course));
            CourseId = course.Id;
        }
    }
}
