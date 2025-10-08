using System.ComponentModel.DataAnnotations;

namespace Library.Attributes;

public class NameAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Name should not be empty");
        }

        var name = value.ToString()?.Trim();

        if (string.IsNullOrEmpty(name))
        {
            return new ValidationResult("Name should not be empty");
        }

        if (name.Length >= 2 && name.Length <= 30)
        {
            return ValidationResult.Success!;
        }

        return new ValidationResult("Invalid name");
    }
}