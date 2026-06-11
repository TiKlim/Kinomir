using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kinomir_backend.Context;
using kinomir_backend.Models;

namespace kinomir_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly KinomirdbContext _context;

    public BookingsController(KinomirdbContext context)
    {
        _context = context;
    }

    // api/bookings/session/{sessionId}
    [HttpGet("session/{sessionId}")]
    public async Task<ActionResult<List<object>>> GetBookedSeats(int sessionId)
    {
        var bookedSeats = await _context.Bookings
            .Where(b => b.SessionId == sessionId)
            .Select(b => new { b.RowNumber, b.SeatNumber })
            .ToListAsync();
        
        return Ok(bookedSeats);
    }

    // (POST) api/bookings
    [HttpPost]
    public async Task<ActionResult<Booking>> CreateBooking(BookingDto bookingDto)
    {
        // Проверяем, не занято ли уже место
        var existingBooking = await _context.Bookings
            .FirstOrDefaultAsync(b => 
                b.SessionId == bookingDto.SessionId && 
                b.RowNumber == bookingDto.RowNumber && 
                b.SeatNumber == bookingDto.SeatNumber);
        
        if (existingBooking != null)
        {
            return BadRequest(new { message = "Место уже занято" });
        }
        
        var booking = new Booking
        {
            SessionId = bookingDto.SessionId,
            RowNumber = bookingDto.RowNumber,
            SeatNumber = bookingDto.SeatNumber,
            UserPhone = bookingDto.UserPhone,
            UserEmail = bookingDto.UserEmail
        };
        
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        
        return Ok(booking);
    }
}

public class BookingDto
{
    public int SessionId { get; set; }
    public short RowNumber { get; set; }
    public short SeatNumber { get; set; }
    public string UserPhone { get; set; }
    public string UserEmail { get; set; }
}