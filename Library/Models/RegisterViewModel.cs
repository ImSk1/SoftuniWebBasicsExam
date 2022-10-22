using Library.Common;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(ValidationConstants.USER_USERNAME_MAXLENGHT, MinimumLength = ValidationConstants.USER_USERNAME_MINLENGHT)]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(ValidationConstants.USER_EMAIL_MAXLENGHT, MinimumLength = ValidationConstants.USER_EMAIL_MINLENGHT)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.USER_PASSWORD_MAXLENGHT, MinimumLength = ValidationConstants.USER_PASSWORD_MINLENGHT)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
