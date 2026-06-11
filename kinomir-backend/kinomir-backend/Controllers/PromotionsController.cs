using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kinomir_backend.Context;
using kinomir_backend.DTO;

namespace kinomir_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PromotionsController : ControllerBase
{
    private readonly KinomirdbContext _context;

    public PromotionsController(KinomirdbContext context)
    {
        _context = context;
    }

    // api/promotions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PromotionDTO>>> GetPromotions()
    {
        var promotions = await _context.Promotions
            .Select(p => new PromotionDTO
            {
                PromotionId = p.PromotionId,
                PromotionTitle = p.PromotionTitle,
                PromotionContent = p.PromotionContent
            })
            .ToListAsync();

        return Ok(promotions);
    }

    // api/promotions/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PromotionDTO>> GetPromotion(int id)
    {
        var promotion = await _context.Promotions
            .Where(p => p.PromotionId == id)
            .Select(p => new PromotionDTO
            {
                PromotionId = p.PromotionId,
                PromotionTitle = p.PromotionTitle,
                PromotionContent = p.PromotionContent
            })
            .FirstOrDefaultAsync();

        if (promotion == null) return NotFound();
        return Ok(promotion);
    }
}