namespace EventManager.Api.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;
        public int TypeId { get; set; }
        public QuestionType Type { get; set; } = null!;
        public int Weight { get; set; } 
        public int Index { get; set; }
    }
}
