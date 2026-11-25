
using EventManager.Api.Data;
using EventManager.Api.DTOs.Question;
using EventManager.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Api.Controllers;

[ApiController]
[Route("api/questions")]
public class QuestionsController : ControllerBase
{
    private readonly EventManagerDbContext _context;

    public QuestionsController(EventManagerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuestionResponse>>> GetAll(
        [FromQuery] int? eventId)
    {
        var query = _context.Questions
            .Include(q => q.Event)
            .Include(q => q.Type)
            .AsQueryable();

        if (eventId.HasValue)
            query = query.Where(q => q.EventId == eventId.Value);

        var questions = await query.ToListAsync();

        return Ok(questions.Adapt<List<QuestionResponse>>());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<QuestionResponse>> GetById(int id)
    {
        var question = await _context.Questions
            .Include(q => q.Event)
            .Include(q => q.Type)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (question == null)
            return NotFound();

        return Ok(question.Adapt<QuestionResponse>());
    }

    [HttpPost]
    public async Task<ActionResult<QuestionResponse>> Create(
        CreateQuestion request)
    {
        var eventItem = await _context.Events.FindAsync(request.EventId);

        if (eventItem is null)
            return BadRequest("Мероприятие не найдено.");

        var typeItem = await _context.QuestionTypes.FindAsync(request.TypeId);

        if (typeItem is null)
            return BadRequest("Тип вопроса не найден.");

        var indexExists = await _context.Questions
            .AnyAsync(q =>
                q.EventId == request.EventId &&
                q.Index == request.Index);

        if (indexExists)
            return Conflict("Вопрос с таким порядковым номером уже существует.");

        var question = request.Adapt<Question>();
        question.Event = eventItem;
        question.Type = typeItem;
        
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();

        var response = question.Adapt<QuestionResponse>();

        return CreatedAtAction(
            nameof(GetById),
            new { id = question.Id },
            response
        );
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<QuestionResponse>> Update(
        int id, UpdateQuestion request)
    {
        var question = await _context.Questions
            .Include(q => q.Event)
            .Include(q => q.Type)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (question == null)
            return NotFound();

        var typeItem = await _context.QuestionTypes.FindAsync(request.TypeId);

        if (typeItem is null)
            return BadRequest("Тип вопроса не найден.");

        var indexExists = await _context.Questions
            .AnyAsync(q =>
                q.Id != id &&
                q.EventId == question.EventId &&
                q.Index == request.Index);

        if (indexExists)
            return Conflict("Вопрос с таким порядковым номером уже существует.");

        question.Text = request.Text;
        question.TypeId = request.TypeId;
        question.Type = typeItem;
        question.Weight = request.Weight;
        question.Index = request.Index;

        await _context.SaveChangesAsync();

        return Ok(question.Adapt<QuestionResponse>());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var question = await _context.Questions
            .FirstOrDefaultAsync(q => q.Id == id);

        if (question == null)
            return NotFound();

        var hasAnswers = await _context.Answers
            .AnyAsync(a => a.QuestionId == id);

        var hasUserAnswers = await _context.UserAnswers
            .AnyAsync(a => a.QuestionId == id);

        if (hasAnswers || hasUserAnswers)
            return Conflict("Нельзя удалить вопрос: с ним связаны варианты ответов или ответы пользователей.");

        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
