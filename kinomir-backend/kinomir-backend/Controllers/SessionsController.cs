using kinomir_backend.Context;
using kinomir_backend.DTO;
using kinomir_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kinomir_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SessionsController : ControllerBase
{
    private readonly KinomirdbContext _context;

    public SessionsController(KinomirdbContext context)
    {
        _context = context;
    }
    
    [HttpGet("schedule")]
    public async Task<ActionResult<List<SessionsDTO>>> GetSchedule([FromQuery] int? theaterId = null)
    {
        var allDates = await _context.Sessions
            .Select(s => s.SessionDate.Value)
            .Distinct()
            .OrderBy(d => d)
            .Take(3)
            .ToListAsync();

        if (!allDates.Any())
        {
            return Ok(new List<SessionsDTO>());
        }
        
        var query = _context.Sessions
            .Include(s => s.SessionMovie)
            .ThenInclude(m => m.MovieAgeRaiting)
            .Where(s => allDates.Contains(s.SessionDate.Value))
            .OrderBy(s => s.SessionDate)
            .ThenBy(s => s.SessionTime);

        // Фильтр по кинотеатру
        if (theaterId.HasValue)
        {
            query = (IOrderedQueryable<Session>)query.Where(s => s.SessionTheater == theaterId.Value);
        }

        var sessions = await query.ToListAsync();
        
        var schedule = sessions
            .GroupBy(s => s.SessionMovieId)
            .Select(g => new SessionsDTO
            {
                MovieId = g.First().SessionMovie.MovieId,
                MovieTitle = g.First().SessionMovie.MovieTitle,
                MoviePosterVertical = g.First().SessionMovie.MoviePosterVertical,
                MovieAgeRaiting = g.First().SessionMovie.MovieAgeRaiting?.AgeRaitingName,
                SessionsByDay = g
                    .GroupBy(s => s.SessionDate.Value.ToString("yyyy-MM-dd"))
                    .ToDictionary(
                        dayGroup => dayGroup.Key,
                        dayGroup => dayGroup.Select(s => new SessionTimeWithId
                        {
                            Time = s.SessionTime.Value.ToString("HH:mm"),
                            SessionId = s.SessionId
                        }).ToList()
                    )
            })
            .ToList();

        return Ok(schedule);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetSession(int id)
    {
        var session = await _context.Sessions
            .Include(s => s.SessionMovie)
            .FirstOrDefaultAsync(s => s.SessionId == id);
    
        if (session == null) return NotFound();
    
        return Ok(new
        {
            session.SessionId,
            session.SessionDate,
            session.SessionTime,
            HallName = $"Зал {session.SessionTheaterHall}"
        });
    }
}