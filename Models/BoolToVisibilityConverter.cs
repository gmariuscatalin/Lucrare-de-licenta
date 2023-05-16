using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NewBank2.Models
{
    // This converter class is used to convert a boolean value to a Visibility enumeration value.
    public class BoolToVisibilityConverter : IValueConverter
    {
        /* This method converts a boolean value to a Visibility value. If the input value is true, it returns Visibility.Visible,
         otherwise, it returns Visibility.Collapsed.*/
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}