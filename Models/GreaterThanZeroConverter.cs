using System;
using System.Globalization;
using System.Windows.Data;

namespace NewBank2.Models
{
    /* GreaterThanZeroConverter is a value converter that checks if a decimal value
     is greater than zero. It implements the IValueConverter interface to be used
     in data binding scenarios.*/
    public class GreaterThanZeroConverter : IValueConverter
    {
        /* The Convert method takes a value and checks if it's a decimal and greater than zero.
         Returns true if the value is greater than zero, otherwise returns false.*/
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal decimalValue)
            {
                return decimalValue > 0;
            }

            return false;
        }

        /* The ConvertBack method is not implemented in this converter,
         as it's not required for the current use case.*/
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
