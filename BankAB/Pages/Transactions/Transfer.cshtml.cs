using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankAB.Pages.Transactions
{
    public class TransferModel : PageModel
    {
        private readonly BankAppDataContext _context;

        public TransferModel(BankAppDataContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int AccountId { get; set; }

        [BindProperty]
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Target account ID is required")]
        public int TargetAccountId { get; set; }

        [BindProperty]
        public string? Comment { get; set; }

    
      public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var sourceAccount = _context.Accounts.FirstOrDefault(a => a.AccountId == AccountId);
            var targetAccount = _context.Accounts.FirstOrDefault(a => a.AccountId == TargetAccountId);

            if (sourceAccount == null || targetAccount == null)
            {
                ModelState.AddModelError("", "One or both accounts were not found.");
                return Page();
            }

            if (sourceAccount.Balance < Amount)
            {
                ModelState.AddModelError("", "Insufficient funds in the source account.");
                return Page();
            }

            // Withdraw from source
            sourceAccount.Balance -= Amount;
            _context.Transactions.Add(new Transaction
            {
                AccountId = sourceAccount.AccountId,
                Amount = -Amount,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Type = "transfer",
                Operation = "Transfer to account " + TargetAccountId,
                Balance = sourceAccount.Balance,
            });

            // Deposit to target
            targetAccount.Balance += Amount;
            _context.Transactions.Add(new Transaction
            {
                AccountId = targetAccount.AccountId,
                Amount = Amount,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Type = "transfer",
                Operation = "Transfer from account " + AccountId,
                Balance = targetAccount.Balance,
            });

            _context.SaveChanges();

            TempData["SuccessMessage"] = "? Transfer completed successfully!";
            return RedirectToPage("/Accounts/Account", new { id = AccountId });
        }
    }
}

