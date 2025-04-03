using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using BankAB.ViewModels;
using DataAccessLayer.Models;

namespace BankAB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICategoryService _categoryService;

       public List<CategoryViewModel> Categories { get; set; }
        public IndexModel(ILogger<IndexModel> logger
            	, ICategoryService categoryService)

        {
            _logger = logger;
            _categoryService = categoryService;

        }

        public void OnGet()
        {
            Categories = new List<CategoryViewModel>
        {
            new CategoryViewModel { GroupType = "Country", Description = "View all customers grouped by country" },
            new CategoryViewModel { GroupType = "City", Description = "View all customers grouped by city" },
            new CategoryViewModel { GroupType = "Gender", Description = "View all customers grouped by gender" }
        };
        }
    }
}
