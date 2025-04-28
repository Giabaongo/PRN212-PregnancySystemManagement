using System;
using System.Collections.Generic;

namespace PregnancySystemManagement.Models
{
    public class PregnancyProfile
    {
        public int Id { get; set; }
        public string MotherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastMenstrualPeriod { get; set; }
        public DateTime EstimatedDueDate { get; set; }
        public virtual ICollection<FetalMeasurement> FetalMeasurements { get; set; }

        public PregnancyProfile()
        {
            FetalMeasurements = new List<FetalMeasurement>();
        }
    }
}