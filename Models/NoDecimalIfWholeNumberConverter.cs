using System;
using System.Globalization;
using System.Windows.Data;

namespace NewBank2.Models
{
    /* This class is responsible for converting decimal values in the ViewModel
     to a formatted string for display in the View.*/
    public class NoDecimalIfWholeNumberConverter : IValueConverter
    {
        /* Convert method takes a value (decimal value in this case), targetType,
         parameter, and culture information to convert the decimal value into
         a formatted string.*/
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if the provided value is a decimal.
            if (value is decimal decimalValue)
            {
                string formattedBalance;

                // Check if the decimal value is a whole number (no decimal part).
                if (decimalValue % 1 == 0)
                {
                    /* If the value is a whole number, convert it to an integer
                     and format it using the "N0" (number with no decimal places) format string.*/
                    formattedBalance = decimal.ToInt32(decimalValue).ToString("N0", culture);
                }
                else
                {
                    /* If the value has a decimal part, format it using the "N2"
                     (number with 2 decimal places) format string.*/
                    formattedBalance = decimalValue.ToString("N2", culture);
                }

                // Replace the culture's default group separator (e.g., "," in English) with a space.
                return formattedBalance.Replace(culture.NumberFormat.NumberGroupSeparator, " ");
            }
            // If the value is not a decimal, return it as-is.
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
