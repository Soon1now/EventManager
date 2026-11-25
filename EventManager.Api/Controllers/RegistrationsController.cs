
using EventManager.Api.Data;
using EventManager.Api.DTOs.Registration;
using EventManager.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Api.Controllers;

[ApiController]
[Route("api/registrations")]
public class RegistrationsController : ControllerBase
{
    private readonly EventManagerDbContext _context;

    public RegistrationsController(EventManagerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RegistrationResponse>>> GetAll()
    {
        var registrations = await _context.Registrations
            .Include(r => r.User)
            .Include(r => r.Event)
            .Include(r => r.Status)
            .ToListAsync();

        return Ok(registrations.Adapt<List<RegistrationResponse>>());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RegistrationResponse>> GetById(int id)
    {
        var registration = await _context.Registrations
            .Include(r => r.User)
            .Include(r => r.Event)
            .Include(r => r.Status)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (registration == null)
            return NotFound();

        return Ok(registration.Adapt<RegistrationResponse>());
    }

    [HttpPost]
    public async Task<ActionResult<RegistrationResponse>> Create(
        CreateRegistration request)
    {
        var user = await _context.Users.FindAsync(request.UserId);
        if (user is null)
            return BadRequest("Пользователь не найден.");

        var eventItem = await _context.Events.FindAsync(request.EventId);

        if (eventItem is null)
            return BadRequest("Мероприятие не найдено.");

        var alreadyRegistered = await _context.Registrations
            .AnyAsync(r =>
                r.UserId == request.UserId &&
                r.EventId == request.EventId);
        if (alreadyRegistered)
            return Conflict("Пользователь уже зарегистрирован на это мероприятие.");

        var registeredStatus = await _context.RegistrationStatuses.FirstOrDefaultAsync(s => s.Status == "registered");
        if (registeredStatus is null)
            return BadRequest("Статус отсутствует в справочнике.");

        var registration = request.Adapt<Registration>();

        registration.StatusId = registeredStatus.Id;
        registration.Status = registeredStatus;
        registration.User = user;
        registration.Event = eventItem;
        
        await _context.Registrations.AddAsync(registration);
        await _context.SaveChangesAsync();

        var response = registration.Adapt<RegistrationResponse>();

        return CreatedAtAction(
            nameof(GetById),
            new { id = registration.Id },
            response
        );
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<RegistrationResponse>> Update(
        int id, UpdateRegistration request)
    {
        var registration = await _context.Registrations
            .Include(r => r.User)
            .Include(r => r.Event)
            .Include(r => r.Status)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (registration == null)
            return NotFound();

        if (request.EventId.HasValue)
        {
            var eventExists = await _context.Events
                .AnyAsync(e => e.Id == request.EventId.Value);

            if (!eventExists)
                return BadRequest("Мероприятие не найдено.");

            var duplicate = await _context.Registrations
                .AnyAsync(r =>
                    r.Id != id &&
                    r.UserId == registration.UserId &&
                    r.EventId == request.EventId.Value);

            if (duplicate)
                return Conflict("Пользователь уже зарегистрирован на это мероприятие.");

            registration.EventId = request.EventId.Value;
        }

        if (request.StatusId.HasValue)
        {
            var statusExists = await _context.RegistrationStatuses
                .AnyAsync(s => s.Id == request.StatusId.Value);

            if (!statusExists)
                return BadRequest("Статус регистрации не найден.");

            registration.StatusId = request.StatusId.Value;
        }

        await _context.SaveChangesAsync();

        await _context.Entry(registration).Reference(r => r.Event).LoadAsync();
        await _context.Entry(registration).Reference(r => r.Status).LoadAsync();

        return Ok(registration.Adapt<RegistrationResponse>());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var registration = await _context.Registrations
            .FirstOrDefaultAsync(r => r.Id == id);

        if (registration == null)
            return NotFound();

        var hasWorks = await _context.ContestWorks
            .AnyAsync(w => w.RegistrationId == id);

        var hasAnswers = await _context.UserAnswers
            .AnyAsync(a => a.RegistrationId == id);

        if (hasWorks || hasAnswers)
            return Conflict("Нельзя удалить регистрацию: с ней связаны работы или ответы.");

        _context.Registrations.Remove(registration);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
