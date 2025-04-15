using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;


namespace BankAB.Pages.Accounts
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
        public Account? Account { get; set; }

        public void OnGet(int accountId)
        {
            Account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
        }
        public IActionResult OnPost(int accountId)
        {
            Account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId); // Needed for table on error

            if (!ModelState.IsValid) return Page();
            if (Account == null) return NotFound();

            Account.Balance += Amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = Account.AccountId,
                Amount = Amount,
                Date = DateOnly.FromDateTime(DepositDate),
                Type = "Deposit",
                Operation = Comment,
                Balance = Account.Balance
            });

            _context.SaveChanges();
            return RedirectToPage("/Accounts/Account", new { id = accountId });
        }
    }
}

