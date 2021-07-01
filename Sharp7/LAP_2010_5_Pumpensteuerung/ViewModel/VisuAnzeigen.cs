using System.Windows.Controls;
using System.Windows.Media;

namespace LAP_2010_5_Pumpensteuerung.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private const double HoeheFuellBalken = 9 * 35;

        private readonly MainWindow _mainWindow;
        private readonly Model.Pumpensteuerung _pumpensteuerung;

        public VisuAnzeigen(MainWindow mw, Model.Pumpensteuerung nr)
        {
            _mainWindow = mw;
            _pumpensteuerung = nr;

            SpsVersionsInfoSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            ClickModeBtnS3 = ClickMode.Press;

            ColorThermorelaisF1 = Brushes.LawnGreen;

            ColorAbleitungOben = Brushes.LightBlue;
            ColorAbleitungUnten = Brushes.LightBlue;
            ColorZuleitungLinksWaagrecht = Brushes.LightBlue;
            ColorZuleitungLinksSenkrecht = Brushes.LightBlue;

            ColorCircleP1 = Brushes.LightGray;
            ColorCircleP2 = Brushes.LightGray;

            VisibilityQ1Ein = Visibility.Visible;
            VisibilityQ1Aus = Visibility.Hidden;

            VisibilityB1Ein = Visibility.Hidden;
            VisibilityB1Aus = Visibility.Visible;

            VisibilityB2Ein = Visibility.Visible;
            VisibilityB2Aus = Visibility.Hidden;

            VisibilityY1Ein = Visibility.Visible;
            VisibilityY1Aus = Visibility.Hidden;

            WinkelSchalter = 0;

            Margin1 = new Thickness(0, 30, 0, 0);

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeTherorelais_F1(_pumpensteuerung.F1);
                FarbeCircle_P1(_pumpensteuerung.P1);
                FarbeCircle_P2(_pumpensteuerung.P2);
                FarbeZuleitungLinksWaagrecht(_pumpensteuerung.Q1);
                FarbeZuleitungLinksSenkrecht(_pumpensteuerung.Q1);
                FarbeAbleitungOben(_pumpensteuerung.Pegel > 0.01);
                FarbeAbleitungUnten(_pumpensteuerung.Pegel > 0.01 && _pumpensteuerung.Y1);

                SichtbarkeitQ1(_pumpensteuerung.Q1);
                SichtbarkeitB1(_pumpensteuerung.B1);
                SichtbarkeitB2(_pumpensteuerung.B2);
                SichtbarkeitY1(_pumpensteuerung.Y1);

                Margin_1(_pumpensteuerung.Pegel);

                SchalterWinkel(_pumpensteuerung.S1, _pumpensteuerung.S2);

                if (_mainWindow.Plc != null)
                {
                    SpsVersionLokal = _mainWindow.VersionInfoLokal;
                    SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                    SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    SpsColor = _mainWindow.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void SetS3() => _pumpensteuerung.S3 = ClickModeButtonS3();


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

        #region SchalterWinkel

        private void SchalterWinkel(bool s1, bool s2)
        {
            WinkelSchalter = 0;
            if (s1) WinkelSchalter = -45;
            if (s2) WinkelSchalter = 45;
        }

        private int _winkelSchalter;

        public int WinkelSchalter
        {
            get => _winkelSchalter;
            set
            {
                _winkelSchalter = value;
                OnPropertyChanged(nameof(WinkelSchalter));
            }
        }

        #endregion SchalterWinkel

        #region ClickModeButtonS3

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

        #endregion ClickModeButtonS3

        #region Color Thermorelais F1

        public void FarbeTherorelais_F1(bool val) => ColorThermorelaisF1 = val ? Brushes.LawnGreen : Brushes.Red;

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

        #endregion Color Thermorelais F1

        #region Color P1

        public void FarbeCircle_P1(bool val) => ColorCircleP1 = val ? Brushes.LawnGreen : Brushes.LightGray;

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

        #endregion Color P1

        #region Color P2

        public void FarbeCircle_P2(bool val) => ColorCircleP2 = val ? Brushes.Red : Brushes.LightGray;

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

        #endregion Color P2

        #region Color AbleitungOben

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

        #endregion Color AbleitungOben

        #region Color AbleitungUnten

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

        #endregion Color AbleitungUnten

        #region Color ZuleitungLinksWaagrecht

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

        #endregion Color ZuleitungLinksWaagrecht

        #region Color ZuleitungLinksSenkrecht

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

        #endregion Color ZuleitungLinksSenkrecht

        #region Sichtbarkeit Q1

        public void SichtbarkeitQ1(bool val)
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

        #endregion Sichtbarkeit Q1

        #region Sichtbarkeit B1

        public void SichtbarkeitB1(bool val)
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

        #endregion Sichtbarkeit B1

        #region Sichtbarkeit B2

        public void SichtbarkeitB2(bool val)
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

        #endregion Sichtbarkeit B2

        #region Sichtbarkeit Y1

        public void SichtbarkeitY1(bool val)
        {
            if (val)
            {
                VisibilityY1Ein = Visibility.Visible;
                VisibilityY1Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityY1Ein = Visibility.Hidden;
                VisibilityY1Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityY1Ein;

        public Visibility VisibilityY1Ein
        {
            get => _visibilityY1Ein;
            set
            {
                _visibilityY1Ein = value;
                OnPropertyChanged(nameof(VisibilityY1Ein));
            }
        }

        private Visibility _visibilityY1Aus;

        public Visibility VisibilityY1Aus
        {
            get => _visibilityY1Aus;
            set
            {
                _visibilityY1Aus = value;
                OnPropertyChanged(nameof(VisibilityY1Aus));
            }
        }

        #endregion Sichtbarkeit Y1

        #region Margin1

        public void Margin_1(double pegel)
        {
            Margin1 = new Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);
        }

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