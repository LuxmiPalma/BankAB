using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.ViewModels;
using DataAccessLayer.Models;

namespace BankAB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICategoryService _categoryService; 
        private readonly IAccountService _accountService;
        public Dictionary<string, (int customers, int accounts, decimal totalBalance)> DataPerCountry { get; set; }

        public int TotalCustomers { get; set; }
        public int TotalAccounts { get; set; }
        public decimal TotalAssets { get; set; }
        public int TotalCountries { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public IndexModel(ILogger<IndexModel> logger
            	, ICategoryService categoryService,IAccountService accountService)

        {
            _logger = logger;
            _categoryService = categoryService;
            _accountService = accountService;

        }

        public void OnGet()
        {
            Categories = new List<CategoryViewModel>
            {
            new CategoryViewModel { GroupType = "Country", Description = "View all customers grouped by country" },
            new CategoryViewModel { GroupType = "City", Description = "View all customers grouped by city" },
            new CategoryViewModel { GroupType = "Gender", Description = "View all customers grouped by gender" }
            };
            DataPerCountry = _accountService.GetDataPerCountry();
            TotalCustomers = DataPerCountry.Sum(x => x.Value.customers);
            TotalAccounts = DataPerCountry.Sum(x => x.Value.accounts);
            TotalAssets = DataPerCountry.Sum(x => x.Value.totalBalance);
            TotalCountries = DataPerCountry.Count;

        }
    }
}
