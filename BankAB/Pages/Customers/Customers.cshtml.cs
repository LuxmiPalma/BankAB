using Azure;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Services;
using Services.Infrastructure.Paging;

namespace BankAB.Pages.Customers
{
    public class CustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;


        public CustomersModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        public string Q { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int PageNo { get; set; }

        public PagedResult<CustomerDTO> PagedCustomers { get; set; }

        public void OnGet(string q = "", string sortColumn = "CustomerId", string sortOrder = "desc", int pageNo = 1)
        {
            Q = q;
            SortColumn = sortColumn;
            SortOrder = sortOrder;
            PageNo = pageNo;

            var customers = _customerService.GetCustomers().AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                customers = customers.Where(c =>
                    c.CustomerId.ToString().Contains(q) ||
                    (c.Surname != null && c.Surname.Contains(q)) ||
                    (c.Givenname != null && c.Givenname.Contains(q)) ||
                    (c.City != null && c.City.Contains(q)) ||
                    (c.Country != null && c.Country.Contains(q)));
            }

            customers = SortColumn switch
            {
                "CustomerId" => SortOrder == "desc" ? customers.OrderByDescending(c => c.CustomerId) : customers.OrderBy(c => c.CustomerId),
                "Surname" => SortOrder == "desc" ? customers.OrderByDescending(c => c.Surname) : customers.OrderBy(c => c.Surname),
                "City" => SortOrder == "desc" ? customers.OrderByDescending(c => c.City) : customers.OrderBy(c => c.City),
                "Country" => SortOrder == "desc" ? customers.OrderByDescending(c => c.Country) : customers.OrderBy(c => c.Country),
                _ => customers.OrderByDescending(c => c.CustomerId)
            };

            PagedCustomers = customers.GetPaged(PageNo, 10);
        }
    }
}
