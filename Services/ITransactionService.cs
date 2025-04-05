using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;


namespace Services
{
    public interface ITransactionService
    {
        List<TransactionViewModel> GetAllTransactions(string? sortColumn = null, string? sortOrder = null);
        List<TransactionViewModel> SearchByTransactionId(int transactionId);
    }
}
