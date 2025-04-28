using System.Windows;
using System.Windows.Controls;
using DAL.Models;

namespace PregnancySystemManagement
{
    public partial class NewPregnancyProfileWindow : Window
    {
        private readonly PregnancyTrackingSystemContext _context;
        private readonly User _currentUser;

        public NewPregnancyProfileWindow(PregnancyTrackingSystemContext context, User currentUser)
        {
            InitializeComponent();
            _context = context;
            _currentUser = currentUser;

            SetupEventHandlers();
            SetDefaultValues();
        }

        private void SetupEventHandlers()
        {
            SaveButton.Click += SaveButton_Click;
            CancelButton.Click += CancelButton_Click;
            ConceptionDatePicker.SelectedDateChanged += ConceptionDatePicker_SelectedDateChanged;
        }

        private void SetDefaultValues()
        {
            ConceptionDatePicker.SelectedDate = DateTime.Today;
            DueDatePicker.SelectedDate = DateTime.Today.AddDays(280); // Standard pregnancy duration
            StatusComboBox.SelectedIndex = 0; // Set to "Active" by default
        }

        private void ConceptionDatePicker_SelectedDateChanged(object sender, RoutedEventArgs e)
        {
            if (ConceptionDatePicker.SelectedDate.HasValue)
            {
                // Update due date to be 280 days (40 weeks) after conception
                DueDatePicker.SelectedDate = ConceptionDatePicker.SelectedDate.Value.AddDays(280);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var profile = new PregnancyProfile
            {
                UserId = _currentUser.Id,
                Name = NameTextBox.Text.Trim(),
                ConceptionDate = ConceptionDatePicker.SelectedDate.Value,
                DueDate = DueDatePicker.SelectedDate.Value,
                CreatedAt = DateTime.Now,
                PregnancyStatus = ((ComboBoxItem)StatusComboBox.SelectedItem).Content.ToString()
            };

            try
            {
                _context.PregnancyProfiles.Add(profile);
                _context.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool ValidateInput()
        {
            return !string.IsNullOrWhiteSpace(NameTextBox.Text) &&
                   ConceptionDatePicker.SelectedDate.HasValue &&
                   DueDatePicker.SelectedDate.HasValue &&
                   StatusComboBox.SelectedItem != null;
        }
    }
}