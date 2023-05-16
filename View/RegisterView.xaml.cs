using NewBank2.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace NewBank2.View
{
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Allow the window to be dragged by clicking and dragging the left mouse button
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            // Minimize the window
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // Shutdown the application
            Application.Current.Shutdown();
        }

        private void btnRegisterNow_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Create a new instance of LoginView
            LoginView objLoginView = new LoginView();
            // Hide the current RegisterView
            this.Visibility = Visibility.Hidden;
            // Show the LoginView
            objLoginView.Show();
        }
    }
}
