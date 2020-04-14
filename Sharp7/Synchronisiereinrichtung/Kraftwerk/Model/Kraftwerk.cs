using System;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using Utilities;

namespace Synchronisiereinrichtung
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

    public enum SynchronisierungAuswahl
    {
        U_f = 0,
        U_f_Phase,
        U_f_Phase_Leistung,
        U_f_Phase_Leistungsfaktor,
        Unbekannt
    }
}

namespace Synchronisiereinrichtung.kraftwerk.Model
{
    public class Kraftwerk
    {
        public readonly Drehstromgenerator generator = new Drehstromgenerator(0.35, (1 / 30.0));
        public readonly Statemachine kraftwerkStatemachine;

        public double FrequenzDifferenz { get; set; }
        public double OptimalerSpannungswert { get; set; }
        public bool Q1 { get; set; }
        public bool KraftwerkStarten { get; set; }
        public bool KraftwerkStoppen { get; set; }
        public double Ventil_Y { get; set; }
        public double Generator_Ie { get; set; }
        public double Generator_n { get; set; }
        public double Generator_f { get; set; }
        public double Generator_U { get; set; }
        public double Generator_P { get; set; }
        public double Generator_CosPhi { get; set; }
        public double SpannungsdifferenzGeneratorNetz { get; set; }
        public double Netz_U { get; set; }
        public double Netz_f { get; set; }
        public double Netz_P { get; set; }
        public double Netz_CosPhi { get; set; }
        public bool MessgeraetAnzeigen { get; set; }
        public bool MaschineTot { get; set; }
        public SynchronisierungAuswahl SynchAuswahl { get; set; }
        public double SpannungsDifferenz { get; set; }
        public double ManualVentilstellung { get; set; }
        public double ManualErregerstrom { get; set; }
        public double MessgeraetOptimalerBereich { get; set; }


        public Kraftwerk()
        {
            FrequenzDifferenz = 5;
            kraftwerkStatemachine = new Statemachine(this);
            System.Threading.Tasks.Task.Run(() => KraftwerkTask());
        }

        public void KraftwerkTask()
        {
            const double Zeitdauer = 10;//ms

            double Generator_Winkel = 0;
            double Netz_Winkel = 0;
            Punkt Generator_Momentanspannung;
            Punkt Netz_Momentanspannung;

            while (true)
            {
                kraftwerkStatemachine.Fire(Statemachine.Trigger.Aktualisieren);

                Netz_Winkel = DrehstromZeiger.WinkelBerechnen(Zeitdauer, Netz_f, Netz_Winkel);
                Generator_Winkel = DrehstromZeiger.WinkelBerechnen(Zeitdauer, Generator_f, Generator_Winkel);

                Netz_Momentanspannung = DrehstromZeiger.GetSpannung(Netz_Winkel, Netz_U);
                Generator_Momentanspannung = DrehstromZeiger.GetSpannung(Generator_Winkel, Generator_U);

                FrequenzDifferenz = Math.Abs(Netz_f - Generator_f);
                Zeiger SpannungsDiff = new Zeiger(Generator_Momentanspannung, Netz_Momentanspannung);

                switch (SynchAuswahl)
                {
                    case SynchronisierungAuswahl.U_f_Phase:
                    case SynchronisierungAuswahl.U_f_Phase_Leistung:
                    case SynchronisierungAuswahl.U_f_Phase_Leistungsfaktor:
                        SpannungsdifferenzGeneratorNetz = SpannungsDiff.Laenge();
                        break;

                    default:
                        SpannungsdifferenzGeneratorNetz = Math.Abs(Netz_U - Generator_U);
                        break;
                }

                SpannungsDifferenz = SpannungsdifferenzGeneratorNetz;

                Thread.Sleep((int)Zeitdauer);
            }
        }

        internal void Starten()
        {
            KraftwerkStarten = true;
            KraftwerkStoppen = false;
        }

        internal void Stoppen()
        {
            KraftwerkStoppen = true;
            KraftwerkStarten = false;
        }

        internal void Synchronisieren() => kraftwerkStatemachine.Fire(Statemachine.Trigger.Synchronisieren);
        internal void Reset() => kraftwerkStatemachine.Fire(Statemachine.Trigger.Reset);
    }
}