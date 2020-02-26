using System;
using System.ComponentModel;
using System.Threading;

namespace Synchronisiereinrichtung.kraftwerk.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Synchronisiereinrichtung.kraftwerk.Model.Kraftwerk kraftwerk;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen(MainWindow mw, Synchronisiereinrichtung.kraftwerk.Model.Kraftwerk kw)
        {
            mainWindow = mw;
            kraftwerk = kw;

            SpsStatus = "-";
            SpsColor = "LightBlue";

            ManualVentilstellung = 0;
            ManualErregerstrom = 0;

            NetzSpannungSlider = 400;
            NetzFrequenzSlider = 50;
            NetzPhasenverschiebungSlider = 90; // der Einstellbereich geht von 0 ..180
            NetzLeistungSlider = 600;
            SynchAuswahl = SynchronisierungAuswahl.U_f;

            VisibilityMaschineTot = "Hidden";
            VisibilityVentilAus = "Visible";
            VisibilityVentilEin = "Hidden";

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                if (mainWindow.DebugWindowAktiv)
                {
                    kraftwerk.Ventil_Y = Y();
                    kraftwerk.Generator_Ie = Ie();
                }


                kraftwerk.Netz_f = Netz_f();
                kraftwerk.Netz_U = Netz_U();
                kraftwerk.Netz_P = Netz_P();
                kraftwerk.Netz_CosPhi = Netz_CosPhi();


                VentilEinschalten(kraftwerk.Ventil_Y > 1);
                LeistungsschalterEinschalten(kraftwerk.Q1);

                Y(kraftwerk.Ventil_Y);
                Ie(kraftwerk.Generator_Ie);

                N(kraftwerk.Generator_n);
                Generator_U(kraftwerk.Generator_U);
                Generator_f(kraftwerk.Generator_f);
                Generator_P(kraftwerk.Generator_P);
                Generator_CosPhi(kraftwerk.Generator_CosPhi);

                Netz_U(kraftwerk.Netz_U);
                Netz_f(kraftwerk.Netz_f);
                Netz_P(kraftwerk.Netz_P);
                Netz_CosPhi(kraftwerk.Netz_CosPhi);

                SpannungsDifferenz = kraftwerk.SpannungsDifferenz;

                Status(kraftwerk.kraftwerkStatemachine.StatusAusgeben());

                MessgeraetAnzeigen(kraftwerk.MessgeraetAnzeigen);

                kraftwerk.SynchAuswahl = SynchAuswahl;

                switch (SynchAuswahl)
                {
                    case SynchronisierungAuswahl.U_f:
                        if (kraftwerk.OptimalerSpannungswert != 10)
                        {
                            kraftwerk.OptimalerSpannungswert = 10;
                            kraftwerk.MessgeraetOptimalerBereich = kraftwerk.OptimalerSpannungswert;
                        }
                        break;

                    default:
                        if (kraftwerk.OptimalerSpannungswert != 100)
                        {
                            kraftwerk.OptimalerSpannungswert = 100;
                            kraftwerk.MessgeraetOptimalerBereich = kraftwerk.OptimalerSpannungswert;
                        }
                        break;
                }


                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }

        #region SPS Status und Farbe
        private string _spsStatus;
        public string SpsStatus
        {
            get { return _spsStatus; }
            set
            {
                _spsStatus = value;
                OnPropertyChanged(nameof(SpsStatus));
            }
        }

        private string _spsColor;
        public string SpsColor
        {
            get { return _spsColor; }
            set
            {
                _spsColor = value;
                OnPropertyChanged(nameof(SpsColor));
            }
        }
        #endregion

        #region Ventil

        public double Y() { return ManualVentilstellung; }

        private double _manualVentilstellung;

        public double ManualVentilstellung
        {
            get { return _manualVentilstellung; }
            set
            {
                _manualVentilstellung = value;
                OnPropertyChanged(nameof(ManualVentilstellung));
            }
        }

        #endregion

        #region Erregerstrom
        public double Ie() { return ManualErregerstrom; }

        private double _manualErregerstrom;

        public double ManualErregerstrom
        {
            get { return _manualErregerstrom; }
            set
            {
                _manualErregerstrom = value;
                OnPropertyChanged(nameof(ManualErregerstrom));
            }
        }

        #endregion

        #region Netzspannung

        public double Netz_U() { return NetzSpannungSlider; }

        private double _netzSpannungSlider;

        public double NetzSpannungSlider
        {
            get { return _netzSpannungSlider; }
            set
            {
                _netzSpannungSlider = value;
                OnPropertyChanged(nameof(NetzSpannungSlider));
            }
        }

        #endregion

        #region Netzfrequenz

        public double Netz_f() { return NetzFrequenzSlider; }

        private double _netzFrequenzSlider;

        public double NetzFrequenzSlider
        {
            get { return _netzFrequenzSlider; }
            set
            {
                _netzFrequenzSlider = value;
                OnPropertyChanged(nameof(NetzFrequenzSlider));
            }
        }

        #endregion

        #region NetzLeistungsfaktor

        public double Netz_CosPhi()
        {
            // Der Slider geht fast von 0 bis 180 ==> -90° bis 90°
            if (_netzPhasenverschiebungSlider < 90) return (-1) * Math.Cos(Math.PI * (_netzPhasenverschiebungSlider - 90) / 180);
            else return Math.Cos(Math.PI * (_netzPhasenverschiebungSlider - 90) / 180);
        }

        private double _netzPhasenverschiebungSlider;

        public double NetzPhasenverschiebungSlider
        {
            get { return _netzPhasenverschiebungSlider; }
            set
            {
                _netzPhasenverschiebungSlider = value;
                OnPropertyChanged(nameof(NetzPhasenverschiebungSlider));
            }
        }

        #endregion

        #region Netzleistung

        public double Netz_P() { return NetzLeistungSlider; }

        private double _netzLeistungSlider;

        public double NetzLeistungSlider
        {
            get { return _netzLeistungSlider; }
            set
            {
                _netzLeistungSlider = value;
                OnPropertyChanged(nameof(NetzLeistungSlider));
            }
        }

        #endregion

        #region SynchronisierungAuswahl
        private SynchronisierungAuswahl _synchAuswahl;

        public SynchronisierungAuswahl SynchAuswahl
        {
            get { return _synchAuswahl; }
            set
            {
                _synchAuswahl = value;
                OnPropertyChanged(nameof(SynchAuswahl));
            }
        }
        #endregion




        #region Leistungsfaktor

        public void Generator_CosPhi(double val)
        {
            GeneratorCosPhiString = $"cos φ={val.ToString("N2")}";
        }

        public void Netz_CosPhi(double val)
        {
            NetzCosPhiString = $"cos φ={val.ToString("N2")}";
        }

        private double _generatorCosPhiString;

        public string GeneratorCosPhiString
        {
            get { return "cos φ=" + _generatorCosPhiString; }
            set
            {
                _generatorCosPhiString = System.Convert.ToDouble(value.Substring(6));
                OnPropertyChanged(nameof(GeneratorCosPhiString));
            }
        }



        private double _netzCosPhiString;

        public string NetzCosPhiString
        {
            get { return "cos φ=" + _netzCosPhiString; }
            set
            {
                _netzCosPhiString = System.Convert.ToDouble(value.Substring(6));
                OnPropertyChanged(nameof(NetzCosPhiString));
            }
        }

        #endregion

        #region Leistung

        public void Generator_P(double val)
        {
            GeneratorLeistungString = $"P={val.ToString("N1")}W";
        }

        public void Netz_P(double val)
        {
            NetzLeistungString = $"P={val}W";
        }


        private double _generatorLeistungString;

        public string GeneratorLeistungString
        {
            get { return "P=" + _generatorLeistungString + "W"; }
            set
            {
                _generatorLeistungString = System.Convert.ToDouble(value.Substring(2, value.Length - 3));
                OnPropertyChanged(nameof(GeneratorLeistungString));
            }
        }

        private double _netzLeistungString;

        public string NetzLeistungString
        {
            get { return "P=" + _netzLeistungString + "W"; }
            set
            {
                _netzLeistungString = System.Convert.ToDouble(value.Substring(2, value.Length - 3));
                OnPropertyChanged(nameof(NetzLeistungString));
            }
        }

        #endregion

        #region Freqenz
        public void Generator_f(double val)
        {
            GeneratorFrequenzString = $"f={val.ToString("N2")}Hz";
        }

        public void Netz_f(double val)
        {
            NetzFrequenzString = $"f={val}Hz";
        }

        private double _generatorFrequenzString;

        public string GeneratorFrequenzString
        {
            get { return "f=" + _generatorFrequenzString + "Hz"; }
            set
            {
                _generatorFrequenzString = System.Convert.ToDouble(value.Substring(2, value.Length - 4));
                OnPropertyChanged(nameof(GeneratorFrequenzString));
            }
        }
        private double _netzFrequenzString;

        public string NetzFrequenzString
        {
            get { return "f=" + _netzFrequenzString + "Hz"; }
            set
            {
                _netzFrequenzString = System.Convert.ToDouble(value.Substring(2, value.Length - 4));
                OnPropertyChanged(nameof(NetzFrequenzString));
            }
        }
        #endregion

        #region Spannung

        public void Generator_U(double val)
        {
            GeneratorSpannungString = $"U={val.ToString("N1")}V";
        }

        public void Netz_U(double val)
        {
            NetzSpannungString = $"U={val}V";
        }


        private double _generatorSpannungString;

        public string GeneratorSpannungString
        {
            get { return "U=" + _generatorSpannungString + "V"; }
            set
            {
                _generatorSpannungString = System.Convert.ToDouble(value.Substring(2, value.Length - 3));
                OnPropertyChanged(nameof(GeneratorSpannungString));
            }
        }

        private double _netzSpannungString;

        public string NetzSpannungString
        {
            get { return "U=" + _netzSpannungString + "V"; }
            set
            {
                _netzSpannungString = System.Convert.ToDouble(value.Substring(2, value.Length - 3));
                OnPropertyChanged(nameof(NetzSpannungString));
            }
        }

        #endregion

        #region IE

        public void Ie(double val)
        {
            Erregerstrom = $"IE={ val.ToString("N1")}A";
        }

        private double _erregerstrom;

        public string Erregerstrom
        {
            get { return "IE=" + _erregerstrom + "A"; }
            set
            {
                _erregerstrom = System.Convert.ToDouble(value.Substring(3, value.Length - 4));
                OnPropertyChanged(nameof(Erregerstrom));
            }
        }

        #endregion

        #region n

        public void N(double val)
        {
            Drehzahl = $"n={val.ToString("N1")}RPM";
        }

        private double _drehzahl;
        public string Drehzahl
        {
            get { return "n=" + _drehzahl + "RPM"; }
            set
            {
                _drehzahl = System.Convert.ToDouble(value.Substring(2, value.Length - 5));
                OnPropertyChanged(nameof(Drehzahl));
            }
        }

        #endregion

        #region Messgerät
        //"{Binding Kraftwerk.ViAnzeige.MessgeraetOptimalerBereich}"

        private double _messgeraetOptimalerBereich;
        public double MessgeraetOptimalerBereich
        {
            get { return _messgeraetOptimalerBereich; }
            set
            {
                _messgeraetOptimalerBereich = value;
                OnPropertyChanged(nameof(MessgeraetOptimalerBereich));
            }
        }
        public void MessgeraetAnzeigen(bool val)
        {
            if (val) VisibilityMessgeraetSichtbar = "Visible";
            else VisibilityMessgeraetSichtbar = "Hidden";
        }

        private string _visibilityMessgeraetSichtbar;

        public string VisibilityMessgeraetSichtbar
        {
            get { return _visibilityMessgeraetSichtbar; }
            set
            {
                _visibilityMessgeraetSichtbar = value;
                OnPropertyChanged(nameof(VisibilityMessgeraetSichtbar));
            }
        }

        private double _spannungsDifferenz;

        public double SpannungsDifferenz
        {
            get { return _spannungsDifferenz; }
            set
            {
                _spannungsDifferenz = value;
                OnPropertyChanged(nameof(SpannungsDifferenz));
            }
        }

        #endregion Messgerät

        #region Maschine tot

        public void MaschineTot(bool val)
        {
            if (val) VisibilityMaschineTot = "Visible"; else VisibilityMaschineTot = "Hidden";
        }

        public bool IstMaschineTot()
        {
            if (VisibilityMaschineTot == "Visible") return true; else return false;
        }

        private string _visibilityMaschineTotAnzeigen;

        public string VisibilityMaschineTot
        {
            get { return _visibilityMaschineTotAnzeigen; }
            set
            {
                _visibilityMaschineTotAnzeigen = value;
                OnPropertyChanged(nameof(VisibilityMaschineTot));
            }
        }

        #endregion Maschine tot

        #region Leistungsschalter

        public void LeistungsschalterEinschalten(bool val)
        {
            if (val)
            {
                LeistungsschalterEin = "Visible";
                LeistungsschalterAus = "Hidden";
            }
            else
            {
                LeistungsschalterEin = "Hidden";
                LeistungsschalterAus = "Visible";
            }
        }

        private string _leistungsschalterEin;

        public string LeistungsschalterEin
        {
            get { return _leistungsschalterEin; }
            set
            {
                _leistungsschalterEin = value;
                OnPropertyChanged(nameof(LeistungsschalterEin));
            }
        }

        private string _leistungsschalterAus;

        public string LeistungsschalterAus
        {
            get { return _leistungsschalterAus; }
            set
            {
                _leistungsschalterAus = value;
                OnPropertyChanged(nameof(LeistungsschalterAus));
            }
        }

        #endregion Leistungsschalter

        #region Ventil


        private double _ventilPosition;

        public string VentilPosition
        {
            get { return "Y=" + _ventilPosition + "%"; }
            set
            {
                _ventilPosition = System.Convert.ToDouble(value.Substring(2, value.Length - 3));
                OnPropertyChanged(nameof(VentilPosition));
            }
        }

        public void Y(double val)
        {
            VentilPosition = $"Y={ val.ToString("N1")}%";
        }

        private string _visibilityVentilAus;

        public string VisibilityVentilAus
        {
            get { return _visibilityVentilAus; }
            set
            {
                _visibilityVentilAus = value;
                OnPropertyChanged(nameof(VisibilityVentilAus));
            }
        }

        private string _visibilityVentilEin;

        public string VisibilityVentilEin
        {
            get { return _visibilityVentilEin; }
            set
            {
                _visibilityVentilEin = value;
                OnPropertyChanged(nameof(VisibilityVentilEin));
            }
        }

        public void VentilEinschalten(bool val)
        {
            if (val)
            {
                VisibilityVentilEin = "Visible";
                VisibilityVentilAus = "Hidden";
            }
            else
            {
                VisibilityVentilEin = "Hidden";
                VisibilityVentilAus = "Visible";
            }
        }

        #endregion Ventil

        #region Kraftwerk Status

        public void Status(string val)
        {
            KraftwerkStatus = "Status: " + val;
        }

        private string _kraftwerkStatus;

        public string KraftwerkStatus
        {
            get { return _kraftwerkStatus; }
            set
            {
                _kraftwerkStatus = value;
                OnPropertyChanged(nameof(KraftwerkStatus));
            }
        }

        #endregion

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members
    }
}