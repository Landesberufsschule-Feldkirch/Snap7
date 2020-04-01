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

            ColorF1 = "LawnGreen";
            ColorP1 = "LawnGreen";
            ColorP2 = "LawnGreen";
            ColorQ1 = "LawnGreen";
            ColorQ2 = "LawnGreen";
            ColorQ3 = "LawnGreen";
            ColorB1 = "LawnGreen";

            VisibilityB1Ein = "hidden";
            VisibilityB1Aus = "visible";

            VisibilityB2Ein = "Visible";
            VisibilityB2Aus = "Hidden";

            VisibilityKurzschluss = "Hidden";

            SpsStatus = "-";
            SpsColor = "LightBlue";

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }
        private void VisuAnzeigenTask()
        {
            while (true)
            {
                Druck = kompressoranlage.Druck;

                FarbeF1(kompressoranlage.F1);
                FarbeP1(kompressoranlage.P1);
                FarbeP2(kompressoranlage.P2);
                FarbeQ1(kompressoranlage.Q1);
                FarbeQ2(kompressoranlage.Q2);
                FarbeQ3(kompressoranlage.Q3);
                FarbeS7(kompressoranlage.B1);

                SichtbarkeitS7(kompressoranlage.B1);
                SichtbarkeitS8(kompressoranlage.B2);

                if (kompressoranlage.Q2 && kompressoranlage.Q3) VisibilityKurzschluss = "Visible"; else VisibilityKurzschluss = "Hidden";

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



        #region Color F1
        public void FarbeF1(bool val)
        {
            if (val) ColorF1 = "LawnGreen"; else ColorF1 = "Red";
        }

        private string _colorF1;
        public string ColorF1
        {
            get { return _colorF1; }
            set
            {
                _colorF1 = value;
                OnPropertyChanged(nameof(ColorF1));
            }
        }
        #endregion

        #region Color P1
        public void FarbeP1(bool val)
        {
            if (val) ColorP1 = "Red"; else ColorP1 = "White";
        }

        private string _colorP1;
        public string ColorP1
        {
            get { return _colorP1; }
            set
            {
                _colorP1 = value;
                OnPropertyChanged(nameof(ColorP1));
            }
        }
        #endregion

        #region Color P2
        public void FarbeP2(bool val)
        {
            if (val) ColorP2 = "LawnGreen"; else ColorP2 = "White";
        }

        private string _colorP2;
        public string ColorP2
        {
            get { return _colorP2; }
            set
            {
                _colorP2 = value;
                OnPropertyChanged(nameof(ColorP2));
            }
        }
        #endregion

        #region Color Q1
        public void FarbeQ1(bool val)
        {
            if (val) ColorQ1 = "LawnGreen"; else ColorQ1 = "White";
        }

        private string _colorQ1;
        public string ColorQ1
        {
            get { return _colorQ1; }
            set
            {
                _colorQ1 = value;
                OnPropertyChanged(nameof(ColorQ1));
            }
        }
        #endregion

        #region Color Q2
        public void FarbeQ2(bool val)
        {
            if (val) ColorQ2 = "LawnGreen"; else ColorQ2 = "White";
        }

        private string _colorQ2;
        public string ColorQ2
        {
            get { return _colorQ2; }
            set
            {
                _colorQ2 = value;
                OnPropertyChanged(nameof(ColorQ2));
            }
        }
        #endregion

        #region Color Q3
        public void FarbeQ3(bool val)
        {
            if (val) ColorQ3 = "LawnGreen"; else ColorQ3 = "White";
        }

        private string _colorQ3;
        public string ColorQ3
        {
            get { return _colorQ3; }
            set
            {
                _colorQ3 = value;
                OnPropertyChanged(nameof(ColorQ3));
            }
        }
        #endregion

        #region Color S7
        public void FarbeS7(bool val)
        {
            if (val) ColorB1 = "LawnGreen"; else ColorB1 = "Red";
        }

        private string _ColorB1;
        public string ColorB1
        {
            get { return _ColorB1; }
            set
            {
                _ColorB1 = value;
                OnPropertyChanged(nameof(ColorB1));
            }
        }
        #endregion



        #region VisibilityKurzschluss 
        private string _visibilityKurzschluss;
        public string VisibilityKurzschluss
        {
            get { return _visibilityKurzschluss; }
            set
            {
                _visibilityKurzschluss = value;
                OnPropertyChanged(nameof(VisibilityKurzschluss));
            }
        }
        #endregion

        #region Sichtbarkeit S7
        public void SichtbarkeitS7(bool val)
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

        private string _VisibilityB1Ein;
        public string VisibilityB1Ein
        {
            get { return _VisibilityB1Ein; }
            set
            {
                _VisibilityB1Ein = value;
                OnPropertyChanged(nameof(VisibilityB1Ein));
            }
        }

        private string _VisibilityB1Aus;
        public string VisibilityB1Aus
        {
            get { return _VisibilityB1Aus; }
            set
            {
                _VisibilityB1Aus = value;
                OnPropertyChanged(nameof(VisibilityB1Aus));
            }
        }
        #endregion

        #region Sichtbarkeit S8
        public void SichtbarkeitS8(bool val)
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

        private string _VisibilityB2Ein;
        public string VisibilityB2Ein
        {
            get { return _VisibilityB2Ein; }
            set
            {
                _VisibilityB2Ein = value;
                OnPropertyChanged(nameof(VisibilityB2Ein));
            }
        }

        private string _VisibilityB2Aus;

        public string VisibilityB2Aus
        {
            get { return _VisibilityB2Aus; }
            set
            {
                _VisibilityB2Aus = value;
                OnPropertyChanged(nameof(VisibilityB2Aus));
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
