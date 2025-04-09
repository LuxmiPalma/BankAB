using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using Services;
using BankAB.ViewModels;
using Microsoft.EntityFrameworkCore;
using BankAB.Infrastructure.Paging;



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

        public void OnGet(string sortColumn = "TransactionId", string sortOrder = "asc", int pageNo = 1)
        {

            var query = _transactionService
            .GetAllTransactions(sortColumn, sortOrder)
            .Select(t => new TransactionViewModel
        {
            TransactionId = t.TransactionId,
            AccountId = t.AccountId,
            Date = t.Date,
            Amount = t.Amount
        })
        .AsQueryable();


            Result = query.GetPaged(pageNo, 20);
        }

    }
}
