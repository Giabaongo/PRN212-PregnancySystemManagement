using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Faq
{
    public int Id { get; set; }

    public string? Question { get; set; }

    public string? Answer { get; set; }

    public string? Category { get; set; }

    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
