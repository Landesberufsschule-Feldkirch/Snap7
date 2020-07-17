namespace Nadeltelegraph.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
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

            SpsVersionsInfo = true;
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
                    string vInfo = mainWindow.S7_1200.GetVersion();
                    SpsVersionsInfo = mainWindow.Versionsinfo == vInfo;

                    SpsColor = mainWindow.S7_1200.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }
                
                Thread.Sleep(10);
            }
        }

        #region SPS Versionsinfo, Status und Farbe

        private bool _spsVersionsInfo;
        public bool SpsVersionsInfo
        {
            get => _spsVersionsInfo;
            set
            {
                _spsVersionsInfo = value;
                OnPropertyChanged(nameof(SpsVersionsInfo));
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

        #endregion SPS Status und Farbe

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

        public void FarbeP0(bool val) { if (val) ColorP0 = "Red"; else ColorP0 = "LightGray"; }

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

        #region Winkel Nadel 1

        public void WinkelNadel1(bool rechts, bool links)
        {
            AngleNeedle1 = 0;
            if (rechts) AngleNeedle1 = WinkelNadel;
            if (links) AngleNeedle1 = -WinkelNadel;
        }

        private int _angleNeedle1;

        public int AngleNeedle1
        {
            get => _angleNeedle1;
            set
            {
                _angleNeedle1 = value;
                OnPropertyChanged(nameof(AngleNeedle1));
            }
        }

        #endregion Winkel Nadel 1

        #region Winkel Nadel 2

        public void WinkelNadel2(bool rechts, bool links)
        {
            AngleNeedle2 = 0;
            if (rechts) AngleNeedle2 = WinkelNadel;
            if (links) AngleNeedle2 = -WinkelNadel;
        }

        private int _angleNeedle2;

        public int AngleNeedle2
        {
            get => _angleNeedle2;
            set
            {
                _angleNeedle2 = value;
                OnPropertyChanged(nameof(AngleNeedle2));
            }
        }

        #endregion Winkel Nadel 2

        #region Winkel Nadel 3

        public void WinkelNadel3(bool rechts, bool links)
        {
            AngleNeedle3 = 0;
            if (rechts) AngleNeedle3 = WinkelNadel;
            if (links) AngleNeedle3 = -WinkelNadel;
        }

        private int _angleNeedle3;

        public int AngleNeedle3
        {
            get => _angleNeedle3;
            set
            {
                _angleNeedle3 = value;
                OnPropertyChanged(nameof(AngleNeedle3));
            }
        }

        #endregion Winkel Nadel 3

        #region Winkel Nadel 4

        public void WinkelNadel4(bool rechts, bool links)
        {
            AngleNeedle4 = 0;
            if (rechts) AngleNeedle4 = WinkelNadel;
            if (links) AngleNeedle4 = -WinkelNadel;
        }

        private int _angleNeedle4;

        public int AngleNeedle4
        {
            get => _angleNeedle4;
            set
            {
                _angleNeedle4 = value;
                OnPropertyChanged(nameof(AngleNeedle4));
            }
        }

        #endregion Winkel Nadel 4

        #region Winkel Nadel 5

        public void WinkelNadel5(bool rechts, bool links)
        {
            AngleNeedle5 = 0;
            if (rechts) AngleNeedle5 = WinkelNadel;
            if (links) AngleNeedle5 = -WinkelNadel;
        }

        private int _angleNeedle5;

        public int AngleNeedle5
        {
            get => _angleNeedle5;
            set
            {
                _angleNeedle5 = value;
                OnPropertyChanged(nameof(AngleNeedle5));
            }
        }

        #endregion Winkel Nadel 5

        #region Breite 1 UpRight

        public void Breite1UpRight(bool val) { if (val) Width1UpRight = BreiteBreit; else Width1UpRight = BreiteSchmal; }

        private int _width1UpRight;

        public int Width1UpRight
        {
            get => _width1UpRight;
            set
            {
                _width1UpRight = value;
                OnPropertyChanged(nameof(Width1UpRight));
            }
        }

        #endregion Breite 1 UpRight

        #region Breite 2 UpRight

        public void Breite2UpRight(bool val) { if (val) Width2UpRight = BreiteBreit; else Width2UpRight = BreiteSchmal; }

        private int _width2UpRight;

        public int Width2UpRight
        {
            get => _width2UpRight;
            set
            {
                _width2UpRight = value;
                OnPropertyChanged(nameof(Width2UpRight));
            }
        }

        #endregion Breite 2 UpRight

        #region Breite 3 UpRight

        public void Breite3UpRight(bool val) { if (val) Width3UpRight = BreiteBreit; else Width3UpRight = BreiteSchmal; }

        private int _width3UpRight;

        public int Width3UpRight
        {
            get => _width3UpRight;
            set
            {
                _width3UpRight = value;
                OnPropertyChanged(nameof(Width3UpRight));
            }
        }

        #endregion Breite 3 UpRight

        #region Breite 4 UpRight

        public void Breite4UpRight(bool val) { if (val) Width4UpRight = BreiteBreit; else Width4UpRight = BreiteSchmal; }

        private int _width4UpRight;

        public int Width4UpRight
        {
            get => _width4UpRight;
            set
            {
                _width4UpRight = value;
                OnPropertyChanged(nameof(Width4UpRight));
            }
        }

        #endregion Breite 4 UpRight

        #region Breite 2 UpLeft

        public void Breite2UpLeft(bool val) { if (val) Width2UpLeft = BreiteBreit; else Width2UpLeft = BreiteSchmal; }

        private int _width2UpLeft;

        public int Width2UpLeft
        {
            get => _width2UpLeft;
            set
            {
                _width2UpLeft = value;
                OnPropertyChanged(nameof(Width2UpLeft));
            }
        }

        #endregion Breite 2 UpLeft

        #region Breite 3 UpLeft

        public void Breite3UpLeft(bool val) { if (val) Width3UpLeft = BreiteBreit; else Width3UpLeft = BreiteSchmal; }

        private int _width3UpLeft;

        public int Width3UpLeft
        {
            get => _width3UpLeft;
            set
            {
                _width3UpLeft = value;
                OnPropertyChanged(nameof(Width3UpLeft));
            }
        }

        #endregion Breite 3 UpLeft

        #region Breite 4 UpLeft

        public void Breite4UpLeft(bool val) { if (val) Width4UpLeft = BreiteBreit; else Width4UpLeft = BreiteSchmal; }

        private int _width4UpLeft;

        public int Width4UpLeft
        {
            get => _width4UpLeft;
            set
            {
                _width4UpLeft = value;
                OnPropertyChanged(nameof(Width4UpLeft));
            }
        }

        #endregion Breite 4 UpLeft

        #region Breite 5 UpLeft

        public void Breite5UpLeft(bool val) { if (val) Width5UpLeft = BreiteBreit; else Width5UpLeft = BreiteSchmal; }

        private int _width5UpLeft;

        public int Width5UpLeft
        {
            get => _width5UpLeft;
            set
            {
                _width5UpLeft = value;
                OnPropertyChanged(nameof(Width5UpLeft));
            }
        }

        #endregion Breite 5 UpLeft

        #region Breite 1 DownRight

        public void Breite1DownRight(bool val) { if (val) Width1DownRight = BreiteBreit; else Width1DownRight = BreiteSchmal; }

        private int _width1DownRight;

        public int Width1DownRight
        {
            get => _width1DownRight;
            set
            {
                _width1DownRight = value;
                OnPropertyChanged(nameof(Width1DownRight));
            }
        }

        #endregion Breite 1 DownRight

        #region Breite 2 DownRight

        public void Breite2DownRight(bool val) { if (val) Width2DownRight = BreiteBreit; else Width2DownRight = BreiteSchmal; }

        private int _width2DownRight;

        public int Width2DownRight
        {
            get => _width2DownRight;
            set
            {
                _width2DownRight = value;
                OnPropertyChanged(nameof(Width2DownRight));
            }
        }

        #endregion Breite 2 DownRight

        #region Breite 3 DownRight

        public void Breite3DownRight(bool val) { if (val) Width3DownRight = BreiteBreit; else Width3DownRight = BreiteSchmal; }

        private int _width3DownRight;

        public int Width3DownRight
        {
            get => _width3DownRight;
            set
            {
                _width3DownRight = value;
                OnPropertyChanged(nameof(Width3DownRight));
            }
        }

        #endregion Breite 3 DownRight

        #region Breite 4 DownRight

        public void Breite4DownRight(bool val) { if (val) Width4DownRight = BreiteBreit; else Width4DownRight = BreiteSchmal; }

        private int _width4DownRight;

        public int Width4DownRight
        {
            get => _width4DownRight;
            set
            {
                _width4DownRight = value;
                OnPropertyChanged(nameof(Width4DownRight));
            }
        }

        #endregion Breite 4 DownRight

        #region Breite 2 DownLeft

        public void Breite2DownLeft(bool val) { if (val) Width2DownLeft = BreiteBreit; else Width2DownLeft = BreiteSchmal; }

        private int _width2DownLeft;

        public int Width2DownLeft
        {
            get => _width2DownLeft;
            set
            {
                _width2DownLeft = value;
                OnPropertyChanged(nameof(Width2DownLeft));
            }
        }

        #endregion Breite 2 DownLeft

        #region Breite 3 DownLeft

        public void Breite3DownLeft(bool val) { if (val) Width3DownLeft = BreiteBreit; else Width3DownLeft = BreiteSchmal; }

        private int _width3DownLeft;

        public int Width3DownLeft
        {
            get => _width3DownLeft;
            set
            {
                _width3DownLeft = value;
                OnPropertyChanged(nameof(Width3DownLeft));
            }
        }

        #endregion Breite 3 DownLeft

        #region Breite 4 DownLeft

        public void Breite4DownLeft(bool val) { if (val) Width4DownLeft = BreiteBreit; else Width4DownLeft = BreiteSchmal; }

        private int _width4DownLeft;

        public int Width4DownLeft
        {
            get => _width4DownLeft;
            set
            {
                _width4DownLeft = value;
                OnPropertyChanged(nameof(Width4DownLeft));
            }
        }

        #endregion Breite 4 DownLeft

        #region Breite 5 DownLeft

        public void Breite5DownLeft(bool val) { if (val) Width5DownLeft = BreiteBreit; else Width5DownLeft = BreiteSchmal; }

        private int _width5DownLeft;

        public int Width5DownLeft
        {
            get => _width5DownLeft;
            set
            {
                _width5DownLeft = value;
                OnPropertyChanged(nameof(Width5DownLeft));
            }
        }

        #endregion Breite 5 DownLeft


        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}