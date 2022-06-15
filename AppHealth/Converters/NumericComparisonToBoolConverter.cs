using AppHealth.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace AppHealth.Converters
{
    [ValueConversion(typeof(object), typeof(bool))]
    public class NumericComparisonToBoolConverter : MarkupExtension, IValueConverter
    {
        public double ComparisonWithPercent { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var firstValue = value as ResultUser;
            if (firstValue.AverageSteps * ComparisonWithPercent < firstValue.BestResult)
                return true;
            else 
                return false;
            }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
