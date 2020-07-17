using Heizungsregler.Model;
using System.ComponentModel;
using System.Threading;

namespace Heizungsregler.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Heizungsregler.Model.Heizungsregler heizungsregler;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Heizungsregler hr)
        {
            mainWindow = mw;
            heizungsregler = hr;

            SpsVersionsInfo = true;
            SpsStatus = "x";
            SpsColor = "LightBlue";

            ColorPumpe = "White";

            WitterungsTempMitEinheit = "0°C";
            WitterungsTemperaturSlider = 20;

            BetriebsartAuswahl = Betriebsarten.Aus;

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                if (mainWindow.S7_1200 != null)
                {
                    string vInfo = mainWindow.S7_1200.GetVersion();
                    SpsVersionsInfo = mainWindow.Versionsinfo == vInfo;

                    SpsColor = mainWindow.S7_1200.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                WitterungsTempMitEinheit = SliderWitterungstemperatur().ToString() + "°C";

                Pumpenfarbe(heizungsregler.HeizungsPumpe);

                
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


        #region ColorPumpe
        public void Pumpenfarbe(bool status)
        {
            if (status) ColorPumpe = "LawnGreen"; else ColorPumpe = "White";
        }
        private string _colorPumpe;
        public string ColorPumpe
        {
            get => _colorPumpe;
            set
            {
                _colorPumpe = value;
                OnPropertyChanged(nameof(ColorPumpe));
            }
        }
        #endregion


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

        #endregion

        #region WitterungsTempMitEinheit
        private string _witterungsTempMitEinheit;
        public string WitterungsTempMitEinheit
        {
            get => SliderWitterungstemperatur().ToString() + "°C";
            set
            {
                _witterungsTempMitEinheit = value;
                OnPropertyChanged(nameof(WitterungsTempMitEinheit));
            }
        }

        #endregion


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


        #endregion


        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}