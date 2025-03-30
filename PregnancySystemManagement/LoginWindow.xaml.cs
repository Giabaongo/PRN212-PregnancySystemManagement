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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UserService _userService;

        public LoginWindow( )
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            User user = _userService.GetUserLogin(email, password);

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
