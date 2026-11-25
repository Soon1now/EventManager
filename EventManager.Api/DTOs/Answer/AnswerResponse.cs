namespace EventManager.Api.DTOs.Answer
{
    public class AnswerResponse
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public string Question { get; set; } = string.Empty;
        public bool IsCorrect { get; set; } = false;
    }
}
