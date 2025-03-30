using System.Windows;
using BLL.Services;

namespace PregnancySystemManagement
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UserService _userService;

        public LoginWindow()
        {
            InitializeComponent();
            // You'll need to properly configure dependency injection
            // This is a temporary solution - ideally use proper DI
            _userService = new UserService();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = txtUsername.Text;
            string password = txtPassword.Password;

            try
            {
                var user = _userService.GetUser(email, password);

                if (user != null && (user.UserType == "3" || user.UserType == "4"))
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid email or password!", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
