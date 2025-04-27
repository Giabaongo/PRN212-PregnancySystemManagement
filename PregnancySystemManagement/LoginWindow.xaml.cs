using System.Windows;
using BLL.Service;
using BLL.Services;
using DAL.Models;
using DAL.Repositories;

namespace PregnancySystemManagement
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UserService _userService;
        private readonly MembershipService _membershipService;

        public LoginWindow()
        {
            InitializeComponent();
            _userService = new UserService();
            _membershipService = new MembershipService(new MembershipRepository(new PregnancyTrackingSystemContext()));
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            User user = _userService.GetUserLogin(email, password);

            if (user != null && user.UserType == "1")
            {
                var membership = await _membershipService.GetActiveMembershipByUserId(user.Id);
                if (membership != null)
                {
                    MessageBox.Show("Bạn đã có membership", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    UserWindow userWindow = new UserWindow(user, _userService);
                    userWindow.Show();
                    this.Close();
                }
                else
                {
                    UserWindow userWindow = new UserWindow(user, _userService);
                    userWindow.Show();
                    this.Close();
                }
            }
            else if (user != null && (user.UserType == "3" || user.UserType == "4"))
            {
                ManageWindow manageWindow = new ManageWindow(user, _userService);
                manageWindow.Show();
                this.Close();
            }
            else
            {
                txtError.Text = "Invalid email/password or unauthorized access.";
            }
        }
    }
}
