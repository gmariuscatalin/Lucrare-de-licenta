using NewBank2.Commands;
using NewBank2.Models;
using NewBank2.View;
using System;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Input;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace NewBank2.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        // Visibility properties for login and verification panels
        private Visibility _loginPanelVisibility = Visibility.Visible;
        public Visibility LoginPanelVisibility
        {
            get => _loginPanelVisibility;
            set
            {
                _loginPanelVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _verificationPanelVisibility = Visibility.Collapsed;
        public Visibility VerificationPanelVisibility
        {
            get => _verificationPanelVisibility;
            set
            {
                _verificationPanelVisibility = value;
                OnPropertyChanged();
            }
        }

        // Property to store the user input verification code
        public string VerificationCode { get; set; }

        // ICommand property for the Verify button
        public ICommand VerifyCommand { get; set; }

        // Instance of the currently logged in user
        private User _loggedInUser;

        // Error message property
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

        // Instance of the LoginContext (database context)
        private LoginContext _context;

        public LoginViewModel()
        {
            _context = new LoginContext();
            LoginCommand = new RelayCommand(Login, CanLogin);
            VerifyCommand = new RelayCommand(Verify, CanVerify);
        }

        // Properties for username and password
        public string Username { get; set; }
        public SecureString Password { get; set; }

        // ICommand property for the Login button
        public ICommand LoginCommand { get; set; }

        // Check if the login button can be enabled
        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrEmpty(Username) && Password != null && Password.Length > 0;
        }

        // Delegate for closing the login window
        public Action CloseAction { get; set; }

        // Check if the verify button can be enabled
        private bool CanVerify(object parameter)
        {
            return !string.IsNullOrEmpty(VerificationCode);
        }

        // Login method, executed when the Login button is clicked
        private async void Login(object parameter)
        {
            string password = new System.Net.NetworkCredential(string.Empty, Password).Password;
            _loggedInUser = _context.Users.FirstOrDefault(u => u.Username == Username && u.Password == password);

            if (_loggedInUser != null)
            {
                // Generate a random verification code
                Random random = new Random();
                int code = random.Next(100000, 999999);
                _loggedInUser.VerificationCode = code.ToString();
                _loggedInUser.VerificationCodeExpiration = DateTime.UtcNow.AddMinutes(10);
                _context.SaveChanges();

                // Send the email with the verification code
                await SendVerificationEmail(_loggedInUser.VerificationCode);

                // Switch the visibility of the panels
                LoginPanelVisibility = Visibility.Collapsed;
                VerificationPanelVisibility = Visibility.Visible;
            }
            else
            {
                ErrorMessage = "Invalid email or password.";
            }
        }

        // Method to send the verification email
        private async Task SendVerificationEmail(string code)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("NewBank", "newbank1903@gmail.com"));
            message.To.Add(new MailboxAddress("", Username)); // Use the Username property as the email recipient
            message.Subject = "Verification Code";
            message.Body = new TextPart("plain") { Text = $"Your verification code is: {code}" };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("newbank1903@gmail.com", "brdrevqrhdxkhaot");
                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }
        // Verify method, executed when the Verify button is clicked
        private void Verify(object parameter)
        {
            if (_loggedInUser != null &&
                _loggedInUser.VerificationCode == VerificationCode &&
                _loggedInUser.VerificationCodeExpiration > DateTime.UtcNow)
            {
                // Clear the verification code and its expiration
                _loggedInUser.VerificationCode = null;
                _loggedInUser.VerificationCodeExpiration = null;
                _context.SaveChanges();

                // Store user information in StoreUserViewModel
                StoreUserViewModel.Id = _loggedInUser.Id;
                StoreUserViewModel.Username = _loggedInUser.Username;
                StoreUserViewModel.Password = _loggedInUser.Password;
                StoreUserViewModel.Name = _loggedInUser.Name;
                StoreUserViewModel.LastName = _loggedInUser.LastName;
                StoreUserViewModel.ProfilePicture = _loggedInUser.ProfilePicture;

                // Create and set up the main view and view model
                MainViewModel mainViewModel = new MainViewModel();
                MainView objMainView = new MainView();
                objMainView.DataContext = mainViewModel;

                // Set the main window of the application and show the main view
                Application.Current.MainWindow = objMainView;
                objMainView.Show();

                // Close the login view
                CloseAction?.Invoke();
            }
            else
            {
                ErrorMessage = "Invalid verification code or code expired.";
            }
        }
    }
}