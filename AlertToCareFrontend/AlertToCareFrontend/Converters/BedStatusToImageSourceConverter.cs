using System;
using System.Globalization;
using System.Windows.Data;

namespace AlertToCareFrontend.Converters
{
    class BedStatusToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && bool.TryParse(value.ToString(), out bool isBedOccupied))
            {
                return GetImageForBedStatus(isBedOccupied);
            }
            return value;
        }

        private string GetImageForBedStatus(bool isBedOccupied)
        {
            return isBedOccupied ? "../Resources/icons/bed-occupied.png" : "../Resources/icons/bed-empty.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
