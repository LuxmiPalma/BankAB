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

        [BindProperty]
        public Customer Customer { get; set; } = new();

        [BindProperty]
        [Range(1900, 2100)]
        public int? BirthdayYear { get; set; }

        [BindProperty]
        public int? BirthdayMonth { get; set; }

        [BindProperty]
        public int? BirthdayDay { get; set; }

        public List<SelectListItem> GenderList { get; set; } = new();
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

            if (BirthdayYear.HasValue && BirthdayMonth.HasValue && BirthdayDay.HasValue)
            {
                try
                {
                    Customer.Birthday = new DateOnly(BirthdayYear.Value, BirthdayMonth.Value, BirthdayDay.Value);
                }
                catch
                {
                    ModelState.AddModelError("", "Invalid birth date.");
                    return Page();
                }
            }

            _context.Customers.Add(Customer);
            _context.SaveChanges();

            return RedirectToPage("/Customers/Index"); // Or wherever you list your customers
        }

        private void LoadDropdowns()
        {
            GenderList = Enum.GetValues(typeof(Gender))
                .Cast<Gender>()
                .Select(g => new SelectListItem
                {
                    Value = ((int)g).ToString(),
                    Text = g.ToString()
                }).ToList();

            CountryList = _context.Countries
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CountryName
                }).ToList();
        }
    }
}
   
