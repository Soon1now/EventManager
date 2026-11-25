namespace EventManager.Api.Models
{
    public class EventBranch
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
    }
}