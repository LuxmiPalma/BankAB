using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace BankAB.Pages
{
    public class CustomerGroupModelModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerGroupModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public void OnGet()
        {
        }
    }
}
