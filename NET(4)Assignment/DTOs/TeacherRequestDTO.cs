using System.ComponentModel.DataAnnotations;

namespace NET3Assignment.DTOs
{
    public class TeacherRequestDTO:UserRequestDTO
    {
        [Required(ErrorMessage= "UserId is required!")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "DepartmentId is required!")]
        public Guid DepartmentId { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        [MaxLength(500, ErrorMessage = "Description can be up to 500 characters only!")]
        public string Description { get; set; }
        public string? Specialty { get; set; }
    }
}
