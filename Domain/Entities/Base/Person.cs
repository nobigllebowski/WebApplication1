using Domain.Enums;

namespace Domain.Entities.Base
{
    public class Person : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty");

            FirstName = firstName.Trim();
        }

        public void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty");

            LastName = lastName.Trim();
        }

        public void SetDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.Today)
                throw new ArgumentException("Date of birth cannot be in the future", nameof(dateOfBirth));

            var minDate = DateTime.Today.AddYears(-16);
            if (dateOfBirth > minDate)
                throw new ArgumentException($"Person must be at least 16 years old. Minimum date: {minDate:yyyy-MM-dd}", nameof(dateOfBirth));

            var maxDate = DateTime.Today.AddYears(-100);
            if (dateOfBirth < maxDate)
                throw new ArgumentException($"Date of birth is too old. Maximum date: {maxDate:yyyy-MM-dd}", nameof(dateOfBirth));

            if (DateOfBirth != default && dateOfBirth > DateOfBirth)
                throw new InvalidOperationException($"Date of birth cannot be changed to a later date. Current date: {DateOfBirth:yyyy-MM-dd}");

            DateOfBirth = dateOfBirth;
        }
    }
}
