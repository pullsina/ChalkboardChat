using System.ComponentModel.DataAnnotations;

namespace ChalkboardChat.UI.ViewModels
{
    public class ChangeUsernameInput
    {
        [Required]
        public string NewUsername { get; set; }
    }
}
