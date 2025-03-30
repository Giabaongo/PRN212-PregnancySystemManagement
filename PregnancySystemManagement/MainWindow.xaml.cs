using System.Windows;
using System.Windows.Controls;
using BLL.Services;
using DAL.Models;

namespace PregnancySystemManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UserService _userService;

        public MainWindow()
        {
            InitializeComponent();
            _userService = new UserService();
            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = _userService.GetAllUsers();
            dgPreOders.ItemsSource = users;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement search functionality
        }

        private void dgPreOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPreOders.SelectedItem is User selectedUser)
            {
                // Populate the information details form with selected user data
                txtID.Text = selectedUser.Id.ToString();
                txtEmail.Text = selectedUser.Email;
                txtPassword.Text = selectedUser.Password;
                txtUserType.Text = selectedUser.UserType;
                txtFirstname.Text = selectedUser.FirstName;
                txtLastName.Text = selectedUser.LastName;
                txtGender.Text = selectedUser.Gender;
                txtPhone.Text = selectedUser.Phone;
                txtStatus.Text = selectedUser.Status;
                dpCreatedAt.SelectedDate = selectedUser.CreatedAt;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement add functionality
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement update functionality
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement delete functionality
        }
    }
}