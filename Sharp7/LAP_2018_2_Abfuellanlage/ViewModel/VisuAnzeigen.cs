using LAP_2018_2_Abfuellanlage.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Utilities;

namespace LAP_2018_2_Abfuellanlage.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Abfuellanlage _abfuellanlage;
        private readonly MainWindow _mainWindow;
        private const double HoeheFuellBalken = 9 * 35;

        public VisuAnzeigen(MainWindow mw, Abfuellanlage af)
        {
            _mainWindow = mw;
            _abfuellanlage = af;

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
                PosLinks.Add(0.0);
                PosOben.Add(0.0);
            }

            PosBilder(new Punkt(10, 10), 31);
            PosBilder(new Punkt(20, 20), 32);
            PosBilder(new Punkt(30, 30), 33);
            PosBilder(new Punkt(40, 40), 34);
            PosBilder(new Punkt(50, 50), 35);
            PosBilder(new Punkt(60, 60), 36);

            Margin1 = new Thickness(0, 30, 0, 0);

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }
        private void VisuAnzeigenTask()
        {
            while (true)
            {
                PosBilder(_abfuellanlage.AlleFlaschen[0].Flasche.GetPosition(), 31);
                PosBilder(_abfuellanlage.AlleFlaschen[1].Flasche.GetPosition(), 32);
                PosBilder(_abfuellanlage.AlleFlaschen[2].Flasche.GetPosition(), 33);
                PosBilder(_abfuellanlage.AlleFlaschen[3].Flasche.GetPosition(), 34);
                PosBilder(_abfuellanlage.AlleFlaschen[4].Flasche.GetPosition(), 35);
                PosBilder(_abfuellanlage.AlleFlaschen[5].Flasche.GetPosition(), 36);

                SichtbarkeitUmschalten(_abfuellanlage.AlleFlaschen[0].Sichtbar, 31);
                SichtbarkeitUmschalten(_abfuellanlage.AlleFlaschen[1].Sichtbar, 32);
                SichtbarkeitUmschalten(_abfuellanlage.AlleFlaschen[2].Sichtbar, 33);
                SichtbarkeitUmschalten(_abfuellanlage.AlleFlaschen[3].Sichtbar, 34);
                SichtbarkeitUmschalten(_abfuellanlage.AlleFlaschen[4].Sichtbar, 35);
                SichtbarkeitUmschalten(_abfuellanlage.AlleFlaschen[5].Sichtbar, 36);

                SichtbarkeitUmschalten(_abfuellanlage.B1, 1);
                SichtbarkeitUmschalten(_abfuellanlage.K1, 3);
                SichtbarkeitUmschalten(_abfuellanlage.K2, 4);
                SichtbarkeitUmschalten(_abfuellanlage.K1 && _abfuellanlage.Pegel > 0.01, 20);

                KisteAnzeigen(_abfuellanlage.FlaschenInDerKiste);

                FarbeUmschalten(_abfuellanlage.F1, 2, Brushes.LawnGreen, Brushes.Red);
                FarbeUmschalten(_abfuellanlage.P1, 5, Brushes.LawnGreen, Brushes.LightGray);
                FarbeUmschalten(_abfuellanlage.P2, 6, Brushes.Red, Brushes.LightGray);
                FarbeUmschalten(_abfuellanlage.Q1, 7, Brushes.LawnGreen, Brushes.LightGray);
                FarbeUmschalten(_abfuellanlage.Pegel > 0.01, 21, Brushes.Blue, Brushes.LightBlue); // Zuleitung

                Margin_1(_abfuellanlage.Pegel);

                FuellstandProzent = (100 * _abfuellanlage.Pegel).ToString("F0") + "%";

 if ( _mainWindow.PlcDaemon != null &&  _mainWindow.PlcDaemon.Plc != null)
                {
                    SpsVersionLokal = _mainWindow.VersionInfoLokal;
                    SpsVersionEntfernt = _mainWindow.PlcDaemon.Plc.GetVersion();
                    SpsSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    SpsColor = _mainWindow.PlcDaemon.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.PlcDaemon.Plc?.GetSpsStatus();

                    FensterTitel = _mainWindow.PlcDaemon.Plc.GetPlcBezeichnung() + ": " +_mainWindow.VersionInfoLokal;
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
                case 11: _abfuellanlage.S1 = gedrueckt; break;
                case 12: _abfuellanlage.S2 = !gedrueckt; break;
                case 13: _abfuellanlage.S3 = gedrueckt; break;
                case 14: _abfuellanlage.S4 = gedrueckt; break;
                case 15: _abfuellanlage.TankNachfuellen(); break;
                case 16: _abfuellanlage.AllesReset(); break;
                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
        internal void Schalter(object id)
        {
            if (id is not string ascii) return;

            var schalterId = short.Parse(ascii);

            _abfuellanlage.F1 = schalterId switch
            {
                2 => !_abfuellanlage.F1,
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }

        private void KisteAnzeigen(int anzahlFlaschen)
        {
            var alleFohrenburgerKisten = new bool[10];
            var alleMohrenKisten = new bool[10];

            for (var i = 0; i < 10; i++)
            {
                alleFohrenburgerKisten[i] = false;
                alleMohrenKisten[i] = false;
            }

            if (_abfuellanlage.AktuellesBier == Abfuellanlage.Bier.Fohrenburger)
            {
                alleFohrenburgerKisten[anzahlFlaschen] = true;
            }
            else
            {
                alleMohrenKisten[anzahlFlaschen] = true;
            }

            for (var i = 0; i < 8; i++)
            {
                SichtbarkeitUmschalten(alleFohrenburgerKisten[i], 40 + i);
                SichtbarkeitUmschalten(alleMohrenKisten[i], 50 + i);
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

        private string _fuellstandProzent;
        public string FuellstandProzent
        {
            get => _fuellstandProzent;
            set
            {
                _fuellstandProzent = value;
                OnPropertyChanged(nameof(FuellstandProzent));
            }
        }

        #region Positionen
        internal void PosBilder(Punkt pos, Index i)
        {
            PosLinks[i] = pos.X;
            PosOben[i] = pos.Y;
        }

        private ObservableCollection<double> _posOben = new();
        public ObservableCollection<double> PosOben
        {
            get => _posOben;
            set
            {
                _posOben = value;
                OnPropertyChanged(nameof(PosOben));
            }
        }

        private ObservableCollection<double> _posLinks = new();
        public ObservableCollection<double> PosLinks
        {
            get => _posLinks;
            set
            {
                _posLinks = value;
                OnPropertyChanged(nameof(PosLinks));
            }
        }

        #endregion Positionen

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
