using System.ComponentModel.DataAnnotations;

namespace EventManager.Api.DTOs.Answer
{
    public class UpdateAnswer
    {
        public string? Text { get; set; } = string.Empty;

        public bool? IsCorrect { get; set; }
    }
}
