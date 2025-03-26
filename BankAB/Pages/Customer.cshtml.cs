using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAB.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly BankAppDataContext _dbContext;

        public CustomerModel(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string FullName { get; set; }
        public string Email { get; set; }

        public void OnGet(int id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.CustomerId == id);

            if (customer != null)
            {
                FullName = customer.Givenname + " " + customer.Surname;
                Email = customer.Emailaddress;
            }
        }
    }
}
