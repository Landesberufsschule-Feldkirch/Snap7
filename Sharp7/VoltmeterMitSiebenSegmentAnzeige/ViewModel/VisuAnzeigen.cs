using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

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
            SpsVersionsInfoSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            HintergrundFarbe = Brushes.Aqua;
            SegmentFarbe = Brushes.DarkOrange;

            for (var i = 0; i < 100; i++) AlleLed.Add(Visibility.Visible);

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            const byte bitmusterHintergrundfarbe = (byte)1;
            const byte bitmusterSegmentfarbe = (byte)16;

            while (true)
            {
                if (_mainWindow.Plc != null)
                {
                    if (_mainWindow.Plc.GetPlcModus() == "S7-1200")
                    {
                        VersionNr = _mainWindow.VersionNummer;
                        SpsVersionLokal = _mainWindow.VersionInfoLokal;
                        SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                        SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    }

                    SpsColor = _mainWindow.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                HintergrundFarbe = (_voltmeter.AlleVoltmeter[0] & bitmusterHintergrundfarbe) == bitmusterHintergrundfarbe ? Brushes.Chartreuse : Brushes.WhiteSmoke;
                SegmentFarbe = (_voltmeter.AlleVoltmeter[0] & bitmusterSegmentfarbe) == bitmusterSegmentfarbe ? Brushes.Red : Brushes.Green;

                for (var i = 1; i < 6; i++) SegmenteSchalten(_voltmeter.AlleVoltmeter[i], i * 8);

                Thread.Sleep(100);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private void SegmenteSchalten(byte achtSegmente, int adresseSegmentA)
        {
            for (var bitIndex = 0; bitIndex < 8; bitIndex++)
            {
                var bitMuster = (byte)(1 << bitIndex);

                if ((achtSegmente & bitMuster) == bitMuster) AlleLed[adresseSegmentA + bitIndex] = Visibility.Visible;
                else AlleLed[adresseSegmentA + bitIndex] = Visibility.Hidden;
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

        #region Farben
        private SolidColorBrush _hintergrundFarbe;
        public SolidColorBrush HintergrundFarbe
        {
            get => _hintergrundFarbe;
            set
            {
                _hintergrundFarbe = value;
                OnPropertyChanged(nameof(HintergrundFarbe));
            }
        }

        private SolidColorBrush _segmentFarbe;
        public SolidColorBrush SegmentFarbe
        {
            get => _segmentFarbe;
            set
            {
                _segmentFarbe = value;
                OnPropertyChanged(nameof(SegmentFarbe));
            }
        }
        #endregion


        private ObservableCollection<Visibility> _alleLed = new();
        public ObservableCollection<Visibility> AlleLed
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