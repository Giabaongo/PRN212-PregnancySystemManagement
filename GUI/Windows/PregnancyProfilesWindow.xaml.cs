using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DAL.Models;
using DAL.Repositories;

namespace GUI.Windows
{
    public partial class PregnancyProfilesWindow : Window
    {
        private readonly PregnancyProfileRepository _profileRepository;
        private readonly int _userId;
        private PregnancyProfile _selectedProfile;

        public PregnancyProfilesWindow(int userId)
        {
            InitializeComponent();
            _profileRepository = new PregnancyProfileRepository();
            _userId = userId;
            LoadProfiles();
        }

        private void LoadProfiles()
        {
            var profiles = _profileRepository.GetByUserId(_userId);
            dgProfiles.ItemsSource = profiles;
        }

        private void btnNewProfile_Click(object sender, RoutedEventArgs e)
        {
            var newProfileWindow = new NewPregnancyProfileWindow(_userId);
            if (newProfileWindow.ShowDialog() == true)
            {
                LoadProfiles();
            }
        }

        private void dgProfiles_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedProfile = dgProfiles.SelectedItem as PregnancyProfile;
            btnSelect.IsEnabled = _selectedProfile != null;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedProfile != null)
            {
                var fetalMeasurementWindow = new FetalMeasurementWindow(_selectedProfile);
                fetalMeasurementWindow.ShowDialog();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}