using System;
using System.Collections.Generic;

namespace kinomir_backend.Models;

public partial class AgeRaiting
{
    public int AgeRaitingId { get; set; }

    public string? AgeRaitingName { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
