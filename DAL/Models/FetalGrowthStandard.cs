using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class FetalGrowthStandard
{
    public int Id { get; set; }

    public int WeekNumber { get; set; }

    public decimal WeightGrams { get; set; }

    public decimal HeightCm { get; set; }

    public decimal BiparietalDiameterCm { get; set; }

    public decimal FemoralLengthCm { get; set; }

    public decimal HeadCircumferenceCm { get; set; }

    public decimal AbdominalCircumferenceCm { get; set; }
}
