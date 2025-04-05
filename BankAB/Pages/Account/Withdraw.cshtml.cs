using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;


namespace BankAB.Pages.Account
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
    }
}
