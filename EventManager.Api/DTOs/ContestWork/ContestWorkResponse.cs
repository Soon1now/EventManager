namespace EventManager.Api.DTOs.ContestWork
{
    public class ContestWorkResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int RegistrationId { get; set; }
        public string Registration { get; set; } = string.Empty;

        public string ContentURL { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
