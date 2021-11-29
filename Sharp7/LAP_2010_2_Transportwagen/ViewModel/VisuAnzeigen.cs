using System;
using System.Collections.ObjectModel;
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
                FarbeUmschalten(_transportwagen.F1, 3, Brushes.LawnGreen, Brushes.Red);
                FarbeUmschalten(_transportwagen.P1, 4, Brushes.Red, Brushes.White);
                FarbeUmschalten(_transportwagen.Q1, 5, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_transportwagen.Q2, 6, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_transportwagen.S2, 12, Brushes.LawnGreen, Brushes.Red);

                SichtbarkeitUmschalten(_transportwagen.B1, 1);
                SichtbarkeitUmschalten(_transportwagen.B2, 2);
                SichtbarkeitUmschalten(_transportwagen.Fuellen, 20);
                SichtbarkeitUmschalten(_transportwagen.Q1 && _transportwagen.Q2, 21);

                PositionRadLinks = _transportwagen.Position + abstandRadWagen;
                PositionRadRechts = _transportwagen.Position + _transportwagen.AbstandRadRechts + abstandRadWagen;
                PositionWagenkasten = _transportwagen.Position;

                if (_mainWindow.PlcDaemon != null && _mainWindow.PlcDaemon.Plc != null)
                {
                    SpsVersionLokal = _mainWindow.VersionInfoLokal;
                    SpsVersionEntfernt = _mainWindow.PlcDaemon.Plc.GetVersion();
                    SpsSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    SpsColor = _mainWindow.PlcDaemon.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.PlcDaemon.Plc?.GetSpsStatus();

                    FensterTitel = _mainWindow.PlcDaemon.Plc.GetPlcBezeichnung() + ": " + _mainWindow.VersionInfoLokal;
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
                case 11: _transportwagen.S1 = gedrueckt; break;
                case 13: _transportwagen.S3 = gedrueckt; break;
                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
        internal void Schalter(object id)
        {
            if (id is not string ascii) return;

            var schalterId = short.Parse(ascii);

            switch (schalterId)
            {
                case 12: _transportwagen.S2 = !_transportwagen.S2; break;
                case 3: _transportwagen.F1 = !_transportwagen.F1; break;
                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
        }

        #region SPS Version, Status und Farbe
        private string fensterTitel;
        public string FensterTitel
        {
            get => fensterTitel;
            set
            {
                fensterTitel = value;
                OnPropertyChanged(nameof(FensterTitel));
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

        #region Position
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
        #endregion Position

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