using LaborLinearachse.Model;
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

            for (var i = 0; i < 20; i++) ClickModeBtn.Add(ClickMode.Press);

            PositionSchlitten = 200;

            VisibilityB1Ein = Visibility.Hidden;
            VisibilityB1Aus = Visibility.Visible;

            VisibilityB2Ein = Visibility.Visible;
            VisibilityB2Aus = Visibility.Hidden;

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
                PositionSchlitten = _linearachse.PositionSchlitten;

                SichtbarkeitB1(_linearachse.B1);
                SichtbarkeitB2(_linearachse.B2);

                FarbeP1(_linearachse.P1);
                FarbeP2(_linearachse.P2);
                FarbeP3(_linearachse.P3);
                FarbeP4(_linearachse.P4);

                if (_mainWindow.Plc != null)
                {
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

        #region Sichtbarkeit B1

        // ReSharper disable once UnusedMember.Global
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

        #endregion Sichtbarkeit B1

        #region Sichtbarkeit B2

        // ReSharper disable once UnusedMember.Global
        public void SichtbarkeitB2(bool val)
        {
            if (val)
            {
                VisibilityB2Ein = Visibility.Visible;
                VisibilityB2Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityB2Ein = Visibility.Hidden;
                VisibilityB2Aus = Visibility.Visible;
            }
        }
        private Visibility _visibilityB2Ein;
        public Visibility VisibilityB2Ein
        {
            get => _visibilityB2Ein;
            set
            {
                _visibilityB2Ein = value;
                OnPropertyChanged(nameof(VisibilityB2Ein));
            }
        }
        private Visibility _visibilityB2Aus;
        public Visibility VisibilityB2Aus
        {
            get => _visibilityB2Aus;
            set
            {
                _visibilityB2Aus = value;
                OnPropertyChanged(nameof(VisibilityB2Aus));
            }
        }

        #endregion Sichtbarkeit B2

        #region Lampen
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
        public void FarbeP2(bool val) => ColorP2 = val ? Brushes.White : Brushes.LightGray;
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
        public void FarbeP4(bool val) => ColorP4 = val ? Brushes.GreenYellow : Brushes.LightGray;
        private Brush _colorP4;
        public Brush ColorP4
        {
            get => _colorP4;
            set
            {
                _colorP4 = value;
                OnPropertyChanged(nameof(ColorP4));
            }
        }
        #endregion

        #region ClickModeAlleTaster
        internal void Taster(object id)
        {
            if (id is not string ascii) return;

            var tasterId = short.Parse(ascii);

            var gedrueckt = ClickModeButton(tasterId);

            switch (tasterId)
            {
                case 1: _linearachse.S1 = gedrueckt; break;
                case 2: _linearachse.S2 = !gedrueckt; break;
                case 3: _linearachse.S3 = gedrueckt; break;
                case 4: _linearachse.S4 = gedrueckt; break;
                case 5: _linearachse.S5 = gedrueckt; break;
                case 6: _linearachse.S6 = gedrueckt; break;
                case 7: _linearachse.S7 = gedrueckt; break;
                case 8: _linearachse.S8 = gedrueckt; break;
                case 9: _linearachse.S9 = !gedrueckt; break;
                case 10:
                    _linearachse.S10 = !gedrueckt;
                    _linearachse.S11 = !_linearachse.S10;
                    break;
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

        #endregion ClickModeAlleTaster

        #region iNotifyPeropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}