using System;
using System.Windows.Data;

namespace discrete_machine_app
{
    [ValueConversion(typeof(int), typeof(int))]
    class IntToBitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var digit = parameter as int?;
            var val = value as int?;
            if (val.HasValue && digit.HasValue)
                return (val.Value & digit.Value) > 0 ? 1 : 0;
            else
                return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
