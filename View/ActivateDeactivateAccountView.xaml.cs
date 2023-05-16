using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NewBank2.ViewModel;

namespace NewBank2.View
{
    public partial class ActivateDeactivateAccountView : UserControl
    {
        public ActivateDeactivateAccountView()
        {
            InitializeComponent();
            DataContext = new ActivateDeactivateAccountViewModel();
        }
        // The following 3 event handlers uncheck the other radio buttons when the RON/Euro/Dollar checkbox is checked.
        private void OnRonChecked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.IsChecked.HasValue && radioButton.IsChecked.Value)
            {
                // Uncheck the other radio buttons
                euroRadioButton.IsChecked = false;
                dollarRadioButton.IsChecked = false;
            }
        }

        private void OnEuroChecked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.IsChecked.HasValue && radioButton.IsChecked.Value)
            {
                // Uncheck the other radio buttons
                ronRadioButton.IsChecked = false;
                dollarRadioButton.IsChecked = false;
            }
        }

        private void OnDollarChecked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.IsChecked.HasValue && radioButton.IsChecked.Value)
            {
                // Uncheck the other radio buttons
                ronRadioButton.IsChecked = false;
                euroRadioButton.IsChecked = false;
            }
        }
        // This method finds the parent frame of the given child element in the visual tree.
        private Frame FindParentFrame(DependencyObject child)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            if (parent == null) return null;
            if (parent is Frame frame) return frame;
            return FindParentFrame(parent);
        }
        private void btnActivateDeactivateAccount_Click(object sender, RoutedEventArgs e)
        {

        }
        // This event handler navigates back to the SettingsView when the Back button is clicked.
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var frame = FindParentFrame(this);
            frame.NavigationService.Navigate(new SettingsView());
        }
    }
}
