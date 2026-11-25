
using EventManager.Api.Data;
using EventManager.Api.DTOs.UserAnswer;
using EventManager.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserAnswersController : ControllerBase
{
    private readonly EventManagerDbContext _context;

    public UserAnswersController(EventManagerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserAnswerResponse>>> GetAll(
        [FromQuery] int? registrationId)
    {
        var query = _context.UserAnswers
            .Include(x => x.Registration)
                .ThenInclude(x => x.User)
            .Include(x => x.Registration)
                .ThenInclude(x => x.Event)
            .Include(x => x.Question)
            .Include(x => x.Answer)
            .AsQueryable();

        if (registrationId.HasValue)
            query = query.Where(x => x.RegistrationId == registrationId.Value);

        var userAnswers = await query.ToListAsync();

        return Ok(userAnswers.Adapt<List<UserAnswerResponse>>());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserAnswerResponse>> GetById(int id)
    {
        var userAnswer = await _context.UserAnswers
            .Include(x => x.Registration)
                .ThenInclude(x => x.User)
            .Include(x => x.Registration)
                .ThenInclude(x => x.Event)
            .Include(x => x.Question)
            .Include(x => x.Answer)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (userAnswer is null)
            return NotFound("Ответ пользователя не найден.");

        return Ok(userAnswer.Adapt<UserAnswerResponse>());
    }

    [HttpPost]
    public async Task<ActionResult<UserAnswerResponse>> Create(
        CreateUserAnswer request)
    {
        var registration = await _context.Registrations.FindAsync(request.RegistrationId);

        if (registration is null)
            return BadRequest("Регистрация не найдена.");

        var question = await _context.Questions.FindAsync(request.QuestionId);

        if (question is null)
            return BadRequest("Вопрос не найден.");

        Answer? answer = null;

        if (request.AnswerId.HasValue)
        {
            answer = await _context.Answers.FindAsync(request.AnswerId.Value);

            if (answer is null)
                return BadRequest("Вариант ответа не найден.");

            if (answer.QuestionId != question.Id)
                return BadRequest("Вариант ответа не относится к указанному вопросу.");
        }

        var userAnswer = request.Adapt<UserAnswer>();
        userAnswer.Registration = registration;
        userAnswer.Question = question;
        userAnswer.Answer = answer;
        userAnswer.AnsweredAt = DateTime.UtcNow;

        _context.UserAnswers.Add(userAnswer);
        await _context.SaveChangesAsync();

        var created = userAnswer.Adapt<UserAnswerResponse>();

        return CreatedAtAction(
            nameof(GetById),
            new { id = userAnswer.Id },
            created
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserAnswerResponse>> Update(
    int id,
    UpdateUserAnswer request)
    {
        var userAnswer = await _context.UserAnswers
            .Include(x => x.Registration)
            .Include(x => x.Question)
            .Include(x => x.Answer)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (userAnswer is null)
            return NotFound("Ответ пользователя не найден.");

        if (request.AnswerId is null && request.TextAnswer is null)
            return BadRequest("Не переданы данные для обновления.");

        if (request.AnswerId.HasValue)
        {
            var answer = await _context.Answers
                .FirstOrDefaultAsync(a => a.Id == request.AnswerId.Value);

            if (answer is null)
                return BadRequest("Вариант ответа не найден.");

            if (answer.QuestionId != userAnswer.QuestionId)
                return BadRequest("Вариант ответа не относится к вопросу.");

            userAnswer.Answer = answer;
        }

        if (request.TextAnswer is not null)
            userAnswer.TextAnswer = request.TextAnswer;


        await _context.SaveChangesAsync();

        return Ok(userAnswer.Adapt<UserAnswerResponse>());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userAnswer = await _context.UserAnswers.FindAsync(id);

        if (userAnswer is null)
            return NotFound("Ответ пользователя не найден.");

        _context.UserAnswers.Remove(userAnswer);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
