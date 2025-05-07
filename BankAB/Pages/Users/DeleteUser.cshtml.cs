using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAB.Pages.Users
{
    public class DeleteUserModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public DeleteUserModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IdentityUser User { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            User = await _userManager.FindByIdAsync(id);
            if (User == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);
            return RedirectToPage("./ManageUsers");
        }
    }
}

