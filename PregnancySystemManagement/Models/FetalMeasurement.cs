using System;

namespace PregnancySystemManagement.Models
{
    public class FetalMeasurement
    {
        public int Id { get; set; }
        public int PregnancyProfileId { get; set; }
        public DateTime MeasurementDate { get; set; }
        public double GestationalAge { get; set; }  // in weeks
        public double HeadCircumference { get; set; }  // in cm
        public double AbdominalCircumference { get; set; }  // in cm
        public double FemurLength { get; set; }  // in cm
        public double EstimatedFetalWeight { get; set; }  // in grams

        public virtual PregnancyProfile PregnancyProfile { get; set; }
    }
}