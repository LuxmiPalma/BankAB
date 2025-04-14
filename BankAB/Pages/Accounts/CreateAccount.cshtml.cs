using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankAB.Pages.Accounts
{
    public class CreateAccountModel : PageModel
    {
        private readonly BankAppDataContext _db;

        public CreateAccountModel(BankAppDataContext db)
        {
            _db = db;
        }

        [BindProperty(SupportsGet = true)]
        public int CustomerId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Frequency is required")]
        public string Frequency { get; set; }

        [BindProperty]
        [Range(50, 50000, ErrorMessage = "Initial deposit must be between 50 and 50.000 SEK.")]
        public decimal Balance { get; set; }

        public void OnGet()
        {
        }
    }
}
