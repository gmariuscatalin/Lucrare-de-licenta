using System.Diagnostics;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Diagnostics;

namespace NewBank2.View
{
    public partial class ContactView : UserControl
    {
        public ContactView()
        {
            InitializeComponent();
        }

        /* This method handles the Click event of the 'Send Email' button.
         It opens the default email client with a pre-filled recipient address.*/
        private void btnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            string recipient = "newbank1903@gmail.com";
            string mailto = $"mailto:{recipient}";

            try
            {
                // Start a new process to open the default email client with the pre-filled recipient address.
                Process.Start(new ProcessStartInfo(mailto) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                // Show an error message if there is any issue opening the email client.
                MessageBox.Show("An error occurred while opening the email client.\n" + ex.Message);
            }
        }
    }
}
