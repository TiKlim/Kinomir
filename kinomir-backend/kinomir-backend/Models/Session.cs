using System;
using System.Collections.Generic;

namespace kinomir_backend.Models;

public partial class Session
{
    public int SessionId { get; set; }

    public int? SessionMovieId { get; set; }

    public DateOnly? SessionDate { get; set; }

    public TimeOnly? SessionTime { get; set; }

    public decimal? SessionsPrice { get; set; }

    public int? SessionTheater { get; set; }

    public string? SessionTheaterHall { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Movie? SessionMovie { get; set; }

    public virtual Theater? SessionTheaterNavigation { get; set; }
}
