using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace LAP_2010_5_Pumpensteuerung.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private const double HoeheFuellBalken = 9 * 35;

        private readonly MainWindow _mainWindow;
        private readonly Model.Pumpensteuerung _pumpensteuerung;

        public VisuAnzeigen(MainWindow mw, Model.Pumpensteuerung nr)
        {
            _mainWindow = mw;
            _pumpensteuerung = nr;

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

            WinkelSchalter = 0;

            Margin1 = new Thickness(0, 30, 0, 0);

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeUmschalten(_pumpensteuerung.F1, 3, Brushes.LawnGreen, Brushes.Red);
                FarbeUmschalten(_pumpensteuerung.P1, 4, Brushes.LawnGreen, Brushes.LightGray);
                FarbeUmschalten(_pumpensteuerung.P2, 5, Brushes.Red, Brushes.LightGray);

                FarbeUmschalten(_pumpensteuerung.Q1, 20, Brushes.Blue, Brushes.LightBlue);//FarbeZuleitungLinksWaagrecht
                FarbeUmschalten(_pumpensteuerung.Q1, 21, Brushes.Blue, Brushes.LightBlue);//FarbeZuleitungLinksSenkrecht
                FarbeUmschalten(_pumpensteuerung.Pegel > 0.01, 22, Brushes.Blue, Brushes.LightBlue);//FarbeAbleitungOben
                FarbeUmschalten(_pumpensteuerung.Pegel > 0.01 && _pumpensteuerung.Y1, 23, Brushes.Blue, Brushes.LightBlue);//FarbeAbleitungUnten

                SichtbarkeitUmschalten(_pumpensteuerung.B1, 1);
                SichtbarkeitUmschalten(_pumpensteuerung.B2, 2);
                SichtbarkeitUmschalten(_pumpensteuerung.Q1, 6);
                SichtbarkeitUmschalten(_pumpensteuerung.Y1, 14);

                Margin_1(_pumpensteuerung.Pegel);

                SchalterWinkel(_pumpensteuerung.S1, _pumpensteuerung.S2);

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
                case 10: _pumpensteuerung.TasterHand(); break;
                case 11: _pumpensteuerung.TasterAus(); break;
                case 12: _pumpensteuerung.TasterAutomatik(); break;
                case 13: _pumpensteuerung.S3 = !gedrueckt; break;
                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
        internal void Schalter(object id)
        {
            if (id is not string ascii) return;

            var schalterId = short.Parse(ascii);

            switch (schalterId)
            {
                case 3: _pumpensteuerung.F1 = !_pumpensteuerung.F1; break;
                case 14: _pumpensteuerung.Y1 = !_pumpensteuerung.Y1; break;
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

        #region SchalterWinkel
        private void SchalterWinkel(bool s1, bool s2)
        {
            WinkelSchalter = 0;
            if (s1) WinkelSchalter = -45;
            if (s2) WinkelSchalter = 45;
        }

        private int _winkelSchalter;
        public int WinkelSchalter
        {
            get => _winkelSchalter;
            set
            {
                _winkelSchalter = value;
                OnPropertyChanged(nameof(WinkelSchalter));
            }
        }
        #endregion SchalterWinkel

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