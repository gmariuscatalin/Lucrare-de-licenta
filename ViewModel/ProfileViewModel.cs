using Microsoft.EntityFrameworkCore;
using NewBank2.Commands;
using NewBank2.Models;
using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NewBank2.ViewModel
{
    
    public class ProfileViewModel : BaseViewModel
    {
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
        private SecureString _newPassword;
        private SecureString _confirmNewPassword;
        public ICommand ProfilePictureCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public SecureString NewPassword
        {
            get { return _newPassword; }
            set
            {
                SetProperty(ref _newPassword, value);
            }
        }

        public SecureString ConfirmNewPassword
        {
            get { return _confirmNewPassword; }
            set
            {
                SetProperty(ref _confirmNewPassword, value);
            }
        }

        // Property for holding and managing the profile picture bitmap image
        private BitmapImage _profilePictureBitmap;
        public BitmapImage ProfilePictureBitmap
        {
            get { return _profilePictureBitmap; }
            set
            {
                _profilePictureBitmap = value;
                OnPropertyChanged(nameof(ProfilePictureBitmap));
            }
        }

        public ProfileViewModel()
        {
            ProfilePictureCommand = new RelayCommand(ExecuteProfilePictureCommand);
            ChangePasswordCommand = new RelayCommand(ExecuteChangePasswordCommand);
            LoadProfilePicture();
        }

        /* This method executes the profile picture command, allowing the user to choose
         a new profile picture, convert it to a base64 string, and update it in the database*/
        private void ExecuteProfilePictureCommand(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                string base64String = Convert.ToBase64String(imageBytes); // Encode the selected image to Base64

                User currentUser = new User()
                {
                    Id = StoreUserViewModel.Id,
                    Username = StoreUserViewModel.Username,
                    Password = StoreUserViewModel.Password,
                    Name = StoreUserViewModel.Name,
                    LastName = StoreUserViewModel.LastName,
                    ProfilePicture = base64String
                }; // Create a new user object with the updated ProfilePicture property

                using (var context = new LoginContext())
                {
                    context.Entry(currentUser).State = EntityState.Modified; // Set the state of the current user to Modified
                    context.SaveChanges(); // Save the changes to the database
                }

                StoreUserViewModel.ProfilePicture = base64String; // Update the ProfilePicture property in the StoreUserViewModel

                // Convert the base64String back to an image and set the ProfilePictureBitmap property to the converted image
                byte[] imageBytesConverted = Convert.FromBase64String(base64String);
                MemoryStream memoryStream = new MemoryStream(imageBytesConverted);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
                ProfilePictureBitmap = bitmapImage;
            }
        }

        /* This method executes the change password command, allowing the user to change
         their password after validating the entered values*/
        private void ExecuteChangePasswordCommand(object parameter)
        {
            if (!ValidatePasswordFields())
                return;

            User currentUser = new User()
            {
                Id = StoreUserViewModel.Id,
                Username = StoreUserViewModel.Username,
                Password = SecureStringToString(NewPassword),
                Name = StoreUserViewModel.Name,
                LastName = StoreUserViewModel.LastName,
                ProfilePicture = StoreUserViewModel.ProfilePicture
            };
            using (var context = new LoginContext())
            {
                context.Entry(currentUser).State = EntityState.Modified; // Set the state of the current user to Modified
                context.SaveChanges(); // Save the changes to the database
            }
            ErrorMessage="Password change successfully.";
        }

        // This method validates the entered password fields
        private bool ValidatePasswordFields()
        {
            if (NewPassword == null)
            {
                ErrorMessage = "Please enter a new password.";
                return false;
            }

            if (ConfirmNewPassword == null)
            {
                ErrorMessage = "Please confirm your new password.";
                return false;
            }
            if (NewPassword.Length < 8)
            {
                ErrorMessage = "Password must be at least 8 characters long.";
                return false;
            }

            string newPasswordStr = SecureStringToString(NewPassword);
            if (!newPasswordStr.Any(char.IsDigit) || !newPasswordStr.Any(char.IsPunctuation))
            {
                ErrorMessage = "Password must contain at least one digit and one special character.";
                return false;
            }

            if (SecureStringToString(NewPassword) != SecureStringToString(ConfirmNewPassword))
            {
                ErrorMessage = "New password and confirmation do not match.";
                return false;
            }

            return true;
        }

        // This method converts a SecureString to a regular string
        private string SecureStringToString(SecureString secureString)
        {
            IntPtr insecureStringPtr = IntPtr.Zero;
            try
            {
                insecureStringPtr = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return System.Runtime.InteropServices.Marshal.PtrToStringUni(insecureStringPtr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(insecureStringPtr);
            }
        }

        // This method loads the user's profile picture from the stored base64 string
        private void LoadProfilePicture()
        {
            if (!string.IsNullOrEmpty(StoreUserViewModel.ProfilePicture))
            {
                byte[] imageBytes = Convert.FromBase64String(StoreUserViewModel.ProfilePicture);
                MemoryStream memoryStream = new MemoryStream(imageBytes);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
                ProfilePictureBitmap = bitmapImage;
            }
        }
    }
}
