using EventManager.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Api.DTOs.Event;

public class UpdateEvent
{
    [StringLength(150, MinimumLength = 3)]
    public string? Title { get; set; }

    public string? Description { get; set; }

    [Range(1, int.MaxValue)]
    public int? EventTypeId { get; set; }

    [Range(1, int.MaxValue)]
    public int? StatusId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    [Range(1, int.MaxValue)]
    public int? MaxParticipants { get; set; }

    public int? VisitType { get; set; }
}