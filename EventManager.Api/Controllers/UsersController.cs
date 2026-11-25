using EventManager.Api.Data;
using EventManager.Api.DTOs.User;
using EventManager.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly EventManagerDbContext _context;

    public UsersController(EventManagerDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll()
    {
        var users = await _context.Users
            .Include(u => u.Branch)
            .ToListAsync();

        return Ok(users.Adapt<List<UserResponse>>());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetById(int id)
    {
        var user = await _context.Users
            .Include(u => u.Branch)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return NotFound();

        return Ok(user.Adapt<UserResponse>());
    }

    [HttpPost]
    public async Task<ActionResult<UserResponse>> Create(CreateUser request)
    {
        var emailExists = await _context.Users
            .AnyAsync(u => u.Email == request.Email);
        if (emailExists)
            return Conflict("Пользователь с таким email уже существует.");

        var branch = await _context.Branches.FindAsync(request.BranchId);
        if (branch is null)
            return BadRequest("Указанный филиал не существует.");

        var user = request.Adapt<User>();
        user.Branch = branch;
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var response = user.Adapt<UserResponse>();

        return CreatedAtAction(
            nameof(GetById),
            new { id = user.Id },
            response
        );
    }
}