﻿using Heizungsregler.Model;
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

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
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
                if (mainWindow.S71200 != null)
                {
                    SpsVersionLokal = mainWindow.VersionInfo;
                    SpsVersionEntfernt = mainWindow.S71200.GetVersion();                  
                    SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? "hidden" : "visible";

                    SpsColor = mainWindow.S71200.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = mainWindow.S71200?.GetSpsStatus();
                }

                WitterungsTempMitEinheit = SliderWitterungstemperatur().ToString() + "°C";

                Pumpenfarbe(heizungsregler.HeizungsPumpe);

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

        #region ColorPumpe

        public void Pumpenfarbe(bool status) => ColorPumpe = status ? "LawnGreen" : "White";

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

        #endregion WitterungsTempMitEinheit

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