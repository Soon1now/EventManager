using EventManager.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Api.DTOs.User
{
    public class CreateUser
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Email { get; set; } = string.Empty;

        [Range(0, 1)]
        public int Role { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int BranchId { get; set; }
    }
}
