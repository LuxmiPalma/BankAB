using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAB.Pages.Users
{
    public class EditUserModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EditUserModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IdentityUser User { get; set; }
        public List<string> AllRoles { get; set; }

        [BindProperty]
        public string SelectedRole { get; set; }
        public void OnGet()
        {
        }
    }
}
