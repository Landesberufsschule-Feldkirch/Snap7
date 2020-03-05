namespace LAP_2010_5_Pumpensteuerung.ViewModel
{
    using System;
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

            ColorThermorelais_F5 = "LawnGreen";

            ColorAbleitungOben = "LightBlue";
            ColorAbleitungUnten = "LightBlue";
            ColorZuleitungLinksWaagrecht = "LightBlue";
            ColorZuleitungLinksSenkrecht = "LightBlue";

            ColorCircle_H1 = "LightGray";
            ColorCircle_H2 = "LightGray";

            VisibilityK1Ein = "visible";
            VisibilityK1Aus = "hidden";

            VisibilityS7Ein = "hidden";
            VisibilityS7Aus = "visible";

            VisibilityS8Ein = "Visible";
            VisibilityS8Aus = "Hidden";

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
                FarbeTherorelais_F5(pumpensteuerung.F5);
                FarbeCircle_H1(pumpensteuerung.H1);
                FarbeCircle_H2(pumpensteuerung.H2);
                FarbeZuleitungLinksWaagrecht(pumpensteuerung.K1);
                FarbeZuleitungLinksSenkrecht(pumpensteuerung.K1);
                FarbeAbleitungOben(pumpensteuerung.Pegel > 0.01);
                FarbeAbleitungUnten((pumpensteuerung.Pegel > 0.01) && pumpensteuerung.Y1);

                SichtbarkeitK1(pumpensteuerung.K1);
                SichtbarkeitS7(pumpensteuerung.S7);
                SichtbarkeitS8(pumpensteuerung.S8);
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
        
        internal void SetS3() { pumpensteuerung.S3 = ClickModeButtonS3(); }
        

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

        internal void TasterS3()
        {
            throw new NotImplementedException();
        }
        #endregion

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
            get { return _winkelSchalter; }
            set
            {
                _winkelSchalter = value;
                OnPropertyChanged(nameof(WinkelSchalter));
            }

        }

        #endregion

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
            get { return _clickModeBtnS3; }
            set
            {
                _clickModeBtnS3 = value;
                OnPropertyChanged(nameof(ClickModeBtnS3));
            }
        }
#endregion

        #region Color Thermorelais F5
        public void FarbeTherorelais_F5(bool val)
        {
            if (val) ColorThermorelais_F5 = "LawnGreen"; else ColorThermorelais_F5 = "Red";
        }

        private string _colorThermorelais_F5;
        public string ColorThermorelais_F5
        {
            get { return _colorThermorelais_F5; }
            set
            {
                _colorThermorelais_F5 = value;
                OnPropertyChanged(nameof(ColorThermorelais_F5));
            }
        }
        #endregion


        #region Color H1
        public void FarbeCircle_H1(bool val)
        {
            if (val) ColorCircle_H1 = "lawngreen"; else ColorCircle_H1 = "LightGray";
        }

        private string _colorCircle_H1;
        public string ColorCircle_H1
        {
            get { return _colorCircle_H1; }
            set
            {
                _colorCircle_H1 = value;
                OnPropertyChanged(nameof(ColorCircle_H1));
            }
        }
        #endregion

        #region Color H2
        public void FarbeCircle_H2(bool val)
        {
            if (val) ColorCircle_H2 = "red"; else ColorCircle_H2 = "LightGray";
        }

        private string _colorCircle_H2;
        public string ColorCircle_H2
        {
            get { return _colorCircle_H2; }
            set
            {
                _colorCircle_H2 = value;
                OnPropertyChanged(nameof(ColorCircle_H2));
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
            get { return _visibilityK1Ein; }
            set
            {
                _visibilityK1Ein = value;
                OnPropertyChanged(nameof(VisibilityK1Ein));
            }
        }

        private string _visibilityK1Aus;
        public string VisibilityK1Aus
        {
            get { return _visibilityK1Aus; }
            set
            {
                _visibilityK1Aus = value;
                OnPropertyChanged(nameof(VisibilityK1Aus));
            }
        }
        #endregion

        #region Sichtbarkeit S7
        public void SichtbarkeitS7(bool val)
        {
            if (val)
            {
                VisibilityS7Ein = "visible";
                VisibilityS7Aus = "hidden";
            }
            else
            {
                VisibilityS7Ein = "hidden";
                VisibilityS7Aus = "visible";
            }
        }

        private string _visibilityS7Ein;
        public string VisibilityS7Ein
        {
            get { return _visibilityS7Ein; }
            set
            {
                _visibilityS7Ein = value;
                OnPropertyChanged(nameof(VisibilityS7Ein));
            }
        }

        private string _visibilityS7Aus;
        public string VisibilityS7Aus
        {
            get { return _visibilityS7Aus; }
            set
            {
                _visibilityS7Aus = value;
                OnPropertyChanged(nameof(VisibilityS7Aus));
            }
        }
        #endregion

        #region Sichtbarkeit S8
        public void SichtbarkeitS8(bool val)
        {
            if (val)
            {
                VisibilityS8Ein = "Visible";
                VisibilityS8Aus = "Hidden";
            }
            else
            {
                VisibilityS8Ein = "Hidden";
                VisibilityS8Aus = "Visible";
            }
        }

        private string _visibilityS8Ein;
        public string VisibilityS8Ein
        {
            get { return _visibilityS8Ein; }
            set
            {
                _visibilityS8Ein = value;
                OnPropertyChanged(nameof(VisibilityS8Ein));
            }
        }

        private string _visibilityS8Aus;

        public string VisibilityS8Aus
        {
            get { return _visibilityS8Aus; }
            set
            {
                _visibilityS8Aus = value;
                OnPropertyChanged(nameof(VisibilityS8Aus));
            }
        }
        #endregion

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
            get { return _visibilityY1Ein; }
            set
            {
                _visibilityY1Ein = value;
                OnPropertyChanged(nameof(VisibilityY1Ein));
            }
        }

        private string _visibilityY1Aus;
        public string VisibilityY1Aus
        {
            get { return _visibilityY1Aus; }
            set
            {
                _visibilityY1Aus = value;
                OnPropertyChanged(nameof(VisibilityY1Aus));
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
