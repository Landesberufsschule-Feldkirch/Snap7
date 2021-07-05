using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Kata.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Kata _kata;

        public VisuAnzeigen(Model.Kata kata)
        {
            _kata = kata;

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
                SichtbarkeitUmschalten(_kata.S1, 11);
                SichtbarkeitUmschalten(_kata.S2, 12);
                SichtbarkeitUmschalten(_kata.S3, 13);
                SichtbarkeitUmschalten(_kata.S4, 14);
                SichtbarkeitUmschalten(_kata.S5, 15);
                SichtbarkeitUmschalten(_kata.S6, 16);
                SichtbarkeitUmschalten(_kata.S7, 17);
                SichtbarkeitUmschalten(_kata.S8, 18);

                FarbeUmschalten(_kata.P1, 1, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_kata.P2, 2, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_kata.P3, 3, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_kata.P4, 4, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_kata.P5, 5, Brushes.Yellow, Brushes.White);
                FarbeUmschalten(_kata.P6, 6, Brushes.Yellow, Brushes.White);
                FarbeUmschalten(_kata.P7, 7, Brushes.Red, Brushes.White);
                FarbeUmschalten(_kata.P8, 8, Brushes.Red, Brushes.White);

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
                case 11: _kata.S1 = gedrueckt; break;
                case 12: _kata.S2 = gedrueckt; break;
                case 13: _kata.S3 = !gedrueckt; break;
                case 14: _kata.S4 = !gedrueckt; break;
                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
        internal void Schalter(object id)
        {
            if (id is not string ascii) return;

            var schalterId = short.Parse(ascii);

            switch (schalterId)
            {
                case 15: _kata.S5 = !_kata.S5; break;
                case 16: _kata.S6 = !_kata.S6; break;
                case 17: _kata.S7 = !_kata.S7; break;
                case 18: _kata.S8 = !_kata.S8; break;
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