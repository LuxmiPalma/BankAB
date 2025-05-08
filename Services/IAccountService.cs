using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;



namespace Services
{
    public interface IAccountService
    {
        Dictionary<string, (int customers, int accounts, decimal totalBalance)> GetDataPerCountry();

        Account GetAccountWithCustomers(int accountId);
        void UpdateAccount(Account account);
        void DeleteAccount(int id);


        List<Account> GetAccounts(string sortColumn, string sortOrder);
        ErrorCode Withdraw(int accountId, decimal amount);
        ErrorCode Deposit(int accountId, decimal amount, string comment);

    }

}
