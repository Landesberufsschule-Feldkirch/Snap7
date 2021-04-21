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

            for (var i = 0; i < 100; i++) ClickModeBtn.Add(ClickMode.Press);
            for (var i = 0; i < 100; i++) ColorLampe.Add(Brushes.Yellow);

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = Visibility.Hidden;
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
                        if (_stiegenhausBeleuchtung.GetBewegungsmelder(i)) ClickModeBtn[i] = ClickMode.Release; else ClickModeBtn[i] = ClickMode.Press;
                    }
                }

                _bewegungAktiv = _stiegenhausBeleuchtung.JobAktiv;

                if (_mainWindow.Plc != null)
                {
                    VersionNr = _mainWindow.VersionNummer;
                    SpsVersionLokal = _mainWindow.VersionInfoLokal;
                    SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                    SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    SpsColor = _mainWindow.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        #region SPS Version, Status und Farbe

        private string _versionNr;
        public string VersionNr
        {
            get => _versionNr;
            set
            {
                _versionNr = value;
                OnPropertyChanged(nameof(VersionNr));
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

        private Visibility _spsVersionsInfoSichtbar;
        public Visibility SpsVersionsInfoSichtbar
        {
            get => _spsVersionsInfoSichtbar;
            set
            {
                _spsVersionsInfoSichtbar = value;
                OnPropertyChanged(nameof(SpsVersionsInfoSichtbar));
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
            if (ClickModeBtn[bewegungsmelder] == ClickMode.Press)
            {
                ClickModeBtn[bewegungsmelder] = ClickMode.Release;
                return true;
            }

            ClickModeBtn[bewegungsmelder] = ClickMode.Press;
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