
using EventManager.Api.Data;
using EventManager.Api.DTOs.ContestWork;
using EventManager.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContestWorksController : ControllerBase
{
    private readonly EventManagerDbContext _context;

    public ContestWorksController(EventManagerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContestWorkResponse>>> GetAll(
        [FromQuery] int? registrationId)
    {
        var query = _context.ContestWorks
            .Include(x => x.Registration)
            .AsQueryable();

        if (registrationId.HasValue)
            query = query.Where(x => x.RegistrationId == registrationId.Value);

        var works = await query.ToListAsync();

        return Ok(works.Adapt<List<ContestWorkResponse>>());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContestWorkResponse>> GetById(int id)
    {
        var work = await _context.ContestWorks
            .Include(x => x.Registration)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (work is null)
            return NotFound("Работа не найдена.");

        return Ok(work.Adapt<ContestWorkResponse>());
    }

    [HttpPost]
    public async Task<ActionResult<ContestWorkResponse>> Create(
        CreateContestWork request)
    {
        var registration = await _context.Registrations.FindAsync(request.RegistrationId);

        if (registration is null)
            return BadRequest("Регистрация не найдена.");

        var work = request.Adapt<ContestWork>();
        work.Registration = registration;

        _context.ContestWorks.Add(work);
        await _context.SaveChangesAsync();

        var created = work.Adapt<ContestWorkResponse>();

        return CreatedAtAction(
            nameof(GetById),
            new { id = work.Id },
            created
        );
    }
}
