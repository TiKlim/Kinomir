using System;
using System.Collections.Generic;

namespace kinomir_backend.Models;

public partial class News
{
    public int NewsId { get; set; }

    public string? NewsTitle { get; set; }

    public string? NewsContent { get; set; }
}
