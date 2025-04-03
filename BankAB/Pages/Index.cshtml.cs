using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace BankAB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICategoryService _categoryService;

        public IndexModel(ILogger<IndexModel> logger
            	, ICategoryService categoryService)

        {
            _logger = logger;
            _categoryService = categoryService;

        }

        public void OnGet()
        {

        }
    }
}
