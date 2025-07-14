using System.ComponentModel.DataAnnotations;

namespace Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MaximumAgeAttribute : ValidationAttribute
    {
        private readonly int _maximumAge;

        public MaximumAgeAttribute(int maximumAge)
        {
            _maximumAge = maximumAge;
            ErrorMessage ??= $"Возраст должен быть не более {_maximumAge} лет";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Year;

                if (dateOfBirth.Date > today.AddYears(-age))
                {
                    age--;
                }

                if (age > _maximumAge)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
