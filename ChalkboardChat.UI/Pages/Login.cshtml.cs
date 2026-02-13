using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ChalkboardChat.UI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginModel(UserManager<IdentityUser> userManager,
                             SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty, Required]
        public string Username { get; set; } = null!;

        [BindProperty, Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // TODO: UI -> AuthService.LoginAsync(...)
            
          
            var result = await _signInManager.PasswordSignInAsync(
                Username,
    Password,
    isPersistent: false,
    lockoutOnFailure: false

                );

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return Page();
            }
           
            return RedirectToPage("/Messages");
        }
    }
}
