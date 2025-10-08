using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Attributes
{
    public class BookNumAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"Number of pages is required.");
            }

            if (int.TryParse(value.ToString(), out var num))
            {
                if (num >= 1 && num <= 10000)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult($"Book must have at least 2 pages and not more then 10000 pages.");
            }

            return new ValidationResult("Invalid number format for book pages.");
        }
    }
}