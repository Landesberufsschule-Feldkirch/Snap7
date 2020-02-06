namespace LAP_2018_Abfuellanlage.Model
{
    using System.ComponentModel;
    using System.Windows;
    using Utilities;

    public class VisuAnzeigen : INotifyPropertyChanged
    {

        private readonly double HoeheFuellBalken = 9 * 35;

        public VisuAnzeigen()
        {

            ImageTop1 = 10;
            ImageTop2 = 20;
            ImageTop3 = 30;
            ImageTop4 = 40;
            ImageTop5 = 50;
            ImageTop6 = 60;

            ImageLeft1 = 10;
            ImageLeft2 = 20;
            ImageLeft3 = 30;
            ImageLeft4 = 10;
            ImageLeft5 = 50;
            ImageLeft6 = 60;

            VisibilityImage1 = "visible";
            VisibilityImage2 = "visible";
            VisibilityImage3 = "visible";
            VisibilityImage4 = "visible";
            VisibilityImage5 = "visible";
            VisibilityImage6 = "visible";


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



        #region Image1
        public void PositionImage_1(Punkt pos)
        {
            ImageTop1 = pos.Y;
            ImageLeft1 = pos.X;
        }

        private double _ImageTop1;
        public double ImageTop1
        {
            get { return _ImageTop1; }
            set
            {
                _ImageTop1 = value;
                OnPropertyChanged("ImageTop1");
            }
        }

        private double _ImageLeft1;
        public double ImageLeft1
        {
            get { return _ImageLeft1; }
            set
            {
                _ImageLeft1 = value;
                OnPropertyChanged("ImageLeft1");
            }
        }
        #endregion

        #region Image2
        public void PositionImage_2(Punkt pos)
        {
            ImageTop2 = pos.Y;
            ImageLeft2 = pos.X;
        }

        private double _ImageTop2;
        public double ImageTop2
        {
            get { return _ImageTop2; }
            set
            {
                _ImageTop2 = value;
                OnPropertyChanged("ImageTop2");
            }
        }

        private double _ImageLeft2;
        public double ImageLeft2
        {
            get { return _ImageLeft2; }
            set
            {
                _ImageLeft2 = value;
                OnPropertyChanged("ImageLeft2");
            }
        }
        #endregion

        #region Image3
        public void PositionImage_3(Punkt pos)
        {
            ImageTop3 = pos.Y;
            ImageLeft3 = pos.X;
        }

        private double _ImageTop3;
        public double ImageTop3
        {
            get { return _ImageTop3; }
            set
            {
                _ImageTop3 = value;
                OnPropertyChanged("ImageTop3");
            }
        }

        private double _ImageLeft3;
        public double ImageLeft3
        {
            get { return _ImageLeft3; }
            set
            {
                _ImageLeft3 = value;
                OnPropertyChanged("ImageLeft3");
            }
        }
        #endregion

        #region Image4
        public void PositionImage_4(Punkt pos)
        {
            ImageTop4 = pos.Y;
            ImageLeft4 = pos.X;
        }

        private double _ImageTop4;
        public double ImageTop4
        {
            get { return _ImageTop4; }
            set
            {
                _ImageTop4 = value;
                OnPropertyChanged("ImageTop4");
            }
        }

        private double _ImageLeft4;
        public double ImageLeft4
        {
            get { return _ImageLeft4; }
            set
            {
                _ImageLeft4 = value;
                OnPropertyChanged("ImageLeft4");
            }
        }
        #endregion

        #region Image5
        public void PositionImage_5(Punkt pos)
        {
            ImageTop5 = pos.Y;
            ImageLeft5 = pos.X;
        }

        private double _ImageTop5;
        public double ImageTop5
        {
            get { return _ImageTop5; }
            set
            {
                _ImageTop5 = value;
                OnPropertyChanged("ImageTop5");
            }
        }

        private double _ImageLeft5;
        public double ImageLeft5
        {
            get { return _ImageLeft5; }
            set
            {
                _ImageLeft5 = value;
                OnPropertyChanged("ImageLeft5");
            }
        }
        #endregion

        #region Image6
        public void PositionImage_6(Punkt pos)
        {
            ImageTop6 = pos.Y;
            ImageLeft6 = pos.X;
        }

        private double _ImageTop6;
        public double ImageTop6
        {
            get { return _ImageTop6; }
            set
            {
                _ImageTop6 = value;
                OnPropertyChanged("ImageTop6");
            }
        }

        private double _ImageLeft6;
        public double ImageLeft6
        {
            get { return _ImageLeft6; }
            set
            {
                _ImageLeft6 = value;
                OnPropertyChanged("ImageLeft6");
            }
        }
        #endregion








        #region Visibility Flasche 1
        public void VisibilityFlasche1(bool val)
        {
            if (val) VisibilityImage1 = "visible"; else VisibilityImage1 = "hidden";
        }

        private string _VisibilityImage1;
        public string VisibilityImage1
        {
            get { return _VisibilityImage1; }
            set
            {
                _VisibilityImage1 = value;
                OnPropertyChanged("VisibilityImage1");
            }
        }
        #endregion

        #region Visibility Flasche 2
        public void VisibilityFlasche2(bool val)
        {
            if (val) VisibilityImage2 = "visible"; else VisibilityImage2 = "hidden";
        }

        private string _VisibilityImage2;
        public string VisibilityImage2
        {
            get { return _VisibilityImage2; }
            set
            {
                _VisibilityImage2 = value;
                OnPropertyChanged("VisibilityImage2");
            }
        }
        #endregion

        #region Visibility Flasche 3
        public void VisibilityFlasche3(bool val)
        {
            if (val) VisibilityImage3 = "visible"; else VisibilityImage3 = "hidden";
        }

        private string _VisibilityImage3;
        public string VisibilityImage3
        {
            get { return _VisibilityImage3; }
            set
            {
                _VisibilityImage3 = value;
                OnPropertyChanged("VisibilityImage3");
            }
        }
        #endregion

        #region Visibility Flasche 4
        public void VisibilityFlasche4(bool val)
        {
            if (val) VisibilityImage4 = "visible"; else VisibilityImage4 = "hidden";
        }

        private string _VisibilityImage4;
        public string VisibilityImage4
        {
            get { return _VisibilityImage4; }
            set
            {
                _VisibilityImage4 = value;
                OnPropertyChanged("VisibilityImage4");
            }
        }
        #endregion

        #region Visibility Flasche 5
        public void VisibilityFlasche5(bool val)
        {
            if (val) VisibilityImage5 = "visible"; else VisibilityImage5 = "hidden";
        }

        private string _VisibilityImage5;
        public string VisibilityImage5
        {
            get { return _VisibilityImage5; }
            set
            {
                _VisibilityImage5 = value;
                OnPropertyChanged("VisibilityImage5");
            }
        }
        #endregion

        #region Visibility Flasche 6
        public void VisibilityFlasche6(bool val)
        {
            if (val) VisibilityImage6 = "visible"; else VisibilityImage6 = "hidden";
        }

        private string _VisibilityImage6;
        public string VisibilityImage6
        {
            get { return _VisibilityImage6; }
            set
            {
                _VisibilityImage6 = value;
                OnPropertyChanged("VisibilityImage6");
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
