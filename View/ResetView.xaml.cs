using NewBank2.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace NewBank2.View
{
    public partial class ResetView : Window
    {
        public ResetView()
        {
            InitializeComponent();
            DataContext = new ResetViewModel();
        }

        // Close the application when the close button is clicked
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Allow the user to move the window by clicking and dragging
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        // Minimize the window when the minimize button is clicked
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            
        }

        // Navigate back to the LoginView when the back button is clicked
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoginView objLoginView = new LoginView();
            this.Visibility = Visibility.Hidden;
            objLoginView.Show();
        }
    }
}
