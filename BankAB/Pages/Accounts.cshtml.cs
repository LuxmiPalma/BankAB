using BankAB.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAB.Pages
{
    public class AccountsModel : PageModel
    {
        private readonly BankAppDataContext _dbContext;

        public AccountsModel(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<AccountsViewModel> Accounts { get; set; } = new();

        public void OnGet()
        {
            Accounts = _dbContext.Accounts.Select(s => new AccountsViewModel
            {
                Id = s.AccountId,
                Frequency = s.Frequency,
                Created = s.Created,
                Balance = s.Balance
            }).ToList();


        }
    }
}
