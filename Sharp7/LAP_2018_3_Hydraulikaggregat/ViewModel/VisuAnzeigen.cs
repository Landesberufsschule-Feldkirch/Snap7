namespace LAP_2018_3_Hydraulikaggregat.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly LAP_2018_3_Hydraulikaggregat.Model.Hydraulikaggregat hydraulikaggregat;
        private readonly MainWindow mainWindow;

        private const double fuellBalkenHoehe = 390;
        private const double fuellBalkenOben = -40;

        public VisuAnzeigen(MainWindow mw, LAP_2018_3_Hydraulikaggregat.Model.Hydraulikaggregat ha)
        {
            mainWindow = mw;
            hydraulikaggregat = ha;

            Druck = 1.1;

            ClickModeBtnQ1 = "Press";
            ClickModeBtnQ2 = "Press";
            ClickModeBtnQ3 = "Press";
            ClickModeBtnQ1_Q3 = "Press";

            ClickModeBtnS1 = "Press";
            ClickModeBtnS2 = "Press";
            ClickModeBtnS3 = "Press";

            ColorP1 = "LawnGreen";
            ColorP2 = "LawnGreen";
            ColorP3 = "LawnGreen";
            ColorP4 = "LawnGreen";

            ColorQ1 = "LawnGreen";
            ColorQ2 = "LawnGreen";
            ColorQ3 = "LawnGreen";

            Margin1 = new Thickness(42, 0, 32, 0);

            VisibilityB1Ein = "hidden";
            VisibilityB1Aus = "visible";

            VisibilityB2Ein = "hidden";
            VisibilityB2Aus = "visible";

            VisibilityB3Ein = "hidden";
            VisibilityB3Aus = "visible";

            VisibilityKurzschluss = "Hidden";

            SpsStatus = "-";
            SpsColor = "LightBlue";

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                Druck = hydraulikaggregat.Druck;

                FarbeF1(hydraulikaggregat.F1);

                FarbeP1(hydraulikaggregat.P1);
                FarbeP2(hydraulikaggregat.P2);
                FarbeP3(hydraulikaggregat.P3);
                FarbeP4(hydraulikaggregat.P4);

                FarbeQ1(hydraulikaggregat.Q1);
                FarbeQ2(hydraulikaggregat.Q2);
                FarbeQ3(hydraulikaggregat.Q3);

                Margin_1(hydraulikaggregat.Pegel);

                SichtbarkeitB1(hydraulikaggregat.B1);
                SichtbarkeitB2(hydraulikaggregat.B2);
                SichtbarkeitB3(hydraulikaggregat.B3);

                if (hydraulikaggregat.Q2 && hydraulikaggregat.Q3) VisibilityKurzschluss = "Visible"; else VisibilityKurzschluss = "Hidden";

                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }

        internal void BtnQ1() => hydraulikaggregat.Q1 = ClickModeButtonQ1();
        internal void BtnQ2() => hydraulikaggregat.Q2 = ClickModeButtonQ2();
        internal void BtnQ3() => hydraulikaggregat.Q3 = ClickModeButtonQ3();

        internal void BtnQ1_Q3()
        {
            if (ClickModeButtonQ1_Q3())
            {
                hydraulikaggregat.Q1 = true;
                hydraulikaggregat.Q3 = true;
            }
            else
            {
                hydraulikaggregat.Q1 = false;
                hydraulikaggregat.Q3 = false;
            }
        }

        internal void BtnS1() => hydraulikaggregat.S1 = ClickModeButtonS1();
        internal void BtnS2() => hydraulikaggregat.S2 = ClickModeButtonS2();

        internal void BtnS3()
        {
            hydraulikaggregat.Stopwatch.Restart();
            hydraulikaggregat.S3 = ClickModeButtonS3();
        }

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

        #endregion SPS Status und Farbe

        #region ClickModeBtnQ1

        public bool ClickModeButtonQ1()
        {
            if (ClickModeBtnQ1 == "Press")
            {
                ClickModeBtnQ1 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnQ1 = "Press";
            }
            return false;
        }

        private string _clickModeBtnQ1;

        public string ClickModeBtnQ1
        {
            get { return _clickModeBtnQ1; }
            set
            {
                _clickModeBtnQ1 = value;
                OnPropertyChanged(nameof(ClickModeBtnQ1));
            }
        }

        #endregion ClickModeBtnQ1

        #region ClickModeBtnQ2

        public bool ClickModeButtonQ2()
        {
            if (ClickModeBtnQ2 == "Press")
            {
                ClickModeBtnQ2 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnQ2 = "Press";
            }
            return false;
        }

        private string _clickModeBtnQ2;

        public string ClickModeBtnQ2
        {
            get { return _clickModeBtnQ2; }
            set
            {
                _clickModeBtnQ2 = value;
                OnPropertyChanged(nameof(ClickModeBtnQ2));
            }
        }

        #endregion ClickModeBtnQ2

        #region ClickModeBtnQ3

        public bool ClickModeButtonQ3()
        {
            if (ClickModeBtnQ3 == "Press")
            {
                ClickModeBtnQ3 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnQ3 = "Press";
            }
            return false;
        }

        private string _clickModeBtnQ3;

        public string ClickModeBtnQ3
        {
            get { return _clickModeBtnQ3; }
            set
            {
                _clickModeBtnQ3 = value;
                OnPropertyChanged(nameof(ClickModeBtnQ3));
            }
        }

        #endregion ClickModeBtnQ3

        #region ClickModeBtnQ1_Q3

        public bool ClickModeButtonQ1_Q3()
        {
            if (ClickModeBtnQ1_Q3 == "Press")
            {
                ClickModeBtnQ1_Q3 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnQ1_Q3 = "Press";
            }
            return false;
        }

        private string _clickModeBtnQ1_Q3;

        public string ClickModeBtnQ1_Q3
        {
            get { return _clickModeBtnQ1_Q3; }
            set
            {
                _clickModeBtnQ1_Q3 = value;
                OnPropertyChanged(nameof(ClickModeBtnQ1_Q3));
            }
        }

        #endregion ClickModeBtnQ1_Q3

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

        #endregion ClickModeBtnS1

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

        #endregion ClickModeBtnS2

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

        #endregion ClickModeBtnS3

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

        #endregion Color F1

        #region Color P1

        public void FarbeP1(bool val)
        {
            if (val) ColorP1 = "LawnGreen"; else ColorP1 = "White";
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

        #endregion Color P1

        #region Color P2

        public void FarbeP2(bool val)
        {
            if (val) ColorP2 = "Red"; else ColorP2 = "White";
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

        #endregion Color P2

        #region Color P3

        public void FarbeP3(bool val)
        {
            if (val) ColorP3 = "LawnGreen"; else ColorP3 = "White";
        }

        private string _colorP3;

        public string ColorP3
        {
            get { return _colorP3; }
            set
            {
                _colorP3 = value;
                OnPropertyChanged(nameof(ColorP3));
            }
        }

        #endregion Color P3

        #region Color P4

        public void FarbeP4(bool val)
        {
            if (val) ColorP4 = "Red"; else ColorP4 = "White";
        }

        private string _colorP4;

        public string ColorP4
        {
            get { return _colorP4; }
            set
            {
                _colorP4 = value;
                OnPropertyChanged(nameof(ColorP4));
            }
        }

        #endregion Color P4

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

        #endregion Color Q1

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

        #endregion Color Q2

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

        #endregion Color Q3

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

        #endregion Druck

        #region Margin1

        public void Margin_1(double pegel)
        {
            Margin1 = new System.Windows.Thickness(41, fuellBalkenOben + fuellBalkenHoehe * (1 - pegel), 31, 0);
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

        #endregion Margin1

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
            get { return _visibilityB1Ein; }
            set
            {
                _visibilityB1Ein = value;
                OnPropertyChanged(nameof(VisibilityB1Ein));
            }
        }

        private string _visibilityB1Aus;

        public string VisibilityB1Aus
        {
            get { return _visibilityB1Aus; }
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
                VisibilityB2Ein = "visible";
                VisibilityB2Aus = "hidden";
            }
            else
            {
                VisibilityB2Ein = "hidden";
                VisibilityB2Aus = "visible";
            }
        }

        private string _visibilityB2Ein;

        public string VisibilityB2Ein
        {
            get { return _visibilityB2Ein; }
            set
            {
                _visibilityB2Ein = value;
                OnPropertyChanged(nameof(VisibilityB2Ein));
            }
        }

        private string _visibilityB2Aus;

        public string VisibilityB2Aus
        {
            get { return _visibilityB2Aus; }
            set
            {
                _visibilityB2Aus = value;
                OnPropertyChanged(nameof(VisibilityB2Aus));
            }
        }

        #endregion Sichtbarkeit B2

        #region Sichtbarkeit B3

        public void SichtbarkeitB3(bool val)
        {
            if (val)
            {
                VisibilityB3Ein = "visible";
                VisibilityB3Aus = "hidden";
            }
            else
            {
                VisibilityB3Ein = "hidden";
                VisibilityB3Aus = "visible";
            }
        }

        private string _visibilityB3Ein;

        public string VisibilityB3Ein
        {
            get { return _visibilityB3Ein; }
            set
            {
                _visibilityB3Ein = value;
                OnPropertyChanged(nameof(VisibilityB3Ein));
            }
        }

        private string _visibilityB3Aus;

        public string VisibilityB3Aus
        {
            get { return _visibilityB3Aus; }
            set
            {
                _visibilityB3Aus = value;
                OnPropertyChanged(nameof(VisibilityB3Aus));
            }
        }

        #endregion Sichtbarkeit B3

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

        #endregion VisibilityKurzschluss

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members
    }
}