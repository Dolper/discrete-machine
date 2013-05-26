using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace discrete_machine_app
{
    [ValueConversion(typeof(Point), typeof(Thickness))]
    class PointToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var point = (Point)value;

            var result = new Thickness(point.X, point.Y, 0, 0);
            return result;
        }
  
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("This method should never be called");
        }
    }
}
