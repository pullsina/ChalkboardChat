using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ProfileModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IdentityUser CurrentUser { get; set; }

        [BindProperty]
        public string NewUsername { get; set; }

        [BindProperty]
        public string OldPassword { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        public async Task OnGet()
        {
            CurrentUser = await _userManager.GetUserAsync(User);
        }

        public async Task<IActionResult> OnPostChangeUsernameAsync()
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            if (string.IsNullOrWhiteSpace(NewUsername))
            {
                ModelState.AddModelError("", "Username cannot be empty");
                return Page();
            }

            CurrentUser.UserName = NewUsername;
            CurrentUser.NormalizedUserName = NewUsername.ToUpper();

            var result = await _userManager.UpdateAsync(CurrentUser);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Could not update username");
                return Page();
            }

            await _signInManager.RefreshSignInAsync(CurrentUser);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            var result = await _userManager.ChangePasswordAsync(CurrentUser, OldPassword, NewPassword);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Password change failed");
                return Page();
            }

            await _signInManager.RefreshSignInAsync(CurrentUser);

            return RedirectToPage();
        }
    }
}