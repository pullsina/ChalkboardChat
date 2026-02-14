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
                TempData["StatusMessage"] = "❌ Username cannot be empty.";
                return RedirectToPage();
            }

            CurrentUser.UserName = NewUsername;
            CurrentUser.NormalizedUserName = NewUsername.ToUpper();

            var result = await _userManager.UpdateAsync(CurrentUser);

            if (!result.Succeeded)
            {
                TempData["StatusMessage"] = "❌ Could not update username.";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(CurrentUser);

            TempData["StatusMessage"] = "✔ Username updated successfully!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            if (string.IsNullOrWhiteSpace(OldPassword) || string.IsNullOrWhiteSpace(NewPassword))
            {
                TempData["StatusMessage"] = "❌ Both password fields must be filled in.";
                return RedirectToPage();
            }

            var result = await _userManager.ChangePasswordAsync(CurrentUser, OldPassword, NewPassword);

            if (!result.Succeeded)
            {
                TempData["StatusMessage"] = "❌ Password change failed. Make sure your old password is correct.";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(CurrentUser);

            TempData["StatusMessage"] = "✔ Password changed successfully!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAccountAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                TempData["StatusMessage"] = "❌ Could not delete account.";
                return RedirectToPage();
            }

            await _signInManager.SignOutAsync();
            await _userManager.DeleteAsync(user);

            TempData["StatusMessage"] = "✔ Account deleted.";
            return RedirectToPage("/Login");
        }


    }
}