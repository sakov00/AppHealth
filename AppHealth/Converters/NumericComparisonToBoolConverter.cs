using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace AppHealth.Converters
{
    [ValueConversion(typeof(object), typeof(bool))]
    public class NumericComparisonToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var firstValue = (values[0] != null && values[0] != DependencyProperty.UnsetValue)
                ? System.Convert.ToDouble(values[0]) : double.NaN;

            var secondValue = (values[1] != null && values[1] != DependencyProperty.UnsetValue)
                ? System.Convert.ToDouble(values[1]) : double.NaN;

            var percent = (parameter != null && parameter != DependencyProperty.UnsetValue)
                ? System.Convert.ToDouble(parameter) : double.NaN;

            if (firstValue != double.NaN && secondValue != double.NaN && percent != double.NaN)
            {
                if (firstValue * percent < secondValue)
                    return Brushes.Green;
                else
                    return Brushes.White;
            }
            else
                return Brushes.White;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
