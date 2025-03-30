namespace DAL.Models;

public partial class Appointment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string Status { get; set; } = null!;

    public bool ReminderSent { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ScheduledEmail? ScheduledEmail { get; set; }

    public virtual User User { get; set; } = null!;
}
