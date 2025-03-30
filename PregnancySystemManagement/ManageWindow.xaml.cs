using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL.Service;
using DAL.Models;

namespace PregnancySystemManagement
{
    /// <summary>
    /// Interaction logic for ManageWindow.xaml
    /// </summary>
    public partial class ManageWindow : Window
    {
        private readonly User _currentUser;
        private readonly UserService _userService;
        private readonly PostService _postService;
        private readonly CommentService _commentService;
        private readonly PregnancyProfileService _profileService;

        public ManageWindow(User user, UserService userService)
        {
            InitializeComponent();
            _currentUser = user;
            _userService = userService;
            _postService = new PostService();
            _commentService = new CommentService();
            _profileService = new PregnancyProfileService();

            InitializeWindow();
            LoadData();
        }

        private void InitializeWindow()
        {
            txtUserInfo.Text = $"User: {_currentUser.Email} ({_currentUser.UserType})";
            ClearForms();
        }

        private void LoadData()
        {
            RefreshData();
            RefreshPosts();
            RefreshProfiles();
        }

        private void RefreshData()
        {
            var users = _userService.GetAllUsers()
                .Where(u => int.Parse(u.UserType) <= 2) // Only show users with type 1 and 2
                .ToList();
            dgData.ItemsSource = users;
        }

        private void ClearForms()
        {
            // Clear User form
            txtEmail.Text = string.Empty;
            txtPassword.Password = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            cbDetailUserType.SelectedIndex = -1;
            cbStatus.SelectedIndex = 0;

            // Clear Post form
            txtPostTitle.Text = string.Empty;
            txtPostContent.Text = string.Empty;
            cbPostStatus.SelectedIndex = 0;

            // Clear Profile form
            txtProfileName.Text = string.Empty;
            dpConceptionDate.SelectedDate = DateTime.Now;
            dpDueDate.SelectedDate = DateTime.Now.AddMonths(9);
            cbProfileStatus.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchEmail = txtSearchEmail.Text.Trim();
            string userType = (cbUserType.SelectedItem as ComboBoxItem)?.Content.ToString();

            var users = _userService.GetAllUsers()
                .Where(u => int.Parse(u.UserType) <= 2); // Base restriction

            if (!string.IsNullOrEmpty(searchEmail))
            {
                users = users.Where(u => u.Email.Contains(searchEmail));
            }

            if (!string.IsNullOrEmpty(userType) && userType != "All")
            {
                users = users.Where(u => u.UserType == userType);
            }

            dgData.ItemsSource = users.ToList();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedUser = dgData.SelectedItem as User;
            if (selectedUser != null)
            {
                txtEmail.Text = selectedUser.Email;
                txtPassword.Password = string.Empty;
                txtFirstName.Text = selectedUser.FirstName;
                txtLastName.Text = selectedUser.LastName;
                
                // Fix UserType selection
                foreach (ComboBoxItem item in cbDetailUserType.Items)
                {
                    if (item.Content.ToString() == selectedUser.UserType)
                    {
                        cbDetailUserType.SelectedItem = item;
                        break;
                    }
                }
                
                // Fix Status selection
                foreach (ComboBoxItem item in cbStatus.Items)
                {
                    if (item.Content.ToString() == selectedUser.Status)
                    {
                        cbStatus.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userType = (cbDetailUserType.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (!string.IsNullOrEmpty(userType) && int.Parse(userType) > 2)
                {
                    MessageBox.Show("You cannot create users with this user type!", "Access Denied", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newUser = new User
                {
                    Email = txtEmail.Text.Trim(),
                    Password = txtPassword.Password,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    UserType = userType,
                    Status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    CreatedAt = DateTime.Now
                };

                if (ValidateUser(newUser))
                {
                    _userService.CreateUser(newUser);
                    MessageBox.Show("User added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshData();
                    ClearForms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = dgData.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Please select a user to update", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var newUserType = (cbDetailUserType.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (string.IsNullOrEmpty(newUserType))
                {
                    MessageBox.Show("Please select a user type", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (int.Parse(newUserType) > 2)
                {
                    MessageBox.Show("You cannot change user type to this level!", "Access Denied", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Check if trying to update own user type
                if (selectedUser.Id == _currentUser.Id && newUserType != _currentUser.UserType)
                {
                    MessageBox.Show("You cannot change your own user type!", "Warning", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                selectedUser.Email = txtEmail.Text.Trim();
                if (!string.IsNullOrEmpty(txtPassword.Password))
                {
                    selectedUser.Password = txtPassword.Password;
                }
                selectedUser.FirstName = txtFirstName.Text.Trim();
                selectedUser.LastName = txtLastName.Text.Trim();
                selectedUser.UserType = newUserType;
                selectedUser.Status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (ValidateUser(selectedUser))
                {
                    _userService.UpdateUser(selectedUser);
                    MessageBox.Show("User updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshData();
                    ClearForms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = dgData.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Please select a user to delete", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selectedUser.Id == _currentUser.Id)
            {
                MessageBox.Show("You cannot delete your own account!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _userService.DeleteUser(selectedUser);
                    MessageBox.Show("User deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshData();
                    ClearForms();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool ValidateUser(User user)
        {
            if (string.IsNullOrEmpty(user.Email))
            {
                MessageBox.Show("Email is required!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (user.Id == 0 && string.IsNullOrEmpty(txtPassword.Password)) // New user
            {
                MessageBox.Show("Password is required for new users!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(user.UserType))
            {
                MessageBox.Show("User Type is required!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        #region Post Management
        private void RefreshPosts()
        {
            var posts = _postService.GetAllPosts();
            dgPosts.ItemsSource = posts;
        }

        private void btnSearchPosts_Click(object sender, RoutedEventArgs e)
        {
            string searchTitle = txtSearchPostTitle.Text.Trim();
            string status = (cbSearchPostStatus.SelectedItem as ComboBoxItem)?.Content.ToString();

            var posts = _postService.GetAllPosts();

            if (!string.IsNullOrEmpty(searchTitle))
            {
                posts = posts.Where(p => p.Title.Contains(searchTitle)).ToList();
            }

            if (!string.IsNullOrEmpty(status) && status != "All")
            {
                posts = posts.Where(p => p.Status == status).ToList();
            }

            dgPosts.ItemsSource = posts;
        }

        private void dgPosts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPost = dgPosts.SelectedItem as Post;
            if (selectedPost != null)
            {
                txtPostTitle.Text = selectedPost.Title;
                txtPostContent.Text = selectedPost.Content;
                cbPostStatus.SelectedValue = selectedPost.Status;

                // Enable/disable fields based on ownership
                bool isOwner = selectedPost.UserId == _currentUser.Id;
                txtPostTitle.IsEnabled = isOwner;
                txtPostContent.IsEnabled = isOwner;
                cbPostStatus.IsEnabled = true; // Everyone can change status
            }
        }

        private void btnAddPost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newPost = new Post
                {
                    Title = txtPostTitle.Text.Trim(),
                    Content = txtPostContent.Text.Trim(),
                    Status = (cbPostStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Active",
                    UserId = _currentUser.Id,
                    CreatedAt = DateTime.Now
                };

                if (ValidatePost(newPost))
                {
                    _postService.CreatePost(newPost);
                    MessageBox.Show("Post added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshPosts();
                    ClearPostForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding post: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdatePost_Click(object sender, RoutedEventArgs e)
        {
            var selectedPost = dgPosts.SelectedItem as Post;
            if (selectedPost == null)
            {
                MessageBox.Show("Please select a post to update", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Only update title and content if user is the owner
                if (selectedPost.UserId == _currentUser.Id)
                {
                    selectedPost.Title = txtPostTitle.Text.Trim();
                    selectedPost.Content = txtPostContent.Text.Trim();
                }
                else if (txtPostTitle.Text != selectedPost.Title || txtPostContent.Text != selectedPost.Content)
                {
                    MessageBox.Show("You can only update the status of posts you don't own.", "Warning", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Always allow status update
                selectedPost.Status = (cbPostStatus.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (ValidatePost(selectedPost))
                {
                    _postService.UpdatePost(selectedPost);
                    MessageBox.Show("Post updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshPosts();
                    ClearPostForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating post: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeletePost_Click(object sender, RoutedEventArgs e)
        {
            var selectedPost = dgPosts.SelectedItem as Post;
            if (selectedPost == null)
            {
                MessageBox.Show("Please select a post to delete", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Only allow deletion if user is the owner
            if (selectedPost.UserId != _currentUser.Id)
            {
                MessageBox.Show("You can only delete your own posts!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this post?", "Confirm Delete", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _postService.DeletePost(selectedPost);
                    MessageBox.Show("Post deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshPosts();
                    ClearPostForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting post: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool ValidatePost(Post post)
        {
            if (string.IsNullOrEmpty(post.Title))
            {
                MessageBox.Show("Title is required!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(post.Content))
            {
                MessageBox.Show("Content is required!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(post.Status))
            {
                MessageBox.Show("Status is required!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void ClearPostForm()
        {
            txtPostTitle.Text = string.Empty;
            txtPostContent.Text = string.Empty;
            cbPostStatus.SelectedIndex = 0;
        }
        #endregion

        #region Profile Management
        private void RefreshProfiles()
        {
            var profiles = _profileService.GetAllProfiles();
            dgProfiles.ItemsSource = profiles;
        }

        private void btnSearchProfiles_Click(object sender, RoutedEventArgs e)
        {
            string searchUser = txtSearchProfileUser.Text.Trim();
            string status = (cbSearchProfileStatus.SelectedItem as ComboBoxItem)?.Content.ToString();

            var profiles = _profileService.GetAllProfiles();

            if (!string.IsNullOrEmpty(searchUser))
            {
                profiles = profiles.Where(p => p.User.Email.Contains(searchUser)).ToList();
            }

            if (!string.IsNullOrEmpty(status) && status != "All")
            {
                profiles = profiles.Where(p => p.PregnancyStatus == status).ToList();
            }

            dgProfiles.ItemsSource = profiles;
        }

        private void dgProfiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProfile = dgProfiles.SelectedItem as PregnancyProfile;
            if (selectedProfile != null)
            {
                txtProfileName.Text = selectedProfile.Name;
                dpConceptionDate.SelectedDate = selectedProfile.ConceptionDate;
                dpDueDate.SelectedDate = selectedProfile.DueDate;
                cbProfileStatus.SelectedValue = selectedProfile.PregnancyStatus;
            }
        }

        private void btnAddProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newProfile = new PregnancyProfile
                {
                    Name = txtProfileName.Text.Trim(),
                    ConceptionDate = dpConceptionDate.SelectedDate ?? DateTime.Now,
                    DueDate = dpDueDate.SelectedDate ?? DateTime.Now.AddMonths(9),
                    PregnancyStatus = (cbProfileStatus.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    UserId = _currentUser.Id,
                    CreatedAt = DateTime.Now
                };

                if (ValidateProfile(newProfile))
                {
                    _profileService.CreateProfile(newProfile);
                    MessageBox.Show("Profile added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshProfiles();
                    ClearForms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdateProfile_Click(object sender, RoutedEventArgs e)
        {
            var selectedProfile = dgProfiles.SelectedItem as PregnancyProfile;
            if (selectedProfile == null)
            {
                MessageBox.Show("Please select a profile to update", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                selectedProfile.Name = txtProfileName.Text.Trim();
                selectedProfile.ConceptionDate = dpConceptionDate.SelectedDate ?? DateTime.Now;
                selectedProfile.DueDate = dpDueDate.SelectedDate ?? DateTime.Now.AddMonths(9);
                selectedProfile.PregnancyStatus = (cbProfileStatus.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (ValidateProfile(selectedProfile))
                {
                    _profileService.UpdateProfile(selectedProfile);
                    MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshProfiles();
                    ClearForms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteProfile_Click(object sender, RoutedEventArgs e)
        {
            var selectedProfile = dgProfiles.SelectedItem as PregnancyProfile;
            if (selectedProfile == null)
            {
                MessageBox.Show("Please select a profile to delete", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this profile?", "Confirm Delete", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _profileService.DeleteProfile(selectedProfile);
                    MessageBox.Show("Profile deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshProfiles();
                    ClearForms();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool ValidateProfile(PregnancyProfile profile)
        {
            if (string.IsNullOrEmpty(profile.Name))
            {
                MessageBox.Show("Name is required!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (profile.ConceptionDate > profile.DueDate)
            {
                MessageBox.Show("Conception date cannot be after due date!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(profile.PregnancyStatus))
            {
                MessageBox.Show("Status is required!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
        #endregion

        #region Dialog Windows
        private bool ShowPostDialog(string title, Post post)
        {
            var dialog = new Window
            {
                Title = title,
                Width = 400,
                Height = 300,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };

            var panel = new StackPanel { Margin = new Thickness(10) };
            
            panel.Children.Add(new TextBlock { Text = "Content:" });
            var txtContent = new TextBox 
            { 
                Text = post.Content, 
                Height = 100,
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true,
                Margin = new Thickness(0, 0, 0, 10) 
            };
            panel.Children.Add(txtContent);

            var btnOk = new Button { Content = "OK", Width = 60, Margin = new Thickness(0, 10, 0, 10) };
            panel.Children.Add(btnOk);

            dialog.Content = panel;
            bool? result = false;

            btnOk.Click += (s, e) =>
            {
                post.Content = txtContent.Text;
                result = true;
                dialog.Close();
            };

            dialog.ShowDialog();
            return result ?? false;
        }

        private bool ShowCommentDialog(string title, Comment comment)
        {
            var dialog = new Window
            {
                Title = title,
                Width = 400,
                Height = 300,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };

            var panel = new StackPanel { Margin = new Thickness(10) };
            
            if (comment.Id == 0) // New comment
            {
                panel.Children.Add(new TextBlock { Text = "Post ID:" });
                var txtPostId = new TextBox { Margin = new Thickness(0, 0, 0, 10) };
                panel.Children.Add(txtPostId);
                
                // Add validation for PostId
                txtPostId.TextChanged += (s, e) =>
                {
                    if (int.TryParse(txtPostId.Text, out int postId))
                    {
                        comment.PostId = postId;
                    }
                };
            }

            panel.Children.Add(new TextBlock { Text = "Content:" });
            var txtContent = new TextBox 
            { 
                Text = comment.Content, 
                Height = 100,
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true,
                Margin = new Thickness(0, 0, 0, 10) 
            };
            panel.Children.Add(txtContent);

            var btnOk = new Button { Content = "OK", Width = 60, Margin = new Thickness(0, 10, 0, 10) };
            panel.Children.Add(btnOk);

            dialog.Content = panel;
            bool? result = false;

            btnOk.Click += (s, e) =>
            {
                comment.Content = txtContent.Text;
                result = true;
                dialog.Close();
            };

            dialog.ShowDialog();
            return result ?? false;
        }
        #endregion
    }
}
