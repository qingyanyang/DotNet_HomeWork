using System.ComponentModel.DataAnnotations;

namespace Net_6_Assignment.DTOs
{
    public class CourseRequestDTO
    {
        [Required(ErrorMessage = "Course name is required!")]
        [MaxLength(50, ErrorMessage = "Course name can be up to 50 characters only!")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        [MaxLength(500, ErrorMessage = "Description can be up to 500 characters only!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category id is required!")]
        public Guid CategoryId { get; set; }
    }

    public class CourseUpdateRequestDTO : CourseRequestDTO
    {
        public Guid Id { get; set; }
    }
}
