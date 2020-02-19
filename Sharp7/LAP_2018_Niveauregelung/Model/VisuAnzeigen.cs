﻿namespace LAP_2018_Niveauregelung.Model
{
    using System.ComponentModel;
    using System.Windows;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly double HoeheFuellBalken = 315;
        
        public VisuAnzeigen()
        {
            SpsStatus = "-";
            SpsColor = "LightBlue";

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
        }



        private string _SpsStatus;
        public string SpsStatus
        {
            get { return _SpsStatus; }
            set
            {
                _SpsStatus = value;
                OnPropertyChanged("SpsStatus");
            }
        }

        private string _SpsColor;
        public string SpsColor
        {
            get { return _SpsColor; }
            set
            {
                _SpsColor = value;
                OnPropertyChanged("SpsColor");
            }
        }


        #region Color Thermorelais F1
        public void FarbeTherorelais_F1(bool val)
        {
            if (val) ColorThermorelais_F1 = "Red"; else ColorThermorelais_F1 = "LawnGreen";
        }

        private string _ColorThermorelais_F1;
        public string ColorThermorelais_F1
        {
            get { return _ColorThermorelais_F1; }
            set
            {
                _ColorThermorelais_F1 = value;
                OnPropertyChanged("ColorThermorelais_F1");
            }
        }
        #endregion

        #region Color Thermorelais F2
        public void FarbeTherorelais_F2(bool val)
        {
            if (val) ColorThermorelais_F2 = "Red"; else ColorThermorelais_F2 = "LawnGreen";
        }

        private string _ColorThermorelais_F2;
        public string ColorThermorelais_F2
        {
            get { return _ColorThermorelais_F2; }
            set
            {
                _ColorThermorelais_F2 = value;
                OnPropertyChanged("ColorThermorelais_F2");
            }
        }
        #endregion

        
        #region Color P1
        public void FarbeCircle_P1(bool val)
        {
            if (val) ColorCircle_P1 = "Green"; else ColorCircle_P1 = "White";
        }

        private string _ColorCircle_P1;
        public string ColorCircle_P1
        {
            get { return _ColorCircle_P1; }
            set
            {
                _ColorCircle_P1 = value;
                OnPropertyChanged("ColorCircle_P1");
            }
        }
        #endregion

        #region Color P2
        public void FarbeCircle_P2(bool val)
        {
            if (val) ColorCircle_P2 = "Red"; else ColorCircle_P2 = "White";
        }

        private string _ColorCircle_P2;
        public string ColorCircle_P2
        {
            get { return _ColorCircle_P2; }
            set
            {
                _ColorCircle_P2 = value;
                OnPropertyChanged("ColorCircle_P2");
            }
        }
        #endregion

        #region Color P3
        public void FarbeCircle_P3(bool val)
        {
            if (val) ColorCircle_P3 = "OrangeRed"; else ColorCircle_P3 = "White";
        }

        private string _ColorCircle_P3;
        public string ColorCircle_P3
        {
            get { return _ColorCircle_P3; }
            set
            {
                _ColorCircle_P3 = value;
                OnPropertyChanged("ColorCircle_P3");
            }
        }
        #endregion
               

        #region Color AbleitungOben
        public void FarbeAbleitungOben(bool val)
        {
            if (val) ColorAbleitungOben = "Blue"; else ColorAbleitungOben = "LightBlue";
        }

        private string _ColorAbleitungOben;
        public string ColorAbleitungOben
        {
            get { return _ColorAbleitungOben; }
            set
            {
                _ColorAbleitungOben = value;
                OnPropertyChanged("ColorAbleitungOben");
            }
        }
        #endregion

        #region Color AbleitungUnten
        public void FarbeAbleitungUnten(bool val)
        {
            if (val) ColorAbleitungUnten = "Blue"; else ColorAbleitungUnten = "LightBlue";
        }

        private string _ColorAbleitungUnten;
        public string ColorAbleitungUnten
        {
            get { return _ColorAbleitungUnten; }
            set
            {
                _ColorAbleitungUnten = value;
                OnPropertyChanged("ColorAbleitungUnten");
            }
        }
        #endregion

        #region Color ZuleitungLinksWaagrecht
        public void FarbeZuleitungLinksWaagrecht(bool val)
        {
            if (val) ColorZuleitungLinksWaagrecht = "Blue"; else ColorZuleitungLinksWaagrecht = "LightBlue";
        }

        private string _ColorZuleitungLinksWaagrecht;
        public string ColorZuleitungLinksWaagrecht
        {
            get { return _ColorZuleitungLinksWaagrecht; }
            set
            {
                _ColorZuleitungLinksWaagrecht = value;
                OnPropertyChanged("ColorZuleitungLinksWaagrecht");
            }
        }
        #endregion

        #region Color ZuleitungLinksSenkrecht
        public void FarbeZuleitungLinksSenkrecht(bool val)
        {
            if (val) ColorZuleitungLinksSenkrecht = "Blue"; else ColorZuleitungLinksSenkrecht = "LightBlue";
        }

        private string _ColorZuleitungLinksSenkrecht;
        public string ColorZuleitungLinksSenkrecht
        {
            get { return _ColorZuleitungLinksSenkrecht; }
            set
            {
                _ColorZuleitungLinksSenkrecht = value;
                OnPropertyChanged("ColorZuleitungLinksSenkrecht");
            }
        }
        #endregion

        #region Color ZuleitungRechtsWaagrecht
        public void FarbeZuleitungRechtsWaagrecht(bool val)
        {
            if (val) ColorZuleitungRechtsWaagrecht = "Blue"; else ColorZuleitungRechtsWaagrecht = "LightBlue";
        }

        private string _ColorZuleitungRechtsWaagrecht;
        public string ColorZuleitungRechtsWaagrecht
        {
            get { return _ColorZuleitungRechtsWaagrecht; }
            set
            {
                _ColorZuleitungRechtsWaagrecht = value;
                OnPropertyChanged("ColorZuleitungRechtsWaagrecht");
            }
        }
        #endregion

        #region Color ZuleitungRechtsSenkrecht
        public void FarbeZuleitungRechtsSenkrecht(bool val)
        {
            if (val) ColorZuleitungRechtsSenkrecht = "Blue"; else ColorZuleitungRechtsSenkrecht = "LightBlue";
        }

        private string _ColorZuleitungRechtsSenkrecht;
        public string ColorZuleitungRechtsSenkrecht
        {
            get { return _ColorZuleitungRechtsSenkrecht; }
            set
            {
                _ColorZuleitungRechtsSenkrecht = value;
                OnPropertyChanged("ColorZuleitungRechtsSenkrecht");
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

        private string _Visibility_B1_Ein;
        public string Visibility_B1_Ein
        {
            get { return _Visibility_B1_Ein; }
            set
            {
                _Visibility_B1_Ein = value;
                OnPropertyChanged("Visibility_B1_Ein");
            }
        }

        private string _Visibility_B1_Aus;
        public string Visibility_B1_Aus
        {
            get { return _Visibility_B1_Aus; }
            set
            {
                _Visibility_B1_Aus = value;
                OnPropertyChanged("Visibility_B1_Aus");
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

        private string _Visibility_B2_Ein;
        public string Visibility_B2_Ein
        {
            get { return _Visibility_B2_Ein; }
            set
            {
                _Visibility_B2_Ein = value;
                OnPropertyChanged("Visibility_B2_Ein");
            }
        }

        private string _Visibility_B2_Aus;
        public string Visibility_B2_Aus
        {
            get { return _Visibility_B2_Aus; }
            set
            {
                _Visibility_B2_Aus = value;
                OnPropertyChanged("Visibility_B2_Aus");
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

        private string _Visibility_B3_Ein;
        public string Visibility_B3_Ein
        {
            get { return _Visibility_B3_Ein; }
            set
            {
                _Visibility_B3_Ein = value;
                OnPropertyChanged("Visibility_B3_Ein");
            }
        }

        private string _Visibility_B3_Aus;
        public string Visibility_B3_Aus
        {
            get { return _Visibility_B3_Aus; }
            set
            {
                _Visibility_B3_Aus = value;
                OnPropertyChanged("Visibility_B3_Aus");
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

        private string _Visibility_M1_Ein;
        public string Visibility_M1_Ein
        {
            get { return _Visibility_M1_Ein; }
            set
            {
                _Visibility_M1_Ein = value;
                OnPropertyChanged("Visibility_M1_Ein");
            }
        }

        private string _Visibility_M1_Aus;
        public string Visibility_M1_Aus
        {
            get { return _Visibility_M1_Aus; }
            set
            {
                _Visibility_M1_Aus = value;
                OnPropertyChanged("Visibility_M1_Aus");
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

        private string _Visibility_M2_Ein;
        public string Visibility_M2_Ein
        {
            get { return _Visibility_M2_Ein; }
            set
            {
                _Visibility_M2_Ein = value;
                OnPropertyChanged("Visibility_M2_Ein");
            }
        }

        private string _Visibility_M2_Aus;
        public string Visibility_M2_Aus
        {
            get { return _Visibility_M2_Aus; }
            set
            {
                _Visibility_M2_Aus = value;
                OnPropertyChanged("Visibility_M2_Aus");
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

        private string _Visibility_Ventil_Ein;
        public string Visibility_Ventil_Ein
        {
            get { return _Visibility_Ventil_Ein; }
            set
            {
                _Visibility_Ventil_Ein = value;
                OnPropertyChanged("Visibility_Ventil_Ein");
            }
        }

        private string _Visibility_Ventil_Aus;
        public string Visibility_Ventil_Aus
        {
            get { return _Visibility_Ventil_Aus; }
            set
            {
                _Visibility_Ventil_Aus = value;
                OnPropertyChanged("Visibility_Ventil_Aus");
            }
        }
        #endregion


               

        #region Margin1
        public void Margin_1(double pegel)
        {
            Margin1 = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);
        }

        private Thickness _Margin1;
        public Thickness Margin1
        {
            get { return _Margin1; }
            set
            {
                _Margin1 = value;
                OnPropertyChanged("Margin1");
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
