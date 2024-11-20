using System.ComponentModel.DataAnnotations;
using NET3Assignment.Common.Enums;
using NET3Assignment.Validations;

namespace NET3Assignment.DTOs
{
    public class UserRequestDTO
    {

        [Required(ErrorMessage= "User name is required!")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email format!")]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [PasswordValidation()]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required!")]
        public RoleEnum Role { get; set; }
        public string? Address { get; set; }
        public GenderEnum? Gender { get; set; }

        [AUPhoneValidation()]
        public string? Phone { get; set; }
    }
}
