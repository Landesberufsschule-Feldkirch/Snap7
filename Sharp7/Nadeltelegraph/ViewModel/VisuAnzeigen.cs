using System.Windows;

namespace Nadeltelegraph.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Nadeltelegraph _nadeltelegraph;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Nadeltelegraph nt)
        {
            _mainWindow = mw;
            _nadeltelegraph = nt;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            for (var i = 0; i < 100; i++) ClickModeBtn.Add("Press");
            for (var i = 0; i < 10; i++) AlleWinkel.Add(0);
            for (var i = 0; i < 100; i++) AlleBreiten.Add(0);

            ColorP0 = "LightGray";

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        internal void Buchstabe(object buchstabe)
        {
            if (!(buchstabe is string ascii)) return;
            var asciiCode = ascii[0];
            _nadeltelegraph.Zeichen = ClickModeButton(asciiCode) ? asciiCode : ' ';
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeP0(_nadeltelegraph.P0);

                _nadeltelegraph.AlleZeiger[1].SetPosition(_nadeltelegraph.P1R, _nadeltelegraph.P1L);
                _nadeltelegraph.AlleZeiger[2].SetPosition(_nadeltelegraph.P2R, _nadeltelegraph.P2L);
                _nadeltelegraph.AlleZeiger[3].SetPosition(_nadeltelegraph.P3R, _nadeltelegraph.P3L);
                _nadeltelegraph.AlleZeiger[4].SetPosition(_nadeltelegraph.P4R, _nadeltelegraph.P4L);
                _nadeltelegraph.AlleZeiger[5].SetPosition(_nadeltelegraph.P5R, _nadeltelegraph.P5L);

                for (var i = 1; i < 6; i++)
                {
                    AlleWinkel[i] = _nadeltelegraph.AlleZeiger[i].GetWinkel();
                    AlleBreiten[10 + i] = _nadeltelegraph.AlleZeiger[i].GetBreiteUpLeft();
                    AlleBreiten[20 + i] = _nadeltelegraph.AlleZeiger[i].GetBreiteUpRight();
                    AlleBreiten[30 + i] = _nadeltelegraph.AlleZeiger[i].GetBreiteDownLeft();
                    AlleBreiten[40 + i] = _nadeltelegraph.AlleZeiger[i].GetBreiteDownRight();
                }

                if (_mainWindow.Plc != null)
                {
                    if (_mainWindow.Plc.GetPlcModus() == "S7-1200")
                    {
                        VersionNr = _mainWindow.VersionNummer;
                        SpsVersionLokal = _mainWindow.VersionInfoLokal;
                        SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                        SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    }

                    SpsColor = _mainWindow.Plc.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
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


        private ObservableCollection<int> _alleWinkel = new ObservableCollection<int>();
        public ObservableCollection<int> AlleWinkel
        {
            get => _alleWinkel;
            set
            {
                _alleWinkel = value;
                OnPropertyChanged(nameof(AlleWinkel));
            }
        }



        private ObservableCollection<int> _alleBreiten = new ObservableCollection<int>();
        public ObservableCollection<int> AlleBreiten
        {
            get => _alleBreiten;
            set
            {
                _alleBreiten = value;
                OnPropertyChanged(nameof(AlleBreiten));
            }
        }






        #region ClickModeAlleButtons

        public bool ClickModeButton(int asciiCode)
        {
            if (ClickModeBtn[asciiCode] == "Press")
            {
                ClickModeBtn[asciiCode] = "Release";
                return true;
            }

            ClickModeBtn[asciiCode] = "Press";
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

        public void FarbeP0(bool val) => ColorP0 = val ? "Red" : "LightGray";

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