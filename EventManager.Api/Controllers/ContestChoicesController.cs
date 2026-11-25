
using EventManager.Api.Data;
using EventManager.Api.DTOs.ContestChoice;
using EventManager.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContestChoicesController : ControllerBase
{
    private readonly EventManagerDbContext _context;

    public ContestChoicesController(EventManagerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContestChoiceResponse>>> GetAll(
        [FromQuery] int? workId,
        [FromQuery] int? userId)
    {
        var query = _context.ContestChoices
            .Include(x => x.Work)
            .Include(x => x.User)
            .AsQueryable();

        if (workId.HasValue)
            query = query.Where(x => x.WorkId == workId.Value);

        if (userId.HasValue)
            query = query.Where(x => x.UserId == userId.Value);

        var choices = await query.ToListAsync();

        return Ok(choices.Adapt<List<ContestChoiceResponse>>());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContestChoiceResponse>> GetById(int id)
    {
        var choice = await _context.ContestChoices
            .Include(x => x.Work)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (choice is null)
            return NotFound("Голос не найден.");

        return Ok(choice.Adapt<ContestChoiceResponse>());
    }

    [HttpPost]
    public async Task<ActionResult<ContestChoiceResponse>> Create(
        CreateContestChoice request)
    {
        var work = await _context.ContestWorks.FindAsync(request.WorkId);

        if (work is null)
            return BadRequest("Работа не найдена.");

        var user = await _context.Users.FindAsync(request.UserId);

        if (user is null)
            return BadRequest("Пользователь не найден.");

        var choice = request.Adapt<ContestChoice>();
        choice.Work = work;
        choice.User = user;
        choice.ChoiceAt = DateTime.UtcNow;

        _context.ContestChoices.Add(choice);
        await _context.SaveChangesAsync();

        var created = choice.Adapt<ContestChoiceResponse>();

        return CreatedAtAction(
            nameof(GetById),
            new { id = choice.Id },
            created
        );
    }
}
