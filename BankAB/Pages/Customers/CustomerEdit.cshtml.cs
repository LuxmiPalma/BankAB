using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;


namespace BankAB.Pages.Customers
{
    public class CustomerEditModel : PageModel
    {
        private readonly IPersonService _personService;

        public CustomerEditModel(IPersonService personService)
        {
            _personService = personService;
        }


        [BindProperty] public Customer Customer { get; set; }

        [BindProperty] public int? BirthdayYear { get; set; }
        [BindProperty] public int? BirthdayMonth { get; set; }
        [BindProperty] public int? BirthdayDay { get; set; }

        public List<SelectListItem> CountryList { get; set; } = new();
        public List<string> ChangeMessages { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Customer = await _personService.GetCustomerAsync(id);
            if (Customer == null)
                return NotFound();

            if (Customer.Birthday.HasValue)
            {
                BirthdayYear = Customer.Birthday.Value.Year;
                BirthdayMonth = Customer.Birthday.Value.Month;
                BirthdayDay = Customer.Birthday.Value.Day;
            }

            await LoadDropdowns();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            (Customer updatedCustomer, List<string> changes) = await _personService.UpdateCustomerAsync(
                Customer.CustomerId, Customer.Gender, Customer.Givenname, Customer.Surname, Customer.Streetaddress,
                Customer.City, Customer.Zipcode, Customer.CountryId ?? 0,
                Customer.Emailaddress, Customer.Telephonecountrycode, Customer.Telephonenumber,
                Customer.NationalId, BirthdayYear ?? 0, BirthdayMonth ?? 0, BirthdayDay ?? 0);

            ChangeMessages = changes;

            await LoadDropdowns();
            return Page();
        }

        private async Task LoadDropdowns()
        {
            var countries = await _personService.GetCountriesAsync();
            CountryList = countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CountryName
            }).ToList();
        }
    }
}