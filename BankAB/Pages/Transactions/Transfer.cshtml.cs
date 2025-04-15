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

        public Account? Account { get; set; }
        public Account? TargetAccount { get; set; }

        public void OnGet(int accountId)
        {
            AccountId = accountId;
            Account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
        }

        public IActionResult OnPost()
        {
            Account = _context.Accounts.FirstOrDefault(a => a.AccountId == AccountId);
            TargetAccount = _context.Accounts.FirstOrDefault(a => a.AccountId == TargetAccountId);

            if (!ModelState.IsValid || Account == null || TargetAccount == null)
                return Page();

            if (Account.Balance < Amount)
            {
                ModelState.AddModelError("", "Insufficient funds.");
                return Page();
            }

            Account.Balance -= Amount;
            TargetAccount.Balance += Amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = Account.AccountId,
                Amount = -Amount,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Type = "Transfer",
                Operation = $"To {TargetAccountId}",
                Balance = Account.Balance
            });

            _context.Transactions.Add(new Transaction
            {
                AccountId = TargetAccount.AccountId,
                Amount = Amount,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Type = "Transfer",
                Operation = $"From {AccountId}",
                Balance = TargetAccount.Balance
            });

            _context.SaveChanges();

            TargetAccount = _context.Accounts.FirstOrDefault(a => a.AccountId == TargetAccountId);

            TempData["SuccessMessage"] = " Transfer completed successfully!";
            return Page();
        }
    }
}