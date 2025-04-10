using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Enum;


namespace BankAB.Pages.CustomerEntry
{
    public class CreateModel : PageModel
    {
        private readonly BankAppDataContext _context;

        public CreateModel(BankAppDataContext context)
        {
            _context = context;
        }

        public class CustomerInputModel
        {
            [Required]
            public string Givenname { get; set; } = string.Empty;

            [Required]
            public string Surname { get; set; } = string.Empty;

            [Required]
            public string Gender { get; set; } = string.Empty;

            [Required(ErrorMessage = "Country is required.")]
            public int? CountryId { get; set; }

            [Required]
            public string City { get; set; } = string.Empty;

            [Required]
            public string Streetaddress { get; set; } = string.Empty;

            [Required]
            public string Zipcode { get; set; } = string.Empty;
        }

        [BindProperty]
        public CustomerInputModel Input { get; set; } = new();

        [BindProperty]
        [Range(1900, 2100)]
        public int? BirthdayYear { get; set; }

        [BindProperty]
        public int? BirthdayMonth { get; set; }

        [BindProperty]
        public int? BirthdayDay { get; set; }

        public List<SelectListItem> CountryList { get; set; } = new();

        public void OnGet()
        {
            LoadDropdowns();
        }

        public IActionResult OnPost()
        {
            LoadDropdowns();

            if (!ModelState.IsValid)
                return Page();

            var customer = new Customer
            {
                Givenname = Input.Givenname,
                Surname = Input.Surname,
                Gender = Input.Gender,
                CountryId = Input.CountryId,
                City = Input.City,
                Streetaddress = Input.Streetaddress,
                Zipcode = Input.Zipcode
            };

            if (BirthdayYear.HasValue && BirthdayMonth.HasValue && BirthdayDay.HasValue)
            {
                try
                {
                    customer.Birthday = new DateOnly(BirthdayYear.Value, BirthdayMonth.Value, BirthdayDay.Value);
                }
                catch
                {
                    ModelState.AddModelError("", "Invalid birth date.");
                    return Page();
                }
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "? Customer created successfully!";
            return RedirectToPage("/Customers/Customers");
        }

        private void LoadDropdowns()
        {
            CountryList = _context.Countries
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CountryName
                }).ToList();
        }
    }
}
