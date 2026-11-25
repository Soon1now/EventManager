using EventManager.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Api.DTOs.Event;

public class CreateEvent
{
    [Required]
    [StringLength(150, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(1, int.MaxValue)]
    public int EventTypeId { get; set; }

    [Range(1, int.MaxValue)]
    public int StatusId { get; set; }

    [Range(1, int.MaxValue)]
    public int CreatorId { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Range(1, int.MaxValue)]
    public int? MaxParticipants { get; set; }

    [Range(0, 1)]
    public int VisitType { get; set; }
}