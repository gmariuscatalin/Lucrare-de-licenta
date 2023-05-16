using NewBank2.Commands;
using NewBank2.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows;

namespace NewBank2.ViewModel
{
    public class RegisterViewModel : BaseViewModel
    {
        // Event to notify the view when the password should be cleared
        public event EventHandler ClearPasswordEvent;

        // Invoke the ClearPasswordEvent
        private void OnClearPassword()
        {
            ClearPasswordEvent?.Invoke(this, EventArgs.Empty);
        }

        // Properties for the user's first name, last name, and email
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
                User.Name = _firstName;
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value.Trim();
                OnPropertyChanged();
                User.LastName = _lastName;
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value.Trim();
                OnPropertyChanged();
                User.Username = _email;
            }
        }

        // Reset the user input fields and password boxes
        private void ResetUser()
        {
            User = new User();
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Password = new SecureString();
            ConfirmPassword = new SecureString();
            OnClearPassword();
        }

        // Property for displaying error messages
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

        // Event to notify when a property has changed
        public event PropertyChangedEventHandler PropertyChanged;

        // Property for the User object
        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        // Properties for the password and confirm password fields
        private SecureString _password;
        public SecureString Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private SecureString _confirmPassword;
        public SecureString ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        // Constructor for the RegisterViewModel
        public RegisterViewModel()
        {
            User = new User();
            RegisterCommand = new RelayCommand(Register);
        }

        // RelayCommand for the Register command
        public RelayCommand RegisterCommand { get; private set; }

        // Method to handle the registration process
        private void Register(object parameter)
        {
            if (IsPasswordValid())
            {
                // Check if Name and LastName are not empty
                if (User.Name == null || User.Name.Length == 0)
                {
                    ErrorMessage = "Name cannot be empty.";
                    return;
                }

                if (User.LastName == null || User.LastName.Length == 0)
                {
                    ErrorMessage = "Last name cannot be empty.";
                    return;
                }
                // Check if email already exists in the database
                using (var context = new LoginContext())
                {
                    if (context.Users.Any(u => u.Username == User.Username))
                    {
                        ErrorMessage = "Email already exists. Please choose a different email.";
                        return;
                    }
                }
                // Check for a valid email format
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(User.Username, emailPattern))
                {
                    ErrorMessage = "Invalid email format.";
                    return;
                }

                // If email is unique, create new user and save to database
                User newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Username = User.Username,
                    Password = SecureStringToString(Password),
                    Name = User.Name,
                    LastName = User.LastName
                };

                using (var context = new LoginContext())
                {
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }

                ErrorMessage = "User registered successfully.";

                // Clear the fields after successful registration
                ResetUser();
            }
        }

        // Validate the password input
        private bool IsPasswordValid()
        {
            if (Password == null || Password.Length < 8 || ConfirmPassword == null || ConfirmPassword.Length < 8)
            {
                ErrorMessage = "Password must be at least 8 characters.";
                return false;
            }

            string passwordString = SecureStringToString(Password);
            if (!passwordString.Any(char.IsDigit) || !passwordString.Any(char.IsPunctuation))
            {
                ErrorMessage = "Password must contain at least one digit and one special character.";
                return false;
            }

            if (!SecureStringToString(Password).Equals(SecureStringToString(ConfirmPassword)))
            {
                ErrorMessage = "Passwords do not match.";
                return false;
            }

            return true;
        }

        // Convert a SecureString to a regular string
        private string SecureStringToString(SecureString secureString)
        {
            if (secureString == null)
            {
                return null;
            }

            IntPtr bstr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureString);
            try
            {
                return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(bstr);
            }
        }

        // Method to raise the PropertyChanged event
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}