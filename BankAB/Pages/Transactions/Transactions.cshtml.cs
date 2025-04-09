using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using Services;
using Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Services.Infrastructure.Paging;



namespace BankAB.Pages.Transactions
{
    public class TransactionsModel : PageModel
    {
        private readonly ITransactionService _transactionService;

        public TransactionsModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public List<TransactionViewModel> Transactions { get; set; } = new();
        public PagedResult<TransactionViewModel> Result { get; set; }

        public void OnGet(int? customerId, string sortColumn = "TransactionId", string sortOrder = "asc", int pageNo = 1)
        {
            List<Transaction> transactionList;


            if (customerId.HasValue)
                transactionList = _transactionService.GetTransactionsByCustomerId(customerId.Value, sortColumn, sortOrder);
            else
                transactionList = _transactionService.GetAllTransactions(sortColumn, sortOrder);

            // then you use query.Select(...).GetPaged(...)


            var query = transactionList
                .Select(t => new TransactionViewModel
                {
                    TransactionId = t.TransactionId,
                    AccountId = t.AccountId,
                    Date = t.Date,
                    Amount = t.Amount,
                    CustomerId = t.AccountNavigation?.Dispositions.FirstOrDefault() != null
                     ? t.AccountNavigation.Dispositions.First().CustomerId
                    : 0



                }).AsQueryable();
            Result = _transactionService.GetPagedTransactions(pageNo, 20, sortColumn, sortOrder, customerId);


        }
    }
}
