namespace Nadeltelegraph.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;

    public partial class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly int WinkelNadel = 35;
        private readonly int BreiteBreit = 10;
        private readonly int BreiteSchmal = 1;
        private readonly Model.Nadeltelegraph nadeltelegraph;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Nadeltelegraph nt)
        {
            mainWindow = mw;
            nadeltelegraph = nt;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SPSVersionLokal = "fehlt";
            SPSVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            for (int i = 0; i < 100; i++) ClickModeBtn.Add("Press");

            ColorP0 = "LightGray";

            AngleNeedle1 = 0;
            AngleNeedle2 = 0;
            AngleNeedle3 = 0;
            AngleNeedle4 = 0;
            AngleNeedle5 = 0;

            Width1UpRight = 1;
            Width2UpRight = 1;
            Width3UpRight = 1;
            Width4UpRight = 1;

            Width2UpLeft = 1;
            Width3UpLeft = 1;
            Width4UpLeft = 1;
            Width5UpLeft = 1;

            Width1DownRight = 1;
            Width2DownRight = 1;
            Width3DownRight = 1;
            Width4DownRight = 1;

            Width2DownLeft = 1;
            Width3DownLeft = 1;
            Width4DownLeft = 1;
            Width5DownLeft = 1;

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        internal void Buchstabe(object buchstabe)
        {
            if (buchstabe is string ascii)
            {
                var asciiCode = ascii[0];
                if (ClickModeButton(asciiCode)) nadeltelegraph.Zeichen = asciiCode; else nadeltelegraph.Zeichen = ' ';
            }
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeP0(nadeltelegraph.P0);

                Breite1UpRight(nadeltelegraph.P1R);
                Breite2UpRight(nadeltelegraph.P2R);
                Breite3UpRight(nadeltelegraph.P3R);
                Breite4UpRight(nadeltelegraph.P4R);

                Breite1DownRight(nadeltelegraph.P1L);
                Breite2DownRight(nadeltelegraph.P2L);
                Breite3DownRight(nadeltelegraph.P3L);
                Breite4DownRight(nadeltelegraph.P4L);

                Breite2UpLeft(nadeltelegraph.P2L);
                Breite3UpLeft(nadeltelegraph.P3L);
                Breite4UpLeft(nadeltelegraph.P4L);
                Breite5UpLeft(nadeltelegraph.P5L);

                Breite2DownLeft(nadeltelegraph.P2R);
                Breite3DownLeft(nadeltelegraph.P3R);
                Breite4DownLeft(nadeltelegraph.P4R);
                Breite5DownLeft(nadeltelegraph.P5R);

                WinkelNadel1(nadeltelegraph.P1R, nadeltelegraph.P1L);
                WinkelNadel2(nadeltelegraph.P2R, nadeltelegraph.P2L);
                WinkelNadel3(nadeltelegraph.P3R, nadeltelegraph.P3L);
                WinkelNadel4(nadeltelegraph.P4R, nadeltelegraph.P4L);
                WinkelNadel5(nadeltelegraph.P5R, nadeltelegraph.P5L);

                if (mainWindow.S7_1200 != null)
                {
                    SPSVersionLokal = mainWindow.VersionInfo;
                    SPSVersionEntfernt = mainWindow.S7_1200.GetVersion();
                    if (SPSVersionLokal == SPSVersionEntfernt) SpsVersionsInfoSichtbar = "hidden"; else SpsVersionsInfoSichtbar = "visible";

                    SpsColor = mainWindow.S7_1200.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
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

        private string _sPSVersionLokal;
        public string SPSVersionLokal
        {
            get => _sPSVersionLokal;
            set
            {
                _sPSVersionLokal = value;
                OnPropertyChanged(nameof(SPSVersionLokal));
            }
        }

        private string _sPSVersionEntfernt;
        public string SPSVersionEntfernt
        {
            get => _sPSVersionEntfernt;
            set
            {
                _sPSVersionEntfernt = value;
                OnPropertyChanged(nameof(SPSVersionEntfernt));
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

        #region ClickModeAlleButtons

        public bool ClickModeButton(int AsciiCode)
        {
            if (ClickModeBtn[AsciiCode] == "Press")
            {
                ClickModeBtn[AsciiCode] = "Release";
                return true;
            }
            else
            {
                ClickModeBtn[AsciiCode] = "Press";
            }
            return false;
        }

        private ObservableCollection<string> _clickModeBtn = new ObservableCollection<string>();

        public ObservableCollection<string> ClickModeBtn
        {
            get => _clickModeBtn;
            set
            {
                _clickModeBtn = value;
                OnPropertyChanged(nameof(ClickModeBtn));
            }
        }

        #endregion ClickModeAlleButtons

        #region Color P0

        public void FarbeP0(bool val)
        {
            if (val) ColorP0 = "Red"; else ColorP0 = "LightGray";
        }

        private string _colorP0;

        public string ColorP0
        {
            get => _colorP0;
            set
            {
                _colorP0 = value;
                OnPropertyChanged(nameof(ColorP0));
            }
        }

        #endregion Color P0          

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}