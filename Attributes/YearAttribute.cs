using System.ComponentModel.DataAnnotations;

namespace Library.Attributes
{

    public class YearAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"Birth year is required.");
            }

            if (int.TryParse(value.ToString(), out var birthyear))
            {
                if (birthyear >= 1700 && birthyear <= 2025)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Birth year is invalid.");
            }
            return new ValidationResult("Invalid for birth year.");
        }
    }
}