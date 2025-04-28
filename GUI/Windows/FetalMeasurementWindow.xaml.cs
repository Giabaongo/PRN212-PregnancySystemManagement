using System;
using System.Windows;
using DAL.Models;
using DAL.Repositories;

namespace GUI.Windows
{
    public partial class FetalMeasurementWindow : Window
    {
        private readonly FetalMeasurementRepository _measurementRepository;
        private readonly FetalGrowthStandardRepository _standardRepository;
        private readonly PregnancyProfile _profile;

        public FetalMeasurementWindow(PregnancyProfile profile)
        {
            InitializeComponent();
            _measurementRepository = new FetalMeasurementRepository();
            _standardRepository = new FetalGrowthStandardRepository();
            _profile = profile;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var measurement = new FetalMeasurement
                {
                    ProfileId = _profile.Id,
                    Week = int.Parse(txtWeek.Text),
                    WeightGrams = decimal.Parse(txtWeight.Text),
                    HeightCm = decimal.Parse(txtHeight.Text),
                    BiparietalDiameterCm = decimal.Parse(txtBiparietalDiameter.Text),
                    FemoralLengthCm = decimal.Parse(txtFemoralLength.Text),
                    HeadCircumferenceCm = decimal.Parse(txtHeadCircumference.Text),
                    AbdominalCircumferenceCm = decimal.Parse(txtAbdominalCircumference.Text),
                    Notes = txtNotes.Text,
                    CreatedAt = DateTime.Now
                };

                // Get the standard for this week
                var standard = _standardRepository.GetByWeek(measurement.Week);
                if (standard != null)
                {
                    // Compare measurements with standards
                    var result = CompareWithStandards(measurement, standard);
                    MessageBox.Show(result, "Measurement Analysis", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                _measurementRepository.Add(measurement);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving measurements: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string CompareWithStandards(FetalMeasurement measurement, FetalGrowthStandard standard)
        {
            var result = "Measurement Analysis:\n\n";

            result += CompareValue("Weight", measurement.WeightGrams, standard.WeightGrams, "grams");
            result += CompareValue("Height", measurement.HeightCm, standard.HeightCm, "cm");
            result += CompareValue("Biparietal Diameter", measurement.BiparietalDiameterCm.Value, standard.BiparietalDiameterCm, "cm");
            result += CompareValue("Femoral Length", measurement.FemoralLengthCm.Value, standard.FemoralLengthCm, "cm");
            result += CompareValue("Head Circumference", measurement.HeadCircumferenceCm.Value, standard.HeadCircumferenceCm, "cm");
            result += CompareValue("Abdominal Circumference", measurement.AbdominalCircumferenceCm.Value, standard.AbdominalCircumferenceCm, "cm");

            return result;
        }

        private string CompareValue(string name, decimal measured, decimal standard, string unit)
        {
            var difference = measured - standard;
            var percentage = (difference / standard) * 100;
            var status = Math.Abs(percentage) <= 10 ? "Normal" :
                        percentage > 10 ? "Above Average" : "Below Average";

            return $"{name}: {measured} {unit} (Standard: {standard} {unit})\n" +
                   $"Difference: {difference:F2} {unit} ({percentage:F1}%)\n" +
                   $"Status: {status}\n\n";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}