using System.ComponentModel.DataAnnotations;

namespace EventManager.Api.DTOs.Question
{
    public class UpdateQuestion
    {
        [Required]
        public string Text { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int TypeId { get; set; }

        [Range(0, int.MaxValue)]
        public int Weight { get; set; }

        [Range(1, int.MaxValue)]
        public int Index { get; set; }
    }
}
