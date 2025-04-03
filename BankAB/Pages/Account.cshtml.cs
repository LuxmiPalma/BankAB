using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAB.Pages
{
    public class AccountModel : PageModel
    {
        private readonly BankAppDataContext _dbContext;
        public string AccountId{ get; set; }

        public AccountModel(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet(int id)
        {
            AccountId = _dbContext.Accounts.First(s => s.AccountId == id).Frequency;

        }
    }
}
