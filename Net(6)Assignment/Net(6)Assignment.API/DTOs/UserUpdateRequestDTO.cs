using Net_6_Assignment.Common.Enums;
using Net_6_Assignment.Utilities.Validations;
using System.ComponentModel.DataAnnotations;

namespace Net_6_Assignment.DTOs
{
    public class UserUpdateRequestDTO
    {
        [MaxLength(50, ErrorMessage = "User name can be up to 50 characters only!")]
        public string? UserName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email format!")]
        public string? Email { get; set; }

        [MaxLength(500, ErrorMessage = "Address can be up to 500 characters only!")]
        public string? Address { get; set; }
        public Gender? Gender { get; set; }

        [AUPhoneValidation()]
        public string? Phone { get; set; }
    }
}
