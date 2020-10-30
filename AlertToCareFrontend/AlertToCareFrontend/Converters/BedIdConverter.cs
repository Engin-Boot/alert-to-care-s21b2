using System;
using System.Globalization;
using System.Windows.Data;

namespace AlertToCareFrontend.Converters
{
    class BedIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string bedId = value.ToString();
                return bedId.Replace(Properties.Settings.Default.currentIcuId, "");
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
