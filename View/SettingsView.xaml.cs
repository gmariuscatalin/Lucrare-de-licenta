using NewBank2.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace NewBank2.View
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            DataContext = new SettingsViewModel();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            // Close the current window and open the LoginView
            LoginView objLoginView = new LoginView();
            Application.Current.MainWindow.Close();
            objLoginView.Show();
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the CreateAccountView within the parent Frame
            var frame = FindParentFrame(this);
            frame.NavigationService.Navigate(new CreateAccountView());
        }

        // Helper method to find the parent Frame of the current UserControl
        private Frame FindParentFrame(DependencyObject child)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            if (parent == null) return null;
            if (parent is Frame frame) return frame;
            return FindParentFrame(parent);
        }

        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the DeleteAccountView within the parent Frame
            var frame = FindParentFrame(this);
            frame.NavigationService.Navigate(new DeleteAccountView());
        }

        private void btnStatementBank_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnActivateDeactivateAccount_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the ActivateDeactivateAccountView within the parent Frame
            var frame = FindParentFrame(this);
            frame.NavigationService.Navigate(new ActivateDeactivateAccountView());
        }

        private void btnAccountSettingsBank_Click(object sender, RoutedEventArgs e)
        {
            // Get the storyboard animations for the buttons stack panel
            Storyboard fadeInAnimation = (Storyboard)ButtonsStackPanel.Resources["FadeInAnimation"];
            Storyboard fadeOutAnimation = (Storyboard)ButtonsStackPanel.Resources["FadeOutAnimation"];

            // Toggle the visibility of the buttons stack panel using animations
            if (ButtonsStackPanel.Visibility == Visibility.Collapsed)
            {
                ButtonsStackPanel.Visibility = Visibility.Visible;
                fadeInAnimation.Begin(ButtonsStackPanel);
            }
            else
            {
                fadeOutAnimation.Completed += (s, a) => { ButtonsStackPanel.Visibility = Visibility.Collapsed; };
                fadeOutAnimation.Begin(ButtonsStackPanel);
            }
        }
    }
}
