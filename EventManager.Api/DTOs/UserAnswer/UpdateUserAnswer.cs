using System.ComponentModel.DataAnnotations;

namespace EventManager.Api.DTOs.UserAnswer
{
    public class UpdateUserAnswer
    {
        public int? AnswerId { get; set; }
        public string? TextAnswer { get; set; }
    }
}
