using Net_6_Assignment.Common.Enums;

namespace Net_6_Assignment.Models
{
    public class User:BaseModel
    { 
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public string? Phone { get; set; }
        public virtual ICollection<Course>? Courses { get; set; }
    }
}
