using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LAP_2010_1_Kompressoranlage.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Kompressoranlage _kompressoranlage;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Kompressoranlage ka)
        {
            _mainWindow = mw;
            _kompressoranlage = ka;

            AktuellerDruck = 2;

            for (var i = 0; i < 100; i++)
            {
                ClickModeBtn.Add(ClickMode.Press);
            }

            ColorB2 = Brushes.LawnGreen;
            ColorF1 = Brushes.LawnGreen;
            ColorP1 = Brushes.LawnGreen;
            ColorP2 = Brushes.LawnGreen;
            ColorQ1 = Brushes.LawnGreen;
            ColorQ2 = Brushes.LawnGreen;
            ColorQ3 = Brushes.LawnGreen;

            VisibilityB1Ein = Visibility.Hidden;
            VisibilityB1Aus = Visibility.Visible;

            VisibilityKurzschluss = Visibility.Hidden;

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
                FarbeB2(_kompressoranlage.B2);
                FarbeF1(_kompressoranlage.F1);
                FarbeP1(_kompressoranlage.P1);
                FarbeP2(_kompressoranlage.P2);
                FarbeQ1(_kompressoranlage.Q1);
                FarbeQ2(_kompressoranlage.Q2);
                FarbeQ3(_kompressoranlage.Q3);

                SichtbarkeitB1(_kompressoranlage.B1);

                if (_kompressoranlage.Q2 && _kompressoranlage.Q3) VisibilityKurzschluss = Visibility.Visible; else VisibilityKurzschluss = Visibility.Hidden;

                AktuellerDruck = _kompressoranlage.Druck;

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
        #region Sichtbarkeit
        public void SichtbarkeitB1(bool val)
        {
            if (val)
            {
                VisibilityB1Ein = Visibility.Visible;
                VisibilityB1Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityB1Ein = Visibility.Hidden;
                VisibilityB1Aus = Visibility.Visible;
            }
        }
        private Visibility _visibilityB1Ein;
        public Visibility VisibilityB1Ein
        {
            get => _visibilityB1Ein;
            set
            {
                _visibilityB1Ein = value;
                OnPropertyChanged(nameof(VisibilityB1Ein));
            }
        }
        private Visibility _visibilityB1Aus;
        public Visibility VisibilityB1Aus
        {
            get => _visibilityB1Aus;
            set
            {
                _visibilityB1Aus = value;
                OnPropertyChanged(nameof(VisibilityB1Aus));
            }
        }

        private Visibility _visibilityKurzschluss;
        public Visibility VisibilityKurzschluss
        {
            get => _visibilityKurzschluss;
            set
            {
                _visibilityKurzschluss = value;
                OnPropertyChanged(nameof(VisibilityKurzschluss));
            }
        }
        #endregion Sichtbarkeit

        #region Farbe
        public void FarbeB2(bool val) => ColorB2 = val ? Brushes.LawnGreen : Brushes.Red;
        private Brush _colorB2;
        public Brush ColorB2
        {
            get => _colorB2;
            set
            {
                _colorB2 = value;
                OnPropertyChanged(nameof(ColorB2));
            }
        }

        public void FarbeF1(bool val) => ColorF1 = val ? Brushes.LawnGreen : Brushes.Red;
        private Brush _colorF1;
        public Brush ColorF1
        {
            get => _colorF1;
            set
            {
                _colorF1 = value;
                OnPropertyChanged(nameof(ColorF1));
            }
        }
        public void FarbeP1(bool val) => ColorP1 = val ? Brushes.Red : Brushes.White;
        private Brush _colorP1;

        public Brush ColorP1
        {
            get => _colorP1;
            set
            {
                _colorP1 = value;
                OnPropertyChanged(nameof(ColorP1));
            }
        }
        public void FarbeP2(bool val) => ColorP2 = val ? Brushes.LawnGreen : Brushes.White;
        private Brush _colorP2;
        public Brush ColorP2
        {
            get => _colorP2;
            set
            {
                _colorP2 = value;
                OnPropertyChanged(nameof(ColorP2));
            }
        }

        public void FarbeQ1(bool val) => ColorQ1 = val ? Brushes.LawnGreen : Brushes.White;
        private Brush _colorQ1;
        public Brush ColorQ1
        {
            get => _colorQ1;
            set
            {
                _colorQ1 = value;
                OnPropertyChanged(nameof(ColorQ1));
            }
        }

        public void FarbeQ2(bool val) => ColorQ2 = val ? Brushes.LawnGreen : Brushes.White;
        private Brush _colorQ2;
        public Brush ColorQ2
        {
            get => _colorQ2;
            set
            {
                _colorQ2 = value;
                OnPropertyChanged(nameof(ColorQ2));
            }
        }

        public void FarbeQ3(bool val) => ColorQ3 = val ? Brushes.LawnGreen : Brushes.White;
        private Brush _colorQ3;
        public Brush ColorQ3
        {
            get => _colorQ3;
            set
            {
                _colorQ3 = value;
                OnPropertyChanged(nameof(ColorQ3));
            }
        }
        #endregion Farbe

        #region Aktueller Druck
        private double _aktuellerDruck;
        public double AktuellerDruck
        {
            get => _aktuellerDruck;
            set
            {
                _aktuellerDruck = value;
                OnPropertyChanged(nameof(AktuellerDruck));
            }
        }
        #endregion Aktueller Druck

        #region Schalter / Taster
        internal void Taster(object id)
        {
            if (id is not string ascii) return;

            var tasterId = short.Parse(ascii);
            var gedrueckt = ClickModeButton(tasterId);

            switch (tasterId)
            {
                case 1: _kompressoranlage.S1 = !gedrueckt; break;
                case 2: _kompressoranlage.S2 = gedrueckt; break;

                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
        }

        internal void Schalter(object id)
        {
            if (id is not string ascii) return;

            var schalterId = short.Parse(ascii);

            switch (schalterId)
            {
                case 5: _kompressoranlage.B2 = !_kompressoranlage.B2; break;
                case 6: _kompressoranlage.F1 = !_kompressoranlage.F1; break;
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

        #endregion

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}