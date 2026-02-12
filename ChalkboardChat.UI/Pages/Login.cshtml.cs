using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ChalkboardChat.UI.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
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

            /*
            var result = await _authService.LoginAsync(Username, Password);

            if (!result)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return Page();
            }
            */
            return RedirectToPage("/Messages");
        }
    }
}
