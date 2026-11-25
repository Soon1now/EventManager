namespace EventManager.Api.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public int RegistrationId { get; set; }
        public Registration Registration { get; set; } = null!;
        public int? AnswerId { get; set; }
        public Answer? Answer { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
        public string? TextAnswer { get; set; } = string.Empty;
        public int? Point { get; set; }
        public DateTime AnsweredAt { get; set; }

    }
}
