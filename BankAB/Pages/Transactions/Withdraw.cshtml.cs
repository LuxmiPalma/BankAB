using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;


namespace BankAB.Pages.Transactions
{
    public class WithdrawModel : PageModel
    {
        private readonly BankAppDataContext _context;

        public WithdrawModel(BankAppDataContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Range(100, 10000)]
        public decimal Amount { get; set; }

        public decimal CurrentBalance { get; set; }
        public void OnGet(int accountId)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
            CurrentBalance = account?.Balance ?? 0;
        }
        public IActionResult OnPost(int accountId)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
            if (account == null) return NotFound();

            if (account.Balance < Amount)
            {
                ModelState.AddModelError("Amount", "You don't have that much money!");
                CurrentBalance = account.Balance;
                return Page();
            }

            account.Balance -= Amount;
            _context.SaveChanges();

            return RedirectToPage("/Accounts");
        }
    }
}
  
