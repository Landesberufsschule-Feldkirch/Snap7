using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Schleifmaschine.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Schleifmaschine _schleifmaschine;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Schleifmaschine schleifmaschine)
        {
            _mainWindow = mw;
            _schleifmaschine = schleifmaschine;

            SchleifmaschineDrehzahl = "n=0";

            for (var i = 0; i < 20; i++) ClickMode.Add(System.Windows.Controls.ClickMode.Press);

            WinkelSchleifmaschine = 10;
            AktuelleDrehzahl = 0;

            ColorThermorelaisF1 = Brushes.Lavender;
            ColorThermorelaisF2 = Brushes.Lavender;
            ColorTasterS3 = Brushes.Lavender;

            VisUebersynchron = Visibility.Hidden;

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
                WinkelSchleifmaschine = _schleifmaschine.WinkelSchleifmaschine;

                FarbeTherorelais_F1(_schleifmaschine.F1);
                FarbeTherorelais_F2(_schleifmaschine.F2);

                FarbeP1(_schleifmaschine.P1);
                FarbeP2(_schleifmaschine.P2);
                FarbeP3(_schleifmaschine.P3);
                FarbeTasterS3(_schleifmaschine.S3);

                AktuelleDrehzahl = _schleifmaschine.DrehzahlSchleifmaschine;
                SchleifmaschineDrehzahl = "n=" + _schleifmaschine.DrehzahlSchleifmaschine;

                VisUebersynchron = _schleifmaschine.B1 ? Visibility.Visible : Visibility.Hidden;

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

        private Visibility _visUebersynchron;
        public Visibility VisUebersynchron
        {
            get => _visUebersynchron;
            set
            {
                _visUebersynchron = value;
                OnPropertyChanged(nameof(VisUebersynchron));
            }
        }

        private double _schleifmaschineDrehzahl;
        public string SchleifmaschineDrehzahl
        {
            get => "n=" + _schleifmaschineDrehzahl;
            set
            {
                _schleifmaschineDrehzahl = Math.Floor(Convert.ToDouble(value.Substring(2)));
                OnPropertyChanged(nameof(SchleifmaschineDrehzahl));
            }
        }


        private double _winkelSchleifmaschine;
        public double WinkelSchleifmaschine
        {
            get => _winkelSchleifmaschine;
            set
            {
                _winkelSchleifmaschine = value;
                OnPropertyChanged(nameof(WinkelSchleifmaschine));
            }
        }

        private double _aktuelleDrehzahl;
        public double AktuelleDrehzahl
        {
            get => _aktuelleDrehzahl;
            set
            {
                _aktuelleDrehzahl = value;
                OnPropertyChanged(nameof(AktuelleDrehzahl));
            }
        }


        public void FarbeTasterS3(bool val) => ColorTasterS3 = val ? Brushes.LawnGreen : Brushes.Red;

        private Brush _colorTasterS3;
        public Brush ColorTasterS3
        {
            get => _colorTasterS3;
            set
            {
                _colorTasterS3 = value;
                OnPropertyChanged(nameof(ColorTasterS3));
            }
        }

        public void FarbeTherorelais_F1(bool val) => ColorThermorelaisF1 = val ? Brushes.LawnGreen : Brushes.Red;

        private Brush _colorThermorelaisF1;

        public Brush ColorThermorelaisF1
        {
            get => _colorThermorelaisF1;
            set
            {
                _colorThermorelaisF1 = value;
                OnPropertyChanged(nameof(ColorThermorelaisF1));
            }
        }


        public void FarbeTherorelais_F2(bool val) => ColorThermorelaisF2 = val ? Brushes.LawnGreen : Brushes.Red;

        private Brush _colorThermorelaisF2;

        public Brush ColorThermorelaisF2
        {
            get => _colorThermorelaisF2;
            set
            {
                _colorThermorelaisF2 = value;
                OnPropertyChanged(nameof(ColorThermorelaisF2));
            }
        }

        public void FarbeP1(bool val) => ColorP1 = val ? Brushes.White : Brushes.LightGray;

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

        public void FarbeP2(bool val) => ColorP2 = val ? Brushes.LawnGreen : Brushes.LightGray;

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

        public void FarbeP3(bool val) => ColorP3 = val ? Brushes.Red : Brushes.LightGray;

        private Brush _colorP3;
        public Brush ColorP3
        {
            get => _colorP3;
            set
            {
                _colorP3 = value;
                OnPropertyChanged(nameof(ColorP3));
            }
        }

        internal void Taster(object id)
        {
            if (id is not string ascii) return;

            var tasterId = short.Parse(ascii);

            var gedrueckt = ClickModeButton(tasterId);

            switch (tasterId)
            {
                case 0: _schleifmaschine.S0 = !gedrueckt; break;
                case 1: _schleifmaschine.S1 = gedrueckt; break;
                case 2: _schleifmaschine.S2 = gedrueckt; break;
                case 4:
                    _schleifmaschine.S4 = gedrueckt;
                    _schleifmaschine.B1 = false;
                    break;
            }
        }

        public bool ClickModeButton(int tasterId)
        {
            if (System.Windows.Controls.ClkMode[tasterId] == System.Windows.Controls.ClickMode.Press)
            {
                System.Windows.Controls.ClkMode[tasterId] = System.Windows.Controls.ClickMode.Release;
                return true;
            }

            System.Windows.Controls.ClkMode[tasterId] = System.Windows.Controls.ClickMode.Press;
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


        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}