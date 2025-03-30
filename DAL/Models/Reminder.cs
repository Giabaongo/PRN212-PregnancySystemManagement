using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Reminder
{
    public int Id { get; set; }

    public int Week { get; set; }

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;
}
