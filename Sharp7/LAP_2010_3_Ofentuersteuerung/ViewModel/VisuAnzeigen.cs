namespace LAP_2010_3_Ofentuersteuerung.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {

        private readonly LAP_2010_3_Ofentuersteuerung.Model.OfentuerSteuerung ofentuerSteuerung;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen(MainWindow mw, LAP_2010_3_Ofentuersteuerung.Model.OfentuerSteuerung oSt)
        {
            mainWindow = mw;
            ofentuerSteuerung = oSt;

            SpsStatus = "-";
            SpsColor = "LightBlue";


            ZahnradWinkel = 0;
            ZahnstangePosition = ofentuerSteuerung.PositionZahnstange;
            OfentuerePosition = ofentuerSteuerung.PositionOfentuere;

            ColorH3 = "LawnGreen";
            ColorK1 = "LawnGreen";
            ColorK2 = "LawnGreen";

            ClickModeBtnK1 = "Press";
            ClickModeBtnK2 = "Press";

            ClickModeBtnS1 = "Press";
            ClickModeBtnS2 = "Press";
            ClickModeBtnS3 = "Press";
            ClickModeBtnS9 = "Press";


            VisibilityS7Ein = "Visible";
            VisibilityS7Aus = "Hidden";
            VisibilityS8Ein = "Visible";
            VisibilityS8Aus = "Hidden";
            VisibilityS9Ein = "Visible";
            VisibilityS9Aus = "Hidden";
            VisibilityKurzschluss = "Hidden";

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }
        private void VisuAnzeigenTask()
        {
            while (true)
            {

                OfentuerePosition = ofentuerSteuerung.PositionOfentuere;
                ZahnstangePosition = ofentuerSteuerung.PositionZahnstange;
                ZahnradWinkel = ofentuerSteuerung.WinkelZahnrad;

                SichtbarkeitS7(ofentuerSteuerung.S7);
                SichtbarkeitS8(ofentuerSteuerung.S8);
                SichtbarkeitS9(ofentuerSteuerung.S9);

                FarbeH3(ofentuerSteuerung.H3);
                FarbeK1(ofentuerSteuerung.K1);
                FarbeK2(ofentuerSteuerung.K2);

                if (ofentuerSteuerung.K1 && ofentuerSteuerung.K2) VisibilityKurzschluss = "Visible"; else VisibilityKurzschluss = "Hidden";

                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }

        internal void SetManualK1() { ofentuerSteuerung.K1 = ClickModeButtonK1(); }
        internal void SetManualK2() { ofentuerSteuerung.K2 = ClickModeButtonK2(); }

        internal void SetS1() { ofentuerSteuerung.S1 = ClickModeButtonS1(); }
        internal void SetS2() { ofentuerSteuerung.S2 = ClickModeButtonS2(); }
        internal void SetS3() { ofentuerSteuerung.S3 = ClickModeButtonS3(); }
        internal void SetS9() { ofentuerSteuerung.S9 = !ClickModeButtonS9(); }


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

        internal void BtnS3()
        {
            throw new NotImplementedException();
        }
        #endregion


        #region ZahnradWinkel
        private double _zahnradWinkel;
        public double ZahnradWinkel
        {
            get { return _zahnradWinkel; }
            set
            {
                _zahnradWinkel = value;
                OnPropertyChanged(nameof(ZahnradWinkel));
            }
        }

        #endregion

        #region   ZahnstangePosition 
        private double _zahnstangePosition;
        public double ZahnstangePosition
        {
            get { return _zahnstangePosition; }
            set
            {
                _zahnstangePosition = value;
                OnPropertyChanged(nameof(ZahnstangePosition));
            }
        }

        #endregion

        #region   OfentuerePosition 
        private double _ofentuerePosition;
        public double OfentuerePosition
        {
            get { return _ofentuerePosition; }
            set
            {
                _ofentuerePosition = value;
                OnPropertyChanged(nameof(OfentuerePosition));
            }
        }

        #endregion


        #region Color H3
        public void FarbeH3(bool val)
        {
            if (val) ColorH3 = "LawnGreen"; else ColorH3 = "White";
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

        #region ClickModeBtnS9
        public bool ClickModeButtonS9()
        {
            if (ClickModeBtnS9 == "Press")
            {
                ClickModeBtnS9 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnS9 = "Press";
            }
            return false;
        }

        private string _clickModeBtnS9;
        public string ClickModeBtnS9
        {
            get { return _clickModeBtnS9; }
            set
            {
                _clickModeBtnS9 = value;
                OnPropertyChanged(nameof(ClickModeBtnS9));
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

        #region Sichtbarkeit S9
        public void SichtbarkeitS9(bool val)
        {
            if (val)
            {
                VisibilityS9Ein = "Visible";
                VisibilityS9Aus = "Hidden";
            }
            else
            {
                VisibilityS9Ein = "Hidden";
                VisibilityS9Aus = "Visible";
            }
        }
        private string _visibilityS9Ein;
        public string VisibilityS9Ein
        {
            get { return _visibilityS9Ein; }
            set
            {
                _visibilityS9Ein = value;
                OnPropertyChanged(nameof(VisibilityS9Ein));
            }
        }

        private string _visibilityS9Aus;

        public string VisibilityS9Aus
        {
            get { return _visibilityS9Aus; }
            set
            {
                _visibilityS9Aus = value;
                OnPropertyChanged(nameof(VisibilityS9Aus));
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


        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members

    }
}