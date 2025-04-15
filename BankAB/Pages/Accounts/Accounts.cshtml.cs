using Services.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Azure;
using Services.Infrastructure.Paging;
namespace BankAB.Pages.Accounts
{
    public class AccountsModel : PageModel
    {
        private readonly BankAppDataContext _dbContext;

        public AccountsModel(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<AccountsViewModel> Accounts { get; set; } = new();
        public PagedResult<AccountsViewModel> PagedAccounts { get; set; }

        public string Q { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }


        public void OnGet(string q = "", string sortColumn = "Id", string sortOrder = "asc", int pageNo = 1)
        {
            Q = q;
            SortColumn = sortColumn;
            SortOrder = sortOrder;

            var query = _dbContext.Accounts.Select(s => new AccountsViewModel
            {
                Id = s.AccountId,
                Frequency = s.Frequency,
                Created = s.Created,
            });

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(a =>
                    a.Id.ToString().Contains(q) ||
                    a.Frequency.Contains(q) ||
                    a.Created.ToString().Contains(q));
            }

            query = sortColumn switch
            {
                "Id" => sortOrder == "asc" ? query.OrderBy(s => s.Id) : query.OrderByDescending(s => s.Id),
                "Frequency" => sortOrder == "asc" ? query.OrderBy(s => s.Frequency) : query.OrderByDescending(s => s.Frequency),
                "Created" => sortOrder == "asc" ? query.OrderBy(s => s.Created) : query.OrderByDescending(s => s.Created),
                _ => query.OrderBy(s => s.Id)
            };
            PagedAccounts = query.GetPaged(pageNo, 20);

        }

    }
}
