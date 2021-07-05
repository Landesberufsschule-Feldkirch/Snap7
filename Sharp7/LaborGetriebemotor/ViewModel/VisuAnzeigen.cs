using System;
using LaborGetriebemotor.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LaborGetriebemotor.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Getriebemotor _getriebemotor;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Getriebemotor getriebemotor)
        {
            _mainWindow = mw;
            _getriebemotor = getriebemotor;

            for (var i = 0; i < 100; i++)
            {
                ClkMode.Add(ClickMode.Press);
                SichtbarEin.Add(Visibility.Hidden);
                SichtbarAus.Add(Visibility.Visible);
                Farbe.Add(Brushes.White);
            }

            WinkelGetriebemotor = 10;

            SpsSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }
        private void VisuAnzeigenTask()
        {
            while (true)
            {
                WinkelGetriebemotor = _getriebemotor.WinkelGetriebemotor;

                SichtbarkeitUmschalten(_getriebemotor.B1, 1);
                SichtbarkeitUmschalten(_getriebemotor.B2, 2);

                FarbeUmschalten(_getriebemotor.P1, 3, Brushes.White, Brushes.LightGray);
                FarbeUmschalten(_getriebemotor.P2, 4, Brushes.LawnGreen, Brushes.LightGray);
                FarbeUmschalten(_getriebemotor.P3, 5, Brushes.Red, Brushes.LightGray);

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
                case 11: _getriebemotor.S1 = gedrueckt; break;
                case 12: _getriebemotor.S2 = !gedrueckt; break;
                case 13: _getriebemotor.S3 = gedrueckt; break;
                case 14: _getriebemotor.S4 = !gedrueckt; break;
                case 15: _getriebemotor.S5 = gedrueckt; break;
                case 16: _getriebemotor.S6 = gedrueckt; break;
                case 17: _getriebemotor.S7 = !gedrueckt; break;
                case 18: _getriebemotor.S8 = gedrueckt; break;
                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
        internal void Schalter(object id)
        {
            if (id is not string ascii) return;

            var schalterId = short.Parse(ascii);

            switch (schalterId)
            {
                case 19:
                    _getriebemotor.S91 = !_getriebemotor.S91;
                    _getriebemotor.S92 = !_getriebemotor.S91;
                    break;
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

        #region Getriebemotor
        private double _winkelGetriebemotor;
        public double WinkelGetriebemotor
        {
            get => _winkelGetriebemotor;
            set
            {
                _winkelGetriebemotor = value;
                OnPropertyChanged(nameof(WinkelGetriebemotor));
            }
        }
        #endregion

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