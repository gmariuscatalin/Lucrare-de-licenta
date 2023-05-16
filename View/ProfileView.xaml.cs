using NewBank2.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace NewBank2.View
{
    public partial class ProfileView : UserControl
    {
        public ProfileView()
        {
            InitializeComponent();
            DataContext = new ProfileViewModel();
        }

        private void btnChangeProfilePicture_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
