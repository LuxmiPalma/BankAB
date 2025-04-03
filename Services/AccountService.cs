using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService:IAccountService
    {
        private readonly BankAppDataContext _dbContext;

        public AccountService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Account> GetAccounts(string sortColumn, string sortOrder)
        {
            IQueryable<Account> query = _dbContext.Accounts;

            // Basic sorting logic
            sortColumn = sortColumn?.ToLower();
            sortOrder = sortOrder?.ToLower();

            switch (sortColumn)
            {
                case "balance":
                    query = sortOrder == "desc" ? query.OrderByDescending(a => a.Balance) : query.OrderBy(a => a.Balance);
                    break;
                case "created":
                    query = sortOrder == "desc" ? query.OrderByDescending(a => a.Created) : query.OrderBy(a => a.Created);
                    break;
                case "frequency":
                    query = sortOrder == "desc" ? query.OrderByDescending(a => a.Frequency) : query.OrderBy(a => a.Frequency);
                    break;
                default:
                    query = query.OrderBy(a => a.AccountId); // Default sort
                    break;
            }

            return query.ToList();
        }

    }
}
