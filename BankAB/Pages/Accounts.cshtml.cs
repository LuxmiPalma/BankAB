using BankAB.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

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

        public void OnGet(string sortColumn, string sortOrder)
        {
            var query = _dbContext.Accounts.Select(s => new AccountsViewModel
            {
                Id = s.AccountId,
                Frequency = s.Frequency,
                Created = s.Created,
                //Balance = s.Balance
            });



            if (sortColumn == "Id")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Id);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Id);

            if (sortColumn == "Created")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Created);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Created);

            //if (sortColumn == "Balance")
            //    if (sortOrder == "asc")
            //        query = query.OrderBy(s => s.Balance);
            //    else if (sortOrder == "desc")
            //        query = query.OrderByDescending(s => s.Balance);

            if (sortColumn == "Frequency")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Frequency);
                else if (sortOrder == "Frequency")
                    query = query.OrderByDescending(s => s.Frequency);
            Accounts = query.ToList();


        }
    }
}
