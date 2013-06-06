using discrete_machine.CyclogramElements;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace discrete_machine_app
{
    [ValueConversion(typeof(StepCondition), typeof(string))]
    class StepConditionToSymbolConverter : IValueConverter
    {
        private static Dictionary<StepCondition, string> ConditionsSymbols = new Dictionary<StepCondition, string>
        {
            { StepCondition.More, ">" },
            { StepCondition.Less,  "<" },
            { StepCondition.Equals, "=" },
            { StepCondition.NotEquals, "<>" },
            { StepCondition.LessEquals, "<=" },
            { StepCondition.MoreEquals, "=>" } 
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var condition = (StepCondition)value;
            return ConditionsSymbols[condition];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConditionsSymbols.FirstOrDefault(x => x.Value == value).Key;
        }
    }
}
