using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StiegenhausBeleuchtung.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.StiegenhausBeleuchtung _stiegenhausBeleuchtung;
        private readonly MainWindow _mainWindow;
        private bool _bewegungAktiv;

        public VisuAnzeigen(MainWindow mw, Model.StiegenhausBeleuchtung stiegenhaus)
        {
            _mainWindow = mw;
            _stiegenhausBeleuchtung = stiegenhaus;

            ReiseStart = "sadf:-";
            ReiseZiel = "sadf:-";

            for (var i = 0; i < 100; i++) ClickMode.Add(System.Windows.Controls.ClickMode.Press);
            for (var i = 0; i < 100; i++) ColorLampe.Add(Brushes.Yellow);

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
                for (var i = 0; i < 100; i++) FarbeAlleLampen(i, _stiegenhausBeleuchtung.GetLampen(i));

                if (_bewegungAktiv)
                {
                    for (var i = 0; i < 100; i++)
                    {
                        if (_stiegenhausBeleuchtung.GetBewegungsmelder(i)) System.Windows.Controls.ClkMode[i] = System.Windows.Controls.ClickMode.Release; else System.Windows.Controls.ClkMode[i] = System.Windows.Controls.ClickMode.Press;
                    }
                }

                _bewegungAktiv = _stiegenhausBeleuchtung.JobAktiv;

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

        #region ReiseStart

        private string _reiseStart;

        public string ReiseStart
        {
            get => _reiseStart;
            set
            {
                var v = value.Split(':');
                _reiseStart = v[1].Trim();
                OnPropertyChanged(nameof(ReiseStart));
            }
        }

        #endregion ReiseStart

        #region ReiseZiel

        private string _reiseZiel;

        public string ReiseZiel
        {
            get => _reiseZiel;
            set
            {
                var v = value.Split(':');
                _reiseZiel = v[1].Trim();
                OnPropertyChanged(nameof(ReiseZiel));
            }
        }

        #endregion ReiseZiel

        #region FarbeAlleLampen

        public void FarbeAlleLampen(int lampe, bool val) => ColorLampe[lampe] = val ? Brushes.Yellow : Brushes.White;

        private ObservableCollection<Brush> _colorLampe = new();
        public ObservableCollection<Brush> ColorLampe
        {
            get => _colorLampe;
            set
            {
                _colorLampe = value;
                OnPropertyChanged(nameof(ColorLampe));
            }
        }

        #endregion FarbeAlleLampen

        #region ClickModeAlleButtons

        public bool ClickModeButton(int bewegungsmelder)
        {
            if (System.Windows.Controls.ClkMode[bewegungsmelder] == System.Windows.Controls.ClickMode.Press)
            {
                System.Windows.Controls.ClkMode[bewegungsmelder] = System.Windows.Controls.ClickMode.Release;
                return true;
            }

            System.Windows.Controls.ClkMode[bewegungsmelder] = System.Windows.Controls.ClickMode.Press;
            return false;
        }

        private ObservableCollection<ClickMode> _clickMode = new();

        public ObservableCollection<ClickMode> ClickMode
        {
            get => _clickMode;
            set
            {
                _clickMode = value;
                OnPropertyChanged(nameof(ClickMode));
            }
        }

        #endregion ClickModeAlleButtons

        #region BtnBewegungsmelder

        internal void BtnBewegungsmelder(object buttonName)
        {
            if (buttonName is int bewegungsmelder)
            {
                _stiegenhausBeleuchtung.SetBewegungsmelder(bewegungsmelder, ClickModeButton(bewegungsmelder));
            }
        }

        #endregion BtnBewegungsmelder

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}