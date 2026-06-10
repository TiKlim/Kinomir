using System;
using System.Collections.Generic;

namespace kinomir_backend.Models;

public partial class InTheater
{
    public int InTheatersId { get; set; }

    public bool? InTheatersValue { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
