using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace BankAB.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public string FullName { get; set; }
        public string Email { get; set; }

        public void OnGet(int id)
        {
            var customer = _customerService
                .GetCustomers()
                .FirstOrDefault(c => c.CustomerId == id);

            if (customer != null)
            {
                FullName = customer.Givenname + " " + customer.Surname;
                Email = customer.Emailaddress;
            }
        }
    }
}
