using System.ComponentModel.DataAnnotations;

namespace EventManager.Api.DTOs.UserAnswer
{
    public class CreateUserAnswer
    {
        [Range(1, int.MaxValue)]
        public int RegistrationId { get; set; }

        public int? AnswerId { get; set; }

        [Range(1, int.MaxValue)]
        public int QuestionId { get; set; }
        public string? TextAnswer { get; set; } = string.Empty;

        public int? Point { get; set; } = 0;
    }
}
