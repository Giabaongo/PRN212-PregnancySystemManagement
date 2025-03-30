using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class GrowthAlert
{
    public int Id { get; set; }

    public int MeasurementId { get; set; }

    public string? AlertMessage { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual FetalMeasurement Measurement { get; set; } = null!;
}
