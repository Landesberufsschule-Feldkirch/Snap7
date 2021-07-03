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

            SpsSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            ClickModeBtnS1 = ClickMode.Press;
            ClickModeBtnS2 = ClickMode.Press;
            ClickModeBtnS3 = ClickMode.Press;

            ColorThermorelaisF1 = Brushes.LawnGreen;
            ColorThermorelaisF2 = Brushes.LawnGreen;

            ColorCircleP1 = Brushes.White;
            ColorCircleP2 = Brushes.White;
            ColorCircleP2 = Brushes.White;

            ColorAbleitungOben = Brushes.LightBlue;
            ColorAbleitungUnten = Brushes.LightBlue;
            ColorZuleitungLinksWaagrecht = Brushes.LightBlue;
            ColorZuleitungLinksSenkrecht = Brushes.LightBlue;
            ColorZuleitungRechtsWaagrecht = Brushes.LightBlue;
            ColorZuleitungRechtsSenkrecht = Brushes.LightBlue;

            VisibilityB1Ein = Visibility.Visible;
            VisibilityB2Ein = Visibility.Visible;
            VisibilityB3Ein = Visibility.Visible;
            VisibilityQ1Ein = Visibility.Visible;
            VisibilityQ2Ein = Visibility.Visible;
            VisibilityVentilEin = Visibility.Visible;

            VisibilityB1Aus = Visibility.Hidden;
            VisibilityB2Aus = Visibility.Hidden;
            VisibilityB3Aus = Visibility.Hidden;
            VisibilityQ1Aus = Visibility.Hidden;
            VisibilityQ2Aus = Visibility.Hidden;
            VisibilityVentilAus = Visibility.Hidden;

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
                    SpsVersionLokal = _mainWindow.VersionInfoLokal;
                    SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                    SpsSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    SpsColor = _mainWindow.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
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

        #region ClickModeBtn

        public bool ClickModeButtonS1()
        {
            if (ClickModeBtnS1 == ClickMode.Press)
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


        public bool ClickModeButtonS2()
        {
            if (ClickModeBtnS2 == ClickMode.Press)
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


        public bool ClickModeButtonS3()
        {
            if (ClickModeBtnS3 == ClickMode.Press)
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

        #endregion 

        #region Farben Leuchtmelder

        public void FarbeTherorelais_F1(bool val) => ColorThermorelaisF1 = val ? Brushes.Red : Brushes.LawnGreen;

        private Brush _colorThermorelaisF1;

        public Brush ColorThermorelaisF1
        {
            get => _colorThermorelaisF1;
            set
            {
                _colorThermorelaisF1 = value;
                OnPropertyChanged(nameof(ColorThermorelaisF1));
            }
        }


        public void FarbeTherorelais_F2(bool val) => ColorThermorelaisF2 = val ? Brushes.Red : Brushes.LawnGreen;

        private Brush _colorThermorelaisF2;

        public Brush ColorThermorelaisF2
        {
            get => _colorThermorelaisF2;
            set
            {
                _colorThermorelaisF2 = value;
                OnPropertyChanged(nameof(ColorThermorelaisF2));
            }
        }


        public void FarbeCircle_P1(bool val) => ColorCircleP1 = val ? Brushes.Red : Brushes.White;

        private Brush _colorCircleP1;

        public Brush ColorCircleP1
        {
            get => _colorCircleP1;
            set
            {
                _colorCircleP1 = value;
                OnPropertyChanged(nameof(ColorCircleP1));
            }
        }


        public void FarbeCircle_P2(bool val) => ColorCircleP2 = val ? Brushes.LawnGreen : Brushes.White;

        private Brush _colorCircleP2;

        public Brush ColorCircleP2
        {
            get => _colorCircleP2;
            set
            {
                _colorCircleP2 = value;
                OnPropertyChanged(nameof(ColorCircleP2));
            }
        }

        public void FarbeCircle_P3(bool val) => ColorCircleP3 = val ? Brushes.OrangeRed : Brushes.White;

        private Brush _colorCircleP3;

        public Brush ColorCircleP3
        {
            get => _colorCircleP3;
            set
            {
                _colorCircleP3 = value;
                OnPropertyChanged(nameof(ColorCircleP3));
            }
        }

        #endregion 

        #region Farben Ableitungen, ...

        public void FarbeAbleitungOben(bool val) => ColorAbleitungOben = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorAbleitungOben;

        public Brush ColorAbleitungOben
        {
            get => _colorAbleitungOben;
            set
            {
                _colorAbleitungOben = value;
                OnPropertyChanged(nameof(ColorAbleitungOben));
            }
        }


        public void FarbeAbleitungUnten(bool val) => ColorAbleitungUnten = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorAbleitungUnten;

        public Brush ColorAbleitungUnten
        {
            get => _colorAbleitungUnten;
            set
            {
                _colorAbleitungUnten = value;
                OnPropertyChanged(nameof(ColorAbleitungUnten));
            }
        }


        public void FarbeZuleitungLinksWaagrecht(bool val) => ColorZuleitungLinksWaagrecht = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorZuleitungLinksWaagrecht;

        public Brush ColorZuleitungLinksWaagrecht
        {
            get => _colorZuleitungLinksWaagrecht;
            set
            {
                _colorZuleitungLinksWaagrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungLinksWaagrecht));
            }
        }


        public void FarbeZuleitungLinksSenkrecht(bool val) => ColorZuleitungLinksSenkrecht = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorZuleitungLinksSenkrecht;

        public Brush ColorZuleitungLinksSenkrecht
        {
            get => _colorZuleitungLinksSenkrecht;
            set
            {
                _colorZuleitungLinksSenkrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungLinksSenkrecht));
            }
        }

        public void FarbeZuleitungRechtsWaagrecht(bool val) => ColorZuleitungRechtsWaagrecht = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorZuleitungRechtsWaagrecht;

        public Brush ColorZuleitungRechtsWaagrecht
        {
            get => _colorZuleitungRechtsWaagrecht;
            set
            {
                _colorZuleitungRechtsWaagrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungRechtsWaagrecht));
            }
        }

        public void FarbeZuleitungRechtsSenkrecht(bool val) => ColorZuleitungRechtsSenkrecht = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorZuleitungRechtsSenkrecht;

        public Brush ColorZuleitungRechtsSenkrecht
        {
            get => _colorZuleitungRechtsSenkrecht;
            set
            {
                _colorZuleitungRechtsSenkrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungRechtsSenkrecht));
            }
        }

        #endregion 

        #region Visibility 

        public void VisibilitySensorB1(bool val)
        {
            if (val)
            {
                VisibilityB1Ein = Visibility.Visible;
                VisibilityB1Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityB1Ein = Visibility.Hidden;
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


        public void VisibilitySensorB2(bool val)
        {
            if (val)
            {
                VisibilityB2Ein = Visibility.Visible;
                VisibilityB2Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityB2Ein = Visibility.Hidden;
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

        public void VisibilitySensorB3(bool val)
        {
            if (val)
            {
                VisibilityB3Ein = Visibility.Visible;
                VisibilityB3Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityB3Ein = Visibility.Hidden;
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


        public void VisibilityMotorQ1(bool val)
        {
            if (val)
            {
                VisibilityQ1Ein = Visibility.Visible;
                VisibilityQ1Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityQ1Ein = Visibility.Hidden;
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

        public void VisibilityMotorQ2(bool val)
        {
            if (val)
            {
                VisibilityQ2Ein = Visibility.Visible;
                VisibilityQ2Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityQ2Ein = Visibility.Hidden;
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


        public void VisibilityVentilY1(bool val)
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

        #endregion

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