using NewBank2.Models;
using NewBank2.ViewModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

namespace NewBank2.View
{
    public partial class BankAccountControl : UserControl
    {
        public BankAccountControl()
        {
            InitializeComponent();
        }
        /* This constructor accepts an Account object as a parameter and sets the
         control's DataContext to a new BankAccountControlViewModel with the given account.*/
        public BankAccountControl(Account account) : this()
        {
            DataContext = new BankAccountControlViewModel(account);
        }
        // This event handler is called whenever the text in the search box changes.
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            /* Check if the search box text is null or empty.
             If it is, set the visibility of the search label to Visible.
             Otherwise, set the visibility of the search label to Collapsed.*/
            SearchLabel.Visibility = string.IsNullOrEmpty(SearchTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
