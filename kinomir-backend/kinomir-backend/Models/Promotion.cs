using System;
using System.Collections.Generic;

namespace kinomir_backend.Models;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public string? PromotionTitle { get; set; }

    public string? PromotionContent { get; set; }
}
