using System;
using System.Windows;
using System.Windows.Data;

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
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using System.Windows.Data;
    using Utilities;

    public class Kraftwerk : INotifyPropertyChanged
    {




        private static bool BinErster = false;
        public bool MaschineTot;

        #region Sollwerte zur Bedienung


        public double Spannung;
        public double Frequenz;
        public double Leistung;
        public double Leistungsfaktor;

        #endregion

        #region Werte für die SPS

        public short Gen_Ie;

        public short Gen_n;
        public short Gen_U;
        public short Gen_f;
        public short Gen_P;
        public short Gen_cosPhi;

        public short Netz_U;
        public short Netz_f;
        public short Netz_P;
        public short Netz_cosPhi;

        public short UDiff;
        public short ph;

        #endregion

        Drehstromgenerator generator = new Drehstromgenerator(1 / 2, 1 / 30);

        #region Variablen für MVVM

        public bool Q1;

        public bool KraftwerkStarten;
        public bool KraftwerkStoppen;


        private double _NetzSpannung = 1234;
        public double NetzSpannung
        {
            get { return _NetzSpannung; }
            set
            {
                _NetzSpannung = value;
                OnPropertyChanged("NetzSpannung");
            }
        }


               

        private double _NetzFrequenz = 1234;
        public double NetzFrequenz
        {
            get { return _NetzFrequenz; }
            set
            {
                _NetzFrequenz = value;
                OnPropertyChanged("NetzFrequenz");
            }
        }

        

        private double _NetzCosPhi = 1234;
        public double NetzCosPhi
        {
            get { return _NetzCosPhi; }
            set
            {
                _NetzCosPhi = value;
                OnPropertyChanged("NetzCosPhi");
            }
        }


        private double _NetzLeistung = 1234;
        public double NetzLeistung
        {
            get { return _NetzLeistung; }
            set
            {
                _NetzLeistung = value;
                OnPropertyChanged("NetzLeistung");
            }
        }




        private double _VentilPosition;
        public string VentilPosition
        {
            get
            {
                return "Y=" + _VentilPosition + "%";
            }
            set
            {
                _VentilPosition = System.Convert.ToDouble(value.Substring(2, value.Length - 3));
                OnPropertyChanged("VentilPosition");
            }
        }



        private SynchronisierungAuswahl _SynchAuswahl = SynchronisierungAuswahl.U_f;
        public SynchronisierungAuswahl SynchAuswahl
        {

            get
            {
                return _SynchAuswahl;
            }

            set
            {
                _SynchAuswahl = value;
                OnPropertyChanged("SynchAuswahl");
            }

        }





        public double Generator_n;
        public double Generator_f;
        public double Generator_U;
        public double Generator_P;
        public double Generator_cosPhi;
        public double Generator_Winkel;

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

        public Punkt Generator_Momentanspannung;

        public double Netz_Winkel;
        public Punkt Netz_Momentanspannung;


        public double SpannungsUnterschiedSynchronisieren;
        public double FrequenzDifferenz;
        public double Phasenlage;




        #endregion

        public Kraftwerk()
        {
            // leerer Konstruktor
            MaschineTot = false;

            if (!BinErster)
            {
                BinErster = true;
                System.Threading.Tasks.Task.Run(() => KraftwerkTask());

                VentilPosition = "Y=10%";
            }

        }

        public void KraftwerkTask()
        {


            const double Zeitdauer = 10;//ms

            while (true)
            {
                // _VentilPosition += 0.01; nur zum testen

                VentilPosition = $"Y={_VentilPosition}%";

                generator.MaschineAntreiben(_VentilPosition);
                Generator_n = generator.Drehzahl();
                Generator_U = generator.Spannung(S7Analog.S7_Analog_2_Double(Gen_Ie, 10));
                Generator_f = generator.Frequenz();

                Netz_Winkel = DrehstromZeiger.WinkelBerechnen(Zeitdauer, Frequenz, Netz_Winkel);
                Generator_Winkel = DrehstromZeiger.WinkelBerechnen(Zeitdauer, Generator_f, Generator_Winkel);

                Netz_Momentanspannung = DrehstromZeiger.GetSpannung(Netz_Winkel, Spannung);
                Generator_Momentanspannung = DrehstromZeiger.GetSpannung(Generator_Winkel, Generator_U);

                FrequenzDifferenz = Frequenz - Generator_f;
                Zeiger SpannungsDiff = new Zeiger(Generator_Momentanspannung, Netz_Momentanspannung);

                SpannungsUnterschiedSynchronisieren = SpannungsDiff.Laenge();

                WertebereicheUmrechnen();



                Thread.Sleep((int)Zeitdauer);
            }

        }

        public void WertebereicheUmrechnen()
        {
            // Sollwerte --> SPS
            Netz_U = S7Analog.S7_Analog_2_Short(Spannung, 1000);
            Netz_f = S7Analog.S7_Analog_2_Short(Frequenz, 100);
            Netz_P = S7Analog.S7_Analog_2_Short(Leistung, 1000);
            Netz_cosPhi = S7Analog.S7_Analog_2_Short(Leistungsfaktor, 1);

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
            var SpannungDifferenz = Spannung - Generator_U;

            switch (SynchAuswahl)
            {
                case SynchronisierungAuswahl.U_f:
                    if (FrequenzDifferenz > 2) MaschineTot = true;
                    if (SpannungDifferenz > 25) MaschineTot = true;
                    break;

                case SynchronisierungAuswahl.U_f_Phase:
                    if (FrequenzDifferenz > 1) MaschineTot = true;
                    if (SpannungDifferenz > 10) MaschineTot = true;
                    break;

                case SynchronisierungAuswahl.U_f_Phase_Leistung:
                    if (FrequenzDifferenz > 0.9) MaschineTot = true;
                    if (SpannungDifferenz > 10) MaschineTot = true;
                    break;

                case SynchronisierungAuswahl.U_f_Phase_Leistungsfaktor:
                    if (FrequenzDifferenz > 0.8) MaschineTot = true;
                    if (SpannungDifferenz > 10) MaschineTot = true;
                    break;

                default:

                    break;
            }


        }

        internal void Reset()
        {
            MaschineTot = false;
            Q1 = false;
            Generator_n = 0;

        }




        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion



    }
}
