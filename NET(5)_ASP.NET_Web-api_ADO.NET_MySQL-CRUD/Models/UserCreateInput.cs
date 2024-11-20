using Mysqlx;
using NET_5_Assignment.Common.Enums;
using NET_5_Assignment.Validations;
using System.ComponentModel.DataAnnotations;

namespace NET_5_Assignment.Models
{
    public class UserCreateInput
    {
        [Required(ErrorMessage = "User name is required!")]
        public string UserName { get; set; }

        [PasswordValidation()]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email format!")]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }
        public int? Age { get; set; }
        public GenderEnum? Gender { get; set; }
        public bool? Active { get; set; }
        public string? Address { get; set; }
    }
}
