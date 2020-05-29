namespace LAP_2010_2_Transportwagen.ViewModel
{
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly LAP_2010_2_Transportwagen.Model.Transportwagen transportwagen;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen(MainWindow mw, LAP_2010_2_Transportwagen.Model.Transportwagen tw)
        {
            mainWindow = mw;
            transportwagen = tw;

            SpsStatus = "-";
            SpsColor = "LightBlue";

            ColorF1 = "LawnGreen";
            ColorP1 = "LawnGreen";
            ColorQ1 = "LawnGreen";
            ColorQ2 = "LawnGreen";
            ColorS2 = "LawnGreen";

            ClickModeBtnQ1 = "Press";
            ClickModeBtnQ2 = "Press";
            ClickModeBtnS1 = "Press";
            ClickModeBtnS3 = "Press";

            VisibilityB1Ein = "Visible";
            VisibilityB1Aus = "Hidden";
            VisibilityB2Ein = "Visible";
            VisibilityB2Aus = "Hidden";

            VisibilityKurzschluss = "Hidden";

            PositionRadLinks = 0;
            PositionRadRechts = 0;
            PositionWagenkasten = 0;

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeF1(transportwagen.F1);
                FarbeP1(transportwagen.P1);
                FarbeQ1(transportwagen.Q1);
                FarbeQ2(transportwagen.Q2);
                FarbeS2(transportwagen.S2);

                SichtbarkeitB1(transportwagen.B1);
                SichtbarkeitB2(transportwagen.B2);

                if (transportwagen.Q1 && transportwagen.Q2) VisibilityKurzschluss = "Visible"; else VisibilityKurzschluss = "Hidden";

                PositionRadLinks = transportwagen.Position;
                PositionRadRechts = transportwagen.Position + transportwagen.AbstandRadRechts;
                PositionWagenkasten = transportwagen.Position;

                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }

        internal void SetManualQ1() => transportwagen.Q1 = ClickModeButtonQ1();
        internal void SetManualQ2() => transportwagen.Q2 = ClickModeButtonQ2();
        internal void SetS1() => transportwagen.S1 = ClickModeButtonS1();
        internal void SetS3() => transportwagen.S3 = ClickModeButtonS3();

        #region SPS Status und Farbe

        private string _spsStatus;

        public string SpsStatus
        {
            get => _spsStatus;
            set
            {
                _spsStatus = value;
                OnPropertyChanged(nameof(SpsStatus));
            }
        }

        private string _spsColor;

        public string SpsColor
        {
            get => _spsColor;
            set
            {
                _spsColor = value;
                OnPropertyChanged(nameof(SpsColor));
            }
        }

        #endregion SPS Status und Farbe

        #region Color F1

        public void FarbeF1(bool val) { if (val) ColorF1 = "LawnGreen"; else ColorF1 = "Red"; }

        private string _colorF1;

        public string ColorF1
        {
            get => _colorF1;
            set
            {
                _colorF1 = value;
                OnPropertyChanged(nameof(ColorF1));
            }
        }

        #endregion Color F1

        #region Color P1

        public void FarbeP1(bool val) { if (val) ColorP1 = "Red"; else ColorP1 = "White"; }

        private string _colorP1;

        public string ColorP1
        {
            get => _colorP1;
            set
            {
                _colorP1 = value;
                OnPropertyChanged(nameof(ColorP1));
            }
        }

        #endregion Color P1

        #region Color Q1

        public void FarbeQ1(bool val) { if (val) ColorQ1 = "LawnGreen"; else ColorQ1 = "White"; }

        private string _colorQ1;

        public string ColorQ1
        {
            get => _colorQ1;
            set
            {
                _colorQ1 = value;
                OnPropertyChanged(nameof(ColorQ1));
            }
        }

        #endregion Color Q1

        #region Color Q2

        public void FarbeQ2(bool val) { if (val) ColorQ2 = "LawnGreen"; else ColorQ2 = "White"; }

        private string _colorQ2;

        public string ColorQ2
        {
            get => _colorQ2;
            set
            {
                _colorQ2 = value;
                OnPropertyChanged(nameof(ColorQ2));
            }
        }

        #endregion Color Q2

        #region Color S2

        public void FarbeS2(bool val) { if (val) ColorS2 = "LawnGreen"; else ColorS2 = "Red"; }

        private string _colorS2;

        public string ColorS2
        {
            get => _colorS2;
            set
            {
                _colorS2 = value;
                OnPropertyChanged(nameof(ColorS2));
            }
        }

        #endregion Color S2

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
            get => _clickModeBtnQ1;
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
            get => _clickModeBtnQ2;
            set
            {
                _clickModeBtnQ2 = value;
                OnPropertyChanged(nameof(ClickModeBtnQ2));
            }
        }

        #endregion ClickModeBtnQ2

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
            get => _clickModeBtnS1;
            set
            {
                _clickModeBtnS1 = value;
                OnPropertyChanged(nameof(ClickModeBtnS1));
            }
        }

        #endregion ClickModeBtnS1

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
            get => _clickModeBtnS3;
            set
            {
                _clickModeBtnS3 = value;
                OnPropertyChanged(nameof(ClickModeBtnS3));
            }
        }

        #endregion ClickModeBtnS3

        #region Sichtbarkeit B1

        public void SichtbarkeitB1(bool val)
        {
            if (val)
            {
                VisibilityB1Ein = "Visible";
                VisibilityB1Aus = "Hidden";
            }
            else
            {
                VisibilityB1Ein = "Hidden";
                VisibilityB1Aus = "Visible";
            }
        }

        private string _visibilityB1Ein;

        public string VisibilityB1Ein
        {
            get => _visibilityB1Ein;
            set
            {
                _visibilityB1Ein = value;
                OnPropertyChanged(nameof(VisibilityB1Ein));
            }
        }

        private string _visibilityB1Aus;

        public string VisibilityB1Aus
        {
            get => _visibilityB1Aus;
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
                VisibilityB2Ein = "Visible";
                VisibilityB2Aus = "Hidden";
            }
            else
            {
                VisibilityB2Ein = "Hidden";
                VisibilityB2Aus = "Visible";
            }
        }

        private string _visibilityB2Ein;

        public string VisibilityB2Ein
        {
            get => _visibilityB2Ein;
            set
            {
                _visibilityB2Ein = value;
                OnPropertyChanged(nameof(VisibilityB2Ein));
            }
        }

        private string _visibilityB2Aus;

        public string VisibilityB2Aus
        {
            get => _visibilityB2Aus;
            set
            {
                _visibilityB2Aus = value;
                OnPropertyChanged(nameof(VisibilityB2Aus));
            }
        }

        #endregion Sichtbarkeit B2

        #region VisibilityKurzschluss

        private string _visibilityKurzschluss;

        public string VisibilityKurzschluss
        {
            get => _visibilityKurzschluss;
            set
            {
                _visibilityKurzschluss = value;
                OnPropertyChanged(nameof(VisibilityKurzschluss));
            }
        }

        #endregion VisibilityKurzschluss

        #region PositionRadLinks

        private double _positionRadLinks;

        public double PositionRadLinks
        {
            get => _positionRadLinks;
            set
            {
                _positionRadLinks = value;
                OnPropertyChanged(nameof(PositionRadLinks));
            }
        }

        #endregion PositionRadLinks

        #region PositionRadRechts

        private double _positionRadRechts;

        public double PositionRadRechts
        {
            get => _positionRadRechts;
            set
            {
                _positionRadRechts = value;
                OnPropertyChanged(nameof(PositionRadRechts));
            }
        }

        #endregion PositionRadRechts

        #region PositionWagenkasten

        private double _positionWagenkasten;

        public double PositionWagenkasten
        {
            get => _positionWagenkasten;
            set
            {
                _positionWagenkasten = value;
                OnPropertyChanged(nameof(PositionWagenkasten));
            }
        }

        #endregion PositionWagenkasten


        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}