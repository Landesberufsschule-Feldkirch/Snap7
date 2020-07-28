namespace LAP_2018_4_Niveauregelung.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly double HoeheFuellBalken = 315;

        private readonly MainWindow mainWindow;
        private readonly LAP_2018_4_Niveauregelung.Model.NiveauRegelung niveauRegelung;

        public VisuAnzeigen(MainWindow mw, LAP_2018_4_Niveauregelung.Model.NiveauRegelung nr)
        {
            mainWindow = mw;
            niveauRegelung = nr;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SPSVersionLokal = "fehlt";
            SPSVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            ClickModeBtnS1 = "Press";
            ClickModeBtnS2 = "Press";
            ClickModeBtnS3 = "Press";

            ColorThermorelais_F1 = "LawnGreen";
            ColorThermorelais_F2 = "LawnGreen";

            ColorCircle_P1 = "White";
            ColorCircle_P2 = "White";
            ColorCircle_P2 = "White";

            ColorAbleitungOben = "LightBlue";
            ColorAbleitungUnten = "LightBlue";
            ColorZuleitungLinksWaagrecht = "LightBlue";
            ColorZuleitungLinksSenkrecht = "LightBlue";
            ColorZuleitungRechtsWaagrecht = "LightBlue";
            ColorZuleitungRechtsSenkrecht = "LightBlue";

            Visibility_B1_Ein = "visible";
            Visibility_B2_Ein = "visible";
            Visibility_B3_Ein = "visible";
            Visibility_Q1_Ein = "visible";
            Visibility_Q2_Ein = "visible";
            Visibility_Ventil_Ein = "visible";

            Visibility_B1_Aus = "hidden";
            Visibility_B2_Aus = "hidden";
            Visibility_B3_Aus = "hidden";
            Visibility_Q1_Aus = "hidden";
            Visibility_Q2_Aus = "hidden";
            Visibility_Ventil_Aus = "hidden";

            Margin1 = new Thickness(0, 30, 0, 0);
            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeTherorelais_F1(!niveauRegelung.F1);
                FarbeTherorelais_F2(!niveauRegelung.F2);
                FarbeCircle_P1(niveauRegelung.P1);
                FarbeCircle_P2(niveauRegelung.P2);
                FarbeCircle_P3(niveauRegelung.P3);

                FarbeZuleitungLinksWaagrecht(niveauRegelung.Q1);
                FarbeZuleitungLinksSenkrecht(niveauRegelung.Q1);
                FarbeZuleitungRechtsWaagrecht(niveauRegelung.Q2);
                FarbeZuleitungRechtsSenkrecht(niveauRegelung.Q2);
                FarbeAbleitungOben(niveauRegelung.Pegel > 0.01);
                FarbeAbleitungUnten(niveauRegelung.Y1 && (niveauRegelung.Pegel > 0.01));

                VisibilitySensorB1(niveauRegelung.B1);
                VisibilitySensorB2(niveauRegelung.B2);
                VisibilitySensorB3(niveauRegelung.B3);
                VisibilityMotorQ1(niveauRegelung.Q1);
                VisibilityMotorQ2(niveauRegelung.Q2);
                VisibilityVentilY1(niveauRegelung.Y1);

                Margin_1(niveauRegelung.Pegel);

                if (mainWindow.S7_1200 != null)
                {
                    SPSVersionLokal = mainWindow.VersionInfo;
                    SPSVersionEntfernt = mainWindow.S7_1200.GetVersion();                  
                    if (SPSVersionLokal == SPSVersionEntfernt) SpsVersionsInfoSichtbar = "hidden"; else SpsVersionsInfoSichtbar = "visible";

                    SpsColor = mainWindow.S7_1200.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }

        internal void TasterS1() => niveauRegelung.S1 = ClickModeButtonS1();

        internal void TasterS2() => niveauRegelung.S2 = !ClickModeButtonS2();

        internal void TasterS3() => niveauRegelung.S3 = ClickModeButtonS3();

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

        private string _sPSVersionLokal;
        public string SPSVersionLokal
        {
            get => _sPSVersionLokal;
            set
            {
                _sPSVersionLokal = value;
                OnPropertyChanged(nameof(SPSVersionLokal));
            }
        }

        private string _sPSVersionEntfernt;
        public string SPSVersionEntfernt
        {
            get => _sPSVersionEntfernt;
            set
            {
                _sPSVersionEntfernt = value;
                OnPropertyChanged(nameof(SPSVersionEntfernt));
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

        public void FarbeTherorelais_F1(bool val)
        {
            if (val) ColorThermorelais_F1 = "Red"; else ColorThermorelais_F1 = "LawnGreen";
        }

        private string _colorThermorelais_F1;

        public string ColorThermorelais_F1
        {
            get => _colorThermorelais_F1;
            set
            {
                _colorThermorelais_F1 = value;
                OnPropertyChanged(nameof(ColorThermorelais_F1));
            }
        }

        #endregion Color Thermorelais F1

        #region Color Thermorelais F2

        public void FarbeTherorelais_F2(bool val)
        {
            if (val) ColorThermorelais_F2 = "Red"; else ColorThermorelais_F2 = "LawnGreen";
        }

        private string _colorThermorelais_F2;

        public string ColorThermorelais_F2
        {
            get => _colorThermorelais_F2;
            set
            {
                _colorThermorelais_F2 = value;
                OnPropertyChanged(nameof(ColorThermorelais_F2));
            }
        }

        #endregion Color Thermorelais F2

        #region Color P1

        public void FarbeCircle_P1(bool val)
        {
            if (val) ColorCircle_P1 = "Green"; else ColorCircle_P1 = "White";
        }

        private string _colorCircle_P1;

        public string ColorCircle_P1
        {
            get => _colorCircle_P1;
            set
            {
                _colorCircle_P1 = value;
                OnPropertyChanged(nameof(ColorCircle_P1));
            }
        }

        #endregion Color P1

        #region Color P2

        public void FarbeCircle_P2(bool val)
        {
            if (val) ColorCircle_P2 = "Red"; else ColorCircle_P2 = "White";
        }

        private string _colorCircle_P2;

        public string ColorCircle_P2
        {
            get => _colorCircle_P2;
            set
            {
                _colorCircle_P2 = value;
                OnPropertyChanged(nameof(ColorCircle_P2));
            }
        }

        #endregion Color P2

        #region Color P3

        public void FarbeCircle_P3(bool val)
        {
            if (val) ColorCircle_P3 = "OrangeRed"; else ColorCircle_P3 = "White";
        }

        private string _colorCircle_P3;

        public string ColorCircle_P3
        {
            get => _colorCircle_P3;
            set
            {
                _colorCircle_P3 = value;
                OnPropertyChanged(nameof(ColorCircle_P3));
            }
        }

        #endregion Color P3

        #region Color AbleitungOben

        public void FarbeAbleitungOben(bool val)
        {
            if (val) ColorAbleitungOben = "Blue"; else ColorAbleitungOben = "LightBlue";
        }

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

        public void FarbeAbleitungUnten(bool val)
        {
            if (val) ColorAbleitungUnten = "Blue"; else ColorAbleitungUnten = "LightBlue";
        }

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

        public void FarbeZuleitungLinksWaagrecht(bool val)
        {
            if (val) ColorZuleitungLinksWaagrecht = "Blue"; else ColorZuleitungLinksWaagrecht = "LightBlue";
        }

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

        public void FarbeZuleitungLinksSenkrecht(bool val)
        {
            if (val) ColorZuleitungLinksSenkrecht = "Blue"; else ColorZuleitungLinksSenkrecht = "LightBlue";
        }

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

        public void FarbeZuleitungRechtsWaagrecht(bool val)
        {
            if (val) ColorZuleitungRechtsWaagrecht = "Blue"; else ColorZuleitungRechtsWaagrecht = "LightBlue";
        }

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

        public void FarbeZuleitungRechtsSenkrecht(bool val)
        {
            if (val) ColorZuleitungRechtsSenkrecht = "Blue"; else ColorZuleitungRechtsSenkrecht = "LightBlue";
        }

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
                Visibility_B1_Ein = "visible";
                Visibility_B1_Aus = "hidden";
            }
            else
            {
                Visibility_B1_Ein = "hidden";
                Visibility_B1_Aus = "visible";
            }
        }

        private string _visibility_B1_Ein;

        public string Visibility_B1_Ein
        {
            get => _visibility_B1_Ein;
            set
            {
                _visibility_B1_Ein = value;
                OnPropertyChanged(nameof(Visibility_B1_Ein));
            }
        }

        private string _visibility_B1_Aus;

        public string Visibility_B1_Aus
        {
            get => _visibility_B1_Aus;
            set
            {
                _visibility_B1_Aus = value;
                OnPropertyChanged(nameof(Visibility_B1_Aus));
            }
        }

        #endregion Visibility Sensor B1

        #region Visibility Sensor B2

        public void VisibilitySensorB2(bool val)
        {
            if (val)
            {
                Visibility_B2_Ein = "visible";
                Visibility_B2_Aus = "hidden";
            }
            else
            {
                Visibility_B2_Ein = "hidden";
                Visibility_B2_Aus = "visible";
            }
        }

        private string _visibility_B2_Ein;

        public string Visibility_B2_Ein
        {
            get => _visibility_B2_Ein;
            set
            {
                _visibility_B2_Ein = value;
                OnPropertyChanged(nameof(Visibility_B2_Ein));
            }
        }

        private string _visibility_B2_Aus;

        public string Visibility_B2_Aus
        {
            get => _visibility_B2_Aus;
            set
            {
                _visibility_B2_Aus = value;
                OnPropertyChanged(nameof(Visibility_B2_Aus));
            }
        }

        #endregion Visibility Sensor B2

        #region Visibility Sensor B3

        public void VisibilitySensorB3(bool val)
        {
            if (val)
            {
                Visibility_B3_Ein = "visible";
                Visibility_B3_Aus = "hidden";
            }
            else
            {
                Visibility_B3_Ein = "hidden";
                Visibility_B3_Aus = "visible";
            }
        }

        private string _visibility_B3_Ein;

        public string Visibility_B3_Ein
        {
            get => _visibility_B3_Ein;
            set
            {
                _visibility_B3_Ein = value;
                OnPropertyChanged(nameof(Visibility_B3_Ein));
            }
        }

        private string _visibility_B3_Aus;

        public string Visibility_B3_Aus
        {
            get => _visibility_B3_Aus;
            set
            {
                _visibility_B3_Aus = value;
                OnPropertyChanged(nameof(Visibility_B3_Aus));
            }
        }

        #endregion Visibility Sensor B3

        #region Visibility Motor Q1

        public void VisibilityMotorQ1(bool val)
        {
            if (val)
            {
                Visibility_Q1_Ein = "visible";
                Visibility_Q1_Aus = "hidden";
            }
            else
            {
                Visibility_Q1_Ein = "hidden";
                Visibility_Q1_Aus = "visible";
            }
        }

        private string _visibility_Q1_Ein;

        public string Visibility_Q1_Ein
        {
            get => _visibility_Q1_Ein;
            set
            {
                _visibility_Q1_Ein = value;
                OnPropertyChanged(nameof(Visibility_Q1_Ein));
            }
        }

        private string _visibility_Q1_Aus;

        public string Visibility_Q1_Aus
        {
            get => _visibility_Q1_Aus;
            set
            {
                _visibility_Q1_Aus = value;
                OnPropertyChanged(nameof(Visibility_Q1_Aus));
            }
        }

        #endregion Visibility Motor Q1

        #region Visibility Motor Q2

        public void VisibilityMotorQ2(bool val)
        {
            if (val)
            {
                Visibility_Q2_Ein = "visible";
                Visibility_Q2_Aus = "hidden";
            }
            else
            {
                Visibility_Q2_Ein = "hidden";
                Visibility_Q2_Aus = "visible";
            }
        }

        private string _visibility_Q2_Ein;

        public string Visibility_Q2_Ein
        {
            get => _visibility_Q2_Ein;
            set
            {
                _visibility_Q2_Ein = value;
                OnPropertyChanged(nameof(Visibility_Q2_Ein));
            }
        }

        private string _visibility_Q2_Aus;

        public string Visibility_Q2_Aus
        {
            get => _visibility_Q2_Aus;
            set
            {
                _visibility_Q2_Aus = value;
                OnPropertyChanged(nameof(Visibility_Q2_Aus));
            }
        }

        #endregion Visibility Motor Q2

        #region Visibility Ventil Y1

        public void VisibilityVentilY1(bool val)
        {
            if (val)
            {
                Visibility_Ventil_Ein = "visible";
                Visibility_Ventil_Aus = "hidden";
            }
            else
            {
                Visibility_Ventil_Ein = "hidden";
                Visibility_Ventil_Aus = "visible";
            }
        }

        private string _visibility_Ventil_Ein;

        public string Visibility_Ventil_Ein
        {
            get => _visibility_Ventil_Ein;
            set
            {
                _visibility_Ventil_Ein = value;
                OnPropertyChanged(nameof(Visibility_Ventil_Ein));
            }
        }

        private string _visibility_Ventil_Aus;

        public string Visibility_Ventil_Aus
        {
            get => _visibility_Ventil_Aus;
            set
            {
                _visibility_Ventil_Aus = value;
                OnPropertyChanged(nameof(Visibility_Ventil_Aus));
            }
        }

        #endregion Visibility Ventil Y1

        #region Margin1

        public void Margin_1(double pegel)
        {
            Margin1 = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);
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