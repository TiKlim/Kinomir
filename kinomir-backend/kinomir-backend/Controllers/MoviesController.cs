using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kinomir_backend.Context;
using kinomir_backend.DTO;
using kinomir_backend.Models;

namespace kinomir_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly KinomirdbContext _context;

    public MoviesController(KinomirdbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
    {
        return await _context.Movies.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
        var movie = await _context.Movies
            .Where(m => m.MovieId == id)
            .Include(m => m.MovieAgeRaiting)
            .Include(m => m.Tags)
            .Select(m => new MovieDTO
            {
                MovieId = m.MovieId,
                MovieTitle = m.MovieTitle,
                MovieDescription = m.MovieDescription,
                MoviePosterVertical = m.MoviePosterVertical,
                MoviePosterHorizontal = m.MoviePosterHorizontal,
                MovieReleaseYear = m.MovieReleaseYear,
                MovieDuration = m.MovieDuration,
                MovieAgeRaiting = m.MovieAgeRaiting != null ? m.MovieAgeRaiting.AgeRaitingName : null,
                Tags = m.Tags.Select(t => t.TagName).ToList()
            })
            .FirstOrDefaultAsync();
        
        return Ok(movie);
    }
    
    // GET: api/movies/soon
    [HttpGet("soon")]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetSoonMovies()
    {
        var soonMovies = await _context.Movies
            .Where(m => m.MovieInTheatersId == 2)
            .Include(m => m.MovieAgeRaiting)
            .Select(m => new MovieDTO
            {
                MovieId = m.MovieId,
                MovieTitle = m.MovieTitle,
                MovieDescription = m.MovieDescription,
                MoviePosterVertical = m.MoviePosterVertical,
                MovieReleaseYear = m.MovieReleaseYear,
                MovieDuration = m.MovieDuration,
                MovieAgeRaiting = m.MovieAgeRaiting != null ? m.MovieAgeRaiting.AgeRaitingName : null
            })
            .ToListAsync();
    
        return soonMovies;
    }
    
    // GET: api/movies/now
    [HttpGet("now")]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMoviesNow()
    {
        var nowMovies = await _context.Movies
            .Where(m => m.MovieInTheatersId == 1)
            .Include(m => m.MovieAgeRaiting)
            .Select(m => new MovieDTO
            {
                MovieId = m.MovieId,
                MovieTitle = m.MovieTitle,
                MovieDescription = m.MovieDescription,
                MoviePosterVertical = m.MoviePosterVertical,
                MovieReleaseYear = m.MovieReleaseYear,
                MovieDuration = m.MovieDuration,
                MovieAgeRaiting = m.MovieAgeRaiting != null ? m.MovieAgeRaiting.AgeRaitingName : null
            })
            .ToListAsync();
    
        return nowMovies;
    }
}