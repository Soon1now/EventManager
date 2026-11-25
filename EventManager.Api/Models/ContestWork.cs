namespace EventManager.Api.Models
{
    public class ContestWork
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int RegistrationId { get; set; }
        public Registration Registration { get; set; } = null!;

        public string ContentURL { get; set; } = string.Empty;
        public string ?Description { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}
