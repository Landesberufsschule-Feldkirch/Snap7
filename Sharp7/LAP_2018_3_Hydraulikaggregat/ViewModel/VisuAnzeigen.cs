namespace LAP_2018_3_Hydraulikaggregat.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Hydraulikaggregat _hydraulikaggregat;
        private readonly MainWindow _mainWindow;

        private const double FuellBalkenHoehe = 390;
        private const double FuellBalkenOben = -40;

        public VisuAnzeigen(MainWindow mw, Model.Hydraulikaggregat ha)
        {
            _mainWindow = mw;
            _hydraulikaggregat = ha;

            Druck = 1.1;

            ClickModeBtnQ1 = "Press";
            ClickModeBtnQ2 = "Press";
            ClickModeBtnQ3 = "Press";
            ClickModeBtnQ1Q3 = "Press";

            ClickModeBtnS1 = "Press";
            ClickModeBtnS2 = "Press";
            ClickModeBtnS3 = "Press";

            ColorP1 = "LawnGreen";
            ColorP2 = "LawnGreen";
            ColorP3 = "LawnGreen";
            ColorP4 = "LawnGreen";

            ColorQ1 = "LawnGreen";
            ColorQ2 = "LawnGreen";
            ColorQ3 = "LawnGreen";

            Margin1 = new Thickness(42, 0, 32, 0);

            VisibilityB1Ein = "hidden";
            VisibilityB1Aus = "visible";

            VisibilityB2Ein = "hidden";
            VisibilityB2Aus = "visible";

            VisibilityB3Ein = "hidden";
            VisibilityB3Aus = "visible";

            VisibilityKurzschluss = "Hidden";

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
                Druck = _hydraulikaggregat.Druck;

                FarbeF1(_hydraulikaggregat.F1);

                FarbeP1(_hydraulikaggregat.P1);
                FarbeP2(_hydraulikaggregat.P2);
                FarbeP3(_hydraulikaggregat.P3);
                FarbeP4(_hydraulikaggregat.P4);

                FarbeQ1(_hydraulikaggregat.Q1);
                FarbeQ2(_hydraulikaggregat.Q2);
                FarbeQ3(_hydraulikaggregat.Q3);

                Margin_1(_hydraulikaggregat.Pegel);

                SichtbarkeitB1(_hydraulikaggregat.B1);
                SichtbarkeitB2(_hydraulikaggregat.B2);
                SichtbarkeitB3(_hydraulikaggregat.B3);

                if (_hydraulikaggregat.Q2 && _hydraulikaggregat.Q3) VisibilityKurzschluss = "Visible"; else VisibilityKurzschluss = "Hidden";

                if (_mainWindow.Plc != null)
                {
                    if (_mainWindow.Plc.GetPlcModus() == "S7-1200")
                    {
                        VersionNr = _mainWindow.VersionNummer;
                        SpsVersionLokal = _mainWindow.VersionInfo;
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

        internal void BtnQ1() => _hydraulikaggregat.Q1 = ClickModeButtonQ1();

        internal void BtnQ2() => _hydraulikaggregat.Q2 = ClickModeButtonQ2();

        internal void BtnQ3() => _hydraulikaggregat.Q3 = ClickModeButtonQ3();

        internal void BtnQ1Q3()
        {
            if (ClickModeButtonQ1Q3())
            {
                _hydraulikaggregat.Q1 = true;
                _hydraulikaggregat.Q3 = true;
            }
            else
            {
                _hydraulikaggregat.Q1 = false;
                _hydraulikaggregat.Q3 = false;
            }
        }

        internal void BtnS1() => _hydraulikaggregat.S1 = ClickModeButtonS1();

        internal void BtnS2() => _hydraulikaggregat.S2 = ClickModeButtonS2();

        internal void BtnS3()
        {
            _hydraulikaggregat.Stopwatch.Restart();
            _hydraulikaggregat.S3 = ClickModeButtonS3();
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

        #region ClickModeBtnQ1

        public bool ClickModeButtonQ1()
        {
            if (ClickModeBtnQ1 == "Press")
            {
                ClickModeBtnQ1 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnQ1 = "Press";
            }
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

        #region ClickModeBtnQ2

        public bool ClickModeButtonQ2()
        {
            if (ClickModeBtnQ2 == "Press")
            {
                ClickModeBtnQ2 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnQ2 = "Press";
            }
            return false;
        }

        private string _clickModeBtnQ2;

        public string ClickModeBtnQ2
        {
            get => _clickModeBtnQ2;
            set
            {
                _clickModeBtnQ2 = value;
                OnPropertyChanged(nameof(ClickModeBtnQ2));
            }
        }

        #endregion ClickModeBtnQ2

        #region ClickModeBtnQ3

        public bool ClickModeButtonQ3()
        {
            if (ClickModeBtnQ3 == "Press")
            {
                ClickModeBtnQ3 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnQ3 = "Press";
            }
            return false;
        }

        private string _clickModeBtnQ3;

        public string ClickModeBtnQ3
        {
            get => _clickModeBtnQ3;
            set
            {
                _clickModeBtnQ3 = value;
                OnPropertyChanged(nameof(ClickModeBtnQ3));
            }
        }

        #endregion ClickModeBtnQ3

        #region ClickModeBtnQ1_Q3

        public bool ClickModeButtonQ1Q3()
        {
            if (ClickModeBtnQ1Q3 == "Press")
            {
                ClickModeBtnQ1Q3 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnQ1Q3 = "Press";
            }
            return false;
        }

        private string _clickModeBtnQ1Q3;

        public string ClickModeBtnQ1Q3
        {
            get => _clickModeBtnQ1Q3;
            set
            {
                _clickModeBtnQ1Q3 = value;
                OnPropertyChanged(nameof(ClickModeBtnQ1Q3));
            }
        }

        #endregion ClickModeBtnQ1Q3

        #region ClickModeBtnS1

        public bool ClickModeButtonS1()
        {
            if (ClickModeBtnS1 == "Press")
            {
                ClickModeBtnS1 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnS1 = "Press";
            }
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
            else
            {
                ClickModeBtnS2 = "Press";
            }
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

        #region ClickModeBtnS3

        public bool ClickModeButtonS3()
        {
            if (ClickModeBtnS3 == "Press")
            {
                ClickModeBtnS3 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnS3 = "Press";
            }
            return false;
        }

        private string _clickModeBtnS3;

        public string ClickModeBtnS3
        {
            get => _clickModeBtnS3;
            set
            {
                _clickModeBtnS3 = value;
                OnPropertyChanged(nameof(ClickModeBtnS3));
            }
        }

        #endregion ClickModeBtnS3

        #region Color F1

        public void FarbeF1(bool val) => ColorF1 = val ? "LawnGreen" : "Red";

        private string _colorF1;

        public string ColorF1
        {
            get => _colorF1;
            set
            {
                _colorF1 = value;
                OnPropertyChanged(nameof(ColorF1));
            }
        }

        #endregion Color F1

        #region Color P1

        public void FarbeP1(bool val) => ColorP1 = val ? "LawnGreen" : "White";

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

        #region Color P2

        public void FarbeP2(bool val) => ColorP2 = val ? "Red" : "White";

        private string _colorP2;

        public string ColorP2
        {
            get => _colorP2;
            set
            {
                _colorP2 = value;
                OnPropertyChanged(nameof(ColorP2));
            }
        }

        #endregion Color P2

        #region Color P3

        public void FarbeP3(bool val) => ColorP3 = val ? "LawnGreen" : "White";

        private string _colorP3;

        public string ColorP3
        {
            get => _colorP3;
            set
            {
                _colorP3 = value;
                OnPropertyChanged(nameof(ColorP3));
            }
        }

        #endregion Color P3

        #region Color P4

        public void FarbeP4(bool val) => ColorP4 = val ? "Red" : "White";

        private string _colorP4;

        public string ColorP4
        {
            get => _colorP4;
            set
            {
                _colorP4 = value;
                OnPropertyChanged(nameof(ColorP4));
            }
        }

        #endregion Color P4

        #region Color Q1

        public void FarbeQ1(bool val) => ColorQ1 = val ? "LawnGreen" : "White";

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

        #region Color Q2

        public void FarbeQ2(bool val) => ColorQ2 = val ? "LawnGreen" : "White";

        private string _colorQ2;

        public string ColorQ2
        {
            get => _colorQ2;
            set
            {
                _colorQ2 = value;
                OnPropertyChanged(nameof(ColorQ2));
            }
        }

        #endregion Color Q2

        #region Color Q3

        public void FarbeQ3(bool val) => ColorQ3 = val ? "LawnGreen" : "White";

        private string _colorQ3;

        public string ColorQ3
        {
            get => _colorQ3;
            set
            {
                _colorQ3 = value;
                OnPropertyChanged(nameof(ColorQ3));
            }
        }

        #endregion Color Q3

        #region Druck

        private double _druck;

        public double Druck
        {
            get => _druck;
            set
            {
                _druck = value;
                OnPropertyChanged(nameof(Druck));
            }
        }

        #endregion Druck

        #region Margin1

        public void Margin_1(double pegel)
        {
            Margin1 = new Thickness(41, FuellBalkenOben + FuellBalkenHoehe * (1 - pegel), 31, 0);
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

        #region Sichtbarkeit B1

        public void SichtbarkeitB1(bool val)
        {
            if (val)
            {
                VisibilityB1Ein = "visible";
                VisibilityB1Aus = "hidden";
            }
            else
            {
                VisibilityB1Ein = "hidden";
                VisibilityB1Aus = "visible";
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
                VisibilityB2Ein = "visible";
                VisibilityB2Aus = "hidden";
            }
            else
            {
                VisibilityB2Ein = "hidden";
                VisibilityB2Aus = "visible";
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

        #region Sichtbarkeit B3

        public void SichtbarkeitB3(bool val)
        {
            if (val)
            {
                VisibilityB3Ein = "visible";
                VisibilityB3Aus = "hidden";
            }
            else
            {
                VisibilityB3Ein = "hidden";
                VisibilityB3Aus = "visible";
            }
        }

        private string _visibilityB3Ein;

        public string VisibilityB3Ein
        {
            get => _visibilityB3Ein;
            set
            {
                _visibilityB3Ein = value;
                OnPropertyChanged(nameof(VisibilityB3Ein));
            }
        }

        private string _visibilityB3Aus;

        public string VisibilityB3Aus
        {
            get => _visibilityB3Aus;
            set
            {
                _visibilityB3Aus = value;
                OnPropertyChanged(nameof(VisibilityB3Aus));
            }
        }

        #endregion Sichtbarkeit B3

        #region VisibilityKurzschluss

        private string _visibilityKurzschluss;

        public string VisibilityKurzschluss
        {
            get => _visibilityKurzschluss;
            set
            {
                _visibilityKurzschluss = value;
                OnPropertyChanged(nameof(VisibilityKurzschluss));
            }
        }

        #endregion VisibilityKurzschluss

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}