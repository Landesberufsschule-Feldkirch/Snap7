using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace _3dHofschiebetor.ViewModel
{
    public static class IdEintraege
    {

    }

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.HofSchiebeTor _hofSchiebeTor;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.HofSchiebeTor ht)
        {
            _mainWindow = mw;
            _hofSchiebeTor = ht;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

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
                    SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;

                    SpsColor = _mainWindow.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(100);
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


        internal void AllesReset()
        {

        }


        public double SchiebeTorSliderPosition() => SchiebeTorPosSlider;

        private double _schiebeTorSliderPosition;

        public double SchiebeTorPosSlider
        {
            get => _schiebeTorSliderPosition;
            set
            {
                _schiebeTorSliderPosition = value;
                OnPropertyChanged(nameof(SchiebeTorPosSlider));
            }
        }

        
        public double LkwSliderPosition() => LkwPosSlider;

        private double _lkwSliderPosition;

        public double LkwPosSlider
        {
            get => _lkwSliderPosition;
            set
            {
                _lkwSliderPosition = value;
                OnPropertyChanged(nameof(LkwPosSlider));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}