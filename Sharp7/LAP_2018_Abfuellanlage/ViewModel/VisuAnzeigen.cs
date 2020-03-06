namespace LAP_2018_Abfuellanlage.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using Utilities;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Abfuellanlage alleFlaschen;
        private readonly MainWindow mainWindow;
        private readonly double HoeheFuellBalken = 9 * 35;

        public VisuAnzeigen(MainWindow mw, Model.Abfuellanlage af)
        {
            mainWindow = mw;
            alleFlaschen = af;

            SpsStatus = "-";
            SpsColor = "LightBlue";


            ClickModeBtnS1 = "Press";
            ClickModeBtnS2 = "Press";
            ClickModeBtnS3 = "Press";
            ClickModeBtnS4 = "Press";
            ClickModeBtnK1 = "Press";
            ClickModeBtnK2 = "Press";
            ClickModeBtnM1 = "Press";


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

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                PositionImage_1(alleFlaschen.AlleFlaschen[0].Position.Punkt);
                PositionImage_2(alleFlaschen.AlleFlaschen[1].Position.Punkt);
                PositionImage_3(alleFlaschen.AlleFlaschen[2].Position.Punkt);
                PositionImage_4(alleFlaschen.AlleFlaschen[3].Position.Punkt);
                PositionImage_5(alleFlaschen.AlleFlaschen[4].Position.Punkt);
                PositionImage_6(alleFlaschen.AlleFlaschen[5].Position.Punkt);

                VisibilityFlasche1(alleFlaschen.AlleFlaschen[0].Sichtbar);
                VisibilityFlasche2(alleFlaschen.AlleFlaschen[1].Sichtbar);
                VisibilityFlasche3(alleFlaschen.AlleFlaschen[2].Sichtbar);
                VisibilityFlasche4(alleFlaschen.AlleFlaschen[3].Sichtbar);
                VisibilityFlasche5(alleFlaschen.AlleFlaschen[4].Sichtbar);
                VisibilityFlasche6(alleFlaschen.AlleFlaschen[5].Sichtbar);

                VisibilitySensorB1(alleFlaschen.B1);
                VisibilityVentilK1(alleFlaschen.K1);
                VisibilityVentilK2(alleFlaschen.K2);
                VisibilityAbleitung(alleFlaschen.K1 && (alleFlaschen.Pegel > 0.01));

                FarbeCircle_F5(alleFlaschen.F5);
                FarbeCircle_M1(alleFlaschen.M1);
                FarbeCircle_P1(alleFlaschen.P1);
                FarbeCircle_P2(alleFlaschen.P2);

                FarbeRectangleZuleitung(alleFlaschen.Pegel > 0.01);

                Margin_1(alleFlaschen.Pegel);


                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }
               
        internal void TasterS1() { alleFlaschen.S1 = ClickModeButtonS1(); }
        internal void TasterS2() { alleFlaschen.S2 = !ClickModeButtonS2(); }
        internal void TasterS3() { alleFlaschen.S3 = ClickModeButtonS3(); }
        internal void TasterS4() { alleFlaschen.S4 = ClickModeButtonS4(); }
        internal void SetManualK1() { alleFlaschen.K1 = ClickModeButtonK1(); }
        internal void SetManualK2() { alleFlaschen.K2 = ClickModeButtonK2(); }
        internal void SetManualM1() { alleFlaschen.M1 = ClickModeButtonM1(); }



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

        #region ClickModeBtnS4
        public bool ClickModeButtonS4()
        {
            if (ClickModeBtnS4 == "Press")
            {
                ClickModeBtnS4 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnS4 = "Press";
            }
            return false;
        }

        private string _clickModeBtnS4;
        public string ClickModeBtnS4
        {
            get { return _clickModeBtnS4; }
            set
            {
                _clickModeBtnS4 = value;
                OnPropertyChanged(nameof(ClickModeBtnS4));
            }
        }
        #endregion

        #region ClickModeBtnK1
        public bool ClickModeButtonK1()
        {
            if (ClickModeBtnK1 == "Press")
            {
                ClickModeBtnK1 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnK1 = "Press";
            }
            return false;
        }

        private string _clickModeBtnK1;
        public string ClickModeBtnK1
        {
            get { return _clickModeBtnK1; }
            set
            {
                _clickModeBtnK1 = value;
                OnPropertyChanged(nameof(ClickModeBtnK1));
            }
        }
        #endregion

        #region ClickModeBtnK2
        public bool ClickModeButtonK2()
        {
            if (ClickModeBtnK2 == "Press")
            {
                ClickModeBtnK2 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnK2 = "Press";
            }
            return false;
        }

        private string _clickModeBtnK2;
        public string ClickModeBtnK2
        {
            get { return _clickModeBtnK2; }
            set
            {
                _clickModeBtnK2 = value;
                OnPropertyChanged(nameof(ClickModeBtnK2));
            }
        }
        #endregion

        #region ClickModeBtnM1
        public bool ClickModeButtonM1()
        {
            if (ClickModeBtnM1 == "Press")
            {
                ClickModeBtnM1 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnM1 = "Press";
            }
            return false;
        }

        private string _clickModeBtnM1;
        public string ClickModeBtnM1
        {
            get { return _clickModeBtnM1; }
            set
            {
                _clickModeBtnM1 = value;
                OnPropertyChanged(nameof(ClickModeBtnM1));
            }
        }
        #endregion





        #region Image1
        public void PositionImage_1(Punkt pos)
        {
            ImageLeft1 = pos.X;
            ImageTop1 = pos.Y;
        }

        private double _imageTop1;
        public double ImageTop1
        {
            get { return _imageTop1; }
            set
            {
                _imageTop1 = value;
                OnPropertyChanged(nameof(ImageTop1));
            }
        }

        private double _imageLeft1;
        public double ImageLeft1
        {
            get { return _imageLeft1; }
            set
            {
                _imageLeft1 = value;
                OnPropertyChanged(nameof(ImageLeft1));
            }
        }
        #endregion

        #region Image2
        public void PositionImage_2(Punkt pos)
        {
            ImageLeft2 = pos.X;
            ImageTop2 = pos.Y;
        }

        private double _imageTop2;
        public double ImageTop2
        {
            get { return _imageTop2; }
            set
            {
                _imageTop2 = value;
                OnPropertyChanged(nameof(ImageTop2));
            }
        }

        private double _imageLeft2;
        public double ImageLeft2
        {
            get { return _imageLeft2; }
            set
            {
                _imageLeft2 = value;
                OnPropertyChanged(nameof(ImageLeft2));
            }
        }
        #endregion

        #region Image3
        public void PositionImage_3(Punkt pos)
        {
            ImageLeft3 = pos.X;
            ImageTop3 = pos.Y;
        }

        private double _imageTop3;
        public double ImageTop3
        {
            get { return _imageTop3; }
            set
            {
                _imageTop3 = value;
                OnPropertyChanged(nameof(ImageTop3));
            }
        }

        private double _imageLeft3;
        public double ImageLeft3
        {
            get { return _imageLeft3; }
            set
            {
                _imageLeft3 = value;
                OnPropertyChanged(nameof(ImageLeft3));
            }
        }
        #endregion

        #region Image4
        public void PositionImage_4(Punkt pos)
        {
            ImageLeft4 = pos.X;
            ImageTop4 = pos.Y;
        }

        private double _imageTop4;
        public double ImageTop4
        {
            get { return _imageTop4; }
            set
            {
                _imageTop4 = value;
                OnPropertyChanged(nameof(ImageTop4));
            }
        }

        private double _imageLeft4;
        public double ImageLeft4
        {
            get { return _imageLeft4; }
            set
            {
                _imageLeft4 = value;
                OnPropertyChanged(nameof(ImageLeft4));
            }
        }
        #endregion

        #region Image5
        public void PositionImage_5(Punkt pos)
        {
            ImageLeft5 = pos.X;
            ImageTop5 = pos.Y;
        }

        private double _imageTop5;
        public double ImageTop5
        {
            get { return _imageTop5; }
            set
            {
                _imageTop5 = value;
                OnPropertyChanged(nameof(ImageTop5));
            }
        }

        private double _imageLeft5;
        public double ImageLeft5
        {
            get { return _imageLeft5; }
            set
            {
                _imageLeft5 = value;
                OnPropertyChanged(nameof(ImageLeft5));
            }
        }
        #endregion

        #region Image6
        public void PositionImage_6(Punkt pos)
        {
            ImageLeft6 = pos.X;
            ImageTop6 = pos.Y;
        }

        private double _imageTop6;
        public double ImageTop6
        {
            get { return _imageTop6; }
            set
            {
                _imageTop6 = value;
                OnPropertyChanged(nameof(ImageTop6));
            }
        }

        private double _imageLeft6;
        public double ImageLeft6
        {
            get { return _imageLeft6; }
            set
            {
                _imageLeft6 = value;
                OnPropertyChanged(nameof(ImageLeft6));
            }
        }
        #endregion




        #region Visibility Flasche 1
        public void VisibilityFlasche1(bool val)
        {
            if (val) VisibilityImage1 = "visible"; else VisibilityImage1 = "hidden";
        }

        private string _visibilityImage1;
        public string VisibilityImage1
        {
            get { return _visibilityImage1; }
            set
            {
                _visibilityImage1 = value;
                OnPropertyChanged(nameof(VisibilityImage1));
            }
        }
        #endregion

        #region Visibility Flasche 2
        public void VisibilityFlasche2(bool val)
        {
            if (val) VisibilityImage2 = "visible"; else VisibilityImage2 = "hidden";
        }

        private string _visibilityImage2;
        public string VisibilityImage2
        {
            get { return _visibilityImage2; }
            set
            {
                _visibilityImage2 = value;
                OnPropertyChanged(nameof(VisibilityImage2));
            }
        }
        #endregion

        #region Visibility Flasche 3
        public void VisibilityFlasche3(bool val)
        {
            if (val) VisibilityImage3 = "visible"; else VisibilityImage3 = "hidden";
        }

        private string _visibilityImage3;
        public string VisibilityImage3
        {
            get { return _visibilityImage3; }
            set
            {
                _visibilityImage3 = value;
                OnPropertyChanged(nameof(VisibilityImage3));
            }
        }
        #endregion

        #region Visibility Flasche 4
        public void VisibilityFlasche4(bool val)
        {
            if (val) VisibilityImage4 = "visible"; else VisibilityImage4 = "hidden";
        }

        private string _visibilityImage4;
        public string VisibilityImage4
        {
            get { return _visibilityImage4; }
            set
            {
                _visibilityImage4 = value;
                OnPropertyChanged(nameof(VisibilityImage4));
            }
        }
        #endregion

        #region Visibility Flasche 5
        public void VisibilityFlasche5(bool val)
        {
            if (val) VisibilityImage5 = "visible"; else VisibilityImage5 = "hidden";
        }

        private string _visibilityImage5;
        public string VisibilityImage5
        {
            get { return _visibilityImage5; }
            set
            {
                _visibilityImage5 = value;
                OnPropertyChanged(nameof(VisibilityImage5));
            }
        }
        #endregion

        #region Visibility Flasche 6
        public void VisibilityFlasche6(bool val)
        {
            if (val) VisibilityImage6 = "visible"; else VisibilityImage6 = "hidden";
        }

        private string _visibilityImage6;
        public string VisibilityImage6
        {
            get { return _visibilityImage6; }
            set
            {
                _visibilityImage6 = value;
                OnPropertyChanged(nameof(VisibilityImage6));
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

        private string _visibility_K1_Ein;
        public string Visibility_K1_Ein
        {
            get { return _visibility_K1_Ein; }
            set
            {
                _visibility_K1_Ein = value;
                OnPropertyChanged(nameof(Visibility_K1_Ein));
            }
        }

        private string _visibility_K1_Aus;
        public string Visibility_K1_Aus
        {
            get { return _visibility_K1_Aus; }
            set
            {
                _visibility_K1_Aus = value;
                OnPropertyChanged(nameof(Visibility_K1_Aus));
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

        private string _visibility_K2_Ein;
        public string Visibility_K2_Ein
        {
            get { return _visibility_K2_Ein; }
            set
            {
                _visibility_K2_Ein = value;
                OnPropertyChanged(nameof(Visibility_K2_Ein));
            }
        }

        private string _visibility_K2_Aus;
        public string Visibility_K2_Aus
        {
            get { return _visibility_K2_Aus; }
            set
            {
                _visibility_K2_Aus = value;
                OnPropertyChanged(nameof(Visibility_K2_Aus));
            }
        }
        #endregion

        #region Visibility Ableitung
        public void VisibilityAbleitung(bool val)
        {
            if (val) VisibilityRectangleAbleitung = "visible"; else VisibilityRectangleAbleitung = "hidden";
        }

        private string _visibilityRectangleAbleitung;
        public string VisibilityRectangleAbleitung
        {
            get { return _visibilityRectangleAbleitung; }
            set
            {
                _visibilityRectangleAbleitung = value;
                OnPropertyChanged(nameof(VisibilityRectangleAbleitung));
            }
        }
        #endregion




        #region Color F5
        public void FarbeCircle_F5(bool val)
        {
            if (val) ColorCircle_F5 = "LawnGreen"; else ColorCircle_F5 = "Red";
        }

        private string _colorCircle_F5;
        public string ColorCircle_F5
        {
            get { return _colorCircle_F5; }
            set
            {
                _colorCircle_F5 = value;
                OnPropertyChanged(nameof(ColorCircle_F5));
            }
        }
        #endregion

        #region Color M1
        public void FarbeCircle_M1(bool val)
        {
            if (val) ColorCircle_M1 = "lawngreen"; else ColorCircle_M1 = "LightGray";
        }

        private string _colorCircle_M1;
        public string ColorCircle_M1
        {
            get { return _colorCircle_M1; }
            set
            {
                _colorCircle_M1 = value;
                OnPropertyChanged(nameof(ColorCircle_M1));
            }
        }
        #endregion

        #region Color P1
        public void FarbeCircle_P1(bool val)
        {
            if (val) ColorCircle_P1 = "lawngreen"; else ColorCircle_P1 = "LightGray";
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
            if (val) ColorCircle_P2 = "red"; else ColorCircle_P2 = "LightGray";
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

        #region Color Zuleitung
        public void FarbeRectangleZuleitung(bool val)
        {
            if (val) ColorRectangleZuleitung = "blue"; else ColorRectangleZuleitung = "lightblue";
        }

        private string _colorRectangleZuleitung;
        public string ColorRectangleZuleitung
        {
            get { return _colorRectangleZuleitung; }
            set
            {
                _colorRectangleZuleitung = value;
                OnPropertyChanged(nameof(ColorRectangleZuleitung));
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
