using System.ComponentModel;
using System.Threading;

namespace LAP_2018_1_Silosteuerung.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        public readonly Model.Foerderanlage foerderanlage;

        public VisuAnzeigen(Model.Foerderanlage fa)
        {
            foerderanlage = fa;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SPSVersionLokal = "fehlt";
            SPSVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
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

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}