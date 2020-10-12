using System.ComponentModel;
using System.Threading;

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

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            ClickModeBtnS1 = "Press";
            ClickModeBtnS2 = "Press";
            ClickModeBtnS3 = "Press";
            ClickModeBtnS4 = "Press";
            ClickModeBtnS5 = "Press";

            ColorCircleP1 = "LightGray";


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
                    if (_mainWindow.Plc.GetPlcModus() == "S7-1200")
                    {
                        VersionNr = _mainWindow.VersionNummer;
                        SpsVersionLokal = _mainWindow.VersionInfo;
                        SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                        SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? "hidden" : "visible";
                    }

                    SpsColor = _mainWindow.Plc.GetSpsError() ? "Red" : "LightGray";
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

        private string _spsVersionsInfoSichtbar;
        public string SpsVersionsInfoSichtbar
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

        private string _spsColor;

        public string SpsColor
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
            if (ClickModeBtnS1 == "Press")
            {
                ClickModeBtnS1 = "Release";
                return true;
            }

            ClickModeBtnS1 = "Press";
            return false;
        }

        private string _clickModeBtnS1;
        public string ClickModeBtnS1
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
            if (ClickModeBtnS2 == "Press")
            {
                ClickModeBtnS2 = "Release";
                return true;
            }

            ClickModeBtnS2 = "Press";
            return false;
        }

        private string _clickModeBtnS2;
        public string ClickModeBtnS2
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
            if (ClickModeBtnS3 == "Press")
            {
                ClickModeBtnS3 = "Release";
                return true;
            }

            ClickModeBtnS3 = "Press";
            return false;
        }

        private string _clickModeBtnS3;
        public string ClickModeBtnS3
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
            if (ClickModeBtnS4 == "Press")
            {
                ClickModeBtnS4 = "Release";
                return true;
            }

            ClickModeBtnS4 = "Press";
            return false;
        }

        private string _clickModeBtnS4;
        public string ClickModeBtnS4
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
            if (ClickModeBtnS5 == "Press")
            {
                ClickModeBtnS5 = "Release";
                return true;
            }

            ClickModeBtnS5 = "Press";
            return false;
        }

        private string _clickModeBtnS5;

        public string ClickModeBtnS5
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

        public void FarbeCircle_P1(bool val) => ColorCircleP1 = val ? "lawngreen" : "LightGray";

        private string _colorCircleP1;

        public string ColorCircleP1
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