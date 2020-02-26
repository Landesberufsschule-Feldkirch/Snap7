﻿namespace WordClock.ViewModel
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

            SpsStatus = "-";
            SpsColor = "LightBlue";

            GeschwindigkeitSlider = 1;

            WinkelStunden = 0;
            WinkelMinuten = 0;
            WinkelSekunden = 0;

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
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
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }



        #region SPS Status und Farbe
        private string _spsStatus;
        public string SpsStatus
        {
            get { return _spsStatus; }
            set
            {
                _spsStatus = value;
                OnPropertyChanged(nameof(SpsStatus));
            }
        }

        private string _spsColor;
        public string SpsColor
        {
            get { return _spsColor; }
            set
            {
                _spsColor = value;
                OnPropertyChanged(nameof(SpsColor));
            }
        }
        #endregion

        #region GeschwindigkeitZeit
        public int GeschwindigkeitZeit() { return (int)GeschwindigkeitSlider; }

        private double _geschwindigkeitSlider;

        public double GeschwindigkeitSlider
        {
            get { return _geschwindigkeitSlider; }
            set
            {
                _geschwindigkeitSlider = value;
                OnPropertyChanged(nameof(GeschwindigkeitSlider));
                zeiten.SetGeschwindigkeit(GeschwindigkeitSlider);
            }
        }
        #endregion


        #region WinkelStunden
        private double _winkelStunden;
        public double WinkelStunden
        {
            get { return _winkelStunden; }
            set
            {
                _winkelStunden = value;
                OnPropertyChanged(nameof(WinkelStunden));
            }
        }
        #endregion

        #region WinkelMinuten
        private double _winkelMinuten;
        public double WinkelMinuten
        {
            get { return _winkelMinuten; }
            set
            {
                _winkelMinuten = value;
                OnPropertyChanged(nameof(WinkelMinuten));
            }
        }
        #endregion

        #region WinkelSekunden
        private double _winkelSekunden;
        public double WinkelSekunden
        {
            get { return _winkelSekunden; }
            set
            {
                _winkelSekunden = value;
                OnPropertyChanged(nameof(WinkelSekunden));
            }
        }
        #endregion



        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members
    }
}
