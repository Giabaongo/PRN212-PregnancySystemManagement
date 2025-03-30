using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class ScheduledEmail
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }

    public string RecipientEmail { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public DateTime ScheduledTime { get; set; }

    public bool IsSent { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;
}
