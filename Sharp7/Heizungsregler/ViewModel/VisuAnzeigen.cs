using Heizungsregler.Model;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace Heizungsregler.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw)
        {
            _mainWindow = mw;

            SpsSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            ColorPumpe = Brushes.White;

            WitterungsTempMitEinheit = "0°C";
            WitterungsTemperaturSlider = 20;



            BetriebsartAuswahl = Betriebsarten.Aus;

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
 if ( _mainWindow.PlcDaemon != null &&  _mainWindow.PlcDaemon.Plc != null)
                {
                    SpsVersionLokal = _mainWindow.VersionInfoLokal;
                    SpsVersionEntfernt = _mainWindow.PlcDaemon.Plc.GetVersion();
                    SpsSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    SpsColor = _mainWindow.PlcDaemon.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.PlcDaemon.Plc?.GetSpsStatus();

                    FensterTitel = _mainWindow.PlcDaemon.Plc.GetPlcBezeichnung() + ": " + _mainWindow.VersionInfoLokal;
                }         

                if (_mainWindow.WohnHaus != null)
                {
                    WitterungsTempMitEinheit = SliderWitterungstemperatur().ToString("F1") + "°C";
                    _mainWindow.WohnHaus.WitterungsTemperatur = WitterungsTemperaturSlider;
                    _mainWindow.WohnHaus.Betriebsart = BetriebsartAuswahl;

                    KesselTemperaturMitEinheit = "Kesseltemperatur: " + _mainWindow.WohnHaus.KesselTemperatur.ToString("F1") + "°C";

                    VentilStellungMitEinheit = "Y: " + _mainWindow.WohnHaus.DreiwegeVentil.GetPosition().ToString("F1") + "%";


                    VorlaufIstMitAllem = "Ist: " + _mainWindow.WohnHaus.VorlaufSolltemperatur.ToString("F1") + "°C";
                    VorlaufSollMitAllem = "Soll: " + _mainWindow.WohnHaus.VorlaufSolltemperatur.ToString("F1") + "°C";

                    Pumpenfarbe(_mainWindow.WohnHaus.HeizungsPumpe);
                    OelGasBrennerfarbe(_mainWindow.WohnHaus.BrennerEin);

                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        #region SPS Version, Status und Farbe
        private string fensterTitel;
        public string FensterTitel
        {
            get => fensterTitel;
            set
            {
                fensterTitel = value;
                OnPropertyChanged(nameof(FensterTitel));
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

        private Visibility _spsSichtbar;
        public Visibility SpsSichtbar
        {
            get => _spsSichtbar;
            set
            {
                _spsSichtbar = value;
                OnPropertyChanged(nameof(SpsSichtbar));
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

        #region ColorPumpe
        public void Pumpenfarbe(bool status) => ColorPumpe = status ? Brushes.LawnGreen : Brushes.White;

        private Brush _colorPumpe;
        public Brush ColorPumpe
        {
            get => _colorPumpe;
            set
            {
                _colorPumpe = value;
                OnPropertyChanged(nameof(ColorPumpe));
            }
        }
        #endregion ColorPumpe

        #region ColorOelGasBrenner
        public void OelGasBrennerfarbe(bool status) => ColorOelGasBrenner = status ? Brushes.Red : Brushes.White;

        private Brush _colorOelGasBrenner;
        public Brush ColorOelGasBrenner
        {
            get => _colorOelGasBrenner;
            set
            {
                _colorOelGasBrenner = value;
                OnPropertyChanged(nameof(ColorOelGasBrenner));
            }
        }
        #endregion ColorPumpe




        #region WitterungsTemperaturSlider

        public double SliderWitterungstemperatur() => WitterungsTemperaturSlider;


        private double _witterungsTemperaturSlider;

        public double WitterungsTemperaturSlider
        {
            get => _witterungsTemperaturSlider;
            set
            {
                _witterungsTemperaturSlider = value;
                OnPropertyChanged(nameof(WitterungsTemperaturSlider));
            }
        }

        #endregion WitterungsTemperaturSlider






        private string _kesselTemperaturMitEinheit;

        public string KesselTemperaturMitEinheit
        {
            get => _kesselTemperaturMitEinheit;
            set
            {
                _kesselTemperaturMitEinheit = value;
                OnPropertyChanged(nameof(KesselTemperaturMitEinheit));
            }
        }


        private string _ventilStellungMitEinheit;
        public string VentilStellungMitEinheit
        {
            get => _ventilStellungMitEinheit;
            set
            {
                _ventilStellungMitEinheit = value;
                OnPropertyChanged(nameof(VentilStellungMitEinheit));
            }
        }

        private string _vorlaufIstMitAllem;
        public string VorlaufIstMitAllem
        {
            get => _vorlaufIstMitAllem;
            set
            {
                _vorlaufIstMitAllem = value;
                OnPropertyChanged(nameof(VorlaufIstMitAllem));
            }
        }

        private string _vorlaufSollMitAllem;
        public string VorlaufSollMitAllem
        {
            get => _vorlaufSollMitAllem;
            set
            {
                _vorlaufSollMitAllem = value;
                OnPropertyChanged(nameof(VorlaufSollMitAllem));
            }
        }




        private string _witterungsTempMitEinheit;

        public string WitterungsTempMitEinheit
        {
            get => SliderWitterungstemperatur() + "°C";
            set
            {
                _witterungsTempMitEinheit = value;
                OnPropertyChanged(nameof(WitterungsTempMitEinheit));
            }
        }


        #region BetriebsartAuswahl

        private Betriebsarten _betriebsartAuswahl;

        public Betriebsarten BetriebsartAuswahl
        {
            get => _betriebsartAuswahl;
            set
            {
                _betriebsartAuswahl = value;
                OnPropertyChanged(nameof(BetriebsartAuswahl));
            }
        }

        #endregion BetriebsartAuswahl

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}