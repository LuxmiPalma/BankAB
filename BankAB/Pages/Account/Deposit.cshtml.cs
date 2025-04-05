using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;


namespace BankAB.Pages.Account
{
    public class DepositModel : PageModel
    {
        private readonly BankAppDataContext _context;

        public DepositModel(BankAppDataContext context)
        {
            _context = context;
        }
        [BindProperty]
        [Range(100, 10000)]
        public decimal Amount { get; set; }

        [BindProperty]
        public DateTime DepositDate { get; set; } = DateTime.Now;

        [BindProperty]
        [Required, MinLength(5), MaxLength(250)]
        public string Comment { get; set; }

        public IActionResult OnPost(int accountId)
        {
            if (!ModelState.IsValid) return Page();

            var account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
            if (account == null) return NotFound();

            account.Balance += Amount;
            _context.SaveChanges();

            return RedirectToPage("/Accounts");
        }
    }
}

