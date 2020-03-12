namespace LAP_2010_4_Abfuellanlage.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using Utilities;

    public class VisuAnzeigen : INotifyPropertyChanged
    {

        private readonly LAP_2010_4_Abfuellanlage.Model.AbfuellAnlage abfuellAnlage;
        private readonly MainWindow mainWindow;

        private readonly double HoeheFuellBalken = 9 * 35;


        public VisuAnzeigen(MainWindow mw, LAP_2010_4_Abfuellanlage.Model.AbfuellAnlage aa)
        {
            mainWindow = mw;
            abfuellAnlage = aa;


            ColorH4 = "White";
            ColorK1 = "LawnGreen";
            ColorRectangleZuleitung = "Coral";

            ClickModeBtnS1 = "Press";
            ClickModeBtnS2 = "Press";

            ClickModeBtnK1 = "Press";
            ClickModeBtnK2 = "Press";
            ClickModeBtnK3 = "Press";



            ImageTop1 = 10;
            ImageTop2 = 20;
            ImageTop3 = 30;
            ImageTop4 = 40;

            ImageLeft1 = 10;
            ImageLeft2 = 20;
            ImageLeft3 = 30;
            ImageLeft4 = 10;



            Margin1 = new Thickness(0, 30, 0, 0);



            VisibilityRectangleAbleitung = "visible";

            VisibilityImage1 = "visible";
            VisibilityImage2 = "visible";
            VisibilityImage3 = "visible";
            VisibilityImage4 = "visible";


            Visibility_K2_Ein = "hidden";
            Visibility_K3_Ein = "hidden";

            Visibility_K2_Aus = "visible";
            Visibility_K3_Aus = "visible";

            VisibilityS7Ein = "Visible";
            VisibilityS7Aus = "Hidden";
            VisibilityS8Ein = "Visible";
            VisibilityS8Aus = "Hidden";


            SpsStatus = "-";
            SpsColor = "LightBlue";


            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }



        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeH4(abfuellAnlage.H4);
                FarbeK1(abfuellAnlage.K1);

                FarbeRectangleZuleitung(abfuellAnlage.Pegel > 0.01);

                Margin_1(abfuellAnlage.Pegel);

                PositionImage_1(abfuellAnlage.AlleDosen[0].Position.Punkt);
                PositionImage_2(abfuellAnlage.AlleDosen[1].Position.Punkt);
                PositionImage_3(abfuellAnlage.AlleDosen[2].Position.Punkt);
                PositionImage_4(abfuellAnlage.AlleDosen[3].Position.Punkt);

                VisibilityDose1(abfuellAnlage.AlleDosen[0].Sichtbar);
                VisibilityDose2(abfuellAnlage.AlleDosen[1].Sichtbar);
                VisibilityDose3(abfuellAnlage.AlleDosen[2].Sichtbar);
                VisibilityDose4(abfuellAnlage.AlleDosen[3].Sichtbar);

                VisibilityAbleitung(abfuellAnlage.K3 && (abfuellAnlage.Pegel > 0.01));

                SichtbarkeitK2(abfuellAnlage.K2);
                SichtbarkeitK3(abfuellAnlage.K3);

                SichtbarkeitS7(abfuellAnlage.S7);
                SichtbarkeitS8(abfuellAnlage.S8);

                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }


        internal void SetK1() { abfuellAnlage.K1 = ClickModeButtonK1(); }
        internal void SetK2() { abfuellAnlage.K2 = ClickModeButtonK2(); }
        internal void SetK3() { abfuellAnlage.K3 = ClickModeButtonK3(); }
        internal void SetS1() { abfuellAnlage.S1 = ClickModeButtonS1(); }
        internal void BtnS2() { abfuellAnlage.S2 = ClickModeButtonS2(); }



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




        #region Color H4
        public void FarbeH4(bool val)
        {
            if (val) ColorH4 = "Red"; else ColorH4 = "White";
        }

        private string _colorH4;
        public string ColorH4
        {
            get { return _colorH4; }
            set
            {
                _colorH4 = value;
                OnPropertyChanged(nameof(ColorH4));
            }
        }
        #endregion

        #region Color K1
        public void FarbeK1(bool val)
        {
            if (val) ColorK1 = "LawnGreen"; else ColorK1 = "LightGray";
        }

        private string _colorK1;
        public string ColorK1
        {
            get { return _colorK1; }
            set
            {
                _colorK1 = value;
                OnPropertyChanged(nameof(ColorK1));
            }
        }
        #endregion

        #region Color Zuleitung
        public void FarbeRectangleZuleitung(bool val)
        {
            if (val) ColorRectangleZuleitung = "Coral"; else ColorRectangleZuleitung = "LightCoral";
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

        #region ClickModeBtnK3
        public bool ClickModeButtonK3()
        {
            if (ClickModeBtnK3 == "Press")
            {
                ClickModeBtnK3 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnK3 = "Press";
            }
            return false;
        }

        private string _clickModeBtnK3;
        public string ClickModeBtnK3
        {
            get { return _clickModeBtnK3; }
            set
            {
                _clickModeBtnK3 = value;
                OnPropertyChanged(nameof(ClickModeBtnK3));
            }
        }
        #endregion







        #region Sichtbarkeit K2
        public void SichtbarkeitK2(bool val)
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

        #region Sichtbarkeit K3
        public void SichtbarkeitK3(bool val)
        {
            if (val)
            {
                Visibility_K3_Ein = "visible";
                Visibility_K3_Aus = "hidden";
            }
            else
            {
                Visibility_K3_Ein = "hidden";
                Visibility_K3_Aus = "visible";
            }
        }

        private string _visibility_K3_Ein;
        public string Visibility_K3_Ein
        {
            get { return _visibility_K3_Ein; }
            set
            {
                _visibility_K3_Ein = value;
                OnPropertyChanged(nameof(Visibility_K3_Ein));
            }
        }

        private string _visibility_K3_Aus;
        public string Visibility_K3_Aus
        {
            get { return _visibility_K3_Aus; }
            set
            {
                _visibility_K3_Aus = value;
                OnPropertyChanged(nameof(Visibility_K3_Aus));
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


        #region Visibility Dose 1
        public void VisibilityDose1(bool val)
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

        #region Visibility Dose 2
        public void VisibilityDose2(bool val)
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

        #region Visibility Dose 3
        public void VisibilityDose3(bool val)
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

        #region Visibility Dose 4
        public void VisibilityDose4(bool val)
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



        #region Sichtbarkeit S7
        public void SichtbarkeitS7(bool val)
        {
            if (val)
            {
                VisibilityS7Ein = "Visible";
                VisibilityS7Aus = "Hidden";
            }
            else
            {
                VisibilityS7Ein = "Hidden";
                VisibilityS7Aus = "Visible";
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
