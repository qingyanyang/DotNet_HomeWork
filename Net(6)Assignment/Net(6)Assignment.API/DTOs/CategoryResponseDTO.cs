namespace Net_6_Assignment.DTOs
{
    public class CategoryResponseDTO
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryLevel { get; set; }
        public Guid? ParentId { get; set; }
        public string? ParentCategoryName { get; set; }
        public List<string>? CourseNames { get; set; }
    }
}