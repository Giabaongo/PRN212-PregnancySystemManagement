using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class MembershipPlan
{
    public int Id { get; set; }

    public string PlanName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Duration { get; set; }

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();
}
