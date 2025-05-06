using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAB.Pages.Users
{
    public class ManageUsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        public ManageUsersModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public void OnGet()
        {
        }
    }
}
