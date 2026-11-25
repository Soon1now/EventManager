using System.ComponentModel.DataAnnotations;

namespace EventManager.Api.DTOs.EventBranch
{
    public class CreateEventBranch
    {
        [Range(1, int.MaxValue)]
        public int EventId { get; set; }

        [Range(1, int.MaxValue)]
        public int BranchId { get; set; }
    }
}