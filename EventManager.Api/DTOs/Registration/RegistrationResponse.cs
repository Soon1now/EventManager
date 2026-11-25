using EventManager.Api.Models;

namespace EventManager.Api.DTOs.Registration
{
    public class RegistrationResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string User { get; set; } = string.Empty;
        public int EventId { get; set; }
        public string Event { get; set; } = string.Empty;
        public int StatusId { get; set; } 
        public string Status { get; set; } = string.Empty;
        public DateTime RegistratedAt { get; set; }
    }
}
