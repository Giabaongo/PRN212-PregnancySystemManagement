using System;
using System.Windows;
using DAL.Models;
using DAL.Repositories;

namespace GUI.Windows
{
    public partial class NewPregnancyProfileWindow : Window
    {
        private readonly PregnancyProfileRepository _profileRepository;
        private readonly int _userId;

        public NewPregnancyProfileWindow(int userId)
        {
            InitializeComponent();
            _profileRepository = new PregnancyProfileRepository();
            _userId = userId;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Please enter a profile name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (dpConceptionDate.SelectedDate == null)
                {
                    MessageBox.Show("Please select a conception date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (dpDueDate.SelectedDate == null)
                {
                    MessageBox.Show("Please select a due date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var profile = new PregnancyProfile
                {
                    UserId = _userId,
                    Name = txtName.Text,
                    ConceptionDate = dpConceptionDate.SelectedDate.Value,
                    DueDate = dpDueDate.SelectedDate.Value,
                    CreatedAt = DateTime.Now,
                    PregnancyStatus = "Active"
                };

                _profileRepository.Add(profile);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}