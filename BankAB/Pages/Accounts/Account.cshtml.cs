using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Services;

namespace BankAB.Pages.Accounts
{
    public class AccountModel : PageModel
    {
        private readonly IAccountService _accountService;
        public int Id { get; set; }
        public decimal Balance { get; set; }

        public AccountModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public void OnGet(int id)
        {
            var account = _accountService.GetAccount(id); // ? use service method

            if (account != null)
            {
                Id = account.AccountId;
                Balance = account.Balance;
            }
        }

    }
}
