using System.ComponentModel.DataAnnotations;

namespace ChalkboardChat.UI.ViewModels
{
    public class ChangePasswordInput
    {
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
