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

            for (var i = 0; i < 100; i++) FarbeSensor.Add(Brushes.LightGray);
            for (var i = 0; i < 100; i++) AutoSichtbar.Add(Visibility.Visible);

            ColorP0 = Brushes.LightGray;
            ClickModeBtnZufall = ClickMode.Press;

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
                    AutoSichtbar[i] = BitMaskierenArray(_parkhaus.BesetzteParkPlaetze, i) ? Visibility.Visible : Visibility.Hidden;

                    FarbeSensor[i] = AutoSichtbar[i] == Visibility.Visible ? Brushes.Red : Brushes.LawnGreen;
                }

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


        internal bool BitMaskierenArray(byte[] besetzteParkPlaetzte, int i)
        {
            var ibyte = i / 8;
            var bitMuster = (byte)(1 << i % 8);

            return (besetzteParkPlaetzte[ibyte] & bitMuster) == bitMuster;
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



        internal void TasterZufall()
        {
            if (ClickModeButtonZufall())
            {
                // eine neue Anordnung der Auto errechnen
                _random.NextBytes(_parkhaus.BesetzteParkPlaetze);
            }
        }


        public bool ClickModeButtonZufall()
        {
            if (ClickModeBtnZufall == ClickMode.Press)
            {
                ClickModeBtnZufall = ClickMode.Release;
                return true;
            }

            ClickModeBtnZufall = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnZufall;
        public ClickMode ClickModeBtnZufall
        {
            get => _clickModeBtnZufall;
            set
            {
                _clickModeBtnZufall = value;
                OnPropertyChanged(nameof(ClickModeBtnZufall));
            }
        }


        internal void ClickAuto(object auto)
        {
            if (!(auto is string nrAuto)) return;

            var autoNummer = Convert.ToInt32(nrAuto);
            AutoSichtbar[autoNummer] = AutoSichtbar[autoNummer] == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }


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

        private ObservableCollection<Visibility> _autoSichtbar = new();
        public ObservableCollection<Visibility> AutoSichtbar
        {
            get => _autoSichtbar;
            set
            {
                _autoSichtbar = value;
                OnPropertyChanged(nameof(AutoSichtbar));
            }
        }



        private ObservableCollection<Brush> _farbeSensor = new();
        public ObservableCollection<Brush> FarbeSensor
        {
            get => _farbeSensor;
            set
            {
                _farbeSensor = value;
                OnPropertyChanged(nameof(FarbeSensor));
            }
        }






        #region ClickModeAlleButtons

        // ReSharper disable once UnusedMember.Global
        public bool ClickModeButton(int asciiCode)
        {
            if (ClkMode[asciiCode] == ClickMode.Press)
            {
                ClkMode[asciiCode] = ClickMode.Release;
                return true;
            }

            ClkMode[asciiCode] = ClickMode.Press;
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

        #endregion ClickModeAlleButtons

        #region Color P0

        // ReSharper disable once UnusedMember.Global
        public void FarbeP0(bool val) => ColorP0 = val ? Brushes.Red : Brushes.LightGray;

        private Brush _colorP0;
        public Brush ColorP0
        {
            get => _colorP0;
            set
            {
                _colorP0 = value;
                OnPropertyChanged(nameof(ColorP0));
            }
        }

        #endregion Color P0          

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members

        public void Auto(object obj)
        {
            throw new NotImplementedException();
        }
    }
}