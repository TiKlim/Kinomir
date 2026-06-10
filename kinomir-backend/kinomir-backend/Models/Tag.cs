using System;
using System.Collections.Generic;

namespace kinomir_backend.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string? TagName { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
