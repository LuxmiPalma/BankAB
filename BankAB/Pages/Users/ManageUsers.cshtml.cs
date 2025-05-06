using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankAB.Pages.Users
{
    public class ManageUsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        public ManageUsersModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IList<IdentityUser> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _userManager.Users.ToListAsync();
        }
        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null && user.Email != User.Identity.Name)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToPage();
        }
    }
}
