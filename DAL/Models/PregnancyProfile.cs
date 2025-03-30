using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class PregnancyProfile
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime ConceptionDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? PregnancyStatus { get; set; }

    public virtual ICollection<FetalMeasurement> FetalMeasurements { get; set; } = new List<FetalMeasurement>();

    public virtual User User { get; set; } = null!;
}
