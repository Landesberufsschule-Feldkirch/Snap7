using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LAP_2010_2_Transportwagen.ViewModel
{
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Transportwagen _transportwagen;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Transportwagen tw)
        {
            _mainWindow = mw;
            _transportwagen = tw;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            ColorF1 = Brushes.LawnGreen;
            ColorP1 = Brushes.LawnGreen;
            ColorQ1 = Brushes.LawnGreen;
            ColorQ2 = Brushes.LawnGreen;
            ColorS2 = Brushes.LawnGreen;

            ClickModeBtnQ1 = ClickMode.Press;
            ClickModeBtnQ2 = ClickMode.Press;
            ClickModeBtnS1 = ClickMode.Press;
            ClickModeBtnS3 = ClickMode.Press;

            VisibilityB1Ein = Visibility.Visible;
            VisibilityB1Aus = Visibility.Hidden;
            VisibilityB2Ein = Visibility.Visible;
            VisibilityB2Aus = Visibility.Hidden;

            VisibilityFuellen = Visibility.Hidden;
            VisibilityKurzschluss = Visibility.Hidden;

            PositionRadLinks = 0;
            PositionRadRechts = 0;
            PositionWagenkasten = 0;

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            const double abstandRadWagen = 10;

            while (true)
            {
                FarbeF1(_transportwagen.F1);
                FarbeP1(_transportwagen.P1);
                FarbeQ1(_transportwagen.Q1);
                FarbeQ2(_transportwagen.Q2);
                FarbeS2(_transportwagen.S2);

                SichtbarkeitB1(_transportwagen.B1);
                SichtbarkeitB2(_transportwagen.B2);

                if (_transportwagen.Q1 && _transportwagen.Q2) VisibilityKurzschluss = Visibility.Visible; else VisibilityKurzschluss = Visibility.Hidden;
                VisibilityFuellen = _transportwagen.Fuellen ? Visibility.Visible : Visibility.Hidden;

                PositionRadLinks = _transportwagen.Position + abstandRadWagen;
                PositionRadRechts = _transportwagen.Position + _transportwagen.AbstandRadRechts + abstandRadWagen;
                PositionWagenkasten = _transportwagen.Position;

                if (_mainWindow.Plc != null)
                {
                    if (_mainWindow.Plc.GetPlcModus() == "S7-1200")
                    {
                        VersionNr = _mainWindow.VersionNummer;
                        SpsVersionLokal = _mainWindow.VersionInfoLokal;
                        SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                        SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    }

                    SpsColor = _mainWindow.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void SetManualQ1() => _transportwagen.Q1 = ClickModeButtonQ1();

        internal void SetManualQ2() => _transportwagen.Q2 = ClickModeButtonQ2();

        internal void SetS1() => _transportwagen.S1 = ClickModeButtonS1();

        internal void SetS3() => _transportwagen.S3 = ClickModeButtonS3();

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

        #region Color F1

        public void FarbeF1(bool val) => ColorF1 = val ? Brushes.LawnGreen : Brushes.Red;

        private Brush _colorF1;

        public Brush ColorF1
        {
            get => _colorF1;
            set
            {
                _colorF1 = value;
                OnPropertyChanged(nameof(ColorF1));
            }
        }

        #endregion Color F1

        #region Color P1

        public void FarbeP1(bool val) => ColorP1 = val ? Brushes.Red : Brushes.White;

        private Brush _colorP1;

        public Brush ColorP1
        {
            get => _colorP1;
            set
            {
                _colorP1 = value;
                OnPropertyChanged(nameof(ColorP1));
            }
        }

        #endregion Color P1

        #region Color Q1

        public void FarbeQ1(bool val) => ColorQ1 = val ? Brushes.LawnGreen : Brushes.White;

        private Brush _colorQ1;

        public Brush ColorQ1
        {
            get => _colorQ1;
            set
            {
                _colorQ1 = value;
                OnPropertyChanged(nameof(ColorQ1));
            }
        }

        #endregion Color Q1

        #region Color Q2

        public void FarbeQ2(bool val) => ColorQ2 = val ? Brushes.LawnGreen : Brushes.White;

        private Brush _colorQ2;

        public Brush ColorQ2
        {
            get => _colorQ2;
            set
            {
                _colorQ2 = value;
                OnPropertyChanged(nameof(ColorQ2));
            }
        }

        #endregion Color Q2

        #region Color S2

        public void FarbeS2(bool val) => ColorS2 = val ? Brushes.LawnGreen : Brushes.Red;

        private Brush _colorS2;

        public Brush ColorS2
        {
            get => _colorS2;
            set
            {
                _colorS2 = value;
                OnPropertyChanged(nameof(ColorS2));
            }
        }

        #endregion Color S2

        #region ClickModeBtnQ1

        public bool ClickModeButtonQ1()
        {
            if (ClickModeBtnQ1 == ClickMode.Press)
            {
                ClickModeBtnQ1 = ClickMode.Release;
                return true;
            }

            ClickModeBtnQ1 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnQ1;

        public ClickMode ClickModeBtnQ1
        {
            get => _clickModeBtnQ1;
            set
            {
                _clickModeBtnQ1 = value;
                OnPropertyChanged(nameof(ClickModeBtnQ1));
            }
        }

        #endregion ClickModeBtnQ1

        #region ClickModeBtnQ2

        public bool ClickModeButtonQ2()
        {
            if (ClickModeBtnQ2 == ClickMode.Press)
            {
                ClickModeBtnQ2 = ClickMode.Release;
                return true;
            }

            ClickModeBtnQ2 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnQ2;

        public ClickMode ClickModeBtnQ2
        {
            get => _clickModeBtnQ2;
            set
            {
                _clickModeBtnQ2 = value;
                OnPropertyChanged(nameof(ClickModeBtnQ2));
            }
        }

        #endregion ClickModeBtnQ2

        #region ClickModeBtnS1

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

        #endregion ClickModeBtnS1

        #region ClickModeBtnS3

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

        #endregion ClickModeBtnS3

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

        #region VisibilityKurzschluss

        private Visibility _visibilityKurzschluss;

        public Visibility VisibilityKurzschluss
        {
            get => _visibilityKurzschluss;
            set
            {
                _visibilityKurzschluss = value;
                OnPropertyChanged(nameof(VisibilityKurzschluss));
            }
        }

        #endregion VisibilityKurzschluss

        #region VisibilityFuellen

        private Visibility _visibilityFuellen;

        public Visibility VisibilityFuellen
        {
            get => _visibilityFuellen;
            set
            {
                _visibilityFuellen = value;
                OnPropertyChanged(nameof(VisibilityFuellen));
            }
        }

        #endregion VisibilityFuellen

        #region PositionRadLinks

        private double _positionRadLinks;

        public double PositionRadLinks
        {
            get => _positionRadLinks;
            set
            {
                _positionRadLinks = value;
                OnPropertyChanged(nameof(PositionRadLinks));
            }
        }

        #endregion PositionRadLinks

        #region PositionRadRechts

        private double _positionRadRechts;

        public double PositionRadRechts
        {
            get => _positionRadRechts;
            set
            {
                _positionRadRechts = value;
                OnPropertyChanged(nameof(PositionRadRechts));
            }
        }

        #endregion PositionRadRechts

        #region PositionWagenkasten

        private double _positionWagenkasten;

        public double PositionWagenkasten
        {
            get => _positionWagenkasten;
            set
            {
                _positionWagenkasten = value;
                OnPropertyChanged(nameof(PositionWagenkasten));
            }
        }

        #endregion PositionWagenkasten

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}