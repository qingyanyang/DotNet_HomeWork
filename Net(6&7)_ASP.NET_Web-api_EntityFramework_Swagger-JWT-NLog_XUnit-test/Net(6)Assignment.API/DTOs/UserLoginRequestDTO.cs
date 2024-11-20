using System.ComponentModel.DataAnnotations;
using Net_6_Assignment.Utilities.Validations;

namespace Net_6_Assignment.DTOs
{
    public class UserLoginRequestDTO
    {

        [EmailAddress(ErrorMessage = "Invalid Email format!")]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [PasswordValidation()]
        public string Password { get; set; }
    }
}
