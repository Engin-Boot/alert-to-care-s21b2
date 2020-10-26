using System;
using System.Globalization;
using System.Windows.Data;

namespace AlertToCareFrontend.Converters
{
    class BedStatusToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (bool.TryParse(value.ToString(), out bool valueFromSource))
            {
                return valueFromSource ? "../Resources/icons/bed-occupied.png" : "../Resources/icons/bed-empty.png";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
