using Hydraulik;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Data;

namespace Heizungsregler.Model
{
    public class EnumBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter == null) return null;

            var parameterString = parameter.ToString();
            if (parameterString == null) return DependencyProperty.UnsetValue;
            if (value != null && !Enum.IsDefined(value.GetType(), value)) return DependencyProperty.UnsetValue;

            var parameterValue = Enum.Parse(value.GetType(), parameterString);

            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter == null) return null;

            var parameterString = parameter.ToString();
            return parameterString == null ? DependencyProperty.UnsetValue : Enum.Parse(targetType, parameterString);
        }
    }

    public enum Betriebsarten
    {
        Aus = 0,
        Tag,
        Nacht,
        Hand
    }





    public class WohnHaus
    {
        public bool BrennerEin { get; set; }
        public bool HeizungsPumpe { get; set; }
        public DreiwegeVentil DreiwegeVentil { get; set; }
        public double WitterungsTemperatur { get; set; }
        public double KesselTemperatur { get; set; }
        public double VorlaufSolltemperatur { get; set; }

        public Betriebsarten Betriebsart { get; set; }


        private readonly Heizkessel _heizkessel;
        private readonly Raumheizung _raumheizung;

        private const double BrennerleistungMax = 100.0;
        private const double BrennerleistungMin = 0.0;

        public WohnHaus()
        {
            _heizkessel = new Heizkessel();
            _raumheizung = new Raumheizung();

            DreiwegeVentil = new DreiwegeVentil(0, 100, 10);

            System.Threading.Tasks.Task.Run(HeizungsreglerTask);
        }

        private void HeizungsreglerTask()
        {
            while (true)
            {
                _heizkessel.SetBrenner(BrennerEin ? BrennerleistungMax : BrennerleistungMin);
                KesselTemperatur = _heizkessel.GetKesselTemperatur();

                _raumheizung.SetBetriebsart(Betriebsart);
                _raumheizung.SetWitterungsTemperatur(WitterungsTemperatur);
                _raumheizung.RaumheizungAktualisieren();
                VorlaufSolltemperatur = _raumheizung.GetVorlaufSolltemperatur();


                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void Reset()
        {
            //
        }
    }
}