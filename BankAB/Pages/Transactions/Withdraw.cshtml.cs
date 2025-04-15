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
        public string Comment { get; set; }

        public decimal CurrentBalance { get; set; }
        public Account? Account { get; set; }

        public void OnGet(int accountId)
        {
            Account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
            CurrentBalance = Account?.Balance ?? 0;
        }

        public IActionResult OnPost(int accountId)
        {
            Account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId); // ? assign to property

            if (Account == null) return NotFound();

            if (Account.Balance < Amount)
            {
                ModelState.AddModelError("Amount", "You don't have that much money!");
                CurrentBalance = Account.Balance;
                return Page();
            }

            Account.Balance -= Amount;

            var transaction = new Transaction
            {
                AccountId = accountId,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Amount = -Amount,
                Operation = "Comment",
                Type = "Credit",
                Balance = Account.Balance
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return RedirectToPage("/Accounts/Account", new { id = accountId });
        }
    }
}


