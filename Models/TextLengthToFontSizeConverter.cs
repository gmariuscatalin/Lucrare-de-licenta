using System;
using System.Globalization;
using System.Windows.Data;

namespace NewBank2.Models
{
    /* TextLengthToFontSizeConverter is a value converter that adjusts the font size
     based on the length of a decimal value. It implements the IValueConverter interface
     to be used in data binding.*/
    public class TextLengthToFontSizeConverter : IValueConverter
    {
        /* The Convert method takes a value, checks if it's a decimal, and adjusts
         the font size based on the length of the decimal value.*/
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is decimal))
            {
                return 40;
            }

            decimal balance = (decimal)value;
            int balanceLength = balance.ToString("F2").Length;

            // Set the font size based on the length of the decimal value
            if (balanceLength <= 10)
            {
                return 40;
            }
            else
            {
                return 30;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}