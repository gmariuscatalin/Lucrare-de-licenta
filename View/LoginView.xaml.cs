using NewBank2.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace NewBank2.View
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel { CloseAction = () => Close() };
        }

        /* This event handler is triggered when the left mouse button is clicked and dragged.
         It allows the user to move the window by clicking and dragging it.*/
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        // This event handler minimizes the window when the minimize button is clicked.
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // This event handler closes the application when the close button is clicked.
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
        }

        // This event handler navigates to the RegisterView when the register button is clicked.
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterView objRegisterView = new RegisterView();
            this.Visibility = Visibility.Hidden;
            objRegisterView.Show();
        }

        // This event handler navigates to the ResetView when the reset button is clicked.
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetView objResetView = new ResetView();
            this.Visibility = Visibility.Hidden;
            objResetView.Show();
        }

        private void btnVerify_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
