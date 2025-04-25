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

        public void OnGet(int pageNo = 1, string sortColumn = "TransactionId", string sortOrder = "asc")
        {
            Result = _transactionService.GetPagedTransactions(pageNo, 20, sortColumn, sortOrder);
        }


       

    }
}
