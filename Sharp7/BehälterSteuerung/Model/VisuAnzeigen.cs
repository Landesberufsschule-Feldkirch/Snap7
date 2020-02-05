namespace BehaelterSteuerung.Model
{
    using System.ComponentModel;
    using System.Windows;

    public class VisuAnzeigen : INotifyPropertyChanged
    {

        private readonly double HoeheFuellBalken = 200.0;

        public VisuAnzeigen()
        {

            VisibilityVentilQ1Ein = "hidden";
            VisibilityVentilQ2Ein = "hidden";
            VisibilityVentilQ3Ein = "hidden";
            VisibilityVentilQ4Ein = "hidden";
            VisibilityVentilQ5Ein = "hidden";
            VisibilityVentilQ6Ein = "hidden";
            VisibilityVentilQ7Ein = "hidden";
            VisibilityVentilQ8Ein = "hidden";

            VisibilityVentilQ1Aus = "visible";
            VisibilityVentilQ2Aus = "visible";
            VisibilityVentilQ3Aus = "visible";
            VisibilityVentilQ4Aus = "visible";
            VisibilityVentilQ5Aus = "visible";
            VisibilityVentilQ6Aus = "visible";
            VisibilityVentilQ7Aus = "visible";
            VisibilityVentilQ8Aus = "visible";

            ColorZuleitung_1b = "blue";
            ColorZuleitung_2b = "blue";
            ColorZuleitung_3b = "blue";
            ColorZuleitung_4b = "blue";

            ColorAbleitung_1a = "blue";
            ColorAbleitung_2a = "blue";
            ColorAbleitung_3a = "blue";
            ColorAbleitung_4a = "blue";

            ColorAbleitung_1a = "blue";
            ColorAbleitung_1b = "blue";
            ColorAbleitung_2a = "blue";
            ColorAbleitung_2b = "blue";
            ColorAbleitung_3a = "blue";
            ColorAbleitung_3b = "blue";
            ColorAbleitung_4a = "blue";
            ColorAbleitung_4b = "blue";

            ColorAbleitung_Gesamt = "blue";

            ColorLabelB1 = "red";
            ColorLabelB2 = "red";
            ColorLabelB3 = "red";
            ColorLabelB4 = "red";
            ColorLabelB5 = "red";
            ColorLabelB6 = "red";
            ColorLabelB7 = "red";
            ColorLabelB8 = "red";

            ColorCircle_P1 = "lightgray";


            EnableAutomatik1234 = "true";
            EnableAutomatik1324 = "true";
            EnableAutomatik1432 = "true";
            EnableAutomatik4321 = "true";


            Margin1 = new Thickness(0, 30, 0, 0);
            Margin2 = new Thickness(0, 50, 0, 0);
            Margin3 = new Thickness(0, 70, 0, 0);
            Margin4 = new Thickness(0, 90, 0, 0);
        }



        #region Visibility Ventil Q1
        public void VisibilityVentilQ1(bool val)
        {
            if (val)
            {
                VisibilityVentilQ1Ein = "visible";
                VisibilityVentilQ1Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ1Ein = "hidden";
                VisibilityVentilQ1Aus = "visible";
            }
        }

        private string _VisibilityVentilQ1Ein;
        public string VisibilityVentilQ1Ein
        {
            get { return _VisibilityVentilQ1Ein; }
            set
            {
                _VisibilityVentilQ1Ein = value;
                OnPropertyChanged("VisibilityVentilQ1Ein");
            }
        }

        private string _VisibilityVentilQ1Aus;
        public string VisibilityVentilQ1Aus
        {
            get { return _VisibilityVentilQ1Aus; }
            set
            {
                _VisibilityVentilQ1Aus = value;
                OnPropertyChanged("VisibilityVentilQ1Aus");
            }
        }
        #endregion

        #region Visibility Ventil Q2
        public void VisibilityVentilQ2(bool val)
        {
            if (val)
            {
                VisibilityVentilQ2Ein = "visible";
                VisibilityVentilQ2Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ2Ein = "hidden";
                VisibilityVentilQ2Aus = "visible";
            }
        }

        private string _VisibilityVentilQ2Ein;
        public string VisibilityVentilQ2Ein
        {
            get { return _VisibilityVentilQ2Ein; }
            set
            {
                _VisibilityVentilQ2Ein = value;
                OnPropertyChanged("VisibilityVentilQ2Ein");
            }
        }

        private string _VisibilityVentilQ2Aus;
        public string VisibilityVentilQ2Aus
        {
            get { return _VisibilityVentilQ2Aus; }
            set
            {
                _VisibilityVentilQ2Aus = value;
                OnPropertyChanged("VisibilityVentilQ2Aus");
            }
        }
        #endregion

        #region Visibility Ventil Q3

        public void VisibilityVentilQ3(bool val)
        {
            if (val)
            {
                VisibilityVentilQ3Ein = "visible";
                VisibilityVentilQ3Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ3Ein = "hidden";
                VisibilityVentilQ3Aus = "visible";
            }
        }

        private string _VisibilityVentilQ3Ein;
        public string VisibilityVentilQ3Ein
        {
            get { return _VisibilityVentilQ3Ein; }
            set
            {
                _VisibilityVentilQ3Ein = value;
                OnPropertyChanged("VisibilityVentilQ3Ein");
            }
        }

        private string _VisibilityVentilQ3Aus;
        public string VisibilityVentilQ3Aus
        {
            get { return _VisibilityVentilQ3Aus; }
            set
            {
                _VisibilityVentilQ3Aus = value;
                OnPropertyChanged("VisibilityVentilQ3Aus");
            }
        }



        #endregion

        #region Visibility Ventil Q4
        public void VisibilityVentilQ4(bool val)
        {
            if (val)
            {
                VisibilityVentilQ4Ein = "visible";
                VisibilityVentilQ4Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ4Ein = "hidden";
                VisibilityVentilQ4Aus = "visible";
            }
        }

        private string _VisibilityVentilQ4Ein;
        public string VisibilityVentilQ4Ein
        {
            get { return _VisibilityVentilQ4Ein; }
            set
            {
                _VisibilityVentilQ4Ein = value;
                OnPropertyChanged("VisibilityVentilQ4Ein");
            }
        }

        private string _VisibilityVentilQ4Aus;
        public string VisibilityVentilQ4Aus
        {
            get { return _VisibilityVentilQ4Aus; }
            set
            {
                _VisibilityVentilQ4Aus = value;
                OnPropertyChanged("VisibilityVentilQ4Aus");
            }
        }
        #endregion

        #region Visibility Ventil Q5
        public void VisibilityVentilQ5(bool val)
        {
            if (val)
            {
                VisibilityVentilQ5Ein = "visible";
                VisibilityVentilQ5Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ5Ein = "hidden";
                VisibilityVentilQ5Aus = "visible";
            }
        }

        private string _VisibilityVentilQ5Ein;
        public string VisibilityVentilQ5Ein
        {
            get { return _VisibilityVentilQ5Ein; }
            set
            {
                _VisibilityVentilQ5Ein = value;
                OnPropertyChanged("VisibilityVentilQ5Ein");
            }
        }

        private string _VisibilityVentilQ5Aus;
        public string VisibilityVentilQ5Aus
        {
            get { return _VisibilityVentilQ5Aus; }
            set
            {
                _VisibilityVentilQ5Aus = value;
                OnPropertyChanged("VisibilityVentilQ5Aus");
            }
        }
        #endregion

        #region Visibility Ventil Q6
        public void VisibilityVentilQ6(bool val)
        {
            if (val)
            {
                VisibilityVentilQ6Ein = "visible";
                VisibilityVentilQ6Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ6Ein = "hidden";
                VisibilityVentilQ6Aus = "visible";
            }
        }

        private string _VisibilityVentilQ6Ein;
        public string VisibilityVentilQ6Ein
        {
            get { return _VisibilityVentilQ6Ein; }
            set
            {
                _VisibilityVentilQ6Ein = value;
                OnPropertyChanged("VisibilityVentilQ6Ein");
            }
        }

        private string _VisibilityVentilQ6Aus;
        public string VisibilityVentilQ6Aus
        {
            get { return _VisibilityVentilQ6Aus; }
            set
            {
                _VisibilityVentilQ6Aus = value;
                OnPropertyChanged("VisibilityVentilQ6Aus");
            }
        }
        #endregion

        #region Visibility Ventil Q7
        public void VisibilityVentilQ7(bool val)
        {
            if (val)
            {
                VisibilityVentilQ7Ein = "visible";
                VisibilityVentilQ7Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ7Ein = "hidden";
                VisibilityVentilQ7Aus = "visible";
            }
        }

        private string _VisibilityVentilQ7Ein;
        public string VisibilityVentilQ7Ein
        {
            get { return _VisibilityVentilQ7Ein; }
            set
            {
                _VisibilityVentilQ7Ein = value;
                OnPropertyChanged("VisibilityVentilQ7Ein");
            }
        }

        private string _VisibilityVentilQ7Aus;
        public string VisibilityVentilQ7Aus
        {
            get { return _VisibilityVentilQ7Aus; }
            set
            {
                _VisibilityVentilQ7Aus = value;
                OnPropertyChanged("VisibilityVentilQ7Aus");
            }
        }
        #endregion

        #region Visibility Ventil Q8
        public void VisibilityVentilQ8(bool val)
        {
            if (val)
            {
                VisibilityVentilQ8Ein = "visible";
                VisibilityVentilQ8Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ8Ein = "hidden";
                VisibilityVentilQ8Aus = "visible";
            }
        }

        private string _VisibilityVentilQ8Ein;
        public string VisibilityVentilQ8Ein
        {
            get { return _VisibilityVentilQ8Ein; }
            set
            {
                _VisibilityVentilQ8Ein = value;
                OnPropertyChanged("VisibilityVentilQ8Ein");
            }
        }

        private string _VisibilityVentilQ8Aus;
        public string VisibilityVentilQ8Aus
        {
            get { return _VisibilityVentilQ8Aus; }
            set
            {
                _VisibilityVentilQ8Aus = value;
                OnPropertyChanged("VisibilityVentilQ8Aus");
            }
        }
        #endregion




        #region Color Zuleitung_1b
        public void FarbeZuleitung1b(bool val)
        {
            if (val) ColorZuleitung_1b = "blue"; else ColorZuleitung_1b = "lightblue";
        }

        private string _ColorZuleitung_1b;
        public string ColorZuleitung_1b
        {
            get { return _ColorZuleitung_1b; }
            set
            {
                _ColorZuleitung_1b = value;
                OnPropertyChanged("ColorZuleitung_1b");
            }
        }
        #endregion

        #region Color Zuleitung_2b
        public void FarbeZuleitung2b(bool val)
        {
            if (val) ColorZuleitung_2b = "blue"; else ColorZuleitung_2b = "lightblue";
        }

        private string _ColorZuleitung_2b;
        public string ColorZuleitung_2b
        {
            get { return _ColorZuleitung_2b; }
            set
            {
                _ColorZuleitung_2b = value;
                OnPropertyChanged("ColorZuleitung_2b");
            }
        }
        #endregion

        #region Color Zuleitung_3b
        public void FarbeZuleitung3b(bool val)
        {
            if (val) ColorZuleitung_3b = "blue"; else ColorZuleitung_3b = "lightblue";
        }

        private string _ColorZuleitung_3b;
        public string ColorZuleitung_3b
        {
            get { return _ColorZuleitung_3b; }
            set
            {
                _ColorZuleitung_3b = value;
                OnPropertyChanged("ColorZuleitung_3b");
            }
        }
        #endregion

        #region Color Zuleitung_4b
        public void FarbeZuleitung4b(bool val)
        {
            if (val) ColorZuleitung_4b = "blue"; else ColorZuleitung_4b = "lightblue";
        }

        private string _ColorZuleitung_4b;
        public string ColorZuleitung_4b
        {
            get { return _ColorZuleitung_4b; }
            set
            {
                _ColorZuleitung_4b = value;
                OnPropertyChanged("ColorZuleitung_4b");
            }
        }
        #endregion



        #region Color Ableitung_1a
        public void FarbeAbleitung1a(bool val)
        {
            if (val) ColorAbleitung_1a = "blue"; else ColorAbleitung_1a = "lightblue";
        }

        private string _ColorAbleitung_1a;
        public string ColorAbleitung_1a
        {
            get { return _ColorAbleitung_1a; }
            set
            {
                _ColorAbleitung_1a = value;
                OnPropertyChanged("ColorAbleitung_1a");
            }
        }
        #endregion

        #region Color Ableitung_2a
        public void FarbeAbleitung2a(bool val)
        {
            if (val) ColorAbleitung_2a = "blue"; else ColorAbleitung_2a = "lightblue";
        }

        private string _ColorAbleitung_2a;
        public string ColorAbleitung_2a
        {
            get { return _ColorAbleitung_2a; }
            set
            {
                _ColorAbleitung_2a = value;
                OnPropertyChanged("ColorAbleitung_2a");
            }
        }
        #endregion

        #region Color Ableitung_3a
        public void FarbeAbleitung3a(bool val)
        {
            if (val) ColorAbleitung_3a = "blue"; else ColorAbleitung_3a = "lightblue";
        }

        private string _ColorAbleitung_3a;
        public string ColorAbleitung_3a
        {
            get { return _ColorAbleitung_3a; }
            set
            {
                _ColorAbleitung_3a = value;
                OnPropertyChanged("ColorAbleitung_3a");
            }
        }
        #endregion

        #region Color Ableitung_4a
        public void FarbeAbleitung4a(bool val)
        {
            if (val) ColorAbleitung_4a = "blue"; else ColorAbleitung_4a = "lightblue";
        }

        private string _ColorAbleitung_4a;
        public string ColorAbleitung_4a
        {
            get { return _ColorAbleitung_4a; }
            set
            {
                _ColorAbleitung_4a = value;
                OnPropertyChanged("ColorAbleitung_4a");
            }
        }
        #endregion




        #region Color Ableitung_1b
        public void FarbeAbleitung1b(bool val)
        {
            if (val) ColorAbleitung_1b = "blue"; else ColorAbleitung_1b = "lightblue";
        }

        private string _ColorAbleitung_1b;
        public string ColorAbleitung_1b
        {
            get { return _ColorAbleitung_1b; }
            set
            {
                _ColorAbleitung_1b = value;
                OnPropertyChanged("ColorAbleitung_1b");
            }
        }
        #endregion

        #region Color Ableitung_2b
        public void FarbeAbleitung2b(bool val)
        {
            if (val) ColorAbleitung_2b = "blue"; else ColorAbleitung_2b = "lightblue";
        }

        private string _ColorAbleitung_2b;
        public string ColorAbleitung_2b
        {
            get { return _ColorAbleitung_2b; }
            set
            {
                _ColorAbleitung_2b = value;
                OnPropertyChanged("ColorAbleitung_2b");
            }
        }
        #endregion

        #region Color Ableitung_3b
        public void FarbeAbleitung3b(bool val)
        {
            if (val) ColorAbleitung_3b = "blue"; else ColorAbleitung_3b = "lightblue";
        }

        private string _ColorAbleitung_3b;
        public string ColorAbleitung_3b
        {
            get { return _ColorAbleitung_3b; }
            set
            {
                _ColorAbleitung_3b = value;
                OnPropertyChanged("ColorAbleitung_3b");
            }
        }
        #endregion

        #region Color Ableitung_4b
        public void FarbeAbleitung4b(bool val)
        {
            if (val) ColorAbleitung_4b = "blue"; else ColorAbleitung_4b = "lightblue";
        }

        private string _ColorAbleitung_4b;
        public string ColorAbleitung_4b
        {
            get { return _ColorAbleitung_4b; }
            set
            {
                _ColorAbleitung_4b = value;
                OnPropertyChanged("ColorAbleitung_4b");
            }
        }
        #endregion



        #region Color Ableitung_Gesamt
        public void FarbeAbleitungGesamt(bool val)
        {
            if (val) ColorAbleitung_Gesamt = "blue"; else ColorAbleitung_Gesamt = "lightblue";
        }

        private string _ColorAbleitung_Gesamt;
        public string ColorAbleitung_Gesamt
        {
            get { return _ColorAbleitung_Gesamt; }
            set
            {
                _ColorAbleitung_Gesamt = value;
                OnPropertyChanged("ColorAbleitung_Gesamt");
            }
        }
        #endregion



        #region Color LabelB1
        public void FarbeLabelB1(bool val)
        {
            if (val) ColorLabelB1 = "red"; else ColorLabelB1 = "lawngreen";
        }

        private string _ColorLabelB1;
        public string ColorLabelB1
        {
            get { return _ColorLabelB1; }
            set
            {
                _ColorLabelB1 = value;
                OnPropertyChanged("ColorLabelB1");
            }
        }
        #endregion

        #region Color LabelB2
        public void FarbeLabelB2(bool val)
        {
            if (val) ColorLabelB2 = "red"; else ColorLabelB2 = "lawngreen";
        }

        private string _ColorLabelB2;
        public string ColorLabelB2
        {
            get { return _ColorLabelB2; }
            set
            {
                _ColorLabelB2 = value;
                OnPropertyChanged("ColorLabelB2");
            }
        }
        #endregion

        #region Color LabelB3
        public void FarbeLabelB3(bool val)
        {
            if (val) ColorLabelB3 = "red"; else ColorLabelB3 = "lawngreen";
        }

        private string _ColorLabelB3;
        public string ColorLabelB3
        {
            get { return _ColorLabelB3; }
            set
            {
                _ColorLabelB3 = value;
                OnPropertyChanged("ColorLabelB3");
            }
        }
        #endregion

        #region Color LabelB4
        public void FarbeLabelB4(bool val)
        {
            if (val) ColorLabelB4 = "red"; else ColorLabelB4 = "lawngreen";
        }

        private string _ColorLabelB4;
        public string ColorLabelB4
        {
            get { return _ColorLabelB4; }
            set
            {
                _ColorLabelB4 = value;
                OnPropertyChanged("ColorLabelB4");
            }
        }
        #endregion

        #region Color LabelB5
        public void FarbeLabelB5(bool val)
        {
            if (val) ColorLabelB5 = "red"; else ColorLabelB5 = "lawngreen";
        }

        private string _ColorLabelB5;
        public string ColorLabelB5
        {
            get { return _ColorLabelB5; }
            set
            {
                _ColorLabelB5 = value;
                OnPropertyChanged("ColorLabelB5");
            }
        }
        #endregion

        #region Color LabelB6
        public void FarbeLabelB6(bool val)
        {
            if (val) ColorLabelB6 = "red"; else ColorLabelB6 = "lawngreen";
        }

        private string _ColorLabelB6;
        public string ColorLabelB6
        {
            get { return _ColorLabelB6; }
            set
            {
                _ColorLabelB6 = value;
                OnPropertyChanged("ColorLabelB6");
            }
        }
        #endregion

        #region Color LabelB7
        public void FarbeLabelB7(bool val)
        {
            if (val) ColorLabelB7 = "red"; else ColorLabelB7 = "lawngreen";
        }

        private string _ColorLabelB7;
        public string ColorLabelB7
        {
            get { return _ColorLabelB7; }
            set
            {
                _ColorLabelB7 = value;
                OnPropertyChanged("ColorLabelB7");
            }
        }
        #endregion

        #region Color LabelB8
        public void FarbeLabelB8(bool val)
        {
            if (val) ColorLabelB8 = "red"; else ColorLabelB8 = "lawngreen";
        }

        private string _ColorLabelB8;
        public string ColorLabelB8
        {
            get { return _ColorLabelB8; }
            set
            {
                _ColorLabelB8 = value;
                OnPropertyChanged("ColorLabelB8");
            }
        }
        #endregion



        #region Color P1
        public void FarbeCircle_P1(bool val)
        {
            if (val) ColorCircle_P1 = "lawngreen"; else ColorCircle_P1 = "lightgray";
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



        #region EnableAutomatik1234
        private string _EnableAutomatik1234;
        public string EnableAutomatik1234
        {
            get { return _EnableAutomatik1234; }
            set
            {
                _EnableAutomatik1234 = value;
                OnPropertyChanged("EnableAutomatik1234");
            }
        }
        #endregion

        #region EnableAutomatik1324
        private string _EnableAutomatik1324;
        public string EnableAutomatik1324
        {
            get { return _EnableAutomatik1324; }
            set
            {
                _EnableAutomatik1324 = value;
                OnPropertyChanged("EnableAutomatik1324");
            }
        }
        #endregion

        #region EnableAutomatik1432
        private string _EnableAutomatik1432;
        public string EnableAutomatik1432
        {
            get { return _EnableAutomatik1432; }
            set
            {
                _EnableAutomatik1432 = value;
                OnPropertyChanged("EnableAutomatik1432");
            }
        }
        #endregion

        #region EnableAutomatik4321
        private string _EnableAutomatik4321;
        public string EnableAutomatik4321
        {
            get { return _EnableAutomatik4321; }
            set
            {
                _EnableAutomatik4321 = value;
                OnPropertyChanged("EnableAutomatik4321");
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

        #region Margin2
        public void Margin_2(double pegel)
        {
            Margin2 = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);
        }

        private Thickness _Margin2;
        public Thickness Margin2
        {
            get { return _Margin2; }
            set
            {
                _Margin2 = value;
                OnPropertyChanged("Margin2");
            }
        }
        #endregion

        #region Margin3
        public void Margin_3(double pegel)
        {
            Margin3 = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);
        }

        private Thickness _Margin3;
        public Thickness Margin3
        {
            get { return _Margin3; }
            set
            {
                _Margin3 = value;
                OnPropertyChanged("Margin3");
            }
        }
        #endregion

        #region Margin4
        public void Margin_4(double pegel)
        {
            Margin4 = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);
        }

        private Thickness _Margin4;
        public Thickness Margin4
        {
            get { return _Margin4; }
            set
            {
                _Margin4 = value;
                OnPropertyChanged("Margin4");
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
