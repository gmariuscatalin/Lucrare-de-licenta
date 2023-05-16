using NewBank2.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace NewBank2.View
{
    public partial class TransactionView : UserControl
    {
        public TransactionView()
        {
            InitializeComponent();
            DataContext = new TransactionViewModel();
        }

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            // Access the ViewModel and call the RadioButtonChecked method
            if (DataContext is TransactionViewModel viewModel)
            {
                viewModel.RadioButtonChecked(null);
            }

            // Update the visibility of the borders based on which RadioButton was selected
            if (radioButton == radOtherUsers)
            {
                if (OtherUsersBorder != null && BetweenAccountsBorder != null && PayUtilityBorder != null)
                {
                    OtherUsersBorder.Visibility = Visibility.Visible;
                    BetweenAccountsBorder.Visibility = Visibility.Collapsed;
                    PayUtilityBorder.Visibility = Visibility.Collapsed;
                    PlayAnimations();
                }
            }
            else if (radioButton == radBetweenAccounts)
            {
                if (OtherUsersBorder != null && BetweenAccountsBorder != null && PayUtilityBorder != null)
                {
                    OtherUsersBorder.Visibility = Visibility.Collapsed;
                    BetweenAccountsBorder.Visibility = Visibility.Visible;
                    PayUtilityBorder.Visibility = Visibility.Collapsed;
                    PlayAnimations();
                }
            }
            else if (radioButton == radPayUtility)
            {
                if (OtherUsersBorder != null && BetweenAccountsBorder != null && PayUtilityBorder != null)
                {
                    OtherUsersBorder.Visibility = Visibility.Collapsed;
                    BetweenAccountsBorder.Visibility = Visibility.Collapsed;
                    PayUtilityBorder.Visibility = Visibility.Visible;
                    PlayAnimations();
                }
            }
        }

        // Method to play the slide-in animations for the borders
        private void PlayAnimations()
        {
            var storyboard = (Storyboard)OtherUsersBorder.FindResource("SlideInStoryboard");

            if (storyboard != null)
            {
                storyboard.Begin(OtherUsersBorder);
                storyboard.Begin(BetweenAccountsBorder);
                storyboard.Begin(PayUtilityBorder);
            }
        }

        private void OnGridLoaded(object sender, RoutedEventArgs e)
        {
            OtherUsersBorder.Visibility = Visibility.Visible;
            BetweenAccountsBorder.Visibility = Visibility.Collapsed;
            PayUtilityBorder.Visibility = Visibility.Collapsed;

            PlayAnimations();
        }

        private void Amount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            /* Cast sender to TextBox and assign it to the textBox variable.
             If the cast fails and textBox is null, mark the event as handled and return.*/
            TextBox textBox = sender as TextBox;
            if (textBox == null)
            {
                e.Handled = true;
                return;
            }

            string text = textBox.Text + e.Text;
            e.Handled = !IsValidDecimal(text);
        }

        // Method to validate if the input text is a valid decimal number
        private bool IsValidDecimal(string text)
        {
            int decimalPointCount = 0;
            int digitsAfterDecimalCount = 0;

            foreach (char c in text)
            {
                if (c == '.')
                {
                    decimalPointCount++;
                }
                else if (decimalPointCount == 1 && char.IsDigit(c))
                {
                    digitsAfterDecimalCount++;
                }
                else if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return decimalPointCount <= 1 && digitsAfterDecimalCount <= 2;
        }
    }
}
