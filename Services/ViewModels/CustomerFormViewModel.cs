using Microsoft.AspNetCore.Mvc;
using Services.Infrastructure.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class CustomerFormViewModel
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string Givenname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sur name is required.")]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required.")]
        public int? CountryId { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Streetaddress is required.")]
        public string Streetaddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zipcode is required.")]
        public string Zipcode { get; set; } = string.Empty;
        public string? NationalId { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Emailaddress { get; set; }
        [PhoneNumber]
        public string? Telephonenumber { get; set; }
        public string? Telephonecountrycode { get; set; }
        [BindProperty]
        [Range(1900, 2100)]
        public int? BirthdayYear { get; set; }

        [BindProperty]
        public int? BirthdayMonth { get; set; }

        [BindProperty]
        public int? BirthdayDay { get; set; }

    }
}
