using NET_5_Assignment.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace NET_5_Assignment.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public GenderEnum Gender { get; set; }
        public bool Active { get; set; }
        public string Address { get; set; }
    }
}
