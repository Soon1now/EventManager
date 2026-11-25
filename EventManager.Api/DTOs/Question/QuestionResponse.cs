using EventManager.Api.Models;

namespace EventManager.Api.DTOs.Question
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int EventId { get; set; }
        public string Event { get; set; } = string.Empty;
        public int TypeId { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Weight { get; set; } = 0;
        public int Index { get; set; } = 1;
    }
}
