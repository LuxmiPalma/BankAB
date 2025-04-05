using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;


namespace BankAB.Pages.Account
{
    public class DepositModel : PageModel
    {
        private readonly BankAppDataContext _context;

        public DepositModel(BankAppDataContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
    }
}
