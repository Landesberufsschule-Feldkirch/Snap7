using System.Collections.ObjectModel;

namespace VoltmeterMitSiebenSegmentAnzeige.ViewModel
{
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Voltmeter _voltmeter;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Voltmeter vm)
        {
            _mainWindow = mw;
            _voltmeter = vm;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            for (var i = 0; i < 100; i++) AlleLed.Add("Visible");

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
                        SpsVersionLokal = _mainWindow.VersionInfoLokal;
                        SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                        SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? "hidden" : "visible";
                    }

                    SpsColor = _mainWindow.Plc.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();

                    for (var i = 0; i < 5; i++) SegmenteSchalten(_voltmeter.AlleVoltmeter[i], i * 8);



                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private void SegmenteSchalten(byte achtSegmente, int adresseSegmentA)
        {
            for (var bitIndex = 0; bitIndex < 8; bitIndex++)
            {
                var bitMuster = (byte)(1 << bitIndex);

                if ((achtSegmente & bitMuster) == bitMuster)
                {
                    AlleLed[adresseSegmentA + bitIndex] = "visible";
                }
                else
                {
                    AlleLed[adresseSegmentA + bitIndex] = "invisible";
                }

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


        private ObservableCollection<string> _alleLed = new ObservableCollection<string>();

        public ObservableCollection<string> AlleLed
        {
            get => _alleLed;
            set
            {
                _alleLed = value;
                OnPropertyChanged(nameof(AlleLed));
            }
        }


        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}