namespace Parkhaus.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Parkhaus _parkhaus;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Parkhaus parkhaus)
        {
            _mainWindow = mw;
            _parkhaus = parkhaus;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
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
            if (buchstabe is string ascii)
            {
              
            }
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
             
            

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

        public void Auto(object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}