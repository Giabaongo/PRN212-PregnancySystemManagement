using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Membership
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PlanId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Payment? Payment { get; set; }

    public virtual MembershipPlan Plan { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
