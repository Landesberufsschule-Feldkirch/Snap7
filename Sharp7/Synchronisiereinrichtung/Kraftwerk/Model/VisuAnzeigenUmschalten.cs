using System.ComponentModel;

namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    public class VisuAnzeigenUmschalten : INotifyPropertyChanged
    {
        public VisuAnzeigenUmschalten()
        {
            VisibilityMaschineTot = "Hidden";

            VisibilityVentilAus = "Visible";
            VisibilityVentilEin = "Hidden";
        }

        private double _VentilPosition;

        public string VentilPosition
        {
            get { return "Y=" + _VentilPosition + "%"; }
            set
            {
                _VentilPosition = System.Convert.ToDouble(value.Substring(2, value.Length - 3));
                OnPropertyChanged("VentilPosition");
            }
        }

        private double _Erregerstrom;

        public string Erregerstrom
        {
            get { return "IE=" + _Erregerstrom + "A"; }
            set
            {
                _Erregerstrom = System.Convert.ToDouble(value.Substring(3, value.Length - 4));
                OnPropertyChanged("Erregerstrom");
            }
        }

        private double _Drehzahl;

        public string Drehzahl
        {
            get { return "n=" + _Drehzahl + "RPM"; }
            set
            {
                _Drehzahl = System.Convert.ToDouble(value.Substring(2, value.Length - 5));
                OnPropertyChanged("Drehzahl");
            }
        }

        private double _GeneratorSpannungString;

        public string GeneratorSpannungString
        {
            get { return "U=" + _GeneratorSpannungString + "V"; }
            set
            {
                _GeneratorSpannungString = System.Convert.ToDouble(value.Substring(2, value.Length - 3));
                OnPropertyChanged("GeneratorSpannungString");
            }
        }

        private double _GeneratorFrequenzString;

        public string GeneratorFrequenzString
        {
            get { return "f=" + _GeneratorFrequenzString + "Hz"; }
            set
            {
                _GeneratorFrequenzString = System.Convert.ToDouble(value.Substring(2, value.Length - 4));
                OnPropertyChanged("GeneratorFrequenzString");
            }
        }

        private double _GeneratorLeistungString;

        public string GeneratorLeistungString
        {
            get { return "P=" + _GeneratorLeistungString + "W"; }
            set
            {
                _GeneratorLeistungString = System.Convert.ToDouble(value.Substring(2, value.Length - 3));
                OnPropertyChanged("GeneratorLeistungString");
            }
        }

        private double _GeneratorCosPhiString;

        public string GeneratorCosPhiString
        {
            get { return "cos φ=" + _GeneratorCosPhiString; }
            set
            {
                _GeneratorCosPhiString = System.Convert.ToDouble(value.Substring(6));
                OnPropertyChanged("GeneratorCosPhiString");
            }
        }

        private double _NetzSpannungString;

        public string NetzSpannungString
        {
            get { return "U=" + _NetzSpannungString + "V"; }
            set
            {
                _NetzSpannungString = System.Convert.ToDouble(value.Substring(2, value.Length - 3));
                OnPropertyChanged("NetzSpannungString");
            }
        }

        private double _NetzFrequenzString;

        public string NetzFrequenzString
        {
            get { return "f=" + _NetzFrequenzString + "Hz"; }
            set
            {
                _NetzFrequenzString = System.Convert.ToDouble(value.Substring(2, value.Length - 4));
                OnPropertyChanged("NetzFrequenzString");
            }
        }

        private double _NetzLeistungString;

        public string NetzLeistungString
        {
            get { return "P=" + _NetzLeistungString + "W"; }
            set
            {
                _NetzLeistungString = System.Convert.ToDouble(value.Substring(2, value.Length - 3));
                OnPropertyChanged("NetzLeistungString");
            }
        }

        private double _NetzCosPhiString;

        public string NetzCosPhiString
        {
            get { return "cos φ=" + _NetzCosPhiString; }
            set
            {
                _NetzCosPhiString = System.Convert.ToDouble(value.Substring(6));
                OnPropertyChanged("NetzCosPhiString");
            }
        }

        #region Messgerät

        public void MessgeraetAnzeigen(bool val)
        {
            if (val) VisibilityMessgeraetSichtbar = "Visible";
            else VisibilityMessgeraetSichtbar = "Hidden";
        }

        private string _VisibilityMessgeraetSichtbar;

        public string VisibilityMessgeraetSichtbar
        {
            get { return _VisibilityMessgeraetSichtbar; }
            set
            {
                _VisibilityMessgeraetSichtbar = value;
                OnPropertyChanged("VisibilityMessgeraetSichtbar");
            }
        }

        private double _SpannungsDifferenz;

        public double SpannungsDifferenz
        {
            get { return _SpannungsDifferenz; }
            set
            {
                _SpannungsDifferenz = value;
                OnPropertyChanged("SpannungsDifferenz");
            }
        }

        #endregion Messgerät

        #region Maschine tot

        public void MaschineTot(bool val)
        {
            if (val) VisibilityMaschineTot = "Visible"; else VisibilityMaschineTot = "Hidden";
        }

        private string _VisibilityMaschineTotAnzeigen;

        public string VisibilityMaschineTot
        {
            get { return _VisibilityMaschineTotAnzeigen; }
            set
            {
                _VisibilityMaschineTotAnzeigen = value;
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

        private string _LeistungsschalterEin;

        public string LeistungsschalterEin
        {
            get { return _LeistungsschalterEin; }
            set
            {
                _LeistungsschalterEin = value;
                OnPropertyChanged("LeistungsschalterEin");
            }
        }

        private string _LeistungsschalterAus;

        public string LeistungsschalterAus
        {
            get { return _LeistungsschalterAus; }
            set
            {
                _LeistungsschalterAus = value;
                OnPropertyChanged("LeistungsschalterAus");
            }
        }

        #endregion Leistungsschalter

        #region Ventil

        private string _VisibilityVentilAus;

        public string VisibilityVentilAus
        {
            get { return _VisibilityVentilAus; }
            set
            {
                _VisibilityVentilAus = value;
                OnPropertyChanged("VisibilityVentilAus");
            }
        }

        private string _VisibilityVentilEin;

        public string VisibilityVentilEin
        {
            get { return _VisibilityVentilEin; }
            set
            {
                _VisibilityVentilEin = value;
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

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members
    }
}