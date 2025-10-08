using System.ComponentModel.DataAnnotations;

namespace Library.Attributes;

public class AgeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Age should not be empty");
        }

        if (int.TryParse(value.ToString(), out var age))
        {
            if (age > 5)
            {
                return ValidationResult.Success!;
            }
        }

        return new ValidationResult("Age must be greater than 5");
    }
}