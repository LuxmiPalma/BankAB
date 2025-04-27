using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace BankAB.Pages.Transactions
{
    public class CustomerTransactionsModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        public void OnGet()
        {
        }
    }
}
