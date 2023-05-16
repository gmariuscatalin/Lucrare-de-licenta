using FontAwesome.Sharp;
using NewBank2.Commands;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NewBank2.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        // Private fields for holding the caption, icon, and user information
        private string _caption;
        private IconChar _icon;
        private string _userName;
        private string _userLastName;
        private BitmapImage _profileImage;
        // Properties for getting and setting the user's profile image
        public BitmapImage ProfileImage
        {
            get { return _profileImage; }
            set { _profileImage = value; OnPropertyChanged(); }
        }


        public string Caption
        {
            get { return _caption; }
            set { _caption = value; OnPropertyChanged(); }
        }

        public IconChar Icon
        {
            get { return _icon; }
            set { _icon = value; OnPropertyChanged(); }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(); }
        }

        public string UserLastName
        {
            get { return _userLastName; }
            set { _userLastName = value; OnPropertyChanged(); }
        }
        // Property for getting the user's full name
        public string FullName => $"{UserName} {UserLastName}";


        public ICommand DashboardCommand { get; }
        public ICommand TransactionCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand ContactCommand { get; }
        public ICommand SettingsCommand { get; }

        public MainViewModel()
        {
            DashboardCommand = new RelayCommand(DashboardViewCommand);
            TransactionCommand = new RelayCommand(TransactionViewCommand);
            ProfileCommand = new RelayCommand(ProfileViewCommand);
            ContactCommand = new RelayCommand(ContactViewCommand);
            SettingsCommand = new RelayCommand(SettingsViewCommand); ;
            UserName = StoreUserViewModel.Name;
            UserLastName = StoreUserViewModel.LastName;
            DashboardViewCommand(null);
            if (!string.IsNullOrEmpty(StoreUserViewModel.ProfilePicture))
            {
                byte[] imageBytes = Convert.FromBase64String(StoreUserViewModel.ProfilePicture);
                using (MemoryStream stream = new MemoryStream(imageBytes))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                    UpdateProfileImage(image);
                }
            }
        }

        // This method updates the profile image asynchronously
        private async void UpdateProfileImage(BitmapImage image)
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ProfileImage = image;
                }));
            });
        }

        // This method loads and updates the user's profile image
        public void UpdateProfileImage()
        {
            if (!string.IsNullOrEmpty(StoreUserViewModel.ProfilePicture))
            {
                byte[] imageBytes = Convert.FromBase64String(StoreUserViewModel.ProfilePicture);
                using (MemoryStream stream = new MemoryStream(imageBytes))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                    UpdateProfileImage(image);
                }
            }
        }





        private void DashboardViewCommand(object obj)
        {
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }

        private void TransactionViewCommand(object obj)
        {
            Caption = "Transaction";
            Icon = IconChar.Wallet;
        }

        private void ProfileViewCommand(object obj)
        {
            Caption = "Profile";
            Icon = IconChar.User;
        }

        private void ContactViewCommand(object obj)
        {
            Caption = "Contact";
            Icon = IconChar.Phone;
        }

        private void SettingsViewCommand(object obj)
        {
            Caption = "Settings";
            Icon = IconChar.Tools;
        }

    }
}
