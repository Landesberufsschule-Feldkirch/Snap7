using System;
using System.Windows;
using System.Windows.Data;

namespace Synchronisiereinrichtung;

public class EnumBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        var parameterString = parameter.ToString();
        if (parameterString == null) return DependencyProperty.UnsetValue;
        if (!Enum.IsDefined(value.GetType(), value)) return DependencyProperty.UnsetValue;

        var parameterValue = Enum.Parse(value.GetType(), parameterString);

        return parameterValue.Equals(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        var parameterString = parameter.ToString();
        return parameterString == null ? DependencyProperty.UnsetValue : Enum.Parse(targetType, parameterString);
    }
}