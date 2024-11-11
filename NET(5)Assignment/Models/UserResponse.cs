using NET_5_Assignment.Common.Enums;

namespace NET_5_Assignment.Models
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public GenderEnum Gender { get; set; }
        public bool Active { get; set; }
        public string Address { get; set; }
    }
}
