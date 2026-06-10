using System;
using System.Collections.Generic;

namespace kinomir_backend.Models;

public partial class Theater
{
    public int TheaterId { get; set; }

    public string? TheaterName { get; set; }

    public string? TheaterPlace { get; set; }

    public string? TheaterAddress { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
