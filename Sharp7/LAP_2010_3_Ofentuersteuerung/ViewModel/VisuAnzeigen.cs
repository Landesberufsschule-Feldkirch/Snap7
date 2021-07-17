using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

            for (var i = 0; i < 100; i++)
            {
                ClkMode.Add(ClickMode.Press);
                SichtbarEin.Add(Visibility.Hidden);
                SichtbarAus.Add(Visibility.Visible);
                Farbe.Add(Brushes.White);
            }

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }
        private void VisuAnzeigenTask()
        {
            while (true)
            {
                OfentuerePosition = _ofentuerSteuerung.PositionOfentuere;
                ZahnstangePosition = _ofentuerSteuerung.PositionZahnstange;
                ZahnradWinkel = _ofentuerSteuerung.WinkelZahnrad;

                SichtbarkeitUmschalten(_ofentuerSteuerung.B1, 1);
                SichtbarkeitUmschalten(_ofentuerSteuerung.B2, 2);
                SichtbarkeitUmschalten(_ofentuerSteuerung.B3, 3);
                SichtbarkeitUmschalten(_ofentuerSteuerung.Q1 && _ofentuerSteuerung.Q2, 20);

                FarbeUmschalten(_ofentuerSteuerung.P1, 4, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_ofentuerSteuerung.Q1, 5, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_ofentuerSteuerung.Q2, 6, Brushes.LawnGreen, Brushes.White);

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

        internal void FarbeUmschalten(bool val, int i, Brush farbe1, Brush farbe2) => Farbe[i] = val ? farbe1 : farbe2;
        internal void SichtbarkeitUmschalten(bool val, int i)
        {
            SichtbarEin[i] = val ? Visibility.Visible : Visibility.Collapsed;
            SichtbarAus[i] = val ? Visibility.Collapsed : Visibility.Visible;
        }
        internal void Taster(object id)
        {
            if (id is not string ascii) return;

            var tasterId = short.Parse(ascii);
            var gedrueckt = ClickModeButton(tasterId);

            switch (tasterId)
            {
                case 3: _ofentuerSteuerung.B3 = !gedrueckt; break;
                case 11: _ofentuerSteuerung.S1 = !gedrueckt; break;
                case 12: _ofentuerSteuerung.S2 = gedrueckt; break;
                case 13: _ofentuerSteuerung.S3 = gedrueckt; break;
                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
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

        #region Sichtbarkeit
        private ObservableCollection<Visibility> _sichtbarEin = new();
        public ObservableCollection<Visibility> SichtbarEin
        {
            get => _sichtbarEin;
            set
            {
                _sichtbarEin = value;
                OnPropertyChanged(nameof(SichtbarEin));
            }
        }

        private ObservableCollection<Visibility> _sichtbarAus = new();
        public ObservableCollection<Visibility> SichtbarAus
        {
            get => _sichtbarAus;
            set
            {
                _sichtbarAus = value;
                OnPropertyChanged(nameof(SichtbarAus));
            }
        }
        #endregion

        #region Farbe
        private ObservableCollection<Brush> _farbe = new();
        public ObservableCollection<Brush> Farbe
        {
            get => _farbe;
            set
            {
                _farbe = value;
                OnPropertyChanged(nameof(Farbe));
            }
        }
        #endregion

        #region Taster/Schalter
        public bool ClickModeButton(int tasterId)
        {
            if (ClkMode[tasterId] == ClickMode.Press)
            {
                ClkMode[tasterId] = ClickMode.Release;
                return true;
            }

            ClkMode[tasterId] = ClickMode.Press;
            return false;
        }

        private ObservableCollection<ClickMode> _clkMode = new();
        public ObservableCollection<ClickMode> ClkMode
        {
            get => _clkMode;
            set
            {
                _clkMode = value;
                OnPropertyChanged(nameof(ClkMode));
            }
        }
        #endregion Taster/Schalter

        #region iNotifyPeropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion iNotifyPeropertyChanged Members
    }
}