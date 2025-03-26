using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace BankAB.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly BankAppDataContext _dbContext;


        public CustomersModel(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        public class CustomerViewModel
        {

            public int Id { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
        }
        public List<CustomerViewModel> Customers { get; set; }

        public void OnGet()
        {
            Customers = _dbContext.Customers
                .Select(s => new CustomerViewModel
                {
                    Id = s.CustomerId,
                    Name = s.Surname,
                    City = s.City,
                    Country = s.Country
                }).ToList();




        }
    }
}
