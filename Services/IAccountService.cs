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
        Account GetAccount(int accountId);
        void UpdateAccount(Account account);

        List<Account> GetAccounts(string sortColumn, string sortOrder);

    }

}
