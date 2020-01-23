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
            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            if (Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;

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

namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    public class Kraftwerk
    {
        private double Spannung;
        private double Frequenz;
        private double Leistung;
        private double Leistungsfaktor;

        private short Gen_Ie;

        private short Gen_n;
        private short Gen_U;
        private short Gen_f;
        private short Gen_P;
        private short Gen_cosPhi;

        private short Netz_U;
        private short Netz_f;
        private short Netz_P;
        private short Netz_cosPhi;

        private short UDiff;
        private short ph;

        private readonly Drehstromgenerator generator = new Drehstromgenerator(0.35, (1 / 30.0));

        #region Variablen für MVVM

        private bool Q1;

        private bool KraftwerkStarten;
        private bool KraftwerkStoppen;

        public double Generator_n { get; set; }
        public double Generator_f { get; set; }
        public double Generator_U { get; set; }
        private double Generator_P;
        private double Generator_cosPhi;

        private double SpannungsUnterschiedSynchronisieren;
        private double FrequenzDifferenz;
        private double Phasenlage;

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

        #endregion Variablen für MVVM

        public VisuSollwerte ViSoll { get; set; }
        public VisuAnzeigenUmschalten ViAnzeige { get; set; }

        public Kraftwerk()
        {
            ViSoll = new VisuSollwerte();
            ViAnzeige = new VisuAnzeigenUmschalten();

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
                AnzeigeUmschalten();

                generator.MaschineAntreiben(ViSoll.ManualVentilstellung);
                Generator_n = generator.Drehzahl();
                Generator_U = generator.Spannung(ViSoll.ManualErregerstrom);
                Generator_f = generator.Frequenz();

                Netz_Winkel = DrehstromZeiger.WinkelBerechnen(Zeitdauer, ViSoll.NetzFrequenzSlider, Netz_Winkel);
                Generator_Winkel = DrehstromZeiger.WinkelBerechnen(Zeitdauer, Generator_f, Generator_Winkel);

                Netz_Momentanspannung = DrehstromZeiger.GetSpannung(Netz_Winkel, ViSoll.NetzSpannungSlider);
                Generator_Momentanspannung = DrehstromZeiger.GetSpannung(Generator_Winkel, Generator_U);

                FrequenzDifferenz = Math.Abs(ViSoll.NetzFrequenzSlider - Generator_f);
                Zeiger SpannungsDiff = new Zeiger(Generator_Momentanspannung, Netz_Momentanspannung);

                SpannungsUnterschiedSynchronisieren = SpannungsDiff.Laenge();

                ViAnzeige.SpannungsDifferenz = SpannungsDiff.Laenge();

                WertebereicheUmrechnen();

                Thread.Sleep((int)Zeitdauer);
            }
        }

        private void AnzeigeUmschalten()
        {
            ViAnzeige.VentilEinschalten(ViSoll.ManualVentilstellung > 1);
            ViAnzeige.LeistungsschalterEinschalten(Q1);
            ViAnzeige.MessgeraetAnzeigen(Math.Abs(FrequenzDifferenz) < 2);

            // Sollwerte von den Slidern übernehmen
            ViAnzeige.VentilPosition = $"Y={ ViSoll.ManualVentilstellung.ToString("N1")}%";
            ViAnzeige.Erregerstrom = $"IE={ ViSoll.ManualErregerstrom.ToString("N1")}A";
            ViAnzeige.Drehzahl = $"n={Generator_n.ToString("N1")}RPM";

            ViAnzeige.NetzSpannungString = $"U={ ViSoll.NetzSpannungSlider}V";
            ViAnzeige.NetzFrequenzString = $"f={ ViSoll.NetzFrequenzSlider}Hz";
            ViAnzeige.NetzLeistungString = $"P={ ViSoll.NetzLeistungSlider}W";
            ViAnzeige.NetzCosPhiString = $"cos φ={ ViSoll.NetzCosPhiSlider}";

            ViAnzeige.GeneratorSpannungString = $"U={Generator_U.ToString("N1")}V";
            ViAnzeige.GeneratorFrequenzString = $"f={Generator_f.ToString("N2")}Hz";
            ViAnzeige.GeneratorLeistungString = $"P={Generator_P.ToString("N1")}W";
            ViAnzeige.GeneratorCosPhiString = $"cos φ={Generator_cosPhi.ToString("N1")}";
        }

        private void WertebereicheUmrechnen()
        {
            // Sollwerte --> SPS
            Netz_U = S7Analog.S7_Analog_2_Short(ViSoll.NetzSpannungSlider, 1000);
            Netz_f = S7Analog.S7_Analog_2_Short(ViSoll.NetzFrequenzSlider, 100);
            Netz_P = S7Analog.S7_Analog_2_Short(ViSoll.NetzLeistungSlider, 1000);
            Netz_cosPhi = S7Analog.S7_Analog_2_Short(ViSoll.NetzCosPhiSlider, 1);

            // Modell --> SPS
            Gen_n = S7Analog.S7_Analog_2_Short(Generator_n, 5000);
            Gen_U = S7Analog.S7_Analog_2_Short(Generator_U, 1000);
            Gen_f = S7Analog.S7_Analog_2_Short(Generator_f, 100);
            Gen_P = S7Analog.S7_Analog_2_Short(Generator_P, 1000);
            UDiff = S7Analog.S7_Analog_2_Short(SpannungsUnterschiedSynchronisieren, 1000);
            ph = S7Analog.S7_Analog_2_Short(Phasenlage, 1);
        }

        internal void Synchronisieren()
        {
            var SpannungDifferenz = Math.Abs(Spannung - Generator_U);
            Q1 = true;

            switch (ViSoll.SynchAuswahl)
            {
                case SynchronisierungAuswahl.U_f:
                    if (FrequenzDifferenz > 2) ViAnzeige.MaschineTot(true);
                    if (SpannungDifferenz > 25) ViAnzeige.MaschineTot(true);
                    break;

                case SynchronisierungAuswahl.U_f_Phase:
                    if (FrequenzDifferenz > 1) ViAnzeige.MaschineTot(true);
                    if (SpannungDifferenz > 10) ViAnzeige.MaschineTot(true);
                    break;

                case SynchronisierungAuswahl.U_f_Phase_Leistung:
                    if (FrequenzDifferenz > 0.9) ViAnzeige.MaschineTot(true);
                    if (SpannungDifferenz > 10) ViAnzeige.MaschineTot(true);
                    break;

                case SynchronisierungAuswahl.U_f_Phase_Leistungsfaktor:
                    if (FrequenzDifferenz > 0.8) ViAnzeige.MaschineTot(true);
                    if (SpannungDifferenz > 10) ViAnzeige.MaschineTot(true);
                    break;

                default:
                    break;
            }
        }

        internal void Reset()
        {
            ViAnzeige.MaschineTot(false);
            Q1 = false;
            Generator_n = 0;
        }
    }
}