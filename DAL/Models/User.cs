using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Phone { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ResetToken { get; set; }

    public DateTime? ResetTokenExpired { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    public virtual ICollection<Message> MessageDoctors { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageMembers { get; set; } = new List<Message>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<PregnancyProfile> PregnancyProfiles { get; set; } = new List<PregnancyProfile>();
}
