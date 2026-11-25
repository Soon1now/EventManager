using System.ComponentModel.DataAnnotations;

namespace EventManager.Api.DTOs.ContestWork
{
    public class CreateContestWork
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int RegistrationId { get; set; }

        [Required]
        [Url]
        public string ContentURL { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
