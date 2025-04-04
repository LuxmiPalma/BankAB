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

        public void OnGet(string groupBy)

        {
            if (!string.IsNullOrEmpty(groupBy))
            {
                ShowGroups = true;
                SelectedGroupBy = groupBy;

                var customers = _customerService.GetCustomers();

                GroupValues = groupBy switch
                {
                    "City" => customers.Where(c => !string.IsNullOrEmpty(c.City)).Select(c => c.City).Distinct().OrderBy(c => c).ToList(),
                    "Country" => customers.Where(c => !string.IsNullOrEmpty(c.Country)).Select(c => c.Country).Distinct().OrderBy(c => c).ToList(),
                    "Gender" => customers.Where(c => !string.IsNullOrEmpty(c.Gender)).Select(c => c.Gender).Distinct().OrderBy(c => c).ToList(),
                    _ => new List<string>()
                };
            }
        }
    }
}
