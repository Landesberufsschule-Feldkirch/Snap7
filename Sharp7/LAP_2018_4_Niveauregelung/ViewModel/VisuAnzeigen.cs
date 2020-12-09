using System.Windows.Controls;
using System.Windows.Media;

namespace LAP_2018_4_Niveauregelung.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private const double HoeheFuellBalken = 315;

        private readonly MainWindow _mainWindow;
        private readonly Model.NiveauRegelung _niveauRegelung;

        public VisuAnzeigen(MainWindow mw, Model.NiveauRegelung nr)
        {
            _mainWindow = mw;
            _niveauRegelung = nr;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Colors.LightBlue;

            ClickModeBtnS1 = ClickMode.Press;
            ClickModeBtnS2 = ClickMode.Press;
            ClickModeBtnS3 = ClickMode.Press;

            ColorThermorelaisF1 = Colors.LawnGreen;
            ColorThermorelaisF2 = Colors.LawnGreen;

            ColorCircleP1 = Colors.White;
            ColorCircleP2 = Colors.White;
            ColorCircleP2 = Colors.White;

            ColorAbleitungOben = Colors.LightBlue;
            ColorAbleitungUnten = Colors.LightBlue;
            ColorZuleitungLinksWaagrecht = Colors.LightBlue;
            ColorZuleitungLinksSenkrecht = Colors.LightBlue;
            ColorZuleitungRechtsWaagrecht = Colors.LightBlue;
            ColorZuleitungRechtsSenkrecht = Colors.LightBlue;

            VisibilityB1Ein = Visibility.Visible;
            VisibilityB2Ein = Visibility.Visible;
            VisibilityB3Ein = Visibility.Visible;
            VisibilityQ1Ein = Visibility.Visible;
            VisibilityQ2Ein = Visibility.Visible;
            VisibilityVentilEin = Visibility.Visible;

            VisibilityB1Aus =  Visibility.Hidden;
            VisibilityB2Aus =  Visibility.Hidden;
            VisibilityB3Aus =  Visibility.Hidden;
            VisibilityQ1Aus =  Visibility.Hidden;
            VisibilityQ2Aus =  Visibility.Hidden;
            VisibilityVentilAus =  Visibility.Hidden;

            Margin1 = new Thickness(0, 30, 0, 0);
            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeTherorelais_F1(!_niveauRegelung.F1);
                FarbeTherorelais_F2(!_niveauRegelung.F2);
                FarbeCircle_P1(_niveauRegelung.P1);
                FarbeCircle_P2(_niveauRegelung.P2);
                FarbeCircle_P3(_niveauRegelung.P3);

                FarbeZuleitungLinksWaagrecht(_niveauRegelung.Q1);
                FarbeZuleitungLinksSenkrecht(_niveauRegelung.Q1);
                FarbeZuleitungRechtsWaagrecht(_niveauRegelung.Q2);
                FarbeZuleitungRechtsSenkrecht(_niveauRegelung.Q2);
                FarbeAbleitungOben(_niveauRegelung.Pegel > 0.01);
                FarbeAbleitungUnten(_niveauRegelung.Y1 && _niveauRegelung.Pegel > 0.01);

                VisibilitySensorB1(_niveauRegelung.B1);
                VisibilitySensorB2(_niveauRegelung.B2);
                VisibilitySensorB3(_niveauRegelung.B3);
                VisibilityMotorQ1(_niveauRegelung.Q1);
                VisibilityMotorQ2(_niveauRegelung.Q2);
                VisibilityVentilY1(_niveauRegelung.Y1);

                Margin_1(_niveauRegelung.Pegel);

                if (_mainWindow.Plc != null)
                {
                    if (_mainWindow.Plc.GetPlcModus() == "S7-1200")
                    {
                        VersionNr = _mainWindow.VersionNummer;
                        SpsVersionLokal = _mainWindow.VersionInfoLokal;
                        SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                        SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    }

                    SpsColor = _mainWindow.Plc.GetSpsError() ? Colors.Red : Colors.LightGray;
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void TasterS1() => _niveauRegelung.S1 = ClickModeButtonS1();

        internal void TasterS2() => _niveauRegelung.S2 = !ClickModeButtonS2();

        internal void TasterS3() => _niveauRegelung.S3 = ClickModeButtonS3();

        #region SPS Version, Status und Farbe

        private string _versionNr;
        public string VersionNr
        {
            get => _versionNr;
            set
            {
                _versionNr = value;
                OnPropertyChanged(nameof(VersionNr));
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

        private Visibility _spsVersionsInfoSichtbar;
        public Visibility SpsVersionsInfoSichtbar
        {
            get => _spsVersionsInfoSichtbar;
            set
            {
                _spsVersionsInfoSichtbar = value;
                OnPropertyChanged(nameof(SpsVersionsInfoSichtbar));
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

        private Color _spsColor;

        public Color SpsColor
        {
            get => _spsColor;
            set
            {
                _spsColor = value;
                OnPropertyChanged(nameof(SpsColor));
            }
        }

        #endregion SPS Versionsinfo, Status und Farbe

        #region ClickModeBtnS1

        public bool ClickModeButtonS1()
        {
            if (ClickModeBtnS1 ==  ClickMode.Press)
            {
                ClickModeBtnS1 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS1 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS1;

        public ClickMode ClickModeBtnS1
        {
            get => _clickModeBtnS1;
            set
            {
                _clickModeBtnS1 = value;
                OnPropertyChanged(nameof(ClickModeBtnS1));
            }
        }

        #endregion ClickModeBtnS1

        #region ClickModeBtnS2

        public bool ClickModeButtonS2()
        {
            if (ClickModeBtnS2 ==  ClickMode.Press)
            {
                ClickModeBtnS2 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS2 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS2;

        public ClickMode ClickModeBtnS2
        {
            get => _clickModeBtnS2;
            set
            {
                _clickModeBtnS2 = value;
                OnPropertyChanged(nameof(ClickModeBtnS2));
            }
        }

        #endregion ClickModeBtnS2

        #region ClickModeBtnS3

        public bool ClickModeButtonS3()
        {
            if (ClickModeBtnS3 ==  ClickMode.Press)
            {
                ClickModeBtnS3 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS3 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS3;

        public ClickMode ClickModeBtnS3
        {
            get => _clickModeBtnS3;
            set
            {
                _clickModeBtnS3 = value;
                OnPropertyChanged(nameof(ClickModeBtnS3));
            }
        }

        #endregion ClickModeBtnS3

        #region Color Thermorelais F1

        public void FarbeTherorelais_F1(bool val) => ColorThermorelaisF1 = val ? Colors.Red : Colors.LawnGreen;

        private Color _colorThermorelaisF1;

        public Color ColorThermorelaisF1
        {
            get => _colorThermorelaisF1;
            set
            {
                _colorThermorelaisF1 = value;
                OnPropertyChanged(nameof(ColorThermorelaisF1));
            }
        }

        #endregion Color Thermorelais F1

        #region Color Thermorelais F2

        public void FarbeTherorelais_F2(bool val) => ColorThermorelaisF2 = val ? Colors.Red : Colors.LawnGreen;

        private Color _colorThermorelaisF2;

        public Color ColorThermorelaisF2
        {
            get => _colorThermorelaisF2;
            set
            {
                _colorThermorelaisF2 = value;
                OnPropertyChanged(nameof(ColorThermorelaisF2));
            }
        }

        #endregion Color Thermorelais F2

        #region Color P1

        public void FarbeCircle_P1(bool val) => ColorCircleP1 = val ? Colors.Green : Colors.White;

        private Color _colorCircleP1;

        public Color ColorCircleP1
        {
            get => _colorCircleP1;
            set
            {
                _colorCircleP1 = value;
                OnPropertyChanged(nameof(ColorCircleP1));
            }
        }

        #endregion Color P1

        #region Color P2

        public void FarbeCircle_P2(bool val) => ColorCircleP2 = val ? Colors.Red : Colors.White;

        private Color _colorCircleP2;

        public Color ColorCircleP2
        {
            get => _colorCircleP2;
            set
            {
                _colorCircleP2 = value;
                OnPropertyChanged(nameof(ColorCircleP2));
            }
        }

        #endregion Color P2

        #region Color P3

        public void FarbeCircle_P3(bool val) => ColorCircleP3 = val ? Colors.OrangeRed : Colors.White;

        private Color _colorCircleP3;

        public Color ColorCircleP3
        {
            get => _colorCircleP3;
            set
            {
                _colorCircleP3 = value;
                OnPropertyChanged(nameof(ColorCircleP3));
            }
        }

        #endregion Color P3

        #region Color AbleitungOben

        public void FarbeAbleitungOben(bool val) => ColorAbleitungOben = val ? Colors.Blue : Colors.LightBlue;

        private Color _colorAbleitungOben;

        public Color ColorAbleitungOben
        {
            get => _colorAbleitungOben;
            set
            {
                _colorAbleitungOben = value;
                OnPropertyChanged(nameof(ColorAbleitungOben));
            }
        }

        #endregion Color AbleitungOben

        #region Color AbleitungUnten

        public void FarbeAbleitungUnten(bool val) => ColorAbleitungUnten = val ? Colors.Blue : Colors.LightBlue;

        private Color _colorAbleitungUnten;

        public Color ColorAbleitungUnten
        {
            get => _colorAbleitungUnten;
            set
            {
                _colorAbleitungUnten = value;
                OnPropertyChanged(nameof(ColorAbleitungUnten));
            }
        }

        #endregion Color AbleitungUnten

        #region Color ZuleitungLinksWaagrecht

        public void FarbeZuleitungLinksWaagrecht(bool val) => ColorZuleitungLinksWaagrecht = val ? Colors.Blue : Colors.LightBlue;

        private Color _colorZuleitungLinksWaagrecht;

        public Color ColorZuleitungLinksWaagrecht
        {
            get => _colorZuleitungLinksWaagrecht;
            set
            {
                _colorZuleitungLinksWaagrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungLinksWaagrecht));
            }
        }

        #endregion Color ZuleitungLinksWaagrecht

        #region Color ZuleitungLinksSenkrecht

        public void FarbeZuleitungLinksSenkrecht(bool val) => ColorZuleitungLinksSenkrecht = val ? Colors.Blue : Colors.LightBlue;

        private Color _colorZuleitungLinksSenkrecht;

        public Color ColorZuleitungLinksSenkrecht
        {
            get => _colorZuleitungLinksSenkrecht;
            set
            {
                _colorZuleitungLinksSenkrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungLinksSenkrecht));
            }
        }

        #endregion Color ZuleitungLinksSenkrecht

        #region Color ZuleitungRechtsWaagrecht

        public void FarbeZuleitungRechtsWaagrecht(bool val) => ColorZuleitungRechtsWaagrecht = val ? Colors.Blue : Colors.LightBlue;

        private Color _colorZuleitungRechtsWaagrecht;

        public Color ColorZuleitungRechtsWaagrecht
        {
            get => _colorZuleitungRechtsWaagrecht;
            set
            {
                _colorZuleitungRechtsWaagrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungRechtsWaagrecht));
            }
        }

        #endregion Color ZuleitungRechtsWaagrecht

        #region Color ZuleitungRechtsSenkrecht

        public void FarbeZuleitungRechtsSenkrecht(bool val) => ColorZuleitungRechtsSenkrecht = val ? Colors.Blue : Colors.LightBlue;

        private Color _colorZuleitungRechtsSenkrecht;

        public Color ColorZuleitungRechtsSenkrecht
        {
            get => _colorZuleitungRechtsSenkrecht;
            set
            {
                _colorZuleitungRechtsSenkrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungRechtsSenkrecht));
            }
        }

        #endregion Color ZuleitungRechtsSenkrecht

        #region Visibility Sensor B1

        public void VisibilitySensorB1(bool val)
        {
            if (val)
            {
                VisibilityB1Ein = Visibility.Visible;
                VisibilityB1Aus =  Visibility.Hidden;
            }
            else
            {
                VisibilityB1Ein =  Visibility.Hidden;
                VisibilityB1Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityB1Ein;

        public Visibility VisibilityB1Ein
        {
            get => _visibilityB1Ein;
            set
            {
                _visibilityB1Ein = value;
                OnPropertyChanged(nameof(VisibilityB1Ein));
            }
        }

        private Visibility _visibilityB1Aus;

        public Visibility VisibilityB1Aus
        {
            get => _visibilityB1Aus;
            set
            {
                _visibilityB1Aus = value;
                OnPropertyChanged(nameof(VisibilityB1Aus));
            }
        }

        #endregion Visibility Sensor B1

        #region Visibility Sensor B2

        public void VisibilitySensorB2(bool val)
        {
            if (val)
            {
                VisibilityB2Ein = Visibility.Visible;
                VisibilityB2Aus =  Visibility.Hidden;
            }
            else
            {
                VisibilityB2Ein =  Visibility.Hidden;
                VisibilityB2Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityB2Ein;

        public Visibility VisibilityB2Ein
        {
            get => _visibilityB2Ein;
            set
            {
                _visibilityB2Ein = value;
                OnPropertyChanged(nameof(VisibilityB2Ein));
            }
        }

        private Visibility _visibilityB2Aus;

        public Visibility VisibilityB2Aus
        {
            get => _visibilityB2Aus;
            set
            {
                _visibilityB2Aus = value;
                OnPropertyChanged(nameof(VisibilityB2Aus));
            }
        }

        #endregion Visibility Sensor B2

        #region Visibility Sensor B3

        public void VisibilitySensorB3(bool val)
        {
            if (val)
            {
                VisibilityB3Ein = Visibility.Visible;
                VisibilityB3Aus =  Visibility.Hidden;
            }
            else
            {
                VisibilityB3Ein =  Visibility.Hidden;
                VisibilityB3Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityB3Ein;

        public Visibility VisibilityB3Ein
        {
            get => _visibilityB3Ein;
            set
            {
                _visibilityB3Ein = value;
                OnPropertyChanged(nameof(VisibilityB3Ein));
            }
        }

        private Visibility _visibilityB3Aus;

        public Visibility VisibilityB3Aus
        {
            get => _visibilityB3Aus;
            set
            {
                _visibilityB3Aus = value;
                OnPropertyChanged(nameof(VisibilityB3Aus));
            }
        }

        #endregion Visibility Sensor B3

        #region Visibility Motor Q1

        public void VisibilityMotorQ1(bool val)
        {
            if (val)
            {
                VisibilityQ1Ein = Visibility.Visible;
                VisibilityQ1Aus =  Visibility.Hidden;
            }
            else
            {
                VisibilityQ1Ein =  Visibility.Hidden;
                VisibilityQ1Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityQ1Ein;

        public Visibility VisibilityQ1Ein
        {
            get => _visibilityQ1Ein;
            set
            {
                _visibilityQ1Ein = value;
                OnPropertyChanged(nameof(VisibilityQ1Ein));
            }
        }

        private Visibility _visibilityQ1Aus;

        public Visibility VisibilityQ1Aus
        {
            get => _visibilityQ1Aus;
            set
            {
                _visibilityQ1Aus = value;
                OnPropertyChanged(nameof(VisibilityQ1Aus));
            }
        }

        #endregion Visibility Motor Q1

        #region Visibility Motor Q2

        public void VisibilityMotorQ2(bool val)
        {
            if (val)
            {
                VisibilityQ2Ein = Visibility.Visible;
                VisibilityQ2Aus =  Visibility.Hidden;
            }
            else
            {
                VisibilityQ2Ein =  Visibility.Hidden;
                VisibilityQ2Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityQ2Ein;

        public Visibility VisibilityQ2Ein
        {
            get => _visibilityQ2Ein;
            set
            {
                _visibilityQ2Ein = value;
                OnPropertyChanged(nameof(VisibilityQ2Ein));
            }
        }

        private Visibility _visibilityQ2Aus;

        public Visibility VisibilityQ2Aus
        {
            get => _visibilityQ2Aus;
            set
            {
                _visibilityQ2Aus = value;
                OnPropertyChanged(nameof(VisibilityQ2Aus));
            }
        }

        #endregion Visibility Motor Q2

        #region Visibility Ventil Y1

        public void VisibilityVentilY1(bool val)
        {
            if (val)
            {
                VisibilityVentilEin = Visibility.Visible;
                VisibilityVentilAus =  Visibility.Hidden;
            }
            else
            {
                VisibilityVentilEin =  Visibility.Hidden;
                VisibilityVentilAus = Visibility.Visible;
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

        #endregion Visibility Ventil Y1

        #region Margin1

        public void Margin_1(double pegel) => Margin1 = new Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);

        private Thickness _margin1;

        public Thickness Margin1
        {
            get => _margin1;
            set
            {
                _margin1 = value;
                OnPropertyChanged(nameof(Margin1));
            }
        }

        #endregion Margin1

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}