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
        public string Password { get; set; } = null!;

        [BindProperty]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
        

    }
}
