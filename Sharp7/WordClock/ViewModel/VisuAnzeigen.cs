namespace WordClock.ViewModel
{
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Zeiten zeiten;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Zeiten zt)
        {
            mainWindow = mw;
            zeiten = zt;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SPSVersionLokal = "fehlt";
            SPSVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            GeschwindigkeitSlider = 1;

            WinkelStunden = 0;
            WinkelMinuten = 0;
            WinkelSekunden = 0;

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                WinkelSekunden = zeiten.GetSekunde() * 6;
                WinkelMinuten = zeiten.GetMinute() * 6;
                WinkelStunden = zeiten.GetStunde() * 30 + zeiten.GetMinute() * 0.5;
   
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

        #region GeschwindigkeitZeit

        public int GeschwindigkeitZeit() => (int)GeschwindigkeitSlider;

        private double _geschwindigkeitSlider;

        public double GeschwindigkeitSlider
        {
            get => _geschwindigkeitSlider;
            set
            {
                _geschwindigkeitSlider = value;
                OnPropertyChanged(nameof(GeschwindigkeitSlider));
                zeiten.SetGeschwindigkeit(GeschwindigkeitSlider);
            }
        }

        #endregion GeschwindigkeitZeit

        #region WinkelStunden

        private double _winkelStunden;

        public double WinkelStunden
        {
            get => _winkelStunden;
            set
            {
                _winkelStunden = value;
                OnPropertyChanged(nameof(WinkelStunden));
            }
        }

        #endregion WinkelStunden

        #region WinkelMinuten

        private double _winkelMinuten;

        public double WinkelMinuten
        {
            get => _winkelMinuten;
            set
            {
                _winkelMinuten = value;
                OnPropertyChanged(nameof(WinkelMinuten));
            }
        }

        #endregion WinkelMinuten

        #region WinkelSekunden

        private double _winkelSekunden;

        public double WinkelSekunden
        {
            get => _winkelSekunden;
            set
            {
                _winkelSekunden = value;
                OnPropertyChanged(nameof(WinkelSekunden));
            }
        }

        #endregion WinkelSekunden

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}