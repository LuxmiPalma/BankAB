using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace BankAB.Pages
{
    public class CustomerEditModel : PageModel
    {
        private readonly IPersonService _personService;

        public CustomerEditModel(IPersonService personService)
        {
            _personService = personService;
        }

        [BindProperty] public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Customer = await _personService.GetCustomerAsync(id);
            if (Customer == null)
                return NotFound();
            return Page();
        }
        public List<string> ChangeMessages { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            (Customer updatedCustomer, List<string> changes) = await _personService.UpdateCustomerAsync(
        Customer.CustomerId, Customer.Gender, Customer.Givenname, Customer.Surname, Customer.Streetaddress,
        Customer.City, Customer.Zipcode, Customer.Country, Customer.CountryCode,
        Customer.Emailaddress, Customer.Telephonecountrycode, Customer.Telephonenumber,
        Customer.NationalId, Customer.Birthday?.Year ?? 0, Customer.Birthday?.Month ?? 0, Customer.Birthday?.Day ?? 0);

            ChangeMessages = changes;

            // Stay on the same page to display changes
            return Page();
        }

    }
}

