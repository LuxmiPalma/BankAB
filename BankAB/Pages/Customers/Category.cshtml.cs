using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace BankAB.Pages.Customers
{
    [ResponseCache(Duration = 30, VaryByQueryKeys = new[] { "groupBy" })]
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
        public int PageNo { get; set; }
        public int TotalPages { get; set; }

        public void OnGet(string groupBy, int pageNo = 1)

        {
            PageNo = pageNo == 0 ? 1 : pageNo;
            const int pageSize = 10;
            {
                if (!string.IsNullOrEmpty(groupBy))
                {
                    ShowGroups = true;
                    SelectedGroupBy = groupBy;

                    var customers = _customerService.GetCustomers();

                    var allValues = groupBy switch
                    {
                        "City" => customers.Where(c => !string.IsNullOrEmpty(c.City)).Select(c => c.City).Distinct().OrderBy(c => c).ToList(),
                        "Country" => customers.Where(c => !string.IsNullOrEmpty(c.Country)).Select(c => c.Country).Distinct().OrderBy(c => c).ToList(),
                        "Gender" => customers.Where(c => !string.IsNullOrEmpty(c.Gender)).Select(c => c.Gender).Distinct().OrderBy(c => c).ToList(),
                        _ => new List<string>()
                    };
                    TotalPages = (int)Math.Ceiling(allValues.Count / (double)pageSize);

                    GroupValues = allValues
                        .Skip((PageNo - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                }
            }
        }
    }
}
