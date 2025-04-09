using BankAB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace BankAB.Pages.Transactions
{
    public class TransactionDetailsModel : PageModel
    {
        private readonly ITransactionService _transactionService;

        public TransactionDetailsModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public TransactionViewModel Transaction { get; set; }
        public void OnGet(int id)
        {
        }
    }
}
