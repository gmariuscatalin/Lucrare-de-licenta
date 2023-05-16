using NewBank2.ViewModel;
using System;
using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace NewBank2.CustomControls
{
    /// <summary>
    /// Interaction logic for BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        // Define the Password DependencyProperty for BindablePasswordBox
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(SecureString), typeof(BindablePasswordBox));

        // Property to get/set the password value
        public SecureString Password
        {
            get { return (SecureString)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Constructor for the BindablePasswordBox
        public BindablePasswordBox()
        {
            InitializeComponent();
            // Add event handler for the PasswordChanged event
            textPassword.PasswordChanged += OnPasswordChanged;
            // Add event handler for the DataContextChanged event
            DataContextChanged += OnDataContextChanged;
        }

        // Event handler for DataContextChanged
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Unsubscribe from the ClearPasswordEvent of the old ViewModel
            if (e.OldValue is RegisterViewModel oldViewModel)
            {
                oldViewModel.ClearPasswordEvent -= ClearPassword;
            }

            // Subscribe to the ClearPasswordEvent of the new ViewModel
            if (e.NewValue is RegisterViewModel newViewModel)
            {
                newViewModel.ClearPasswordEvent += ClearPassword;
            }
        }

        // Event handler for the PasswordChanged event
        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            // Update the Password property when the password is changed
            Password = textPassword.SecurePassword;
        }

        // Event handler for the ClearPassword event
        private void ClearPassword(object sender, EventArgs e)
        {
            // Clear the PasswordBox when the ClearPassword event is raised
            textPassword.Password = string.Empty;
        }
    }
}
