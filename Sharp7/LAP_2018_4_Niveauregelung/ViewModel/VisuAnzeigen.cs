namespace LAP_2018_4_Niveauregelung.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private const double HoeheFuellBalken = 315;

        private readonly MainWindow _mainWindow;
        private readonly Model.NiveauRegelung _niveauRegelung;

        public VisuAnzeigen(MainWindow mw, Model.NiveauRegelung nr)
        {
            _mainWindow = mw;
            _niveauRegelung = nr;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            ClickModeBtnS1 = "Press";
            ClickModeBtnS2 = "Press";
            ClickModeBtnS3 = "Press";

            ColorThermorelaisF1 = "LawnGreen";
            ColorThermorelaisF2 = "LawnGreen";

            ColorCircleP1 = "White";
            ColorCircleP2 = "White";
            ColorCircleP2 = "White";

            ColorAbleitungOben = "LightBlue";
            ColorAbleitungUnten = "LightBlue";
            ColorZuleitungLinksWaagrecht = "LightBlue";
            ColorZuleitungLinksSenkrecht = "LightBlue";
            ColorZuleitungRechtsWaagrecht = "LightBlue";
            ColorZuleitungRechtsSenkrecht = "LightBlue";

            VisibilityB1Ein = "visible";
            VisibilityB2Ein = "visible";
            VisibilityB3Ein = "visible";
            VisibilityQ1Ein = "visible";
            VisibilityQ2Ein = "visible";
            VisibilityVentilEin = "visible";

            VisibilityB1Aus = "hidden";
            VisibilityB2Aus = "hidden";
            VisibilityB3Aus = "hidden";
            VisibilityQ1Aus = "hidden";
            VisibilityQ2Aus = "hidden";
            VisibilityVentilAus = "hidden";

            Margin1 = new Thickness(0, 30, 0, 0);
            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeTherorelais_F1(!_niveauRegelung.F1);
                FarbeTherorelais_F2(!_niveauRegelung.F2);
                FarbeCircle_P1(_niveauRegelung.P1);
                FarbeCircle_P2(_niveauRegelung.P2);
                FarbeCircle_P3(_niveauRegelung.P3);

                FarbeZuleitungLinksWaagrecht(_niveauRegelung.Q1);
                FarbeZuleitungLinksSenkrecht(_niveauRegelung.Q1);
                FarbeZuleitungRechtsWaagrecht(_niveauRegelung.Q2);
                FarbeZuleitungRechtsSenkrecht(_niveauRegelung.Q2);
                FarbeAbleitungOben(_niveauRegelung.Pegel > 0.01);
                FarbeAbleitungUnten(_niveauRegelung.Y1 && (_niveauRegelung.Pegel > 0.01));

                VisibilitySensorB1(_niveauRegelung.B1);
                VisibilitySensorB2(_niveauRegelung.B2);
                VisibilitySensorB3(_niveauRegelung.B3);
                VisibilityMotorQ1(_niveauRegelung.Q1);
                VisibilityMotorQ2(_niveauRegelung.Q2);
                VisibilityVentilY1(_niveauRegelung.Y1);

                Margin_1(_niveauRegelung.Pegel);

                if (_mainWindow.Plc != null)
                {
                    VersionNr = _mainWindow.VersionNummer;
                    SpsVersionLokal = _mainWindow.VersionInfo;
                    SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                    SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? "hidden" : "visible";

                    SpsColor = _mainWindow.Plc.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void TasterS1() => _niveauRegelung.S1 = ClickModeButtonS1();

        internal void TasterS2() => _niveauRegelung.S2 = !ClickModeButtonS2();

        internal void TasterS3() => _niveauRegelung.S3 = ClickModeButtonS3();

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

        #region Color Thermorelais F1

        public void FarbeTherorelais_F1(bool val) => ColorThermorelaisF1 = val ? "Red" : "LawnGreen";

        private string _colorThermorelaisF1;

        public string ColorThermorelaisF1
        {
            get => _colorThermorelaisF1;
            set
            {
                _colorThermorelaisF1 = value;
                OnPropertyChanged(nameof(ColorThermorelaisF1));
            }
        }

        #endregion Color Thermorelais F1

        #region Color Thermorelais F2

        public void FarbeTherorelais_F2(bool val) => ColorThermorelaisF2 = val ? "Red" : "LawnGreen";

        private string _colorThermorelaisF2;

        public string ColorThermorelaisF2
        {
            get => _colorThermorelaisF2;
            set
            {
                _colorThermorelaisF2 = value;
                OnPropertyChanged(nameof(ColorThermorelaisF2));
            }
        }

        #endregion Color Thermorelais F2

        #region Color P1

        public void FarbeCircle_P1(bool val) => ColorCircleP1 = val ? "Green" : "White";

        private string _colorCircleP1;

        public string ColorCircleP1
        {
            get => _colorCircleP1;
            set
            {
                _colorCircleP1 = value;
                OnPropertyChanged(nameof(ColorCircleP1));
            }
        }

        #endregion Color P1

        #region Color P2

        public void FarbeCircle_P2(bool val) => ColorCircleP2 = val ? "Red" : "White";

        private string _colorCircleP2;

        public string ColorCircleP2
        {
            get => _colorCircleP2;
            set
            {
                _colorCircleP2 = value;
                OnPropertyChanged(nameof(ColorCircleP2));
            }
        }

        #endregion Color P2

        #region Color P3

        public void FarbeCircle_P3(bool val) => ColorCircleP3 = val ? "OrangeRed" : "White";

        private string _colorCircleP3;

        public string ColorCircleP3
        {
            get => _colorCircleP3;
            set
            {
                _colorCircleP3 = value;
                OnPropertyChanged(nameof(ColorCircleP3));
            }
        }

        #endregion Color P3

        #region Color AbleitungOben

        public void FarbeAbleitungOben(bool val) => ColorAbleitungOben = val ? "Blue" : "LightBlue";

        private string _colorAbleitungOben;

        public string ColorAbleitungOben
        {
            get => _colorAbleitungOben;
            set
            {
                _colorAbleitungOben = value;
                OnPropertyChanged(nameof(ColorAbleitungOben));
            }
        }

        #endregion Color AbleitungOben

        #region Color AbleitungUnten

        public void FarbeAbleitungUnten(bool val) => ColorAbleitungUnten = val ? "Blue" : "LightBlue";

        private string _colorAbleitungUnten;

        public string ColorAbleitungUnten
        {
            get => _colorAbleitungUnten;
            set
            {
                _colorAbleitungUnten = value;
                OnPropertyChanged(nameof(ColorAbleitungUnten));
            }
        }

        #endregion Color AbleitungUnten

        #region Color ZuleitungLinksWaagrecht

        public void FarbeZuleitungLinksWaagrecht(bool val) => ColorZuleitungLinksWaagrecht = val ? "Blue" : "LightBlue";

        private string _colorZuleitungLinksWaagrecht;

        public string ColorZuleitungLinksWaagrecht
        {
            get => _colorZuleitungLinksWaagrecht;
            set
            {
                _colorZuleitungLinksWaagrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungLinksWaagrecht));
            }
        }

        #endregion Color ZuleitungLinksWaagrecht

        #region Color ZuleitungLinksSenkrecht

        public void FarbeZuleitungLinksSenkrecht(bool val) => ColorZuleitungLinksSenkrecht = val ? "Blue" : "LightBlue";

        private string _colorZuleitungLinksSenkrecht;

        public string ColorZuleitungLinksSenkrecht
        {
            get => _colorZuleitungLinksSenkrecht;
            set
            {
                _colorZuleitungLinksSenkrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungLinksSenkrecht));
            }
        }

        #endregion Color ZuleitungLinksSenkrecht

        #region Color ZuleitungRechtsWaagrecht

        public void FarbeZuleitungRechtsWaagrecht(bool val) => ColorZuleitungRechtsWaagrecht = val ? "Blue" : "LightBlue";

        private string _colorZuleitungRechtsWaagrecht;

        public string ColorZuleitungRechtsWaagrecht
        {
            get => _colorZuleitungRechtsWaagrecht;
            set
            {
                _colorZuleitungRechtsWaagrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungRechtsWaagrecht));
            }
        }

        #endregion Color ZuleitungRechtsWaagrecht

        #region Color ZuleitungRechtsSenkrecht

        public void FarbeZuleitungRechtsSenkrecht(bool val) => ColorZuleitungRechtsSenkrecht = val ? "Blue" : "LightBlue";

        private string _colorZuleitungRechtsSenkrecht;

        public string ColorZuleitungRechtsSenkrecht
        {
            get => _colorZuleitungRechtsSenkrecht;
            set
            {
                _colorZuleitungRechtsSenkrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungRechtsSenkrecht));
            }
        }

        #endregion Color ZuleitungRechtsSenkrecht

        #region Visibility Sensor B1

        public void VisibilitySensorB1(bool val)
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

        #endregion Visibility Sensor B1

        #region Visibility Sensor B2

        public void VisibilitySensorB2(bool val)
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

        #endregion Visibility Sensor B2

        #region Visibility Sensor B3

        public void VisibilitySensorB3(bool val)
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

        #endregion Visibility Sensor B3

        #region Visibility Motor Q1

        public void VisibilityMotorQ1(bool val)
        {
            if (val)
            {
                VisibilityQ1Ein = "visible";
                VisibilityQ1Aus = "hidden";
            }
            else
            {
                VisibilityQ1Ein = "hidden";
                VisibilityQ1Aus = "visible";
            }
        }

        private string _visibilityQ1Ein;

        public string VisibilityQ1Ein
        {
            get => _visibilityQ1Ein;
            set
            {
                _visibilityQ1Ein = value;
                OnPropertyChanged(nameof(VisibilityQ1Ein));
            }
        }

        private string _visibilityQ1Aus;

        public string VisibilityQ1Aus
        {
            get => _visibilityQ1Aus;
            set
            {
                _visibilityQ1Aus = value;
                OnPropertyChanged(nameof(VisibilityQ1Aus));
            }
        }

        #endregion Visibility Motor Q1

        #region Visibility Motor Q2

        public void VisibilityMotorQ2(bool val)
        {
            if (val)
            {
                VisibilityQ2Ein = "visible";
                VisibilityQ2Aus = "hidden";
            }
            else
            {
                VisibilityQ2Ein = "hidden";
                VisibilityQ2Aus = "visible";
            }
        }

        private string _visibilityQ2Ein;

        public string VisibilityQ2Ein
        {
            get => _visibilityQ2Ein;
            set
            {
                _visibilityQ2Ein = value;
                OnPropertyChanged(nameof(VisibilityQ2Ein));
            }
        }

        private string _visibilityQ2Aus;

        public string VisibilityQ2Aus
        {
            get => _visibilityQ2Aus;
            set
            {
                _visibilityQ2Aus = value;
                OnPropertyChanged(nameof(VisibilityQ2Aus));
            }
        }

        #endregion Visibility Motor Q2

        #region Visibility Ventil Y1

        public void VisibilityVentilY1(bool val)
        {
            if (val)
            {
                VisibilityVentilEin = "visible";
                VisibilityVentilAus = "hidden";
            }
            else
            {
                VisibilityVentilEin = "hidden";
                VisibilityVentilAus = "visible";
            }
        }

        private string _visibilityVentilEin;

        public string VisibilityVentilEin
        {
            get => _visibilityVentilEin;
            set
            {
                _visibilityVentilEin = value;
                OnPropertyChanged(nameof(VisibilityVentilEin));
            }
        }

        private string _visibilityVentilAus;

        public string VisibilityVentilAus
        {
            get => _visibilityVentilAus;
            set
            {
                _visibilityVentilAus = value;
                OnPropertyChanged(nameof(VisibilityVentilAus));
            }
        }

        #endregion Visibility Ventil Y1

        #region Margin1

        public void Margin_1(double pegel) => Margin1 = new Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);

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