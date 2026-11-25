namespace EventManager.Api.Models
{
    public class ContestChoice
    {
        public int Id { get; set; }
        public int WorkId { get; set; }
        public ContestWork Work { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime ChoiceAt { get; set; }

    }
}
