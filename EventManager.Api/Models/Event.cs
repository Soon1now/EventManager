namespace EventManager.Api.Models;

public class Event
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }
    public int EventTypeId { get; set; }
    public EventType EventType { get; set; } = null!;
    public int StatusId { get; set; }
    public EventStatus Status { get; set; } = null!;
    public int CreatorId { get; set; }
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int? MaxParticipants { get; set; }

    public int VisitType { get; set; }
    public DateTime CreatedAt { get; set; }
}