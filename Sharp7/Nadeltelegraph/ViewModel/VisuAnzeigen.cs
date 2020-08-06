namespace Nadeltelegraph.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;

    public partial class VisuAnzeigen : INotifyPropertyChanged
    {
        private const int WinkelNadel = 35;
        private const int BreiteBreit = 10;
        private const int BreiteSchmal = 1;
        private readonly Model.Nadeltelegraph _nadeltelegraph;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Nadeltelegraph nt)
        {
            _mainWindow = mw;
            _nadeltelegraph = nt;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            for (var i = 0; i < 100; i++) ClickModeBtn.Add("Press");
            for (var i = 0; i < 10; i++) AlleWinkel.Add(0);

            ColorP0 = "LightGray";

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
                _nadeltelegraph.Zeichen = ClickModeButton(asciiCode) ? asciiCode : ' ';
            }
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeP0(_nadeltelegraph.P0);

                Breite1UpRight(_nadeltelegraph.P1R);
                Breite2UpRight(_nadeltelegraph.P2R);
                Breite3UpRight(_nadeltelegraph.P3R);
                Breite4UpRight(_nadeltelegraph.P4R);

                Breite1DownRight(_nadeltelegraph.P1L);
                Breite2DownRight(_nadeltelegraph.P2L);
                Breite3DownRight(_nadeltelegraph.P3L);
                Breite4DownRight(_nadeltelegraph.P4L);

                Breite2UpLeft(_nadeltelegraph.P2L);
                Breite3UpLeft(_nadeltelegraph.P3L);
                Breite4UpLeft(_nadeltelegraph.P4L);
                Breite5UpLeft(_nadeltelegraph.P5L);

                Breite2DownLeft(_nadeltelegraph.P2R);
                Breite3DownLeft(_nadeltelegraph.P3R);
                Breite4DownLeft(_nadeltelegraph.P4R);
                Breite5DownLeft(_nadeltelegraph.P5R);

                WinkelEinstellen(_nadeltelegraph.P1R, _nadeltelegraph.P1L, 1);
                WinkelEinstellen(_nadeltelegraph.P2R, _nadeltelegraph.P2L,2);
                WinkelEinstellen(_nadeltelegraph.P3R, _nadeltelegraph.P3L,3);
                WinkelEinstellen(_nadeltelegraph.P4R, _nadeltelegraph.P4L,4);
                WinkelEinstellen(_nadeltelegraph.P5R, _nadeltelegraph.P5L,5);

                if (_mainWindow.S71200 != null)
                {
                    SpsVersionLokal = _mainWindow.VersionInfo;
                    SpsVersionEntfernt = _mainWindow.S71200.GetVersion();
                    SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? "hidden" : "visible";

                    SpsColor = _mainWindow.S71200.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = _mainWindow.S71200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
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

        #region ClickModeAlleButtons

        public bool ClickModeButton(int asciiCode)
        {
            if (ClickModeBtn[asciiCode] == "Press")
            {
                ClickModeBtn[asciiCode] = "Release";
                return true;
            }
            else
            {
                ClickModeBtn[asciiCode] = "Press";
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
            ColorP0 = val ? "Red" : "LightGray";
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