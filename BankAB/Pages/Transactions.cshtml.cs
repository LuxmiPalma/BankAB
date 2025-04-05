using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using Services;
using BankAB.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace BankAB.Pages
{
    public class TransactionsModel : PageModel
    {
        private readonly BankAppDataContext _context;

        public TransactionsModel(BankAppDataContext context)
        {
            _context = context;
        }

        public List<TransactionViewModel> Transactions { get; set; } = new();

        public void OnGet()
        {
            Transactions = _context.Transactions
           .Select(t => new TransactionViewModel
                            {
                                TransactionId = t.TransactionId,
                                AccountId = t.AccountId,
                                Date = t.Date,
                                Amount = t.Amount
                            }).ToList();
        }
    }
}
