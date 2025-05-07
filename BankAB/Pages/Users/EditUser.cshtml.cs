using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

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

        public List<string> AllRoles { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Id { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string SelectedRole { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "New Password (leave blank to keep current)")]
            public string NewPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id); 
            if (User == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            Input = new InputModel
            {
                Id = user.Id,
                Email = user.Email,
                SelectedRole = userRoles.FirstOrDefault()
            };

            AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
                return Page();
            }

            var user = await _userManager.FindByIdAsync(Input.Id);
            if (user == null) return NotFound();

            // Update email
            user.Email = Input.Email;
            user.UserName = Input.Email;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
                return Page();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (!currentRoles.Contains(Input.SelectedRole))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, Input.SelectedRole);
            }
            // Update password (only if provided)

            if (!string.IsNullOrWhiteSpace(Input.NewPassword))
            {
                // First remove current password if set (Identity requires this)
                var hasPassword = await _userManager.HasPasswordAsync(user);
                if (hasPassword)
                {
                    var removeResult = await _userManager.RemovePasswordAsync(user);
                    if (!removeResult.Succeeded)
                    {
                        foreach (var error in removeResult.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);

                        AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
                        return Page();
                    }
                }
                var addResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
                if (!addResult.Succeeded)
                {
                    foreach (var error in addResult.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                    AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
                    return Page();
                }
            }

            return RedirectToPage("./ManageUsers");
            }
        }
    }

    

