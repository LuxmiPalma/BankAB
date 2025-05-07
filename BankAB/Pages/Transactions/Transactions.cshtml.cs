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


        public PagedResult<TransactionViewModel> Result { get; set; }

        public void OnGet(int pageNo = 1, string sortColumn = "TransactionId", string sortOrder = "asc", string? transactionId = null)
        {
            int? txnId = null;
            if (int.TryParse(transactionId, out int parsedId))
            {
                txnId = parsedId;
            }

            if (txnId.HasValue)
            {
                var list = _transactionService.SearchByTransactionId(txnId.Value)
                    .Select(t => new TransactionViewModel
                    {
                        TransactionId = t.TransactionId,
                        AccountId = t.AccountId,
                        Amount = t.Amount,
                        Date = t.Date, // Convert if needed
                        CustomerId = t.AccountNavigation?.Dispositions?.FirstOrDefault()?.CustomerId ?? 0,
                        Operation = t.Operation
                    }).ToList();
                Result = new PagedResult<TransactionViewModel>
                {
                    Results = list,
                    CurrentPage = 1,
                    PageSize = 20,
                    RowCount = list.Count ()
                };
            }
            else
            {
                Result = _transactionService.GetPagedTransactions(pageNo, 20, sortColumn, sortOrder);
            }
        }


       

    }
}
