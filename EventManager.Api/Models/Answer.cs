namespace EventManager.Api.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
        public int IsCorrect { get; set; } = 0;

    }
}
