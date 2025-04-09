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
        public IActionResult OnGet(int id)
        {
            var transaction = _transactionService.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound();
            }

            Transaction = new TransactionViewModel
            {
                TransactionId = transaction.TransactionId,
                AccountId = transaction.AccountId,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Operation = transaction.Operation
            };

            return Page();
        }

    }
}