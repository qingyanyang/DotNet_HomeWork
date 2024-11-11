using Net_6_Assignment.Common.Enums;

namespace Net_6_Assignment.Models
{
    public class Category:BaseModel
    {
        public string CategoryName { get; set; }
        public CategoryLevel CategoryLevel { get; set; } //0: parent level 1: children level
        public Guid? ParentId { get; set; } //null: parent level
        public Category? Parent { get; set; }
        public virtual ICollection<Course>? Courses { get; set; }
    }
}
