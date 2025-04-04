using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using BankAB.Infrastructure.Paging;


namespace BankAB.Pages
{
    public class CustomerGroupModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerGroupModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public string GroupBy { get; set; }
        public string GroupValue { get; set; }
        public string Q { get; set; }
        public int PageNo { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        //public List<CustomerDTO> Customers { get; set; }
        public PagedResult<CustomerDTO> PagedCustomers { get; set; }

        public void OnGet(string groupBy, string groupValue, string q, int pageNo = 1, string sortColumn = "Givenname", string sortOrder = "asc")
        {
            Q = q;
            GroupBy = groupBy;
            GroupValue = groupValue;
            SortColumn = sortColumn ?? "Givenname";
            SortOrder = sortOrder ?? "asc";

            var customers = _customerService.GetCustomers()
                .Where(c =>
                    (groupBy == "City" && c.City == groupValue) ||
                    (groupBy == "Country" && c.Country == groupValue) ||
                    (groupBy == "Gender" && c.Gender == groupValue));

            if (!string.IsNullOrEmpty(Q))
            {
                customers = customers.Where(c =>
                (c.Givenname != null && c.Givenname.Contains(Q)) ||
                (c.Surname != null && c.Surname.Contains(Q)) ||
                (c.City != null && c.City.Contains(Q)) ||
                (c.Country != null && c.Country.Contains(Q)) ||
                (c.Gender != null && c.Gender.Contains(Q))
                );
            }
        

            if (SortColumn == "Givenname")
                customers = SortOrder == "desc"
                    ? customers.OrderByDescending(c => c.Givenname)
                    : customers.OrderBy(c => c.Givenname);

            if (SortColumn == "City")
                customers = SortOrder == "desc"
                    ? customers.OrderByDescending(c => c.City)
                    : customers.OrderBy(c => c.City);

            PagedCustomers = customers.AsQueryable().GetPaged(pageNo, 10);
        }
    }
}
