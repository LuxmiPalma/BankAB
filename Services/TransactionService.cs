using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Services.Infrastructure.Paging;
using Services.ViewModels;



namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankAppDataContext _context;

        public TransactionService(BankAppDataContext context)
        {
            _context = context;
        }

        public List<Transaction> GetAllTransactions(string? sortColumn = null, string? sortOrder = null)
        {
            var query = _context.Transactions
                .Include(t => t.AccountNavigation)
                .ThenInclude(a => a.Dispositions)
                .Select(t => new Transaction
            {
                TransactionId = t.TransactionId,
                AccountId = t.AccountId,
                Date = t.Date,
                Amount = t.Amount,
                 AccountNavigation = t.AccountNavigation,

                });

            if (sortColumn == "TransactionId")
                query = sortOrder == "desc" ? query.OrderByDescending(x => x.TransactionId) : query.OrderBy(x => x.TransactionId);
            else if (sortColumn == "Date")
                query = sortOrder == "desc" ? query.OrderByDescending(x => x.Date) : query.OrderBy(x => x.Date);
            else if (sortColumn == "Amount")
                query = sortOrder == "desc" ? query.OrderByDescending(x => x.Amount) : query.OrderBy(x => x.Amount);

            return query.ToList();
        }

        public List<Transaction> SearchByTransactionId(int transactionId)
        {
            return _context.Transactions
                .Include(t => t.AccountNavigation)
            .ThenInclude(a => a.Dispositions)
                .Where(t => t.TransactionId == transactionId)
                .ToList();
        }

        public Transaction? GetTransactionById(int id)
        {
            return _context.Transactions.FirstOrDefault(t => t.TransactionId == id);
        }
        public List<Transaction> GetTransactionsByCustomerId(int customerId, string? sortColumn = null, string? sortOrder = null)
        {
            var query = _context.Transactions
                .AsNoTracking()
                .Include(t => t.AccountNavigation)
                    .ThenInclude(a => a.Dispositions)
                .Where(t => t.AccountNavigation.Dispositions.Any(d => d.CustomerId == customerId));

           

            return query.ToList();
        }
        public PagedResult<TransactionViewModel> GetPagedTransactions(int pageNo, int pageSize, string? sortColumn, string? sortOrder, int? customerId = null)
        {
            var query = _context.Transactions
                .AsNoTracking()
                .Include(t => t.AccountNavigation)
                    .ThenInclude(a => a.Dispositions)
                .Where(t => customerId == null || t.AccountNavigation.Dispositions.Any(d => d.CustomerId == customerId));

            var projectedQuery = query.Select(t => new TransactionViewModel
            {
                TransactionId = t.TransactionId,
                AccountId = t.AccountId,
                Amount = t.Amount,
                Date = t.Date,
                CustomerId = t.AccountNavigation.Dispositions.FirstOrDefault().CustomerId,
                Operation = t.Operation
            });

            if (!string.IsNullOrEmpty(sortColumn))
            {
                if (sortOrder == "desc")
                {
                    projectedQuery = sortColumn switch
                    {
                        "TransactionId" => projectedQuery.OrderByDescending(x => x.TransactionId),
                        "AccountId" => projectedQuery.OrderByDescending(x => x.AccountId),
                        "CustomerId" => projectedQuery.OrderByDescending(x => x.CustomerId),
                        "Date" => projectedQuery.OrderByDescending(x => x.Date),
                        "Amount" => projectedQuery.OrderByDescending(x => x.Amount),
                        _ => projectedQuery.OrderByDescending(x => x.TransactionId)
                    };
                }
                else
                {
                    projectedQuery = sortColumn switch
                    {
                        "TransactionId" => projectedQuery.OrderBy(x => x.TransactionId),
                        "AccountId" => projectedQuery.OrderBy(x => x.AccountId),
                        "CustomerId" => projectedQuery.OrderBy(x => x.CustomerId),
                        "Date" => projectedQuery.OrderBy(x => x.Date),
                        "Amount" => projectedQuery.OrderBy(x => x.Amount),
                        _ => projectedQuery.OrderBy(x => x.TransactionId)
                    };
                }
            }

            return projectedQuery.GetPaged(pageNo, pageSize);
        }

    }
}