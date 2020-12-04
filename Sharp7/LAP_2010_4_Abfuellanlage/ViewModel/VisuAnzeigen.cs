using System.Collections.ObjectModel;

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

            ColorP1 = "White";
            ColorQ1 = "LawnGreen";
            ColorRectangleZuleitung = "Coral";

            ClickModeBtnS1 = "Press";
            ClickModeBtnS2 = "Press";

            ClickModeBtnQ1 = "Press";
            ClickModeBtnK1 = "Press";
            ClickModeBtnK2 = "Press";

            Margin1 = new Thickness(0, 30, 0, 0);

            VisibilityRectangleAbleitung = "visible";

            for (var i = 0; i < 10; i++) VisDose.Add(Visibility.Visible);
            for (var i = 0; i < 10; i++) TopDose.Add(10 + i * 10);
            for (var i = 0; i < 10; i++) LeftDose.Add(10 + i * 10);

            VisibilityK1Ein = "hidden";
            VisibilityK2Ein = "hidden";

            VisibilityK1Aus = "visible";
            VisibilityK2Aus = "visible";

            VisibilityB1Ein = "Visible";
            VisibilityB1Aus = "Hidden";
            VisibilityB2Ein = "Visible";
            VisibilityB2Aus = "Hidden";

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeP1(_abfuellAnlage.P1);
                FarbeQ1(_abfuellAnlage.Q1);

                FarbeRectangleZuleitung(_abfuellAnlage.Pegel > 0.01);

                Margin_1(_abfuellAnlage.Pegel);

                for (var i = 0; i < 5; i++)
                {
                    VisibilityDose1(_abfuellAnlage.AlleDosen[i].Sichtbar, i);
                    PositionDose(_abfuellAnlage.AlleDosen[i].Position.Punkt, i);
                }


                VisibilityAbleitung(_abfuellAnlage.K2 && _abfuellAnlage.Pegel > 0.01);

                SichtbarkeitK1(_abfuellAnlage.K1);
                SichtbarkeitK2(_abfuellAnlage.K2);

                SichtbarkeitB1(_abfuellAnlage.B1);
                SichtbarkeitB2(_abfuellAnlage.B2);

                if (_mainWindow.Plc != null)
                {
                    if (_mainWindow.Plc.GetPlcModus() == "S7-1200")
                    {
                        VersionNr = _mainWindow.VersionNummer;
                        SpsVersionLokal = _mainWindow.VersionInfoLokal;
                        SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                        SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? "hidden" : "visible";
                    }

                    SpsColor = _mainWindow.Plc.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void SetQ1() => _abfuellAnlage.Q1 = ClickModeButtonQ1();
        internal void SetK1() => _abfuellAnlage.K1 = ClickModeButtonK1();
        internal void SetK2() => _abfuellAnlage.K2 = ClickModeButtonK2();
        internal void SetS1() => _abfuellAnlage.S1 = ClickModeButtonS1();
        internal void BtnS2() => _abfuellAnlage.S2 = ClickModeButtonS2();


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


        #region Position Dosen

        private void VisibilityDose1(bool v, int id) => VisDose[id] = v ? Visibility.Visible : Visibility.Hidden;

        private void PositionDose(Punkt p, int id)
        {
            LeftDose[id] = (int)p.X;
            TopDose[id] = (int)p.Y;
        }



        private ObservableCollection<int> _topDose = new ObservableCollection<int>();
        public ObservableCollection<int> TopDose
        {
            get => _topDose;
            set
            {
                _topDose = value;
                OnPropertyChanged(nameof(TopDose));
            }
        }

        private ObservableCollection<int> _leftDose = new ObservableCollection<int>();
        public ObservableCollection<int> LeftDose
        {
            get => _leftDose;
            set
            {
                _leftDose = value;
                OnPropertyChanged(nameof(LeftDose));
            }
        }

        private ObservableCollection<Visibility> _visDose = new ObservableCollection<Visibility>();
        public ObservableCollection<Visibility> VisDose
        {
            get => _visDose;
            set
            {
                _visDose = value;
                OnPropertyChanged(nameof(VisDose));
            }
        }

        #endregion
        
        #region Color P1

        public void FarbeP1(bool val) => ColorP1 = val ? "Red" : "White";

        private string _colorP1;
        public string ColorP1
        {
            get => _colorP1;
            set
            {
                _colorP1 = value;
                OnPropertyChanged(nameof(ColorP1));
            }
        }

        #endregion Color P1

        #region Color Q1

        public void FarbeQ1(bool val) => ColorQ1 = val ? "LawnGreen" : "LightGray";

        private string _colorQ1;
        public string ColorQ1
        {
            get => _colorQ1;
            set
            {
                _colorQ1 = value;
                OnPropertyChanged(nameof(ColorQ1));
            }
        }

        #endregion Color Q1

        #region Color Zuleitung

        public void FarbeRectangleZuleitung(bool val) => ColorRectangleZuleitung = val ? "Coral" : "LightCoral";

        private string _colorRectangleZuleitung;
        public string ColorRectangleZuleitung
        {
            get => _colorRectangleZuleitung;
            set
            {
                _colorRectangleZuleitung = value;
                OnPropertyChanged(nameof(ColorRectangleZuleitung));
            }
        }

        #endregion Color Zuleitung

        #region ClickModeBtnS1

        public bool ClickModeButtonS1()
        {
            if (ClickModeBtnS1 == "Press")
            {
                ClickModeBtnS1 = "Release";
                return true;
            }

            ClickModeBtnS1 = "Press";
            return false;
        }

        private string _clickModeBtnS1;
        public string ClickModeBtnS1
        {
            get => _clickModeBtnS1;
            set
            {
                _clickModeBtnS1 = value;
                OnPropertyChanged(nameof(ClickModeBtnS1));
            }
        }

        #endregion ClickModeBtnS1

        #region ClickModeBtnS2

        public bool ClickModeButtonS2()
        {
            if (ClickModeBtnS2 == "Press")
            {
                ClickModeBtnS2 = "Release";
                return true;
            }

            ClickModeBtnS2 = "Press";
            return false;
        }

        private string _clickModeBtnS2;
        public string ClickModeBtnS2
        {
            get => _clickModeBtnS2;
            set
            {
                _clickModeBtnS2 = value;
                OnPropertyChanged(nameof(ClickModeBtnS2));
            }
        }

        #endregion ClickModeBtnS2

        #region ClickModeBtnQ1

        public bool ClickModeButtonQ1()
        {
            if (ClickModeBtnQ1 == "Press")
            {
                ClickModeBtnQ1 = "Release";
                return true;
            }

            ClickModeBtnQ1 = "Press";
            return false;
        }

        private string _clickModeBtnQ1;
        public string ClickModeBtnQ1
        {
            get => _clickModeBtnQ1;
            set
            {
                _clickModeBtnQ1 = value;
                OnPropertyChanged(nameof(ClickModeBtnQ1));
            }
        }

        #endregion ClickModeBtnQ1

        #region ClickModeBtnK1

        public bool ClickModeButtonK1()
        {
            if (ClickModeBtnK1 == "Press")
            {
                ClickModeBtnK1 = "Release";
                return true;
            }

            ClickModeBtnK1 = "Press";
            return false;
        }

        private string _clickModeBtnK1;
        public string ClickModeBtnK1
        {
            get => _clickModeBtnK1;
            set
            {
                _clickModeBtnK1 = value;
                OnPropertyChanged(nameof(ClickModeBtnK1));
            }
        }

        #endregion ClickModeBtnK1

        #region ClickModeBtnK2

        public bool ClickModeButtonK2()
        {
            if (ClickModeBtnK2 == "Press")
            {
                ClickModeBtnK2 = "Release";
                return true;
            }

            ClickModeBtnK2 = "Press";
            return false;
        }

        private string _clickModeBtnK2;
        public string ClickModeBtnK2
        {
            get => _clickModeBtnK2;
            set
            {
                _clickModeBtnK2 = value;
                OnPropertyChanged(nameof(ClickModeBtnK2));
            }
        }

        #endregion ClickModeBtnK2

        #region Sichtbarkeit K1

        public void SichtbarkeitK1(bool val)
        {
            if (val)
            {
                VisibilityK1Ein = "visible";
                VisibilityK1Aus = "hidden";
            }
            else
            {
                VisibilityK1Ein = "hidden";
                VisibilityK1Aus = "visible";
            }
        }

        private string _visibilityK1Ein;
        public string VisibilityK1Ein
        {
            get => _visibilityK1Ein;
            set
            {
                _visibilityK1Ein = value;
                OnPropertyChanged(nameof(VisibilityK1Ein));
            }
        }

        private string _visibilityK1Aus;

        public string VisibilityK1Aus
        {
            get => _visibilityK1Aus;
            set
            {
                _visibilityK1Aus = value;
                OnPropertyChanged(nameof(VisibilityK1Aus));
            }
        }

        #endregion Sichtbarkeit K1

        #region Sichtbarkeit K2

        public void SichtbarkeitK2(bool val)
        {
            if (val)
            {
                VisibilityK2Ein = "visible";
                VisibilityK2Aus = "hidden";
            }
            else
            {
                VisibilityK2Ein = "hidden";
                VisibilityK2Aus = "visible";
            }
        }

        private string _visibilityK2Ein;
        public string VisibilityK2Ein
        {
            get => _visibilityK2Ein;
            set
            {
                _visibilityK2Ein = value;
                OnPropertyChanged(nameof(VisibilityK2Ein));
            }
        }

        private string _visibilityK2Aus;

        public string VisibilityK2Aus
        {
            get => _visibilityK2Aus;
            set
            {
                _visibilityK2Aus = value;
                OnPropertyChanged(nameof(VisibilityK2Aus));
            }
        }

        #endregion Sichtbarkeit K2

        #region Visibility Ableitung

        public void VisibilityAbleitung(bool val) => VisibilityRectangleAbleitung = val ? "visible" : "hidden";

        private string _visibilityRectangleAbleitung;
        public string VisibilityRectangleAbleitung
        {
            get => _visibilityRectangleAbleitung;
            set
            {
                _visibilityRectangleAbleitung = value;
                OnPropertyChanged(nameof(VisibilityRectangleAbleitung));
            }
        }

        #endregion Visibility Ableitung

        #region Sichtbarkeit B1

        public void SichtbarkeitB1(bool val)
        {
            if (val)
            {
                VisibilityB1Ein = "Visible";
                VisibilityB1Aus = "Hidden";
            }
            else
            {
                VisibilityB1Ein = "Hidden";
                VisibilityB1Aus = "Visible";
            }
        }

        private string _visibilityB1Ein;
        public string VisibilityB1Ein
        {
            get => _visibilityB1Ein;
            set
            {
                _visibilityB1Ein = value;
                OnPropertyChanged(nameof(VisibilityB1Ein));
            }
        }

        private string _visibilityB1Aus;

        public string VisibilityB1Aus
        {
            get => _visibilityB1Aus;
            set
            {
                _visibilityB1Aus = value;
                OnPropertyChanged(nameof(VisibilityB1Aus));
            }
        }

        #endregion Sichtbarkeit B1

        #region Sichtbarkeit B2

        public void SichtbarkeitB2(bool val)
        {
            if (val)
            {
                VisibilityB2Ein = "Visible";
                VisibilityB2Aus = "Hidden";
            }
            else
            {
                VisibilityB2Ein = "Hidden";
                VisibilityB2Aus = "Visible";
            }
        }

        private string _visibilityB2Ein;
        public string VisibilityB2Ein
        {
            get => _visibilityB2Ein;
            set
            {
                _visibilityB2Ein = value;
                OnPropertyChanged(nameof(VisibilityB2Ein));
            }
        }

        private string _visibilityB2Aus;

        public string VisibilityB2Aus
        {
            get => _visibilityB2Aus;
            set
            {
                _visibilityB2Aus = value;
                OnPropertyChanged(nameof(VisibilityB2Aus));
            }
        }

        #endregion Sichtbarkeit B2

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

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}