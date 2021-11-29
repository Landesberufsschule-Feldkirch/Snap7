using LaborLinearachse.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LaborLinearachse.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Linearachse _linearachse;
        private readonly MainWindow _mainWindow;
        public VisuAnzeigen(MainWindow mw, Linearachse linearachse)
        {
            _mainWindow = mw;
            _linearachse = linearachse;

            for (var i = 0; i < 100; i++)
            {
                ClkMode.Add(ClickMode.Press);
                SichtbarEin.Add(Visibility.Hidden);
                SichtbarAus.Add(Visibility.Visible);
                Farbe.Add(Brushes.White);
            }

            PositionSchlitten = 200;

            FensterTitel = "uups";
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
                PositionSchlitten = _linearachse.PositionSchlitten;

                SichtbarkeitUmschalten(_linearachse.B1, 1);
                SichtbarkeitUmschalten(_linearachse.B2, 2);

                FarbeUmschalten(_linearachse.P1, 3, Brushes.White, Brushes.LightGray);
                FarbeUmschalten(_linearachse.P2, 4, Brushes.White, Brushes.LightGray);
                FarbeUmschalten(_linearachse.P3, 5, Brushes.Red, Brushes.LightGray);
                FarbeUmschalten(_linearachse.P4, 6, Brushes.GreenYellow, Brushes.LightGray);

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
                case 11: _linearachse.S1 = gedrueckt; break;
                case 12: _linearachse.S2 = !gedrueckt; break;
                case 13: _linearachse.S3 = gedrueckt; break;
                case 14: _linearachse.S4 = gedrueckt; break;
                case 15: _linearachse.S5 = gedrueckt; break;
                case 16: _linearachse.S6 = gedrueckt; break;
                case 17: _linearachse.S7 = gedrueckt; break;
                case 18: _linearachse.S8 = gedrueckt; break;
                case 19: _linearachse.S9 = !gedrueckt; break;
                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
        internal void Schalter(object id)
        {
            if (id is not string ascii) return;

            var schalterId = short.Parse(ascii);

            switch (schalterId)
            {
                case 20:
                    _linearachse.S10 = !_linearachse.S10;
                    _linearachse.S11 = !_linearachse.S10;
                    break;
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

        #region Schlitten
        private double _positionSchlitten;
        public double PositionSchlitten
        {
            get => _positionSchlitten;
            set
            {
                _positionSchlitten = value;
                OnPropertyChanged(nameof(PositionSchlitten));
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