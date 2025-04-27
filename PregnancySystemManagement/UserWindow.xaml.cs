using System.Windows;
using System.Windows.Controls;
using BLL.Service;
using BLL.Services;
using DAL.Models;
using DAL.Repositories;

namespace PregnancySystemManagement
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private readonly User _user;
        private readonly UserService _userService;
        private readonly MembershipService _membershipService;

        public UserWindow(User user, UserService userService)
        {
            InitializeComponent();
            _user = user;
            _userService = userService;
            _membershipService = new MembershipService(new MembershipRepository(new PregnancyTrackingSystemContext()));

            LoadUserInfo();
            LoadMembershipPlans();
        }

        private async void LoadUserInfo()
        {
            txtWelcome.Text = $"Welcome, {_user.LastName}";
            txtUserInfo.Text = $"Email: {_user.Email}";

            var membership = await _membershipService.GetActiveMembershipByUserId(_user.Id);
            if (membership != null)
            {
                txtWelcome.Text += " (Active Member)";
            }
        }

        private async void LoadMembershipPlans()
        {
            var plans = await _membershipService.GetAllPlans();
            membershipPlansList.ItemsSource = plans;
        }

        private async void SubscribeButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var plan = button.DataContext as MembershipPlan;
            if (plan == null) return;

            try
            {
                var existingMembership = await _membershipService.GetActiveMembershipByUserId(_user.Id);
                if (existingMembership != null)
                {
                    MessageBox.Show("You already have an active membership.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Do you want to subscribe to {plan.PlanName} for {plan.Price:C}?",
                    "Confirm Subscription",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    await _membershipService.SubscribeToPlan(_user.Id, plan.Id);
                    MessageBox.Show("Subscription successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadUserInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
