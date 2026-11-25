namespace EventManager.Api.DTOs.ContestChoice
{
    public class ContestChoiceResponse
    {
        public int Id { get; set; }
        public int WorkId { get; set; }
        public string Work { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string User { get; set; } = string.Empty;
        public DateTime ChoiceAt { get; set; }
    }
}
