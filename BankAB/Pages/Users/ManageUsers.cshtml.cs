using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAB.Pages.Users
{
    public class ManageUsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public void OnGet()
        {
        }
    }
}
