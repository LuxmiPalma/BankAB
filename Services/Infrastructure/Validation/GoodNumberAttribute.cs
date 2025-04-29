using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Infrastructure.Validation
{
    public class GoodNumberAttribute: ValidationAttribute

    {
        public GoodNumberAttribute()
        {
            ErrorMessage = "Det var INTE en bra siffra. Skriv 25, 50, 75 eller 100";
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success; // let [Required] handle null

            if (int.TryParse(value.ToString(), out int number))
            {
                var goodNumbers = new[] { 25, 50, 75, 100 };
                if (goodNumbers.Contains(number))
                    return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}

