using System.ComponentModel.DataAnnotations;

namespace SampleDB.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and confirm password do not matched")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
