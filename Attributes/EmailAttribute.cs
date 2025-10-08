using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Library.Attributes
{
    public class EmailAttribute : ValidationAttribute
    {
        private static readonly Regex _emailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$", 
            RegexOptions.Compiled | RegexOptions.IgnoreCase
        );

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Email cannot be empty.");
            }
            string email = value.ToString()!;

            if (!_emailRegex.IsMatch(email))
            {
                return new ValidationResult("Invalid email format.");
            }
            return ValidationResult.Success;
        }
    }
}