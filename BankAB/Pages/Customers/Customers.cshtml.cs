using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace BankAB.Pages.Customers
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

        public void OnGet(string sortColumn, string q,string sortOrder)
        {
            var query = _dbContext.Customers
                .Include(c => c.Country)
                .Select(c => new CustomerViewModel
                {
                    Id = c.CustomerId,
                    Name = c.Surname,
                    City = c.City,
                    Country = c.Country != null ? c.Country.CountryName : "Unknown"
                });

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(c =>
                    c.Id.ToString().Contains(q) ||
                    (c.Name != null && c.Name.Contains(q)) ||
                    (c.City != null && c.City.Contains(q)) ||
                    (c.Country != null && c.Country.Contains(q)));
            }

            if (sortColumn == "Id")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Id);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Id);

            if (sortColumn == "Surname" || sortColumn == "Name")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Name);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Name);

            if (sortColumn == "Country")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Country);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Country);

            if (sortColumn == "City")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.City);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.City);


            Customers = query.ToList();








        }
    }
}
