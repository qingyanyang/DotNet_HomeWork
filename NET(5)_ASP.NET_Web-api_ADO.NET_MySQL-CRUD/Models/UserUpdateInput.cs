using Mysqlx;
using NET_5_Assignment.Common.Enums;
using NET_5_Assignment.Validations;
using System.ComponentModel.DataAnnotations;

namespace NET_5_Assignment.Models
{
    public class UserUpdateInput
    {
        public string? UserName { get; set; }
        public int? Age { get; set; }
        public GenderEnum? Gender { get; set; }
        public string? Address { get; set; }
    }
}
