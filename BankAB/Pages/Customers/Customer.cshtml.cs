using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace BankAB.Pages.Customers
{
    public class CustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IPersonService _personService;

        public CustomerModel(ICustomerService customerService, IPersonService personService)
        {
            _customerService = customerService;
            _personService = personService;
        }

        public CustomerDTO? SelectedCustomer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            SelectedCustomer = await _personService.GetCustomerDtoByIdAsync(id);

            if (SelectedCustomer == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _personService.DeleteCustomerAsync(id);
            return RedirectToPage("/Customers");
        }
    }
}
