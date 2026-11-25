
using EventManager.Api.Data;
using EventManager.Api.DTOs.Answer;
using EventManager.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Api.Controllers;

[ApiController]
[Route("api/answers")]
public class AnswersController : ControllerBase
{
    private readonly EventManagerDbContext _context;

    public AnswersController(EventManagerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AnswerResponse>>> GetAll(
        [FromQuery] int? questionId)
    {
        var query = _context.Answers
            .Include(a => a.Question)
            .AsQueryable();

        if (questionId.HasValue)
            query = query.Where(a => a.QuestionId == questionId.Value);

        var answers = await query.ToListAsync();

        return Ok(answers.Adapt<List<AnswerResponse>>());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AnswerResponse>> GetById(int id)
    {
        var answer = await _context.Answers
            .Include(a => a.Question)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (answer == null)
            return NotFound();

        return Ok(answer.Adapt<AnswerResponse>());
    }

    [HttpPost]
    public async Task<ActionResult<AnswerResponse>> Create(
        CreateAnswer request)
    {
        var questionExists = await _context.Questions
            .AnyAsync(q => q.Id == request.QuestionId);

        if (!questionExists)
            return BadRequest("Вопрос не найден.");

        var answer = request.Adapt<Answer>();

        _context.Answers.Add(answer);
        await _context.SaveChangesAsync();

        await _context.Entry(answer).Reference(a => a.Question).LoadAsync();

        var response = answer.Adapt<AnswerResponse>();

        return CreatedAtAction(
            nameof(GetById),
            new { id = answer.Id },
            response
        );
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<AnswerResponse>> Update(
        int id, UpdateAnswer request)
    {
        var answer = await _context.Answers
            .Include(a => a.Question)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (answer == null)
            return NotFound();

        if (request.Text == null && request.IsCorrect == null)
            return BadRequest("Не переданы данные для обновления.");

        if (request.Text != null)
            answer.Text = request.Text;

        if (request.IsCorrect.HasValue)
            answer.IsCorrect = request.IsCorrect.Value;

        await _context.SaveChangesAsync();

        return Ok(answer.Adapt<AnswerResponse>());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var answer = await _context.Answers
            .FirstOrDefaultAsync(a => a.Id == id);

        if (answer == null)
            return NotFound();

        var hasUserAnswers = await _context.UserAnswers
            .AnyAsync(a => a.AnswerId == id);

        if (hasUserAnswers)
            return Conflict("Нельзя удалить вариант: на него уже даны ответы.");

        _context.Answers.Remove(answer);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
