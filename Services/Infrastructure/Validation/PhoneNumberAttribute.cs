using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Infrastructure.Validation
{
    public class PhoneNumberAttribute: ValidationAttribute
    {
        public PhoneNumberAttribute()
        {
            ErrorMessage = "Please enter a valid phone number (6-15 digits).";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return ValidationResult.Success; // allow empty if not [Required]

            var phoneNumber = value.ToString();

            // Accept only digits, length between 6 and 15
            if (Regex.IsMatch(phoneNumber!, @"^\d{6,15}$"))
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessage);
        }
    }
}

