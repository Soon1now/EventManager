using EventManager.Api.Models;

namespace EventManager.Api.DTOs.Event;

public class EventResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int EventTypeId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public string Status { get; set; } = string.Empty;
    public int CreatorId { get; set; }
    public int? MaxParticipants { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int VisitType { get; set; }
    public DateTime CreatedAt { get; set; }
}