using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ChalkboardChat.UI.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty, Required]
        public string Username { get; set; } = null!;

        [BindProperty, Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [BindProperty, Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // TODO: UI -> Logic layer
            // var result = await _authService.RegisterAsync(Username, Password);

            // TODO: handle result from service
            // if (!result.Succeeded)
            // {
            //     foreach (var error in result.Errors)
            //         ModelState.AddModelError("", error.Description);
            //     return Page();
            // }

            // TODO: redirect after successful registration
            return RedirectToPage("/Login");
        }
    }
}
