namespace BehaelterSteuerung.ViewModel
{
    using LAP_2010_1_Kompressoranlage;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {

        private readonly LAP_2010_1_Kompressoranlage.Model.Kompressoranlage kompressoranlage;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen(MainWindow mw, LAP_2010_1_Kompressoranlage.Model.Kompressoranlage ka)
        {
            mainWindow = mw;
            kompressoranlage = ka;

            Druck = 1.1;

            ClickModeBtnS1 = "Press";
            ClickModeBtnS2 = "Press";

            ColorF5 = "LawnGreen";
            ColorH1 = "LawnGreen";
            ColorH2 = "LawnGreen";
            ColorK1 = "LawnGreen";
            ColorK2 = "LawnGreen";
            ColorK3 = "LawnGreen";
            ColorS7 = "LawnGreen";

            VisibilityS7Ein = "hidden";
            VisibilityS7Aus = "visible";

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
                Druck = kompressoranlage.Druck;

                FarbeF5(kompressoranlage.F5);
                FarbeH1(kompressoranlage.H1);
                FarbeH2(kompressoranlage.H2);
                FarbeK1(kompressoranlage.K1);
                FarbeK2(kompressoranlage.K2);
                FarbeK3(kompressoranlage.K3);
                FarbeS7(kompressoranlage.S7);

                SichtbarkeitS7(kompressoranlage.S7);
                SichtbarkeitS8(kompressoranlage.S8);

                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }



        internal void SetS1() { kompressoranlage.S1 = ClickModeButtonS1(); }
        internal void BtnS2() { kompressoranlage.S2 = ClickModeButtonS2(); }


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



        #region Color F5
        public void FarbeF5(bool val)
        {
            if (val) ColorF5 = "LawnGreen"; else ColorF5 = "Red";
        }

        private string _colorF5;
        public string ColorF5
        {
            get { return _colorF5; }
            set
            {
                _colorF5 = value;
                OnPropertyChanged(nameof(ColorF5));
            }
        }
        #endregion

        #region Color H1
        public void FarbeH1(bool val)
        {
            if (val) ColorH1 = "LawnGreen"; else ColorH1 = "White";
        }

        private string _colorH1;
        public string ColorH1
        {
            get { return _colorH1; }
            set
            {
                _colorH1 = value;
                OnPropertyChanged(nameof(ColorH1));
            }
        }
        #endregion

        #region Color H2
        public void FarbeH2(bool val)
        {
            if (val) ColorH2 = "Red"; else ColorH2 = "White";
        }

        private string _colorH2;
        public string ColorH2
        {
            get { return _colorH2; }
            set
            {
                _colorH2 = value;
                OnPropertyChanged(nameof(ColorH2));
            }
        }
        #endregion

        #region Color K1
        public void FarbeK1(bool val)
        {
            if (val) ColorK1 = "LawnGreen"; else ColorK1 = "White";
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

        #region Color K2
        public void FarbeK2(bool val)
        {
            if (val) ColorK2 = "LawnGreen"; else ColorK2 = "White";
        }

        private string _colorK2;
        public string ColorK2
        {
            get { return _colorK2; }
            set
            {
                _colorK2 = value;
                OnPropertyChanged(nameof(ColorK2));
            }
        }
        #endregion

        #region Color K3
        public void FarbeK3(bool val)
        {
            if (val) ColorK3 = "LawnGreen"; else ColorK3 = "White";
        }

        private string _colorK3;
        public string ColorK3
        {
            get { return _colorK3; }
            set
            {
                _colorK3 = value;
                OnPropertyChanged(nameof(ColorK3));
            }
        }
        #endregion

        #region Color S7
        public void FarbeS7(bool val)
        {
            if (val) ColorS7 = "LawnGreen"; else ColorS7 = "Red";
        }

        private string _colorS7;
        public string ColorS7
        {
            get { return _colorS7; }
            set
            {
                _colorS7 = value;
                OnPropertyChanged(nameof(ColorS7));
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


        #region Druck

        private double _druck;
        public double Druck
        {
            get { return _druck; }
            set
            {
                _druck = value;
                OnPropertyChanged(nameof(Druck));
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
