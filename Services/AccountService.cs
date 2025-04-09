using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Services
{
    public class AccountService : IAccountService
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
        public Account GetAccountWithCustomers(int accountId)
        {
            return _dbContext.Accounts
                .Where(a => a.AccountId == accountId)
                .Select(a => new Account
                {
                    AccountId = a.AccountId,
                    Balance = a.Balance,
                    Created = a.Created,
                    Frequency = a.Frequency,
                    Dispositions = a.Dispositions.Select(d => new Disposition
                    {
                        CustomerId = d.CustomerId,
                        Type = d.Type,
                        Customer = new Customer
                        {
                            Givenname = d.Customer.Givenname,
                            Surname = d.Customer.Surname,
                            CustomerId = d.Customer.CustomerId
                        }
                    }).ToList()
                }).FirstOrDefault();
        }


        public void UpdateAccount(Account account)
        {
            _dbContext.SaveChanges();
        }
        // NEW: Withdraw
        public ErrorCode Withdraw(int accountId, decimal amount)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.AccountId == accountId);
            if (account == null) return ErrorCode.BalanceTooLow; // fallback

            if (amount < 100 || amount > 10000)
                return ErrorCode.IncorrectAmount;

            if (account.Balance < amount)
                return ErrorCode.BalanceTooLow;

            account.Balance -= amount;
            _dbContext.SaveChanges();
            return ErrorCode.OK;
        }

        //  NEW: Deposit
        public ErrorCode Deposit(int accountId, decimal amount, string comment)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.AccountId == accountId);
            if (account == null) return ErrorCode.IncorrectAmount;

            if (amount < 100 || amount > 10000)
                return ErrorCode.IncorrectAmount;

            if (string.IsNullOrWhiteSpace(comment))
                return ErrorCode.CommentEmpty;

            account.Balance += amount;
            _dbContext.SaveChanges();
            return ErrorCode.OK;
        }
    }
    public enum ErrorCode
    {
        OK,
        BalanceTooLow,
        IncorrectAmount,
        CommentEmpty
    }
}




    

