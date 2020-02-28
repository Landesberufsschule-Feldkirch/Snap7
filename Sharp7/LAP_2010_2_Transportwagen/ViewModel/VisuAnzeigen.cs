namespace LAP_2010_2_Transportwagen.ViewModel
{
    using LAP_2010_2_Transportwagen;
    using System;
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

            ColorH3 = "LawnGreen";
            ColorS2 = "LawnGreen";

            ClickModeBtnK1 = "Press";
            ClickModeBtnK2 = "Press";
            ClickModeBtnS1 = "Press";

            VisibilityS7Ein = "Visible";
            VisibilityS7Aus = "Hidden";
            VisibilityS8Ein = "Visible";
            VisibilityS8Aus = "Hidden";


            PositionRadLinks = 0;
            PositionRadRechts = 0;
            PositionWagenkasten = 0;

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {

                FarbeH3(transportwagen.H3);
                FarbeS2(transportwagen.S2);

                SichtbarkeitS7(transportwagen.S7);
                SichtbarkeitS8(transportwagen.S8);


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

        internal void SetManualK1() { transportwagen.K1 = ClickModeButtonK1(); }
        internal void SetManualK2() { transportwagen.K2 = ClickModeButtonK2(); }
        internal void SetS1() { transportwagen.S1 = ClickModeButtonS1(); }




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


        #region Color H3
        public void FarbeH3(bool val)
        {
            if (val) ColorH3 = "Red"; else ColorH3 = "White";
        }

        private string _colorH3;
        public string ColorH3
        {
            get { return _colorH3; }
            set
            {
                _colorH3 = value;
                OnPropertyChanged(nameof(ColorH3));
            }
        }
        #endregion

        #region Color S2
        public void FarbeS2(bool val)
        {
            if (val) ColorS2 = "LawnGreen"; else ColorS2 = "Red";
        }

        private string _colorS2;
        public string ColorS2
        {
            get { return _colorS2; }
            set
            {
                _colorS2 = value;
                OnPropertyChanged(nameof(ColorS2));
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


        #region   PositionRadLinks 
        private double _positionRadLinks;
        public double PositionRadLinks
        {
            get { return _positionRadLinks; }
            set
            {
                _positionRadLinks = value;
                OnPropertyChanged(nameof(PositionRadLinks));
            }
        }
        #endregion

        #region   PositionRadRechts 
        private double _positionRadRechts;
        public double PositionRadRechts
        {
            get { return _positionRadRechts; }
            set
            {
                _positionRadRechts = value;
                OnPropertyChanged(nameof(PositionRadRechts));
            }
        }
        #endregion

        #region   PositionWagenkasten 
        private double _positionWagenkasten;
        public double PositionWagenkasten
        {
            get { return _positionWagenkasten; }
            set
            {
                _positionWagenkasten = value;
                OnPropertyChanged(nameof(PositionWagenkasten));
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
