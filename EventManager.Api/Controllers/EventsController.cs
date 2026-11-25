using EventManager.Api.Data;
using EventManager.Api.DTOs.Event;
using EventManager.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Api.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly EventManagerDbContext _context;

    public EventsController(EventManagerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventResponse>>> GetAll()
    {
        var events = await _context.Events
            .AsNoTracking()
            .Include(e => e.EventType)
            .Include(e => e.Status)
            .ToListAsync();

        return Ok(events.Adapt<List<EventResponse>>());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EventResponse>> GetById(int id)
    {
        var eventItem = await _context.Events
            .AsNoTracking()
            .Include(e => e.EventType)
            .Include(e => e.Status)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (eventItem is null)
            return NotFound();

        return Ok(eventItem.Adapt<EventResponse>());
    }

    [HttpPost]
    public async Task<ActionResult<EventResponse>> Create(CreateEvent request)
    {
        if (request.EndTime <= request.StartTime)
            return BadRequest("Время окончания события должно быть позже начала");

        if (await _context.Users.FindAsync(request.CreatorId) is null)
            return BadRequest("Указанный пользователь не существует");

        var type = await _context.EventType.FindAsync(request.EventTypeId);
        if (type is null)
            return BadRequest("Указанный тип события не существует");
        
        var status = await _context.EventStatus.FindAsync(request.StatusId);
        if (status is null)
            return BadRequest("Указанный статус события не существует");

        var eventItem = request.Adapt<Event>();
        eventItem.EventType = type;
        eventItem.Status = status;  

        await _context.Events.AddAsync(eventItem);
        await _context.SaveChangesAsync();

        var response = eventItem.Adapt<EventResponse>();

        return CreatedAtAction(
            nameof(GetById),
            new { id = eventItem.Id },
            response
        );
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<EventResponse>> Update(
        int id,
        UpdateEvent request)
    {
        var eventItem = await _context.Events
            .Include(e => e.EventType)
            .Include(e => e.Status)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (eventItem is null)
            return NotFound();

        var startTime = request.StartTime ?? eventItem.StartTime;
        var endTime = request.EndTime ?? eventItem.EndTime;

        if (endTime <= startTime)
            return BadRequest("EndTime must be later than StartTime.");

        if (request.MaxParticipants is <= 0)
            return BadRequest("MaxParticipants must be greater than zero.");
        
        if (request.EventTypeId.HasValue &&
        !await _context.EventType.AnyAsync(x => x.Id == request.EventTypeId.Value))
        {
            return BadRequest("Invalid EventTypeId.");
        }

 
        if (request.StatusId.HasValue &&
        !await _context.EventStatus.AnyAsync(x => x.Id == request.StatusId.Value))
        {
            return BadRequest("Invalid StatusId.");
        }

        if (request.Title is not null)
            eventItem.Title = request.Title;

        if (request.Description is not null)
            eventItem.Description = request.Description;

        if (request.EventTypeId.HasValue)
            eventItem.EventTypeId = request.EventTypeId.Value;

        if (request.StatusId.HasValue)
            eventItem.StatusId = request.StatusId.Value;

        if (request.StartTime.HasValue)
            eventItem.StartTime = request.StartTime.Value;

        if (request.EndTime.HasValue)
            eventItem.EndTime = request.EndTime.Value;

        if (request.MaxParticipants.HasValue)
            eventItem.MaxParticipants = request.MaxParticipants.Value;

        if (request.VisitType.HasValue)
            eventItem.VisitType = request.VisitType.Value;

        await _context.SaveChangesAsync();

        return Ok(eventItem.Adapt<EventResponse>());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eventItem = await _context.Events
            .FirstOrDefaultAsync(e => e.Id == id);

        if (eventItem is null)
            return NotFound();

        _context.Events.Remove(eventItem);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}