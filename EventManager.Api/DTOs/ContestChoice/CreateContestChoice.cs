using System.ComponentModel.DataAnnotations;

namespace EventManager.Api.DTOs.ContestChoice
{
    public class CreateContestChoice
    {
        [Range(1, int.MaxValue)]
        public int WorkId { get; set; }

        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
    }
}
