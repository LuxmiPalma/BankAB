using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Services.Infrastructure.Paging;
using Services.ViewModels;




namespace Services
{
    public interface ITransactionService
    {
        List<Transaction> GetAllTransactions(string? sortColumn = null, string? sortOrder = null);
        List<Transaction> SearchByTransactionId(int transactionId);
        Transaction? GetTransactionById(int id);
        List<Transaction> GetTransactionsByCustomerId(int customerId, string? sortColumn = null, string? sortOrder = null);
        PagedResult<TransactionViewModel> GetPagedTransactions(int pageNo, int pageSize, string? sortColumn, string? sortOrder, int? customerId = null);


    }
}
