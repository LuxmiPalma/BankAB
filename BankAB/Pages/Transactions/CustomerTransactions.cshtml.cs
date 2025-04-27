using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace BankAB.Pages.Transactions
{
    public class CustomerTransactionsModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        public CustomerTransactionsModel(ITransactionService transactionService) // Constructor
        {
            _transactionService = transactionService;
        }
        public int CustomerId { get; set; }
        public void OnGet(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
