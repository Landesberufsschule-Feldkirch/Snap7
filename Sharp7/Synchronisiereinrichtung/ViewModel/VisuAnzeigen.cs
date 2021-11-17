using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace Synchronisiereinrichtung.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Kraftwerk.Model.Kraftwerk _kraftwerk;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Kraftwerk.Model.Kraftwerk kw)
        {
            _mainWindow = mw;
            _kraftwerk = kw;

            SpsSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            ManualVentilstellung = 0;
            ManualErregerstrom = 0;

            NetzSpannungSlider = 400;
            NetzFrequenzSlider = 50;
            NetzPhasenverschiebungSlider = 90; // der Einstellbereich geht von 0 ..180
            NetzLeistungSlider = 600;
            SynchAuswahl = SynchronisierungAuswahl.Uf;

            VisibilityMaschineTot = Visibility.Hidden;
            VisibilityVentilAus = Visibility.Visible;
            VisibilityVentilEin = Visibility.Hidden;

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                if (_mainWindow.DebugWindowAktiv)
                {
                    _kraftwerk.VentilY = _kraftwerk.Generator.VentilRampe.GetWert(ManualY());
                    _kraftwerk.GeneratorIe = _kraftwerk.Generator.ErregerstromRampe.GetWert(ManualIe());
                }

                _kraftwerk.NetzF = SliderNetz_f();
                _kraftwerk.NetzU = SliderNetz_U();
                _kraftwerk.NetzP = SliderNetz_P();
                _kraftwerk.NetzCosPhi = SliderNetz_CosPhi();

                VentilEinschalten(_kraftwerk.VentilY > 1);
                LeistungsschalterEinschalten(_kraftwerk.Q1);

                Y(_kraftwerk.VentilY);
                Ie(_kraftwerk.GeneratorIe);

                N(_kraftwerk.GeneratorN);
                Generator_U(_kraftwerk.GeneratorU);
                Generator_f(_kraftwerk.GeneratorF);
                Generator_P(_kraftwerk.GeneratorP);
                Generator_CosPhi(_kraftwerk.GeneratorCosPhi);

                Netz_U(_kraftwerk.NetzU);
                Netz_f(_kraftwerk.NetzF);
                Netz_P(_kraftwerk.NetzP);
                Netz_CosPhi(_kraftwerk.NetzCosPhi);

                SpannungsDifferenz = _kraftwerk.SpannungsDifferenz;

                Status(_kraftwerk.KraftwerkStatemachine.StatusAusgeben());

                MessgeraetAnzeigen(_kraftwerk.MessgeraetAnzeigen);

                VisibilityMaschineTot = _kraftwerk.MaschineTot ? Visibility.Visible : Visibility.Hidden;



                _kraftwerk.SynchAuswahl = SynchAuswahl;

                switch (SynchAuswahl)
                {
                    case SynchronisierungAuswahl.Uf:
                        if (Math.Abs(_kraftwerk.OptimalerSpannungswert - 10) > 0.001)
                        {
                            _kraftwerk.OptimalerSpannungswert = 10;
                            _kraftwerk.MessgeraetOptimalerBereich = _kraftwerk.OptimalerSpannungswert;
                        }
                        break;

                    case SynchronisierungAuswahl.UfPhase:
                        break;
                    case SynchronisierungAuswahl.UfPhaseLeistung:
                        break;
                    case SynchronisierungAuswahl.UfPhaseLeistungsfaktor:
                        break;
                    default:
                        if (Math.Abs(_kraftwerk.OptimalerSpannungswert - 100) > 0.001)
                        {
                            _kraftwerk.OptimalerSpannungswert = 100;
                            _kraftwerk.MessgeraetOptimalerBereich = _kraftwerk.OptimalerSpannungswert;
                        }
                        break;
                }

                if (_mainWindow.PlcDaemon != null && _mainWindow.PlcDaemon.Plc != null)
                {
                    SpsVersionLokal = _mainWindow.VersionInfoLokal;
                    SpsVersionEntfernt = _mainWindow.PlcDaemon.Plc.GetVersion();
                    SpsSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    SpsColor = _mainWindow.PlcDaemon.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.PlcDaemon.Plc?.GetSpsStatus();

                    FensterTitel = _mainWindow.PlcDaemon.Plc.GetPlcBezeichnung() + ": " + _mainWindow.VersionInfoLokal;
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        #region SPS Version, Status und Farbe
        private string fensterTitel;
        public string FensterTitel
        {
            get => fensterTitel;
            set
            {
                fensterTitel = value;
                OnPropertyChanged(nameof(FensterTitel));
            }
        }
        private string _spsVersionLokal;
        public string SpsVersionLokal
        {
            get => _spsVersionLokal;
            set
            {
                _spsVersionLokal = value;
                OnPropertyChanged(nameof(SpsVersionLokal));
            }
        }

        private string _spsVersionEntfernt;
        public string SpsVersionEntfernt
        {
            get => _spsVersionEntfernt;
            set
            {
                _spsVersionEntfernt = value;
                OnPropertyChanged(nameof(SpsVersionEntfernt));
            }
        }

        private Visibility _spsSichtbar;
        public Visibility SpsSichtbar
        {
            get => _spsSichtbar;
            set
            {
                _spsSichtbar = value;
                OnPropertyChanged(nameof(SpsSichtbar));
            }
        }

        private string _spsStatus;
        public string SpsStatus
        {
            get => _spsStatus;
            set
            {
                _spsStatus = value;
                OnPropertyChanged(nameof(SpsStatus));
            }
        }

        private Brush _spsColor;
        public Brush SpsColor
        {
            get => _spsColor;
            set
            {
                _spsColor = value;
                OnPropertyChanged(nameof(SpsColor));
            }
        }

        #endregion SPS Versionsinfo, Status und Farbe

        #region Ventil

        public double ManualY() => ManualVentilstellung;

        private double _manualVentilstellung;
        public double ManualVentilstellung
        {
            get => _manualVentilstellung;
            set
            {
                _manualVentilstellung = value;
                OnPropertyChanged(nameof(ManualVentilstellung));
            }
        }

        #endregion Ventil

        #region Erregerstrom

        public double ManualIe() => ManualErregerstrom;

        private double _manualErregerstrom;
        public double ManualErregerstrom
        {
            get => _manualErregerstrom;
            set
            {
                _manualErregerstrom = value;
                OnPropertyChanged(nameof(ManualErregerstrom));
            }
        }

        #endregion Erregerstrom

        #region Netzspannung

        public double SliderNetz_U() => NetzSpannungSlider;

        private double _netzSpannungSlider;
        public double NetzSpannungSlider
        {
            get => _netzSpannungSlider;
            set
            {
                _netzSpannungSlider = value;
                OnPropertyChanged(nameof(NetzSpannungSlider));
            }
        }

        #endregion Netzspannung

        #region Netzfrequenz

        public double SliderNetz_f() => NetzFrequenzSlider;

        private double _netzFrequenzSlider;
        public double NetzFrequenzSlider
        {
            get => _netzFrequenzSlider;
            set
            {
                _netzFrequenzSlider = value;
                OnPropertyChanged(nameof(NetzFrequenzSlider));
            }
        }

        #endregion Netzfrequenz

        #region NetzLeistungsfaktor

        public double SliderNetz_CosPhi()
        {
            // Der Slider geht fast von 0 bis 180 ==> -90° bis 90°
            if (_netzPhasenverschiebungSlider < 90) return -1 * Math.Cos(Math.PI * (_netzPhasenverschiebungSlider - 90) / 180);
            return Math.Cos(Math.PI * (_netzPhasenverschiebungSlider - 90) / 180);
        }

        private double _netzPhasenverschiebungSlider;
        public double NetzPhasenverschiebungSlider
        {
            get => _netzPhasenverschiebungSlider;
            set
            {
                _netzPhasenverschiebungSlider = value;
                OnPropertyChanged(nameof(NetzPhasenverschiebungSlider));
            }
        }

        #endregion NetzLeistungsfaktor

        #region Netzleistung

        public double SliderNetz_P() => NetzLeistungSlider;

        private double _netzLeistungSlider;
        public double NetzLeistungSlider
        {
            get => _netzLeistungSlider;
            set
            {
                _netzLeistungSlider = value;
                OnPropertyChanged(nameof(NetzLeistungSlider));
            }
        }

        #endregion Netzleistung

        #region SynchronisierungAuswahl

        private SynchronisierungAuswahl _synchAuswahl;
        public SynchronisierungAuswahl SynchAuswahl
        {
            get => _synchAuswahl;
            set
            {
                _synchAuswahl = value;
                OnPropertyChanged(nameof(SynchAuswahl));
            }
        }

        #endregion SynchronisierungAuswahl

        #region Leistungsfaktor

        public void Generator_CosPhi(double val) => GeneratorCosPhiString = $"cos φ={val:N2}";

        public void Netz_CosPhi(double val) => NetzCosPhiString = $"cos φ={val:N2}";

        private double _generatorCosPhiString;
        public string GeneratorCosPhiString
        {
            get => "cos φ=" + _generatorCosPhiString;
            set
            {
                _generatorCosPhiString = Convert.ToDouble(value[6..]);
                OnPropertyChanged(nameof(GeneratorCosPhiString));
            }
        }

        private double _netzCosPhiString;
        public string NetzCosPhiString
        {
            get => "cos φ=" + _netzCosPhiString;
            set
            {
                _netzCosPhiString = Convert.ToDouble(value[6..]);
                OnPropertyChanged(nameof(NetzCosPhiString));
            }
        }

        #endregion Leistungsfaktor

        #region Leistung

        public void Generator_P(double val) => GeneratorLeistungString = $"P={val:N1}W";

        public void Netz_P(double val) => NetzLeistungString = $"P={val}W";

        private double _generatorLeistungString;
        public string GeneratorLeistungString
        {
            get => "P=" + _generatorLeistungString + "W";
            set
            {
                _generatorLeistungString = Convert.ToDouble(value[2..^1]);
                OnPropertyChanged(nameof(GeneratorLeistungString));
            }
        }

        private double _netzLeistungString;
        public string NetzLeistungString
        {
            get => "P=" + _netzLeistungString + "W";
            set
            {
                _netzLeistungString = Convert.ToDouble(value[2..^1]);
                OnPropertyChanged(nameof(NetzLeistungString));
            }
        }

        #endregion Leistung

        #region Freqenz

        public void Generator_f(double val) => GeneratorFrequenzString = $"f={val:N2}Hz";

        public void Netz_f(double val) => NetzFrequenzString = $"f={val}Hz";

        private double _generatorFrequenzString;
        public string GeneratorFrequenzString
        {
            get => "f=" + _generatorFrequenzString + "Hz";
            set
            {
                _generatorFrequenzString = Convert.ToDouble(value[2..^2]);
                OnPropertyChanged(nameof(GeneratorFrequenzString));
            }
        }

        private double _netzFrequenzString;
        public string NetzFrequenzString
        {
            get => "f=" + _netzFrequenzString + "Hz";
            set
            {
                _netzFrequenzString = Convert.ToDouble(value[2..^2]);
                OnPropertyChanged(nameof(NetzFrequenzString));
            }
        }

        #endregion Freqenz

        #region Spannung

        public void Generator_U(double val) => GeneratorSpannungString = $"U={val:N1}V";

        public void Netz_U(double val) => NetzSpannungString = $"U={val}V";

        private double _generatorSpannungString;
        public string GeneratorSpannungString
        {
            get => "U=" + _generatorSpannungString + "V";
            set
            {
                _generatorSpannungString = Convert.ToDouble(value[2..^1]);
                OnPropertyChanged(nameof(GeneratorSpannungString));
            }
        }

        private double _netzSpannungString;
        public string NetzSpannungString
        {
            get => "U=" + _netzSpannungString + "V";
            set
            {
                _netzSpannungString = Convert.ToDouble(value[2..^1]);
                OnPropertyChanged(nameof(NetzSpannungString));
            }
        }

        #endregion Spannung

        #region IE

        public void Ie(double val) => Erregerstrom = $"IE={ val:N1}A";

        private double _erregerstrom;
        public string Erregerstrom
        {
            get => "IE=" + _erregerstrom + "A";
            set
            {
                _erregerstrom = Convert.ToDouble(value[3..^1]);
                OnPropertyChanged(nameof(Erregerstrom));
            }
        }

        #endregion IE

        #region n

        public void N(double val) => Drehzahl = $"n={val:N1}RPM";

        private double _drehzahl;
        public string Drehzahl
        {
            get => "n=" + _drehzahl + "RPM";
            set
            {
                _drehzahl = Convert.ToDouble(value[2..^3]);
                OnPropertyChanged(nameof(Drehzahl));
            }
        }

        #endregion n

        #region Messgerät

        //"{Binding Kraftwerk.ViAnz.MessgeraetOptimalerBereich}"

        private double _messgeraetOptimalerBereich;
        public double MessgeraetOptimalerBereich
        {
            get => _messgeraetOptimalerBereich;
            set
            {
                _messgeraetOptimalerBereich = value;
                OnPropertyChanged(nameof(MessgeraetOptimalerBereich));
            }
        }

        public void MessgeraetAnzeigen(bool val) => VisibilityMessgeraetSichtbar = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityMessgeraetSichtbar;
        public Visibility VisibilityMessgeraetSichtbar
        {
            get => _visibilityMessgeraetSichtbar;
            set
            {
                _visibilityMessgeraetSichtbar = value;
                OnPropertyChanged(nameof(VisibilityMessgeraetSichtbar));
            }
        }

        private double _spannungsDifferenz;
        public double SpannungsDifferenz
        {
            get => _spannungsDifferenz;
            set
            {
                _spannungsDifferenz = value;
                OnPropertyChanged(nameof(SpannungsDifferenz));
            }
        }

        #endregion Messgerät

        #region Maschine tot

        private Visibility _visibilityMaschineTotAnzeigen;
        public Visibility VisibilityMaschineTot
        {
            get => _visibilityMaschineTotAnzeigen;
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
                LeistungsschalterEin = Visibility.Visible;
                LeistungsschalterAus = Visibility.Hidden;
            }
            else
            {
                LeistungsschalterEin = Visibility.Hidden;
                LeistungsschalterAus = Visibility.Visible;
            }
        }

        private Visibility _leistungsschalterEin;
        public Visibility LeistungsschalterEin
        {
            get => _leistungsschalterEin;
            set
            {
                _leistungsschalterEin = value;
                OnPropertyChanged(nameof(LeistungsschalterEin));
            }
        }

        private Visibility _leistungsschalterAus;
        public Visibility LeistungsschalterAus
        {
            get => _leistungsschalterAus;
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
            get => "Y=" + _ventilPosition + "%";
            set
            {
                _ventilPosition = Convert.ToDouble(value[2..^1]);
                OnPropertyChanged(nameof(VentilPosition));
            }
        }

        public void Y(double val) => VentilPosition = $"Y={ val:N1}%";

        private Visibility _visibilityVentilAus;
        public Visibility VisibilityVentilAus
        {
            get => _visibilityVentilAus;
            set
            {
                _visibilityVentilAus = value;
                OnPropertyChanged(nameof(VisibilityVentilAus));
            }
        }

        private Visibility _visibilityVentilEin;
        public Visibility VisibilityVentilEin
        {
            get => _visibilityVentilEin;
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
                VisibilityVentilEin = Visibility.Visible;
                VisibilityVentilAus = Visibility.Hidden;
            }
            else
            {
                VisibilityVentilEin = Visibility.Hidden;
                VisibilityVentilAus = Visibility.Visible;
            }
        }

        #endregion Ventil

        #region Kraftwerk Status

        public void Status(string val) => KraftwerkStatus = "Status Kraftwerk: " + val;

        private string _kraftwerkStatus;
        public string KraftwerkStatus
        {
            get => _kraftwerkStatus;
            set
            {
                _kraftwerkStatus = value;
                OnPropertyChanged(nameof(KraftwerkStatus));
            }
        }
        #endregion Kraftwerk Status

        #region iNotifyPeropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion iNotifyPeropertyChanged Members
    }
}