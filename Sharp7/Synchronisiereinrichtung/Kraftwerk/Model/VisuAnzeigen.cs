using System.ComponentModel;

namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        public VisuAnzeigen()
        {
            SpsStatus = "-";
            SpsColor = "LightBlue";

            VisibilityMaschineTot = "Hidden";
            VisibilityVentilAus = "Visible";
            VisibilityVentilEin = "Hidden";
        }

#region SPS Status und Farbe
        private string _spsStatus;
        public string SpsStatus
        {
            get { return _spsStatus; }
            set
            {
                _spsStatus = value;
                OnPropertyChanged("SpsStatus");
            }
        }

        private string _spsColor;
        public string SpsColor
        {
            get { return _spsColor; }
            set
            {
                _spsColor = value;
                OnPropertyChanged("SpsColor");
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
                OnPropertyChanged("GeneratorCosPhiString");
            }
        }



        private double _netzCosPhiString;

        public string NetzCosPhiString
        {
            get { return "cos φ=" + _netzCosPhiString; }
            set
            {
                _netzCosPhiString = System.Convert.ToDouble(value.Substring(6));
                OnPropertyChanged("NetzCosPhiString");
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
                OnPropertyChanged("GeneratorLeistungString");
            }
        }

        private double _netzLeistungString;

        public string NetzLeistungString
        {
            get { return "P=" + _netzLeistungString + "W"; }
            set
            {
                _netzLeistungString = System.Convert.ToDouble(value.Substring(2, value.Length - 3));
                OnPropertyChanged("NetzLeistungString");
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
                OnPropertyChanged("GeneratorFrequenzString");
            }
        }
        private double _netzFrequenzString;

        public string NetzFrequenzString
        {
            get { return "f=" + _netzFrequenzString + "Hz"; }
            set
            {
                _netzFrequenzString = System.Convert.ToDouble(value.Substring(2, value.Length - 4));
                OnPropertyChanged("NetzFrequenzString");
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
                OnPropertyChanged("GeneratorSpannungString");
            }
        }

        private double _netzSpannungString;

        public string NetzSpannungString
        {
            get { return "U=" + _netzSpannungString + "V"; }
            set
            {
                _netzSpannungString = System.Convert.ToDouble(value.Substring(2, value.Length - 3));
                OnPropertyChanged("NetzSpannungString");
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
                OnPropertyChanged("Erregerstrom");
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
                OnPropertyChanged("Drehzahl");
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
                OnPropertyChanged("MessgeraetOptimalerBereich");
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
                OnPropertyChanged("VisibilityMessgeraetSichtbar");
            }
        }

        private double _spannungsDifferenz;

        public double SpannungsDifferenz
        {
            get { return _spannungsDifferenz; }
            set
            {
                _spannungsDifferenz = value;
                OnPropertyChanged("SpannungsDifferenz");
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
                OnPropertyChanged("VisibilityMaschineTot");
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
                OnPropertyChanged("LeistungsschalterEin");
            }
        }

        private string _leistungsschalterAus;

        public string LeistungsschalterAus
        {
            get { return _leistungsschalterAus; }
            set
            {
                _leistungsschalterAus = value;
                OnPropertyChanged("LeistungsschalterAus");
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
                OnPropertyChanged("VentilPosition");
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
                OnPropertyChanged("VisibilityVentilAus");
            }
        }

        private string _visibilityVentilEin;

        public string VisibilityVentilEin
        {
            get { return _visibilityVentilEin; }
            set
            {
                _visibilityVentilEin = value;
                OnPropertyChanged("VisibilityVentilEin");
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
                OnPropertyChanged("KraftwerkStatus");
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