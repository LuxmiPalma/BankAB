using DataAccessLayer.DTOs;
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
        public string GroupBy { get; set; }
        public string GroupValue { get; set; }
        public string Q { get; set; }
        public int PageNo { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public List<CustomerDTO> Customers { get; set; }

        public void OnGet()
        {
        }
    }
}
