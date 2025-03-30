using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Post
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? Image { get; set; }

    public string? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User User { get; set; } = null!;
}
