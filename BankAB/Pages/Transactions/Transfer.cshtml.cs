using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankAB.Pages.Transactions
{
    public class TransferModel : PageModel
    {
        private readonly BankAppDataContext _context;

        public TransferModel(BankAppDataContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int AccountId { get; set; }

        [BindProperty]
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Target account ID is required")]
        public int TargetAccountId { get; set; }

        [BindProperty]
        public string? Comment { get; set; }

    }
}
