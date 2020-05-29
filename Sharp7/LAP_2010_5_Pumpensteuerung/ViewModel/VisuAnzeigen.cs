namespace LAP_2010_5_Pumpensteuerung.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly double HoeheFuellBalken = 9 * 35;

        private readonly MainWindow mainWindow;
        private readonly LAP_2010_5_Pumpensteuerung.Model.Pumpensteuerung pumpensteuerung;

        public VisuAnzeigen(MainWindow mw, LAP_2010_5_Pumpensteuerung.Model.Pumpensteuerung nr)
        {
            mainWindow = mw;
            pumpensteuerung = nr;

            SpsStatus = "-";
            SpsColor = "LightBlue";

            ClickModeBtnS3 = "Press";

            ColorThermorelais_F1 = "LawnGreen";

            ColorAbleitungOben = "LightBlue";
            ColorAbleitungUnten = "LightBlue";
            ColorZuleitungLinksWaagrecht = "LightBlue";
            ColorZuleitungLinksSenkrecht = "LightBlue";

            ColorCircle_P1 = "LightGray";
            ColorCircle_P2 = "LightGray";

            VisibilityQ1Ein = "visible";
            VisibilityQ1Aus = "hidden";

            VisibilityB1Ein = "hidden";
            VisibilityB1Aus = "visible";

            VisibilityB2Ein = "Visible";
            VisibilityB2Aus = "Hidden";

            VisibilityY1Ein = "visible";
            VisibilityY1Aus = "hidden";

            WinkelSchalter = 0;

            Margin1 = new Thickness(0, 30, 0, 0);

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeTherorelais_F1(pumpensteuerung.F1);
                FarbeCircle_P1(pumpensteuerung.P1);
                FarbeCircle_P2(pumpensteuerung.P2);
                FarbeZuleitungLinksWaagrecht(pumpensteuerung.Q1);
                FarbeZuleitungLinksSenkrecht(pumpensteuerung.Q1);
                FarbeAbleitungOben(pumpensteuerung.Pegel > 0.01);
                FarbeAbleitungUnten((pumpensteuerung.Pegel > 0.01) && pumpensteuerung.Y1);

                SichtbarkeitQ1(pumpensteuerung.Q1);
                SichtbarkeitB1(pumpensteuerung.B1);
                SichtbarkeitB2(pumpensteuerung.B2);
                SichtbarkeitY1(pumpensteuerung.Y1);

                Margin_1(pumpensteuerung.Pegel);

                SchalterWinkel(pumpensteuerung.S1, pumpensteuerung.S2);

                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }

        internal void SetS3() => pumpensteuerung.S3 = ClickModeButtonS3();


        #region SPS Status und Farbe

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

        #region SchalterWinkel

        private void SchalterWinkel(bool s1, bool s2)
        {
            WinkelSchalter = 0;
            if (s1) WinkelSchalter = -45;
            if (s2) WinkelSchalter = 45;
        }

        private int _winkelSchalter;

        public int WinkelSchalter
        {
            get => _winkelSchalter;
            set
            {
                _winkelSchalter = value;
                OnPropertyChanged(nameof(WinkelSchalter));
            }
        }

        #endregion SchalterWinkel

        #region ClickModeButtonS3

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

        #endregion ClickModeButtonS3

        #region Color Thermorelais F1

        public void FarbeTherorelais_F1(bool val) { if (val) ColorThermorelais_F1 = "LawnGreen"; else ColorThermorelais_F1 = "Red"; }

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

        #region Color P1

        public void FarbeCircle_P1(bool val) { if (val) ColorCircle_P1 = "lawngreen"; else ColorCircle_P1 = "LightGray"; }

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

        public void FarbeCircle_P2(bool val) { if (val) ColorCircle_P2 = "red"; else ColorCircle_P2 = "LightGray"; }

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

        #region Color AbleitungOben

        public void FarbeAbleitungOben(bool val) { if (val) ColorAbleitungOben = "Blue"; else ColorAbleitungOben = "LightBlue"; }

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

        public void FarbeAbleitungUnten(bool val) { if (val) ColorAbleitungUnten = "Blue"; else ColorAbleitungUnten = "LightBlue"; }

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

        public void FarbeZuleitungLinksWaagrecht(bool val) { if (val) ColorZuleitungLinksWaagrecht = "Blue"; else ColorZuleitungLinksWaagrecht = "LightBlue"; }

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

        public void FarbeZuleitungLinksSenkrecht(bool val) { if (val) ColorZuleitungLinksSenkrecht = "Blue"; else ColorZuleitungLinksSenkrecht = "LightBlue"; }

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

        #region Sichtbarkeit Q1

        public void SichtbarkeitQ1(bool val)
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

        #endregion Sichtbarkeit Q1

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

        #region Sichtbarkeit Y1

        public void SichtbarkeitY1(bool val)
        {
            if (val)
            {
                VisibilityY1Ein = "visible";
                VisibilityY1Aus = "hidden";
            }
            else
            {
                VisibilityY1Ein = "hidden";
                VisibilityY1Aus = "visible";
            }
        }

        private string _visibilityY1Ein;

        public string VisibilityY1Ein
        {
            get => _visibilityY1Ein;
            set
            {
                _visibilityY1Ein = value;
                OnPropertyChanged(nameof(VisibilityY1Ein));
            }
        }

        private string _visibilityY1Aus;

        public string VisibilityY1Aus
        {
            get => _visibilityY1Aus;
            set
            {
                _visibilityY1Aus = value;
                OnPropertyChanged(nameof(VisibilityY1Aus));
            }
        }

        #endregion Sichtbarkeit Y1

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