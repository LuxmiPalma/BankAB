using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

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
                AccountNavigation = t.AccountNavigation

                    //CustomerId = t.AccountNavigation.CustomerId
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
                .Where(t => t.TransactionId == transactionId)
                .Select(t => new Transaction
                {
                    TransactionId = t.TransactionId,
                    AccountId = t.AccountId,
                    Date = t.Date,
                    Amount = t.Amount
                }).ToList();
        }

        public Transaction? GetTransactionById(int id)
        {
            return _context.Transactions.FirstOrDefault(t => t.TransactionId == id);
        }

    }
}