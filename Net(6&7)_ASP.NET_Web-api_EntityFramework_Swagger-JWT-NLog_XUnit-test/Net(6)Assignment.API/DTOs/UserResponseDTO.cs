
namespace Net_6_Assignment.DTOs
{
    public class UserResponseDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }
        public string? AccessToken { get; set; }
        public List<string>? Courses { get; set; }
    }
}
