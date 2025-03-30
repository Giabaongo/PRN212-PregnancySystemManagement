using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class FetalMeasurement
{
    public int Id { get; set; }

    public int ProfileId { get; set; }

    public decimal WeightGrams { get; set; }

    public decimal HeightCm { get; set; }

    public decimal? BiparietalDiameterCm { get; set; }

    public decimal? FemoralLengthCm { get; set; }

    public decimal? HeadCircumferenceCm { get; set; }

    public decimal? AbdominalCircumferenceCm { get; set; }

    public string? Notes { get; set; }

    public int Week { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<GrowthAlert> GrowthAlerts { get; set; } = new List<GrowthAlert>();

    public virtual PregnancyProfile Profile { get; set; } = null!;
}
