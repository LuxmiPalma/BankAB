using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankAB.Pages.Accounts
{
    public class CreateAccountModel : PageModel
    {
        private readonly BankAppDataContext _context;

        public CreateAccountModel(BankAppDataContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int CustomerId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Frequency is required")]
        public string Frequency { get; set; }

        [BindProperty]
        [Range(50, 50000, ErrorMessage = "Initial deposit must be between 50 and 50.000 SEK.")]
        public decimal Balance { get; set; }
        public bool Created { get; set; } = false;


        public IActionResult OnGet()
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
                Created = DateOnly.FromDateTime(DateTime.Now),
                Frequency = Frequency
            };

            _context.Accounts.Add(account);
            _context.SaveChanges();

            var disposition = new Disposition
            {
                AccountId = account.AccountId,
                CustomerId = CustomerId,
                Type = "OWNER"
            };

            _context.Dispositions.Add(disposition);
            _context.SaveChanges();

            Created = true;
            ModelState.Clear(); // clear the form
            Frequency = string.Empty;
            Balance = 0;

            return RedirectToPage("/Customers/Customer", new { id = CustomerId });

        }

    }
}
