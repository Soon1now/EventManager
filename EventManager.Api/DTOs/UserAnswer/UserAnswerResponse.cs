using EventManager.Api.Models;

namespace EventManager.Api.DTOs.UserAnswer
{
    public class UserAnswerResponse
    {
        public int Id { get; set; }
        public int RegistrationId { get; set; }
        public string Registration { get; set; } = string.Empty;
        public int? AnswerId { get; set; }
        public string? Answer { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public string Question { get; set; } = string.Empty;
        public string? TextAnswer { get; set; } = string.Empty;
        public int? Point { get; set; } = 0;
        public DateTime AnsweredAt { get; set; }
    }
}
