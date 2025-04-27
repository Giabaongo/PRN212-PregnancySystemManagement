using System.Windows;
using BLL.Service;
using DAL.Models;

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
            _userService = new UserService();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            User user = _userService.GetUserLogin(email, password);

            //if (user != null && user.UserType == "1")
            //{
            //    MainWindow mainWindow = new MainWindow();
            //    mainWindow.Show();
            //    this.Close();
            //}
            //else
            //else if (user != null && user.UserType == "2")
            //{
            //    DoctorWindow doctorWindow = new DoctorWindow(user, _userService);
            //    doctorWindow.Show();
            //    this.Close();
            //}

            if (user != null && (user.UserType == "3" || user.UserType == "4"))
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
