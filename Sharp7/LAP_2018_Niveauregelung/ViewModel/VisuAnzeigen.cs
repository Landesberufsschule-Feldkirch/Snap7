namespace LAP_2018_Niveauregelung.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly double HoeheFuellBalken = 315;

        private readonly MainWindow mainWindow;
        private readonly LAP_2018_Niveauregelung.Model.NiveauRegelung niveauRegelung;
        public VisuAnzeigen(MainWindow mw, LAP_2018_Niveauregelung.Model.NiveauRegelung nr)
        {
            mainWindow = mw;
            niveauRegelung = nr;

            SpsStatus = "-";
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
            Visibility_M1_Ein = "visible";
            Visibility_M2_Ein = "visible";
            Visibility_Ventil_Ein = "visible";

            Visibility_B1_Aus = "hidden";
            Visibility_B2_Aus = "hidden";
            Visibility_B3_Aus = "hidden";
            Visibility_M1_Aus = "hidden";
            Visibility_M2_Aus = "hidden";
            Visibility_Ventil_Aus = "hidden";


            Margin1 = new Thickness(0, 30, 0, 0);
            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
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

                FarbeZuleitungLinksWaagrecht(niveauRegelung.M1);
                FarbeZuleitungLinksSenkrecht(niveauRegelung.M1);
                FarbeZuleitungRechtsWaagrecht(niveauRegelung.M2);
                FarbeZuleitungRechtsSenkrecht(niveauRegelung.M2);
                FarbeAbleitungOben(niveauRegelung.Pegel > 0.01);
                FarbeAbleitungUnten(niveauRegelung.Y1 && (niveauRegelung.Pegel > 0.01));

                VisibilitySensorB1(niveauRegelung.B1);
                VisibilitySensorB2(niveauRegelung.B2);
                VisibilitySensorB3(niveauRegelung.B3);
                VisibilityMotorM1(niveauRegelung.M1);
                VisibilityMotorM2(niveauRegelung.M2);
                VisibilityVentilY1(niveauRegelung.Y1);

                Margin_1(niveauRegelung.Pegel);


                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }

        internal void TasterS1() { niveauRegelung.S1 = ClickModeButtonS1(); }
        internal void TasterS2() { niveauRegelung.S2 = !ClickModeButtonS2(); }
        internal void TasterS3() { niveauRegelung.S3 = ClickModeButtonS3(); }




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
            get { return _clickModeBtnS1; }
            set
            {
                _clickModeBtnS1 = value;
                OnPropertyChanged(nameof(ClickModeBtnS1));
            }
        }
        #endregion

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
            get { return _clickModeBtnS2; }
            set
            {
                _clickModeBtnS2 = value;
                OnPropertyChanged(nameof(ClickModeBtnS2));
            }
        }
        #endregion

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
            get { return _clickModeBtnS3; }
            set
            {
                _clickModeBtnS3 = value;
                OnPropertyChanged(nameof(ClickModeBtnS3));
            }
        }
        #endregion




        #region Color Thermorelais F1
        public void FarbeTherorelais_F1(bool val)
        {
            if (val) ColorThermorelais_F1 = "Red"; else ColorThermorelais_F1 = "LawnGreen";
        }

        private string _colorThermorelais_F1;
        public string ColorThermorelais_F1
        {
            get { return _colorThermorelais_F1; }
            set
            {
                _colorThermorelais_F1 = value;
                OnPropertyChanged(nameof(ColorThermorelais_F1));
            }
        }
        #endregion

        #region Color Thermorelais F2
        public void FarbeTherorelais_F2(bool val)
        {
            if (val) ColorThermorelais_F2 = "Red"; else ColorThermorelais_F2 = "LawnGreen";
        }

        private string _colorThermorelais_F2;
        public string ColorThermorelais_F2
        {
            get { return _colorThermorelais_F2; }
            set
            {
                _colorThermorelais_F2 = value;
                OnPropertyChanged(nameof(ColorThermorelais_F2));
            }
        }
        #endregion


        #region Color P1
        public void FarbeCircle_P1(bool val)
        {
            if (val) ColorCircle_P1 = "Green"; else ColorCircle_P1 = "White";
        }

        private string _colorCircle_P1;
        public string ColorCircle_P1
        {
            get { return _colorCircle_P1; }
            set
            {
                _colorCircle_P1 = value;
                OnPropertyChanged(nameof(ColorCircle_P1));
            }
        }
        #endregion

        #region Color P2
        public void FarbeCircle_P2(bool val)
        {
            if (val) ColorCircle_P2 = "Red"; else ColorCircle_P2 = "White";
        }

        private string _colorCircle_P2;
        public string ColorCircle_P2
        {
            get { return _colorCircle_P2; }
            set
            {
                _colorCircle_P2 = value;
                OnPropertyChanged(nameof(ColorCircle_P2));
            }
        }
        #endregion

        #region Color P3
        public void FarbeCircle_P3(bool val)
        {
            if (val) ColorCircle_P3 = "OrangeRed"; else ColorCircle_P3 = "White";
        }

        private string _colorCircle_P3;
        public string ColorCircle_P3
        {
            get { return _colorCircle_P3; }
            set
            {
                _colorCircle_P3 = value;
                OnPropertyChanged(nameof(ColorCircle_P3));
            }
        }
        #endregion


        #region Color AbleitungOben
        public void FarbeAbleitungOben(bool val)
        {
            if (val) ColorAbleitungOben = "Blue"; else ColorAbleitungOben = "LightBlue";
        }

        private string _colorAbleitungOben;
        public string ColorAbleitungOben
        {
            get { return _colorAbleitungOben; }
            set
            {
                _colorAbleitungOben = value;
                OnPropertyChanged(nameof(ColorAbleitungOben));
            }
        }
        #endregion

        #region Color AbleitungUnten
        public void FarbeAbleitungUnten(bool val)
        {
            if (val) ColorAbleitungUnten = "Blue"; else ColorAbleitungUnten = "LightBlue";
        }

        private string _colorAbleitungUnten;
        public string ColorAbleitungUnten
        {
            get { return _colorAbleitungUnten; }
            set
            {
                _colorAbleitungUnten = value;
                OnPropertyChanged(nameof(ColorAbleitungUnten));
            }
        }
        #endregion

        #region Color ZuleitungLinksWaagrecht
        public void FarbeZuleitungLinksWaagrecht(bool val)
        {
            if (val) ColorZuleitungLinksWaagrecht = "Blue"; else ColorZuleitungLinksWaagrecht = "LightBlue";
        }

        private string _colorZuleitungLinksWaagrecht;
        public string ColorZuleitungLinksWaagrecht
        {
            get { return _colorZuleitungLinksWaagrecht; }
            set
            {
                _colorZuleitungLinksWaagrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungLinksWaagrecht));
            }
        }
        #endregion

        #region Color ZuleitungLinksSenkrecht
        public void FarbeZuleitungLinksSenkrecht(bool val)
        {
            if (val) ColorZuleitungLinksSenkrecht = "Blue"; else ColorZuleitungLinksSenkrecht = "LightBlue";
        }

        private string _colorZuleitungLinksSenkrecht;
        public string ColorZuleitungLinksSenkrecht
        {
            get { return _colorZuleitungLinksSenkrecht; }
            set
            {
                _colorZuleitungLinksSenkrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungLinksSenkrecht));
            }
        }
        #endregion

        #region Color ZuleitungRechtsWaagrecht
        public void FarbeZuleitungRechtsWaagrecht(bool val)
        {
            if (val) ColorZuleitungRechtsWaagrecht = "Blue"; else ColorZuleitungRechtsWaagrecht = "LightBlue";
        }

        private string _colorZuleitungRechtsWaagrecht;
        public string ColorZuleitungRechtsWaagrecht
        {
            get { return _colorZuleitungRechtsWaagrecht; }
            set
            {
                _colorZuleitungRechtsWaagrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungRechtsWaagrecht));
            }
        }
        #endregion

        #region Color ZuleitungRechtsSenkrecht
        public void FarbeZuleitungRechtsSenkrecht(bool val)
        {
            if (val) ColorZuleitungRechtsSenkrecht = "Blue"; else ColorZuleitungRechtsSenkrecht = "LightBlue";
        }

        private string _colorZuleitungRechtsSenkrecht;
        public string ColorZuleitungRechtsSenkrecht
        {
            get { return _colorZuleitungRechtsSenkrecht; }
            set
            {
                _colorZuleitungRechtsSenkrecht = value;
                OnPropertyChanged(nameof(ColorZuleitungRechtsSenkrecht));
            }
        }
        #endregion





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
            get { return _visibility_B1_Ein; }
            set
            {
                _visibility_B1_Ein = value;
                OnPropertyChanged(nameof(Visibility_B1_Ein));
            }
        }

        private string _visibility_B1_Aus;
        public string Visibility_B1_Aus
        {
            get { return _visibility_B1_Aus; }
            set
            {
                _visibility_B1_Aus = value;
                OnPropertyChanged(nameof(Visibility_B1_Aus));
            }
        }
        #endregion

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
            get { return _visibility_B2_Ein; }
            set
            {
                _visibility_B2_Ein = value;
                OnPropertyChanged(nameof(Visibility_B2_Ein));
            }
        }

        private string _visibility_B2_Aus;
        public string Visibility_B2_Aus
        {
            get { return _visibility_B2_Aus; }
            set
            {
                _visibility_B2_Aus = value;
                OnPropertyChanged(nameof(Visibility_B2_Aus));
            }
        }
        #endregion

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
            get { return _visibility_B3_Ein; }
            set
            {
                _visibility_B3_Ein = value;
                OnPropertyChanged(nameof(Visibility_B3_Ein));
            }
        }

        private string _visibility_B3_Aus;
        public string Visibility_B3_Aus
        {
            get { return _visibility_B3_Aus; }
            set
            {
                _visibility_B3_Aus = value;
                OnPropertyChanged(nameof(Visibility_B3_Aus));
            }
        }
        #endregion


        #region Visibility Motor M1
        public void VisibilityMotorM1(bool val)
        {
            if (val)
            {
                Visibility_M1_Ein = "visible";
                Visibility_M1_Aus = "hidden";
            }
            else
            {
                Visibility_M1_Ein = "hidden";
                Visibility_M1_Aus = "visible";
            }
        }

        private string _visibility_M1_Ein;
        public string Visibility_M1_Ein
        {
            get { return _visibility_M1_Ein; }
            set
            {
                _visibility_M1_Ein = value;
                OnPropertyChanged(nameof(Visibility_M1_Ein));
            }
        }

        private string _visibility_M1_Aus;
        public string Visibility_M1_Aus
        {
            get { return _visibility_M1_Aus; }
            set
            {
                _visibility_M1_Aus = value;
                OnPropertyChanged(nameof(Visibility_M1_Aus));
            }
        }
        #endregion

        #region Visibility Motor M2
        public void VisibilityMotorM2(bool val)
        {
            if (val)
            {
                Visibility_M2_Ein = "visible";
                Visibility_M2_Aus = "hidden";
            }
            else
            {
                Visibility_M2_Ein = "hidden";
                Visibility_M2_Aus = "visible";
            }
        }

        private string _visibility_M2_Ein;
        public string Visibility_M2_Ein
        {
            get { return _visibility_M2_Ein; }
            set
            {
                _visibility_M2_Ein = value;
                OnPropertyChanged(nameof(Visibility_M2_Ein));
            }
        }

        private string _visibility_M2_Aus;
        public string Visibility_M2_Aus
        {
            get { return _visibility_M2_Aus; }
            set
            {
                _visibility_M2_Aus = value;
                OnPropertyChanged(nameof(Visibility_M2_Aus));
            }
        }
        #endregion


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
            get { return _visibility_Ventil_Ein; }
            set
            {
                _visibility_Ventil_Ein = value;
                OnPropertyChanged(nameof(Visibility_Ventil_Ein));
            }
        }

        private string _visibility_Ventil_Aus;
        public string Visibility_Ventil_Aus
        {
            get { return _visibility_Ventil_Aus; }
            set
            {
                _visibility_Ventil_Aus = value;
                OnPropertyChanged(nameof(Visibility_Ventil_Aus));
            }
        }
        #endregion




        #region Margin1
        public void Margin_1(double pegel)
        {
            Margin1 = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);
        }

        private Thickness _margin1;
        public Thickness Margin1
        {
            get { return _margin1; }
            set
            {
                _margin1 = value;
                OnPropertyChanged(nameof(Margin1));
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
