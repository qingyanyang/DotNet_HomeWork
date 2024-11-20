using System.ComponentModel.DataAnnotations;

namespace NET3Assignment.Models
{
    public class Teacher
    {
        public Guid TeacherId { get; set; }
        public Guid UserId { get; set; } // foreign key
        public User User { get; set; } // Navigation property to User
        public Guid DepartmentId { get; set; } // foreign key
        public Department Department { get; set; } // Navigation property to Department
        public string Description { get; set; }
        public string? Specialty { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
