using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PaternosterLager.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Paternosterlager _paternosterlager;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Paternosterlager pa, double anzahlKisten)
        {
            _mainWindow = mw;
            _paternosterlager = pa;

            IstPosition = "00";
            SollPosition = "00";

            for (var i = 0; i < 100; i++)
            {
                ClkMode.Add(ClickMode.Press);
                SichtbarEin.Add(Visibility.Hidden);
                SichtbarAus.Add(Visibility.Visible);
            }

            AlleKettengliedRegale = new ObservableCollection<KettengliedRegal>();
            for (var i = 0; i < anzahlKisten; i++) AlleKettengliedRegale.Add(new KettengliedRegal(i, anzahlKisten));

            SpsSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            _paternosterlager.GesamtLaenge = AlleKettengliedRegale[0].GetGesamtLaenge();

            while (true)
            {
                IstPosition = _paternosterlager.IstPos.ToString("D2");
                SollPosition = _paternosterlager.SollPos.ToString("D2");

                SichtbarkeitUmschalten(_paternosterlager.B1, 1);
                SichtbarkeitUmschalten(_paternosterlager.B2, 2);

                if (_mainWindow.FensterAktiv)
                {
                    _mainWindow.Dispatcher.Invoke(() =>
                               {
                                   if (_mainWindow.FensterAktiv) _mainWindow.ZeichenFlaeche.Children.Clear();
                                   foreach (var kettengliedRegal in AlleKettengliedRegale) kettengliedRegal.Zeichnen(_mainWindow, _paternosterlager.Position);
                               });
                }

                if (_mainWindow.Plc != null)
                {
                    SpsVersionLokal = _mainWindow.VersionInfoLokal;
                    SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                    SpsSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    SpsColor = _mainWindow.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(100);
            }
            // ReSharper disable once FunctionNeverReturns
        }
       
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
            var asciiCode = ascii[0];

            if (tasterId == 99)
            {
                _paternosterlager.Position = 0;
            }
            else
            {
                _paternosterlager.Zeichen = gedrueckt ? asciiCode : ' ';
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

        #region KettengliederRegale
        private ObservableCollection<KettengliedRegal> _alleKettengliedRegale = new();
        public ObservableCollection<KettengliedRegal> AlleKettengliedRegale
        {
            get => _alleKettengliedRegale;
            set
            {
                _alleKettengliedRegale = value;
                OnPropertyChanged(nameof(AlleKettengliedRegale));
            }
        }
        #endregion KettengliederRegale
        

        #region Position
        private string _istPosition;
        public string IstPosition
        {
            get => _istPosition;
            set
            {
                _istPosition = value;
                OnPropertyChanged(nameof(IstPosition));
            }
        }

        private string _sollPosition;
        public string SollPosition
        {
            get => _sollPosition;
            set
            {
                _sollPosition = value;
                OnPropertyChanged(nameof(SollPosition));
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