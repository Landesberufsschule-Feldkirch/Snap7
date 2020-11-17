using System.Collections.ObjectModel;
using System.Windows;

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

            ColorSegments = "Yellow";

            for (var i = 0; i < 100; i++) AlleLed.Add(Visibility.Visible);

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
                        SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? "Hidden" : "Visible";
                    }

                    SpsColor = _mainWindow.Plc.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();


                    ColorSegments = ColorSegments == "cyan" ? "red" : "cyan";

                    byte wert = 0;
                    switch (Spannung)
                    {
                        case 0: wert = 0;break;
                        case 10: wert = 1;break;
                        case 20: wert = 3; break;
                        case 30: wert = 5; break;
                        case 50: wert = 7; break;
                        case 60: wert = 15; break;
                        case 70: wert = 63; break;
                        case 80: wert = 127; break;
                        case 90: wert = 255; break;


                    }

                    _voltmeter.AlleVoltmeter[0] = wert;
                    _voltmeter.AlleVoltmeter[1] = wert;
                    _voltmeter.AlleVoltmeter[2] = wert;
                    _voltmeter.AlleVoltmeter[3] = wert;
                    _voltmeter.AlleVoltmeter[4] = wert;
                    _voltmeter.AlleVoltmeter[5] = wert;

                    for (var i = 0; i < 5; i++)
                    {
                        SegmenteSchalten(_voltmeter.AlleVoltmeter[i], i * 8);
                    }

                    Spannung += 10;
                    if (Spannung > 100) Spannung = 0;

                }

                Thread.Sleep(1000);
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


        private double _spannung;

        public double Spannung
        {
            get => _spannung;
            set
            {
                _spannung = value;
                OnPropertyChanged(nameof(Spannung));
            }
        }


        private string _colorSegments;

        public string ColorSegments
        {
            get => _colorSegments;
            set
            {
                _colorSegments = value;
                OnPropertyChanged(nameof(ColorSegments));
            }
        }

        private ObservableCollection<Visibility> _alleLed = new ObservableCollection<Visibility>();

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