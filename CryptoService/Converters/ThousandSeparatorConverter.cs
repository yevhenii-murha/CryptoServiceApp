using System;
using System.Globalization;
using System.Windows.Data;

namespace CryptoService.Converters
{
    public class ThousandSeparatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value is decimal || value is double || value is float)
            {
                return string.Format(CultureInfo.InvariantCulture, "{0:N2}", value).Replace(",", " ");
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
