using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using DAL.Repository;

namespace BLL.Services
{
    public class FetalMeasurementService
    {
        private readonly FetalMeasurementRepository _measurementRepository;
        private readonly PregnancyProfileRepository _profileRepository;

        public FetalMeasurementService(FetalMeasurementRepository measurementRepository, PregnancyProfileRepository profileRepository)
        {
            _measurementRepository = measurementRepository;
            _profileRepository = profileRepository;
        }

        public List<FetalMeasurement> GetMeasurementsByProfileId(int profileId)
        {
            return _measurementRepository.GetByProfileId(profileId);
        }

        public void AddMeasurement(int profileId, int week, decimal weight, decimal height, decimal? biparietalDiameter, decimal? femoralLength, decimal? headCircumference, decimal? abdominalCircumference, string notes)
        {
            if (week < 1 || week > 42)
                throw new ArgumentException("Week must be between 1 and 42");
            if (weight <= 0)
                throw new ArgumentException("Weight must be positive");
            if (height <= 0)
                throw new ArgumentException("Height must be positive");

            var profile = _profileRepository.GetById(profileId);
            if (profile == null)
                throw new ArgumentException("Profile not found");

            var measurement = new FetalMeasurement
            {
                ProfileId = profileId,
                Week = week,
                WeightGrams = weight,
                HeightCm = height,
                BiparietalDiameterCm = biparietalDiameter,
                FemoralLengthCm = femoralLength,
                HeadCircumferenceCm = headCircumference,
                AbdominalCircumferenceCm = abdominalCircumference,
                Notes = notes,
                CreatedAt = DateTime.Now
            };

            _measurementRepository.Add(measurement);
        }

        public string CompareWithStandards(FetalMeasurement measurement)
        {
            var standard = _measurementRepository.GetGrowthStandardByWeek(measurement.Week);
            if (standard == null)
                return "No standard measurements available for this week.";

            var results = new StringBuilder();
            results.AppendLine($"Week {measurement.Week} Growth Analysis:");
            results.AppendLine();

            CompareAndAppend(results, "Weight", measurement.WeightGrams, standard.WeightGrams, "g");
            CompareAndAppend(results, "Height", measurement.HeightCm, standard.HeightCm, "cm");

            if (measurement.BiparietalDiameterCm.HasValue)
                CompareAndAppend(results, "Biparietal Diameter", measurement.BiparietalDiameterCm.Value, standard.BiparietalDiameterCm, "cm");

            if (measurement.FemoralLengthCm.HasValue)
                CompareAndAppend(results, "Femoral Length", measurement.FemoralLengthCm.Value, standard.FemoralLengthCm, "cm");

            if (measurement.HeadCircumferenceCm.HasValue)
                CompareAndAppend(results, "Head Circumference", measurement.HeadCircumferenceCm.Value, standard.HeadCircumferenceCm, "cm");

            if (measurement.AbdominalCircumferenceCm.HasValue)
                CompareAndAppend(results, "Abdominal Circumference", measurement.AbdominalCircumferenceCm.Value, standard.AbdominalCircumferenceCm, "cm");

            return results.ToString();
        }

        private void CompareAndAppend(StringBuilder builder, string measurementName, decimal actual, decimal standard, string unit)
        {
            var difference = ((actual - standard) / standard) * 100;
            var status = difference switch
            {
                < -10 => "Below normal range",
                > 10 => "Above normal range",
                _ => "Within normal range"
            };

            builder.AppendLine($"{measurementName}:");
            builder.AppendLine($"  Measured: {actual:F2} {unit}");
            builder.AppendLine($"  Standard: {standard:F2} {unit}");
            builder.AppendLine($"  Status: {status} ({difference:F1}% difference)");
            builder.AppendLine();
        }
    }
}