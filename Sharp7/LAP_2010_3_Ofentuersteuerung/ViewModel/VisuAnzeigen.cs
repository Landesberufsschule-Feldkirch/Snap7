using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LAP_2010_3_Ofentuersteuerung.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.OfentuerSteuerung _ofentuerSteuerung;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.OfentuerSteuerung oSt)
        {
            _mainWindow = mw;
            _ofentuerSteuerung = oSt;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            ZahnradWinkel = 0;
            ZahnstangePosition = _ofentuerSteuerung.PositionZahnstange;
            OfentuerePosition = _ofentuerSteuerung.PositionOfentuere;

            ColorP1 = Brushes.LawnGreen;
            ColorQ1 = Brushes.LawnGreen;
            ColorQ2 = Brushes.LawnGreen;

            ClickModeBtnQ1 = ClickMode.Press;
            ClickModeBtnQ2 = ClickMode.Press;

            ClickModeBtnS1 = ClickMode.Press;
            ClickModeBtnS2 = ClickMode.Press;
            ClickModeBtnS3 = ClickMode.Press;
            ClickModeBtnB3 = ClickMode.Press;

            VisibilityB1Ein = Visibility.Visible;
            VisibilityB1Aus = Visibility.Hidden;
            VisibilityB2Ein = Visibility.Visible;
            VisibilityB2Aus = Visibility.Hidden;
            VisibilityB3Ein = Visibility.Visible;
            VisibilityB3Aus = Visibility.Hidden;
            VisibilityKurzschluss = Visibility.Hidden;

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                OfentuerePosition = _ofentuerSteuerung.PositionOfentuere;
                ZahnstangePosition = _ofentuerSteuerung.PositionZahnstange;
                ZahnradWinkel = _ofentuerSteuerung.WinkelZahnrad;

                SichtbarkeitB1(_ofentuerSteuerung.B1);
                SichtbarkeitB2(_ofentuerSteuerung.B2);
                SichtbarkeitB3(_ofentuerSteuerung.B3);

                FarbeP1(_ofentuerSteuerung.P1);
                FarbeQ1(_ofentuerSteuerung.Q1);
                FarbeQ2(_ofentuerSteuerung.Q2);

                if (_ofentuerSteuerung.Q1 && _ofentuerSteuerung.Q2) VisibilityKurzschluss = Visibility.Visible; else VisibilityKurzschluss = Visibility.Hidden;

                if (_mainWindow.Plc != null)
                {
                    VersionNr = _mainWindow.VersionNummer;
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

        internal void SetS1() => _ofentuerSteuerung.S1 = !ClickModeButtonS1();

        internal void SetS2() => _ofentuerSteuerung.S2 = ClickModeButtonS2();

        internal void SetS3() => _ofentuerSteuerung.S3 = ClickModeButtonS3();

        internal void SetB3() => _ofentuerSteuerung.B3 = !ClickModeButtonB3();

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

        // ReSharper disable once UnusedMember.Global
        internal void BtnS3() => throw new NotImplementedException();

        #endregion SPS Versionsinfo, Status und Farbe

        #region ZahnradWinkel

        private double _zahnradWinkel;

        public double ZahnradWinkel
        {
            get => _zahnradWinkel;
            set
            {
                _zahnradWinkel = value;
                OnPropertyChanged(nameof(ZahnradWinkel));
            }
        }

        #endregion ZahnradWinkel

        #region ZahnstangePosition

        private double _zahnstangePosition;

        public double ZahnstangePosition
        {
            get => _zahnstangePosition;
            set
            {
                _zahnstangePosition = value;
                OnPropertyChanged(nameof(ZahnstangePosition));
            }
        }

        #endregion ZahnstangePosition

        #region OfentuerePosition

        private double _ofentuerePosition;

        public double OfentuerePosition
        {
            get => _ofentuerePosition;
            set
            {
                _ofentuerePosition = value;
                OnPropertyChanged(nameof(OfentuerePosition));
            }
        }

        #endregion OfentuerePosition

        #region Color P1

        public void FarbeP1(bool val) => ColorP1 = val ? Brushes.LawnGreen : Brushes.White;

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

        #region ClickModeBtnS2

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

        #endregion ClickModeBtnS2

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

        #region ClickModeBtnB3

        public bool ClickModeButtonB3()
        {
            if (ClickModeBtnB3 == ClickMode.Press)
            {
                ClickModeBtnB3 = ClickMode.Release;
                return true;
            }

            ClickModeBtnB3 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnB3;

        public ClickMode ClickModeBtnB3
        {
            get => _clickModeBtnB3;
            set
            {
                _clickModeBtnB3 = value;
                OnPropertyChanged(nameof(ClickModeBtnB3));
            }
        }

        #endregion ClickModeBtnB3

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

        #region Sichtbarkeit B3

        public void SichtbarkeitB3(bool val)
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

        #endregion Sichtbarkeit B3

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

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}