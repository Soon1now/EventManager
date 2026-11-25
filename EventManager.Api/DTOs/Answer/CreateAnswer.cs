using System.ComponentModel.DataAnnotations;

namespace EventManager.Api.DTOs.Answer
{
    public class CreateAnswer
    {
        [Required]
        public string Text { get; set; } = string.Empty;

        [Range(1,int.MaxValue)]
        public int QuestionId { get; set; }

        public bool IsCorrect { get; set; } = false;
    }
}
