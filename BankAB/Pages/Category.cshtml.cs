using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace BankAB.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CategoryModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public string SelectedGroupBy { get; set; }
        public List<string> GroupValues { get; set; } = new();
        public bool ShowGroups { get; set; }

        public void OnGet()
        {
        }
    }
}
