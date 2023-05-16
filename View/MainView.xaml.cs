using NewBank2.ViewModel;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace NewBank2.View
{
    // This class represents the MainView Window, which contains the navigation menu and content area.
    public partial class MainView : Window
    {
        private bool isAnimating = false;
        private DispatcherTimer _timer;
        private bool _RadioButtonChecked = false;

        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            MainFrame.NavigationService.Navigate(new DashboardView());

            // Set up a timer to update the data context every 10 seconds
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(10);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        // Toggle the visibility of the menu buttons when the hamburger menu button is clicked
        private void OnHamburgerMenuButtonClick(object sender, RoutedEventArgs e)
        {
            if (isAnimating)
            {
                return; // Do nothing if the menu is already being animated
            }

            if (MenuItemsPanel.Visibility == Visibility.Collapsed)
            {
                // Show the menu buttons
                MenuItemsPanel.Visibility = Visibility.Visible;

                // Start the fade-in animation
                var fadeInStoryboard = (Storyboard)Resources["MenuButtonsFadeIn"];
                fadeInStoryboard.Completed += (s, ev) => { isAnimating = false; };
                isAnimating = true;
                fadeInStoryboard.Begin(MenuItemsPanel);
            }
            else
            {
                // Start the fade-out animation
                var fadeOutStoryboard = (Storyboard)Resources["MenuButtonsFadeOut"];
                fadeOutStoryboard.Completed += (s, ev) =>
                {
                    MenuItemsPanel.Visibility = Visibility.Collapsed;
                    isAnimating = false;
                };
                isAnimating = true;
                fadeOutStoryboard.Begin(MenuItemsPanel);
            }
        }

        // Navigate to the corresponding view when a radio button is checked
        private void DashboardRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (_RadioButtonChecked)
            {
                MainFrame.NavigationService.Navigate(new DashboardView());
            }
            else
            {
                _RadioButtonChecked = true;
            }
        }

        private void TransactionRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new TransactionView());
        }

        private void ProfileRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new ProfileView());
        }

        private void ContactRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new ContactView());
        }

        private void SettingsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new SettingsView());
        }

        // Hide the navigation UI when the content of the frame is rendered
        private void myFrame_ContentRendered(object sender, EventArgs e)
        {
            MainFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
        }

        // Update the data context when the timer ticks
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Call the MainViewModel to update the data context
            ((MainViewModel)DataContext).UpdateProfileImage();
        }

        // Allow dragging the window when the control bar is clicked and dragged
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }
        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set the maximum height of the window to the height of the primary screen when the mouse enters the control bar
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        // Close the application when the close button is clicked
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Minimize the window when the minimize button is clicked
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Toggle between maximized and normal window state when the maximize button is clicked
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else this.WindowState = WindowState.Normal;
        }
    }
}
