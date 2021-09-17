using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace LAP_2018_3_Hydraulikaggregat.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Hydraulikaggregat _hydraulikaggregat;
        private readonly MainWindow _mainWindow;

        private const double FuellBalkenHoehe = 390;
        private const double FuellBalkenOben = -40;

        public VisuAnzeigen(MainWindow mw, Model.Hydraulikaggregat ha)
        {
            _mainWindow = mw;
            _hydraulikaggregat = ha;

            Druck = 1.1;

            for (var i = 0; i < 100; i++)
            {
                ClkMode.Add(ClickMode.Press);
                SichtbarEin.Add(Visibility.Hidden);
                SichtbarAus.Add(Visibility.Visible);
                Farbe.Add(Brushes.White);
            }

            Margin1 = new Thickness(42, 0, 32, 0);

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
                Druck = _hydraulikaggregat.Druck;

                FarbeUmschalten(_hydraulikaggregat.B3, 3, Brushes.LawnGreen, Brushes.Red);
                FarbeUmschalten(_hydraulikaggregat.B4, 4, Brushes.LawnGreen, Brushes.Red);
                FarbeUmschalten(_hydraulikaggregat.B5, 5, Brushes.LawnGreen, Brushes.Red);

                FarbeUmschalten(_hydraulikaggregat.F1, 6, Brushes.LawnGreen, Brushes.Red);

                FarbeUmschalten(_hydraulikaggregat.K1, 7, Brushes.Red, Brushes.White);
                FarbeUmschalten(_hydraulikaggregat.K2, 8, Brushes.Red, Brushes.White);

                FarbeUmschalten(_hydraulikaggregat.P1, 11, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_hydraulikaggregat.P2, 12, Brushes.Red, Brushes.White);
                FarbeUmschalten(_hydraulikaggregat.P3, 13, Brushes.Red, Brushes.White);
                FarbeUmschalten(_hydraulikaggregat.P4, 14, Brushes.Red, Brushes.White);
                FarbeUmschalten(_hydraulikaggregat.P5, 15, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_hydraulikaggregat.P6, 16, Brushes.Red, Brushes.White);
                FarbeUmschalten(_hydraulikaggregat.P7, 17, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_hydraulikaggregat.P8, 18, Brushes.Red, Brushes.White);

                FarbeUmschalten(_hydraulikaggregat.Q1, 21, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_hydraulikaggregat.Q2, 22, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_hydraulikaggregat.Q3, 23, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_hydraulikaggregat.Q4, 24, Brushes.LawnGreen, Brushes.White);

                FarbeUmschalten(_hydraulikaggregat.S4, 34, Brushes.LawnGreen, Brushes.White);

                SichtbarkeitUmschalten(_hydraulikaggregat.B1, 1);
                SichtbarkeitUmschalten(_hydraulikaggregat.B2, 2);
                SichtbarkeitUmschalten(_hydraulikaggregat.B3, 3);

                SichtbarkeitUmschalten(_hydraulikaggregat.Q2 && _hydraulikaggregat.Q3, 40);

                Margin_1(_hydraulikaggregat.Pegel);

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
        public void SichtbarkeitUmschalten(bool val, int i)
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
                case 31: _hydraulikaggregat.S1 = gedrueckt; break;
                case 32: _hydraulikaggregat.S2 = !gedrueckt; break;
                case 33:
                    _hydraulikaggregat.S3 = gedrueckt;
                    _hydraulikaggregat.Stopwatch.Restart(); break;
                case 34: _hydraulikaggregat.S4 = gedrueckt; break;
                case 35: _hydraulikaggregat.Pegel = 1; break;
                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
        internal void Schalter(object id)
        {
            if (id is not string ascii) return;

            var schalterId = short.Parse(ascii);

            switch (schalterId)
            {
                case 3: _hydraulikaggregat.B3 = !_hydraulikaggregat.B3; break;
                case 4: _hydraulikaggregat.B4 = !_hydraulikaggregat.B4; break;
                case 5: _hydraulikaggregat.B5 = !_hydraulikaggregat.B5; break;
                case 6: _hydraulikaggregat.F1 = !_hydraulikaggregat.F1; break;

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

        #region Druck

        private double _druck;

        public double Druck
        {
            get => _druck;
            set
            {
                _druck = value;
                OnPropertyChanged(nameof(Druck));
            }
        }

        #endregion Druck

        #region Margin1

        public void Margin_1(double pegel)
        {
            Margin1 = new Thickness(41, FuellBalkenOben + FuellBalkenHoehe * (1 - pegel), 31, 0);
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