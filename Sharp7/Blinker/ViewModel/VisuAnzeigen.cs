using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Blinker.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly MainWindow _mainWindow;
        private readonly Model.Blinker _blinker;
        public VisuAnzeigen(MainWindow mw, Model.Blinker blinker)
        {
            _mainWindow = mw;
            _blinker = blinker;

            SpsSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            ClickModeBtnS1 = ClickMode.Press;
            ClickModeBtnS2 = ClickMode.Press;
            ClickModeBtnS3 = ClickMode.Press;
            ClickModeBtnS4 = ClickMode.Press;
            ClickModeBtnS5 = ClickMode.Press;

            ColorCircleP1 = Brushes.LightGray;

            FrequenzAnzeige = "Frequenz: 0Hz";
            TastverhaeltnisAnzeige = "Tastverhältnis: 50%";
            EinZeitAnzeige = "Einzeit: 500ms";
            AusZeitAnzeige = "Auszeit: 500ms";

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                if (_mainWindow.Plc != null)
                {
                    SpsVersionLokal = _mainWindow.VersionInfoLokal;
                    SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                    SpsSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    SpsColor = _mainWindow.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();

                    FrequenzAnzeige = "Frequenz: " + _blinker.Frequenz.ToString("F1") + "Hz";
                    TastverhaeltnisAnzeige = "Tastverhältnis: " + _blinker.Tastverhaeltnis.ToString("F1") + "%";
                    EinZeitAnzeige = "Einzeit: " + _blinker.EinZeit + "ms";
                    AusZeitAnzeige = "Auszeit: " + _blinker.AusZeit + "ms";
                }

                FarbeCircle_P1(_blinker.P1);

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void TasterS1() => _blinker.S1 = ClickModeButtonS1();
        internal void TasterS2() => _blinker.S2 = ClickModeButtonS2();
        internal void TasterS3() => _blinker.S3 = ClickModeButtonS3();
        internal void TasterS4() => _blinker.S4 = ClickModeButtonS4();
        internal void TasterS5() => _blinker.S5 = ClickModeButtonS5();

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

        #region Buttons

        public bool ClickModeButtonS1()
        {
            if (ClickModeBtnS1 == ClickMode.Press)
            {
                ClickModeBtnS1 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS1 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS1;
        public ClickMode ClickModeBtnS1
        {
            get => _clickModeBtnS1;
            set
            {
                _clickModeBtnS1 = value;
                OnPropertyChanged(nameof(ClickModeBtnS1));
            }
        }

        public bool ClickModeButtonS2()
        {
            if (ClickModeBtnS2 == ClickMode.Press)
            {
                ClickModeBtnS2 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS2 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS2;
        public ClickMode ClickModeBtnS2
        {
            get => _clickModeBtnS2;
            set
            {
                _clickModeBtnS2 = value;
                OnPropertyChanged(nameof(ClickModeBtnS2));
            }
        }

        public bool ClickModeButtonS3()
        {
            if (ClickModeBtnS3 == ClickMode.Press)
            {
                ClickModeBtnS3 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS3 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS3;
        public ClickMode ClickModeBtnS3
        {
            get => _clickModeBtnS3;
            set
            {
                _clickModeBtnS3 = value;
                OnPropertyChanged(nameof(ClickModeBtnS3));
            }
        }

        public bool ClickModeButtonS4()
        {
            if (ClickModeBtnS4 == ClickMode.Press)
            {
                ClickModeBtnS4 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS4 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS4;
        public ClickMode ClickModeBtnS4
        {
            get => _clickModeBtnS4;
            set
            {
                _clickModeBtnS4 = value;
                OnPropertyChanged(nameof(ClickModeBtnS4));
            }
        }

        public bool ClickModeButtonS5()
        {
            if (ClickModeBtnS5 == ClickMode.Press)
            {
                ClickModeBtnS5 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS5 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS5;

        public ClickMode ClickModeBtnS5
        {
            get => _clickModeBtnS5;
            set
            {
                _clickModeBtnS5 = value;
                OnPropertyChanged(nameof(ClickModeBtnS5));
            }
        }
        #endregion

        #region Circle

        public void FarbeCircle_P1(bool val) => ColorCircleP1 = val ? Brushes.LawnGreen : Brushes.LightGray;

        private Brush _colorCircleP1;

        public Brush ColorCircleP1
        {
            get => _colorCircleP1;
            set
            {
                _colorCircleP1 = value;
                OnPropertyChanged(nameof(ColorCircleP1));
            }
        }

        #endregion

        #region Texte

        private string _frequenzAnzeige;
        public string FrequenzAnzeige
        {
            get => _frequenzAnzeige;
            set
            {
                _frequenzAnzeige = value;
                OnPropertyChanged(nameof(FrequenzAnzeige));
            }
        }

        private string _tastverhaeltnisAnzeige;
        public string TastverhaeltnisAnzeige
        {
            get => _tastverhaeltnisAnzeige;
            set
            {
                _tastverhaeltnisAnzeige = value;
                OnPropertyChanged(nameof(TastverhaeltnisAnzeige));
            }
        }

        private string _einZeitAnzeige;
        public string EinZeitAnzeige
        {
            get => _einZeitAnzeige;
            set
            {
                _einZeitAnzeige = value;
                OnPropertyChanged(nameof(EinZeitAnzeige));
            }
        }

        private string _ausZeitAnzeige;
        public string AusZeitAnzeige
        {
            get => _ausZeitAnzeige;
            set
            {
                _ausZeitAnzeige = value;
                OnPropertyChanged(nameof(AusZeitAnzeige));
            }
        }
        #endregion

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}