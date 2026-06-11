using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kinomir_backend.Context;
using kinomir_backend.DTO;

namespace kinomir_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly KinomirdbContext _context;

    public NewsController(KinomirdbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NewsDTO>>> GetNews()
    {
        var news = await _context.News
            .Select(n => new NewsDTO
            {
                NewsId = n.NewsId,
                NewsTitle = n.NewsTitle,
                NewsContent = n.NewsContent
            })
            .ToListAsync();

        return Ok(news);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<NewsDTO>> GetNewsItem(int id)
    {
        var newsItem = await _context.News
            .Where(n => n.NewsId == id)
            .Select(n => new NewsDTO
            {
                NewsId = n.NewsId,
                NewsTitle = n.NewsTitle,
                NewsContent = n.NewsContent
            })
            .FirstOrDefaultAsync();

        if (newsItem == null) return NotFound();
        return Ok(newsItem);
    }
}