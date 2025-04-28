using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DAL.Models;
using BLL.Services;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace PregnancySystemManagement
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private readonly PregnancyTrackingSystemContext _context;
        private readonly User _currentUser;
        private readonly PostService _postService;
        private readonly PregnancyProfileService _profileService;
        private readonly FetalMeasurementService _measurementService;
        private PregnancyProfile _selectedProfile;
        private ObservableCollection<PregnancyProfile> _pregnancyProfiles;
        private ObservableCollection<Post> _posts;
        private Post _selectedPost;

        public UserWindow(PregnancyTrackingSystemContext context, User currentUser)
        {
            _context = context;
            _currentUser = currentUser;

            // Initialize repositories
            var postRepository = new PostRepository(_context);
            var profileRepository = new PregnancyProfileRepository(_context);
            var measurementRepository = new FetalMeasurementRepository(_context);

            // Initialize services
            _postService = new PostService(postRepository);
            _profileService = new PregnancyProfileService(profileRepository);
            _measurementService = new FetalMeasurementService(measurementRepository, profileRepository);

            _pregnancyProfiles = new ObservableCollection<PregnancyProfile>();
            _posts = new ObservableCollection<Post>();

            InitializeComponent();
            LoadProfiles();
            LoadPosts();
            SetupEventHandlers();
            DisableMeasurementForm();
        }

        private void LoadProfiles()
        {
            var profiles = _profileService.GetProfilesByUserId(_currentUser.Id);
            _pregnancyProfiles.Clear();
            foreach (var profile in profiles)
            {
                _pregnancyProfiles.Add(profile);
            }

            ProfilesListView.ItemsSource = _pregnancyProfiles;
        }

        private void LoadPosts()
        {
            var posts = _postService.GetAllPosts();
            _posts.Clear();
            foreach (var post in posts)
            {
                _posts.Add(post);
            }

            PostsListView.ItemsSource = _posts;
        }

        private void SetupEventHandlers()
        {
            // Pregnancy Profile handlers
            NewProfileButton.Click += NewProfileButton_Click;
            SelectProfileButton.Click += SelectProfileButton_Click;
            SaveMeasurementButton.Click += SaveMeasurementButton_Click;

            // Post handlers
            PostButton.Click += PostButton_Click;
            CommentButton.Click += CommentButton_Click;
            PostsListView.SelectionChanged += PostsListView_SelectionChanged;
        }

        private void PostsListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedPost = PostsListView.SelectedItem as Post;
        }

        private void PostButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _postService.CreatePost(
                    PostTitleTextBox.Text,
                    PostContentTextBox.Text,
                    _currentUser.Id
                );

                LoadPosts();
                PostTitleTextBox.Clear();
                PostContentTextBox.Clear();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating post: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedPost == null)
            {
                MessageBox.Show("Please select a post to comment on.", "No Post Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _postService.AddComment(
                    _selectedPost.Id,
                    CommentTextBox.Text,
                    _currentUser.Id
                );

                LoadPosts();
                CommentTextBox.Clear();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding comment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NewProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var newProfileWindow = new NewPregnancyProfileWindow(_context, _currentUser);
            if (newProfileWindow.ShowDialog() == true)
            {
                LoadProfiles();
            }
        }

        private void SelectProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProfilesListView.SelectedItem is PregnancyProfile selectedProfile)
            {
                _selectedProfile = selectedProfile;
                EnableMeasurementForm();
            }
            else
            {
                MessageBox.Show("Please select a profile first.", "No Profile Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SaveMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateMeasurements())
                {
                    MessageBox.Show("Please fill in all required fields with valid numbers.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var measurement = new FetalMeasurement
                {
                    ProfileId = _selectedProfile.Id,
                    Week = int.Parse(WeekTextBox.Text),
                    WeightGrams = decimal.Parse(WeightTextBox.Text),
                    HeightCm = decimal.Parse(HeightTextBox.Text),
                    BiparietalDiameterCm = string.IsNullOrEmpty(BPDTextBox.Text) ? null : decimal.Parse(BPDTextBox.Text),
                    FemoralLengthCm = string.IsNullOrEmpty(FLTextBox.Text) ? null : decimal.Parse(FLTextBox.Text),
                    HeadCircumferenceCm = string.IsNullOrEmpty(HCTextBox.Text) ? null : decimal.Parse(HCTextBox.Text),
                    AbdominalCircumferenceCm = string.IsNullOrEmpty(ACTextBox.Text) ? null : decimal.Parse(ACTextBox.Text),
                    Notes = NotesTextBox.Text,
                    CreatedAt = DateTime.Now
                };

                _measurementService.AddMeasurement(
                    measurement.ProfileId,
                    measurement.Week,
                    measurement.WeightGrams,
                    measurement.HeightCm,
                    measurement.BiparietalDiameterCm,
                    measurement.FemoralLengthCm,
                    measurement.HeadCircumferenceCm,
                    measurement.AbdominalCircumferenceCm,
                    measurement.Notes
                );

                ResultsTextBlock.Text = _measurementService.CompareWithStandards(measurement);
                ClearMeasurementForm();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving measurement: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateMeasurements()
        {
            return !string.IsNullOrEmpty(WeekTextBox.Text) &&
                   !string.IsNullOrEmpty(WeightTextBox.Text) &&
                   !string.IsNullOrEmpty(HeightTextBox.Text) &&
                   int.TryParse(WeekTextBox.Text, out _) &&
                   decimal.TryParse(WeightTextBox.Text, out _) &&
                   decimal.TryParse(HeightTextBox.Text, out _);
        }

        private void DisableMeasurementForm()
        {
            MeasurementForm.IsEnabled = false;
            ResultsPanel.Visibility = Visibility.Collapsed;
        }

        private void EnableMeasurementForm()
        {
            MeasurementForm.IsEnabled = true;
            ResultsPanel.Visibility = Visibility.Visible;
        }

        private void ClearMeasurementForm()
        {
            WeekTextBox.Text = string.Empty;
            WeightTextBox.Text = string.Empty;
            HeightTextBox.Text = string.Empty;
            BPDTextBox.Text = string.Empty;
            FLTextBox.Text = string.Empty;
            HCTextBox.Text = string.Empty;
            ACTextBox.Text = string.Empty;
            NotesTextBox.Text = string.Empty;
        }
    }
}
