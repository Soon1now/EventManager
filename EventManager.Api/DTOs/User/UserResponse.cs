using EventManager.Api.Models;

namespace EventManager.Api.DTOs.User
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public int BranchId { get; set; }
        public string Branch { get; set; } = null!;
    }
}
