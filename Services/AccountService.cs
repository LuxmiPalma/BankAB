using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
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
        public Account? GetAccountWithCustomers(int accountId)
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
        public void DeleteAccount(int id)
        {
            var account = _dbContext.Accounts
                .Include(a => a.Dispositions)
                .Include(a => a.Transactions)
                .FirstOrDefault(a => a.AccountId == id);

            if (account != null)
            {
                _dbContext.Transactions.RemoveRange(account.Transactions);
                _dbContext.Dispositions.RemoveRange(account.Dispositions);
                _dbContext.Accounts.Remove(account);
                _dbContext.SaveChanges();
            }
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
        public Dictionary<string, (int customers, int accounts, decimal totalBalance)> GetDataPerCountry()
        {
            return _dbContext.Customers
                .Where(c => c.Country != null)
                .Select(c => new
                {
                    CountryName = c.Country.CountryName,
                    CustomerId = c.CustomerId,
                    Accounts = c.Dispositions
                                .Where(d => d.Account != null)
                                .Select(d => d.Account)
                                .Distinct()
                })
                .AsEnumerable()
                .GroupBy(x => x.CountryName)
                .ToDictionary(
                    g => g.Key,
                    g => (
                        customers: g.Select(x => x.CustomerId).Distinct().Count(),
                        accounts: g.SelectMany(x => x.Accounts).Distinct().Count(),
                        totalBalance: g.SelectMany(x => x.Accounts).Sum(a => a!.Balance)
                    )
                );
        }
        

    }
}
   

public enum ErrorCode
    {
        OK,
        BalanceTooLow,
        IncorrectAmount,
        CommentEmpty
    }






    

