using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace BankAB.Pages.Accounts
{
    public class DeleteModel : PageModel
    {
        private readonly IAccountService _accountService;

        public DeleteModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [BindProperty]
        public Account Account { get; set; }

        public IActionResult OnGet(int id)
        {
            Account = _accountService.GetAccountWithCustomers(id);

            if (Account == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            _accountService.DeleteAccount(id);
            return RedirectToPage("/Accounts/Accounts");
        }
    }
}


