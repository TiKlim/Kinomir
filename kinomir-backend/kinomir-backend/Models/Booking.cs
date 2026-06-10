using System;
using System.Collections.Generic;

namespace kinomir_backend.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? SessionId { get; set; }

    public short? RowNumber { get; set; }

    public short? SeatNumber { get; set; }

    public string? UserPhone { get; set; }

    public string? UserEmail { get; set; }

    public virtual Session? Session { get; set; }
}
