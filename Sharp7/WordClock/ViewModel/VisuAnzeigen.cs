﻿using System.Windows;
using System.Windows.Media;

namespace WordClock.ViewModel
{
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Zeiten _zeiten;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Zeiten zt)
        {
            _mainWindow = mw;
            _zeiten = zt;

            SpsSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

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
                WinkelSekunden = _zeiten.GetSekunde() * 6;
                WinkelMinuten = _zeiten.GetMinute() * 6;
                WinkelStunden = _zeiten.GetStunde() * 30 + _zeiten.GetMinute() * 0.5;

                if (_mainWindow.PlcDaemon != null && _mainWindow.PlcDaemon.Plc != null)
                {
                    SpsVersionLokal = _mainWindow.VersionInfoLokal;
                    SpsVersionEntfernt = _mainWindow.PlcDaemon.Plc.GetVersion();
                    SpsSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    SpsColor = _mainWindow.PlcDaemon.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.PlcDaemon.Plc?.GetSpsStatus();

                    FensterTitel = _mainWindow.PlcDaemon.Plc.GetPlcBezeichnung() + ": " + _mainWindow.VersionInfoLokal;
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

        #region GeschwindigkeitZeit

        private double _geschwindigkeitSlider;

        public double GeschwindigkeitSlider
        {
            get => _geschwindigkeitSlider;
            set
            {
                _geschwindigkeitSlider = value;
                OnPropertyChanged(nameof(GeschwindigkeitSlider));
                _zeiten.SetGeschwindigkeit(GeschwindigkeitSlider);
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