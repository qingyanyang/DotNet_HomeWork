using Net_6_Assignment.Models;

namespace Net_6_Assignment.Models
{
    public class Course:BaseModel
    {
        public string CourseName { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
