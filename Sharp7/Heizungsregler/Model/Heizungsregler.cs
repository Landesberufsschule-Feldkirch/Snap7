using System;
using System.Windows;
using System.Windows.Data;

namespace Heizungsregler.Model
{

    public class EnumBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter.ToString();
            if (parameterString == null) return DependencyProperty.UnsetValue;
            if (!Enum.IsDefined(value.GetType(), value)) return DependencyProperty.UnsetValue;

            object parameterValue = Enum.Parse(value.GetType(), parameterString);

            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter.ToString();
            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            return Enum.Parse(targetType, parameterString);
        }
    }

    public enum Betriebsarten
    {
        Aus = 0,
        Tag,
        Nacht,
        Hand
    }


    public class Heizungsregler
    {






        public Betriebsarten Betriebsar { get; set; }

        public Heizungsregler()
        {

        }

        internal void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
