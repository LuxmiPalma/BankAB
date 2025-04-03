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
            var query = _dbContext.Accounts.AsQueryable();
           

            if (sortColumn == "Frequency")
                query = query.OrderBy(s => s.Created);
            else if (sortColumn == "Created")
                query = query.OrderBy(s => s.Balance);

            return query.ToList();


        }

    }
}
