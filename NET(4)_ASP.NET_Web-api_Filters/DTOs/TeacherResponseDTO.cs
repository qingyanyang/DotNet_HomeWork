
using NET3Assignment.Models;

namespace NET3Assignment.DTOs
{
    public class TeacherResponseDTO:UserResponseDTO
    {
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public string? Specialty { get; set; }
        
    }
}
