﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace LAP_2010_4_Abfuellanlage.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using Utilities;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.AbfuellAnlage _abfuellAnlage;
        private readonly MainWindow _mainWindow;

        private const double HoeheFuellBalken = 9 * 35;

        public VisuAnzeigen(MainWindow mw, Model.AbfuellAnlage aa)
        {
            _mainWindow = mw;
            _abfuellAnlage = aa;

            for (var i = 0; i < 100; i++)
            {
                ClkMode.Add(ClickMode.Press);
                SichtbarEin.Add(Visibility.Hidden);
                SichtbarAus.Add(Visibility.Visible);
                Farbe.Add(Brushes.White);
            }

            Margin1 = new Thickness(0, 30, 0, 0);

            for (var i = 0; i < 10; i++) VisDose.Add(Visibility.Visible);
            for (var i = 0; i < 10; i++) TopDose.Add(10 + i * 10);
            for (var i = 0; i < 10; i++) LeftDose.Add(10 + i * 10);

            SpsSichtbar = Visibility.Hidden;
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
                FarbeUmschalten(_abfuellAnlage.P1, 5, Brushes.Red, Brushes.White);
                FarbeUmschalten(_abfuellAnlage.Q1, 6, Brushes.LawnGreen, Brushes.LightGray);
                FarbeUmschalten(_abfuellAnlage.Pegel > 0.01, 21,Brushes.Coral, Brushes.LightCoral);

                SichtbarkeitUmschalten(_abfuellAnlage.B1, 1);
                SichtbarkeitUmschalten(_abfuellAnlage.B2, 2);
                SichtbarkeitUmschalten(_abfuellAnlage.K1, 3);
                SichtbarkeitUmschalten(_abfuellAnlage.K2, 4);
                SichtbarkeitUmschalten(_abfuellAnlage.K2 && _abfuellAnlage.Pegel > 0.01, 20);

                Margin_1(_abfuellAnlage.Pegel);

                for (var i = 0; i < 5; i++)
                {
                    VisibilityDose1(_abfuellAnlage.AlleDosen[i].Sichtbar, i);
                    PositionDose(_abfuellAnlage.AlleDosen[i].Dose.GetPosition(), i);
                }

                if (_mainWindow.Plc != null)
                {
                    SpsVersionLokal = _mainWindow.VersionInfoLokal;
                    SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                    SpsSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    SpsColor = _mainWindow.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void FarbeUmschalten(bool val, int i, Brush farbe1, Brush farbe2) => Farbe[i] = val ? farbe1 : farbe2;
        internal void SichtbarkeitUmschalten(bool val, int i)
        {
            SichtbarEin[i] = val ? Visibility.Visible : Visibility.Collapsed;
            SichtbarAus[i] = val ? Visibility.Collapsed : Visibility.Visible;
        }
        internal void Taster(object id)
        {
            if (id is not string ascii) return;

            var tasterId = short.Parse(ascii);
            var gedrueckt = ClickModeButton(tasterId);

            switch (tasterId)
            {
                case 11: _abfuellAnlage.S1 = !gedrueckt; break;
                case 12: _abfuellAnlage.S2 = gedrueckt; break;
                case 13: _abfuellAnlage.AllesReset(); break;
                case 14: _abfuellAnlage.Nachfuellen(); break;
                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
        }


        #region SPS Version, Status und Farbe
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

        #region EineDose Dosen
        private void VisibilityDose1(bool v, int id) => VisDose[id] = v ? Visibility.Visible : Visibility.Hidden;
        private void PositionDose(Punkt p, int id)
        {
            LeftDose[id] = (int)p.X;
            TopDose[id] = (int)p.Y;
        }
        private ObservableCollection<int> _topDose = new();
        public ObservableCollection<int> TopDose
        {
            get => _topDose;
            set
            {
                _topDose = value;
                OnPropertyChanged(nameof(TopDose));
            }
        }

        private ObservableCollection<int> _leftDose = new();
        public ObservableCollection<int> LeftDose
        {
            get => _leftDose;
            set
            {
                _leftDose = value;
                OnPropertyChanged(nameof(LeftDose));
            }
        }

        private ObservableCollection<Visibility> _visDose = new();
        public ObservableCollection<Visibility> VisDose
        {
            get => _visDose;
            set
            {
                _visDose = value;
                OnPropertyChanged(nameof(VisDose));
            }
        }

        #endregion EineDose Dosen

        #region Margin1
        public void Margin_1(double pegel)
        {
            Margin1 = new Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);
        }
        private Thickness _margin1;
        public Thickness Margin1
        {
            get => _margin1;
            set
            {
                _margin1 = value;
                OnPropertyChanged(nameof(Margin1));
            }
        }
        #endregion Margin1

        #region Sichtbarkeit
        private ObservableCollection<Visibility> _sichtbarEin = new();
        public ObservableCollection<Visibility> SichtbarEin
        {
            get => _sichtbarEin;
            set
            {
                _sichtbarEin = value;
                OnPropertyChanged(nameof(SichtbarEin));
            }
        }

        private ObservableCollection<Visibility> _sichtbarAus = new();
        public ObservableCollection<Visibility> SichtbarAus
        {
            get => _sichtbarAus;
            set
            {
                _sichtbarAus = value;
                OnPropertyChanged(nameof(SichtbarAus));
            }
        }
        #endregion

        #region Farbe
        private ObservableCollection<Brush> _farbe = new();
        public ObservableCollection<Brush> Farbe
        {
            get => _farbe;
            set
            {
                _farbe = value;
                OnPropertyChanged(nameof(Farbe));
            }
        }
        #endregion

        #region Taster/Schalter
        public bool ClickModeButton(int tasterId)
        {
            if (ClkMode[tasterId] == ClickMode.Press)
            {
                ClkMode[tasterId] = ClickMode.Release;
                return true;
            }

            ClkMode[tasterId] = ClickMode.Press;
            return false;
        }

        private ObservableCollection<ClickMode> _clkMode = new();
        public ObservableCollection<ClickMode> ClkMode
        {
            get => _clkMode;
            set
            {
                _clkMode = value;
                OnPropertyChanged(nameof(ClkMode));
            }
        }
        #endregion Taster/Schalter

        #region iNotifyPeropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion iNotifyPeropertyChanged Members
    }
}