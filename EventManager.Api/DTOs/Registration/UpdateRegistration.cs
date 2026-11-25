using System.ComponentModel.DataAnnotations;

namespace EventManager.Api.DTOs.Registration
{
    public class UpdateRegistration
    {
        [Range(1, int.MaxValue)]
        public int? EventId { get; set; }

        [Range(1, int.MaxValue)]
        public int? StatusId { get; set; }
    }
}
