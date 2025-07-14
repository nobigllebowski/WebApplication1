using System.ComponentModel.DataAnnotations;

namespace Core.Attributes
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
            ErrorMessage ??= $"Возраст должен быть не менее {_minimumAge} лет";
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value is DateTime dateOfBirth)
            {
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Year;

                if (dateOfBirth > today.AddYears(-age))
                    age--; 

                if (age < _minimumAge)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
