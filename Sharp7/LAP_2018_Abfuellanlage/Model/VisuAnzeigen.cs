namespace LAP_2018_Abfuellanlage.Model
{
    using System.ComponentModel;
    using System.Windows;

    public class VisuAnzeigen : INotifyPropertyChanged
    {

        private readonly double HoeheFuellBalken = 9 * 35;

        public VisuAnzeigen()
        {
            Visibility_B1_Ein = "hidden";
            Visibility_K1_Ein = "hidden";
            Visibility_K2_Ein = "hidden";

            Visibility_B1_Aus = "visible";
            Visibility_K1_Aus = "visible";
            Visibility_K2_Aus = "visible";

            VisibilityRectangleAbleitung = "hidden";

            ColorCircle_F5 = "LawnGreen";
            ColorCircle_M1 = "LightGray";
            ColorCircle_P1 = "LightGray";
            ColorCircle_P2 = "LightGray";

            ColorRectangleZuleitung = "LightBlue";

            Margin1 = new Thickness(0, 30, 0, 0);
        }


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

        #region Visibility Ventil K1
        public void VisibilityVentilK1(bool val)
        {
            if (val)
            {
                Visibility_K1_Ein = "visible";
                Visibility_K1_Aus = "hidden";
            }
            else
            {
                Visibility_K1_Ein = "hidden";
                Visibility_K1_Aus = "visible";
            }
        }

        private string _Visibility_K1_Ein;
        public string Visibility_K1_Ein
        {
            get { return _Visibility_K1_Ein; }
            set
            {
                _Visibility_K1_Ein = value;
                OnPropertyChanged("Visibility_K1_Ein");
            }
        }

        private string _Visibility_K1_Aus;
        public string Visibility_K1_Aus
        {
            get { return _Visibility_K1_Aus; }
            set
            {
                _Visibility_K1_Aus = value;
                OnPropertyChanged("Visibility_K1_Aus");
            }
        }
        #endregion

        #region Visibility Ventil K2
        public void VisibilityVentilK2(bool val)
        {
            if (val)
            {
                Visibility_K2_Ein = "visible";
                Visibility_K2_Aus = "hidden";
            }
            else
            {
                Visibility_K2_Ein = "hidden";
                Visibility_K2_Aus = "visible";
            }
        }

        private string _Visibility_K2_Ein;
        public string Visibility_K2_Ein
        {
            get { return _Visibility_K2_Ein; }
            set
            {
                _Visibility_K2_Ein = value;
                OnPropertyChanged("Visibility_K2_Ein");
            }
        }

        private string _Visibility_K2_Aus;
        public string Visibility_K2_Aus
        {
            get { return _Visibility_K2_Aus; }
            set
            {
                _Visibility_K2_Aus = value;
                OnPropertyChanged("Visibility_K2_Aus");
            }
        }
        #endregion
        
        #region Visibility Ableitung
        public void VisibilityAbleitung(bool val)
        {
            if (val) VisibilityRectangleAbleitung = "visible"; else VisibilityRectangleAbleitung = "hidden";
        }

        private string _VisibilityRectangleAbleitung;
        public string VisibilityRectangleAbleitung
        {
            get { return _VisibilityRectangleAbleitung; }
            set
            {
                _VisibilityRectangleAbleitung = value;
                OnPropertyChanged("VisibilityRectangleAbleitung");
            }
        }
        #endregion





        #region Color F5
        public void FarbeCircle_F5(bool val)
        {
            if (val) ColorCircle_F5 = "red"; else ColorCircle_F5 = "lawngreen";
        }

        private string _ColorCircle_F5;
        public string ColorCircle_F5
        {
            get { return _ColorCircle_F5; }
            set
            {
                _ColorCircle_F5 = value;
                OnPropertyChanged("ColorCircle_F5");
            }
        }
        #endregion

        #region Color M1
        public void FarbeCircle_M1(bool val)
        {
            if (val) ColorCircle_M1 = "lawngreen"; else ColorCircle_M1 = "LightGray";
        }

        private string _ColorCircle_M1;
        public string ColorCircle_M1
        {
            get { return _ColorCircle_M1; }
            set
            {
                _ColorCircle_M1 = value;
                OnPropertyChanged("ColorCircle_M1");
            }
        }
        #endregion
                             

        #region Color P1
        public void FarbeCircle_P1(bool val)
        {
            if (val) ColorCircle_P1 = "lawngreen"; else ColorCircle_P1 = "LightGray";
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
            if (val) ColorCircle_P2 = "red"; else ColorCircle_P2 = "LightGray";
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

        #region Color Zuleitung
        public void FarbeRectangleZuleitung(bool val)
        {
            if (val) ColorRectangleZuleitung = "blue"; else ColorRectangleZuleitung = "lightblue";
        }

        private string _ColorRectangleZuleitung;
        public string ColorRectangleZuleitung
        {
            get { return _ColorRectangleZuleitung; }
            set
            {
                _ColorRectangleZuleitung = value;
                OnPropertyChanged("ColorRectangleZuleitung");
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
