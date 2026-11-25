namespace EventManager.Api.DTOs.EventBranch
{
    public class EventBranchResponse
    {
        public int EventId { get; set; }
        public string Event { get; set; } = string.Empty;

        public int BranchId { get; set; }
        public string Branch { get; set; } = string.Empty;
    }
}