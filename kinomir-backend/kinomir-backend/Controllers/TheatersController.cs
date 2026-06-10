using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kinomir_backend.Context;

namespace kinomir_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TheatersController : ControllerBase
{
    private readonly KinomirdbContext _context;

    public TheatersController(KinomirdbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetTheaters()
    {
        var theaters = await _context.Theaters
            .Select(t => new { t.TheaterId, t.TheaterAddress })
            .ToListAsync();
        
        return Ok(theaters);
    }
}