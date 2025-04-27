using System.Windows;
using BLL.Service;
using DAL.Models;

namespace PregnancySystemManagement
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private readonly User _user;
        private readonly UserService _userService;
        public UserWindow(User user, UserService userService)
        {
            InitializeComponent();
            _user = user;
            _userService = userService;
        }
    }
}
