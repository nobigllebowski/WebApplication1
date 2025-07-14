namespace Application.DTOs.Student
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;

                if (DateOfBirth.Date > today.AddYears(-age))
                    age--;

                return age;
            }
        }
        public DateTime DateOfBirth { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public double Average { get; set; }
        public bool IsActive { get; set; }
    }
}
