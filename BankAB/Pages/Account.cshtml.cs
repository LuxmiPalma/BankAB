using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace BankAB.Pages
{
    public class AccountModel : PageModel
    {
        private readonly BankAppDataContext _dbContext;
        public string Id { get; set; }
        public decimal Balance { get; set; }

        public AccountModel(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet(int id)
        {
            var account = _dbContext.Accounts.FirstOrDefault(c => c.AccountId == id);

            Id = account.AccountId.ToString();
            Balance = account.Balance;
        }

    }
}
