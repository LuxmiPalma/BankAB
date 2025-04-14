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
            return Page();

        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var account = new Account
            {
                Balance = Balance,
                Created = DateTime.Now,
                Frequency = Frequency
            };

            _db.Accounts.Add(account);
            _db.SaveChanges();

            var disposition = new Disposition
            {
                AccountId = account.AccountId,
                CustomerId = CustomerId,
                Type = "OWNER"
            };

            _db.Dispositions.Add(disposition);
            _db.SaveChanges();

            return RedirectToPage("/Customers/Customer", new { id = CustomerId });
        }

    }
}
