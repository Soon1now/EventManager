using System.ComponentModel.DataAnnotations;

namespace EventManager.Api.DTOs.Registration
{
    public class CreateRegistration
    {
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Range(1, int.MaxValue)]
        public int EventId { get; set; }

        public int? StatusId { get; set; } = 1;
    }
}
