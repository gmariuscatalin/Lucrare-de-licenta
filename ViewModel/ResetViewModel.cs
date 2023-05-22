using MimeKit;
using NewBank2.Commands;
using NewBank2.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NewBank2.ViewModel
{
    public class ResetViewModel : BaseViewModel
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
        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        public ICommand ResetCommand { get; }

        public ResetViewModel()
        {
            ResetCommand = new RelayCommand(ResetPassword, CanResetPassword);
        }

        public bool CanResetPassword(object parameter)
        {
            return !string.IsNullOrEmpty(Username) && new EmailAddressAttribute().IsValid(Username);
        }

        public async void ResetPassword(object parameter)
        {
            // Check if the email exists and is in the correct email format
            if (!string.IsNullOrEmpty(Username) && new EmailAddressAttribute().IsValid(Username))
            {
                // Create an instance of LoginContext
                using (var loginContext = new LoginContext())
                {
                    // Retrieve user by email from the LoginContext
                    var user = loginContext.GetUserByEmail(Username);

                    if (user != null)
                    {
                        // Generate a new password with at least 8 characters (numbers and letters)
                        string newPassword = GenerateNewPassword();

                        // Update user's password in the LoginContext
                        user.Password = newPassword;
                        loginContext.SaveChanges();

                        // Send the new password to the user's email
                        SendPasswordToEmail(Username, newPassword);

                        ErrorMessage="Password reset successful. Please check your email for the new password.";
                    }
                    else
                    {
                        ErrorMessage="Email not found. Please enter a valid email address.";
                    }
                }
            }
        }

        private string GenerateNewPassword()
        {
            // Generate a random password with at least 8 characters (numbers and letters)
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            string newPassword = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return newPassword;
        }

        private void SendPasswordToEmail(string email, string password)
        {
            // Send the new password to the user's email using Google's SMTP settings and MailKit library
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("NewBank", "newbank1903@gmail.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Password Reset";
            message.Body = new TextPart("plain") { Text = $"Your new password is: {password}" };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate();
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
