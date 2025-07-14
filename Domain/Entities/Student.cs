using Domain.Entities.Base;

namespace Domain.Entities
{
    /// <summary>
    /// Студент
    /// </summary>
    public class Student : Person
    {
        public Student() { }

        public Student(string firstName, string lastName)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
        }

        public double Average { get; private set; } = 0;

        /// <summary>
        /// Факультет
        /// </summary>
        public Guid DepartmentId { get; private set; }
        public Department Department { get; private set; }

        /// <summary>
        /// Оценки
        /// </summary>
        public IList<Grade> Grades { get; set; } = new List<Grade>();

        public void SetAverage(double average)
        {
            Average = average;
        }

        public void SetDepartment(Department department)
        {
            DepartmentId = department.Id;
            Department = department;
        }
        
    }
}
