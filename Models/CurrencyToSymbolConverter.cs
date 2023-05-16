using System;
using System.Globalization;
using System.Windows.Data;

namespace NewBank2.Models
{
    /* This class is responsible for converting currency strings in the ViewModel
     to their corresponding symbols for display in the View.*/
    public class CurrencyToSymbolConverter : IValueConverter
    {
        /* Convert method takes a value (currency string in this case), targetType,
         parameter, and culture information to convert the currency string into
         its corresponding symbol.*/
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if the provided value is a string.
            if (value is string currency)
            {
                /* Use a switch statement to convert the currency string to the corresponding symbol.
                 The string is first converted to uppercase to ensure case-insensitivity.*/
                switch (currency.ToUpper())
                {
                    case "RON":
                        return "lei";
                    case "USD":
                        return "$";
                    case "EUR":
                        return "€";
                    default:
                        // If the currency string doesn't match any of the cases, return it as-is.
                        return currency;
                }
            }

            // If the value is not a string, return an empty string.
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
