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

            SpsVersionsInfoSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            for (var i = 0; i < 100; i++) ClickModeBtn.Add(ClickMode.Press);
            for (var i = 0; i < 100; i++) VisibilityEin.Add(Visibility.Hidden);
            for (var i = 0; i < 100; i++) VisibilityAus.Add(Visibility.Visible);
            for (var i = 0; i < 100; i++) ColorLampe.Add(Brushes.Aqua);

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                SichtbarkeitKontakt(_kata.S1, 1);
                SichtbarkeitKontakt(_kata.S2, 2);
                SichtbarkeitKontakt(_kata.S3, 3);
                SichtbarkeitKontakt(_kata.S4, 4);
                SichtbarkeitKontakt(_kata.S5, 5);
                SichtbarkeitKontakt(_kata.S6, 6);
                SichtbarkeitKontakt(_kata.S7, 7);
                SichtbarkeitKontakt(_kata.S8, 8);

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

        internal void SichtbarkeitKontakt(bool val, int i)
        {
            VisibilityEin[i] = val ? Visibility.Visible : Visibility.Hidden;
            VisibilityAus[i] = val ? Visibility.Hidden : Visibility.Visible;
        }

        private ObservableCollection<Visibility> _visibilityEin = new();
        public ObservableCollection<Visibility> VisibilityEin
        {
            get => _visibilityEin;
            set
            {
                _visibilityEin = value;
                OnPropertyChanged(nameof(VisibilityEin));
            }
        }

        private ObservableCollection<Visibility> _visibilityAus = new();
        public ObservableCollection<Visibility> VisibilityAus
        {
            get => _visibilityAus;
            set
            {
                _visibilityAus = value;
                OnPropertyChanged(nameof(VisibilityAus));
            }
        }

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

        internal void Taster(object id)
        {
            if (id is not string ascii) return;

            var tasterId = short.Parse(ascii);
            var gedrueckt = ClickModeButton(tasterId);

            switch (tasterId)
            {
                case 1: _kata.S1 = gedrueckt; break;
                case 2: _kata.S2 = gedrueckt; break;
                case 3: _kata.S3 = !gedrueckt; break;
                case 4: _kata.S4 = !gedrueckt; break;
                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
        }

        internal void Schalter(object id)
        {
            if (id is not string ascii) return;

            var schalterId = short.Parse(ascii);

            switch (schalterId)
            {
                case 5: _kata.S5 = !_kata.S5; break;
                case 6: _kata.S6 = !_kata.S5; break;
                case 7: _kata.S7 = !_kata.S5; break;
                case 8: _kata.S8 = !_kata.S5; break;
                default: throw new ArgumentOutOfRangeException(nameof(id)); 
            }
        }
        public bool ClickModeButton(int tasterId)
        {
            if (ClickModeBtn[tasterId] == ClickMode.Press)
            {
                ClickModeBtn[tasterId] = ClickMode.Release;
                return true;
            }

            ClickModeBtn[tasterId] = ClickMode.Press;
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

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}