﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace AlertToCareFrontend.Converters
{
    class AlertMessageToStopAlertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !string.IsNullOrEmpty(value.ToString());       
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

