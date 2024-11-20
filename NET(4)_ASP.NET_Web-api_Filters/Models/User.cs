using NET3Assignment.Common.Enums;

namespace NET3Assignment.Models
{
    public class User
    { 
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public RoleEnum? Role { get; set; }
        public string? Address { get; set; }
        public GenderEnum? Gender { get; set; }
        public string? Phone { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
