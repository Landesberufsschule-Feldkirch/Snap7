using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using System.ComponentModel;
using System.Threading;

namespace LAP_2010_3_Ofentuersteuerung.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.OfentuerSteuerung _ofentuerSteuerung;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.OfentuerSteuerung oSt)
        {
            _mainWindow = mw;
            _ofentuerSteuerung = oSt;

            SpsSichtbar = Visibility.Hidden;
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

            for (var i = 0; i < 100; i++)
            {
                ClickModeBtn.Add(ClickMode.Press);
            }

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

        #region Farben
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
        #endregion Farben

        #region Sichtbarkeit 
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
        #endregion Sichtbarkeit 

        #region Taster
        internal void Taster(object id)
        {
            if (id is not string ascii) return;

            var tasterId = short.Parse(ascii);
            var gedrueckt = ClickModeButton(tasterId);

            switch (tasterId)
            {
                case 1: _ofentuerSteuerung.S1 = !gedrueckt; break;
                case 2: _ofentuerSteuerung.S2 = gedrueckt; break;
                case 3: _ofentuerSteuerung.S3 = gedrueckt; break;
                case 4: _ofentuerSteuerung.B3 = !gedrueckt; break;
                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
        public bool ClickModeButton(int tasterId)
        {
            if (ClickModeBtn[tasterId] == ClickMode.Press)
            {
                ClickModeBtn[tasterId] = ClickMode.Release;
                return true;
            }

            ClickModeBtn[tasterId] = ClickMode.Press;
            return false;
        }

        private ObservableCollection<ClickMode> _clickModeBtn = new();
        public ObservableCollection<ClickMode> ClickModeBtn
        {
            get => _clickModeBtn;
            set
            {
                _clickModeBtn = value;
                OnPropertyChanged(nameof(ClickModeBtn));
            }
        }
        #endregion Taster

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}