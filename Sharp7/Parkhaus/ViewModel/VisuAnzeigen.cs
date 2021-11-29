using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Parkhaus.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Parkhaus _parkhaus;
        private readonly MainWindow _mainWindow;
        private readonly Random _random;

        public VisuAnzeigen(MainWindow mw, Model.Parkhaus parkhaus)
        {
            _mainWindow = mw;
            _parkhaus = parkhaus;

            _random = new Random();

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

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                AnzahlFreieParkplaetze = _parkhaus.FreieParkplaetze.ToString();
                AnzahlFreieParkplaetzeSoll = $"( {_parkhaus.FreieParkplaetzeSoll} )";

                for (var i = 0; i < 50; i++)
                {
                    SichtbarkeitUmschalten(BitMaskierenArray(_parkhaus.BesetzteParkPlaetze, i), i);
                    FarbeUmschalten(BitMaskierenArray(_parkhaus.BesetzteParkPlaetze, i), i, Brushes.Red, Brushes.LawnGreen);
                }

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

        internal static void BitInvertieren(byte[] besetzteParkPlaetzte, int i)
        {
            var ibyte = i / 8;
            var bitMuster = (byte)(1 << i % 8);

            if ((besetzteParkPlaetzte[ibyte] & bitMuster) == bitMuster) besetzteParkPlaetzte[ibyte] &= (byte)~bitMuster;
            else besetzteParkPlaetzte[ibyte] |= bitMuster;
        }

        internal static bool BitMaskierenArray(byte[] besetzteParkPlaetzte, int i)
        {
            var ibyte = i / 8;
            var bitMuster = (byte)(1 << i % 8);

            return (besetzteParkPlaetzte[ibyte] & bitMuster) == bitMuster;
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
            if (!ClickModeButton(tasterId)) return;

            switch (tasterId)
            {
                case < 40:
                    BitInvertieren(_parkhaus.BesetzteParkPlaetze, tasterId);
                    break;
                case 99:
                    _random.NextBytes(_parkhaus.BesetzteParkPlaetze);
                    break;
            }
        }

        #region Freie Parkplätze
        private string _anzahlFreieParkplaetze;
        public string AnzahlFreieParkplaetze
        {
            get => _anzahlFreieParkplaetze;
            set
            {
                _anzahlFreieParkplaetze = value;
                OnPropertyChanged(nameof(AnzahlFreieParkplaetze));
            }
        }

        private string _anzahlFreieParkplaetzeSoll;
        public string AnzahlFreieParkplaetzeSoll
        {
            get => _anzahlFreieParkplaetzeSoll;
            set
            {
                _anzahlFreieParkplaetzeSoll = value;
                OnPropertyChanged(nameof(AnzahlFreieParkplaetzeSoll));
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