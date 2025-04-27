using System.Windows;
using BLL.Service;
using BLL.Services;
using DAL.Models;

namespace PregnancySystemManagement
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private readonly MembershipPlanService _membershipPlanService;
        public readonly User _currentUser;
        private readonly UserService _userService;

        public UserWindow(User user, UserService userService)
        {
            InitializeComponent();
            _currentUser = user;
            _userService = userService;
            txtUserInfo.Text = $"User: {_currentUser.Email} ({_currentUser.UserType})";
            _membershipPlanService = new MembershipPlanService();
            LoadMembershipPlans();
        }

        private void LoadMembershipPlans()
        {
            var plans = _membershipPlanService.GetAllPlans();
            membershipPlansList.ItemsSource = plans;
        }

        private void SubscribeButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is MembershipPlan selectedPlan)
            {
                // TODO: Implement subscription logic
                MessageBox.Show($"You have selected the {selectedPlan.PlanName} plan. Price: {selectedPlan.Price:C}");

                // Close the window after selection
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
