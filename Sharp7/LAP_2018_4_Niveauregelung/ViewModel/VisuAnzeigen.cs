using System;
using System.Collections.ObjectModel;
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

            for (var i = 0; i < 100; i++)
            {
                ClkMode.Add(ClickMode.Press);
                SichtbarEin.Add(Visibility.Hidden);
                SichtbarAus.Add(Visibility.Visible);
                Farbe.Add(Brushes.White);
            }

            Margin1 = new Thickness(0, 30, 0, 0);

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }
        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeUmschalten(!_niveauRegelung.F1, 4, Brushes.Red, Brushes.LawnGreen);
                FarbeUmschalten(!_niveauRegelung.F2, 5, Brushes.Red, Brushes.LawnGreen);

                FarbeUmschalten(_niveauRegelung.P1, 11, Brushes.Red, Brushes.White);
                FarbeUmschalten(_niveauRegelung.P2, 12, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_niveauRegelung.P3, 13, Brushes.OrangeRed, Brushes.White);

                FarbeUmschalten(_niveauRegelung.Pegel > 0.01, 31, Brushes.Blue, Brushes.LightBlue);
                FarbeUmschalten(_niveauRegelung.Y1 && _niveauRegelung.Pegel > 0.01, 32, Brushes.Blue, Brushes.LightBlue);
                FarbeUmschalten(_niveauRegelung.Q1, 33, Brushes.Blue, Brushes.LightBlue);
                FarbeUmschalten(_niveauRegelung.Q1, 34, Brushes.Blue, Brushes.LightBlue);
                FarbeUmschalten(_niveauRegelung.Q2, 35, Brushes.Blue, Brushes.LightBlue);
                FarbeUmschalten(_niveauRegelung.Q2, 36, Brushes.Blue, Brushes.LightBlue);


                SichtbarkeitUmschalten(_niveauRegelung.B1,1);
                SichtbarkeitUmschalten(_niveauRegelung.B2,2);
                SichtbarkeitUmschalten(_niveauRegelung.B3,3);
                SichtbarkeitUmschalten(_niveauRegelung.Q1,14);
                SichtbarkeitUmschalten(_niveauRegelung.Q2,15);
                SichtbarkeitUmschalten(_niveauRegelung.Y1,30);

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
                case 21: _niveauRegelung.S1 = gedrueckt; break;
                case 22: _niveauRegelung.S2 = !gedrueckt; break;
                case 23: _niveauRegelung.S3 = gedrueckt; break;
                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
        internal void Schalter(object id)
        {
            if (id is not string ascii) return;

            var schalterId = short.Parse(ascii);

            switch (schalterId)
            {
                case 4: _niveauRegelung.F1 = !_niveauRegelung.F1; break;
                case 5: _niveauRegelung.F2 = !_niveauRegelung.F2; break;
                case 30: _niveauRegelung.Y1 = !_niveauRegelung.Y1; break;
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