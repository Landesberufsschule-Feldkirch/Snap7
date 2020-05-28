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

            ColorP1 = "LawnGreen";
            ColorQ1 = "LawnGreen";
            ColorQ2 = "LawnGreen";

            ClickModeBtnQ1 = "Press";
            ClickModeBtnQ2 = "Press";

            ClickModeBtnS1 = "Press";
            ClickModeBtnS2 = "Press";
            ClickModeBtnS3 = "Press";
            ClickModeBtnB3 = "Press";

            VisibilityB1Ein = "Visible";
            VisibilityB1Aus = "Hidden";
            VisibilityB2Ein = "Visible";
            VisibilityB2Aus = "Hidden";
            VisibilityB3Ein = "Visible";
            VisibilityB3Aus = "Hidden";
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

                SichtbarkeitB1(ofentuerSteuerung.B1);
                SichtbarkeitB2(ofentuerSteuerung.B2);
                SichtbarkeitB3(ofentuerSteuerung.B3);

                FarbeP1(ofentuerSteuerung.P1);
                FarbeQ1(ofentuerSteuerung.Q1);
                FarbeQ2(ofentuerSteuerung.Q2);

                if (ofentuerSteuerung.Q1 && ofentuerSteuerung.Q2) VisibilityKurzschluss = "Visible"; else VisibilityKurzschluss = "Hidden";

                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }
        internal void SetManualQ1() => ofentuerSteuerung.Q1 = ClickModeButtonQ1();
        internal void SetManualQ2() => ofentuerSteuerung.Q2 = ClickModeButtonQ2();
        internal void SetS1() => ofentuerSteuerung.S1 = ClickModeButtonS1();
        internal void SetS2() => ofentuerSteuerung.S2 = ClickModeButtonS2();
        internal void SetS3() => ofentuerSteuerung.S3 = ClickModeButtonS3();
        internal void SetB3() => ofentuerSteuerung.B3 = !ClickModeButtonB3();

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

        internal void BtnS3()
        {
            throw new NotImplementedException();
        }

        #endregion SPS Status und Farbe

        #region ZahnradWinkel

        private double _zahnradWinkel;

        public double ZahnradWinkel
        {
            get => _zahnradWinkel; 
            set
            {
                _zahnradWinkel = value;
                OnPropertyChanged(nameof(ZahnradWinkel));
            }
        }

        #endregion ZahnradWinkel

        #region ZahnstangePosition

        private double _zahnstangePosition;

        public double ZahnstangePosition
        {
            get => _zahnstangePosition; 
            set
            {
                _zahnstangePosition = value;
                OnPropertyChanged(nameof(ZahnstangePosition));
            }
        }

        #endregion ZahnstangePosition

        #region OfentuerePosition

        private double _ofentuerePosition;

        public double OfentuerePosition
        {
            get => _ofentuerePosition; 
            set
            {
                _ofentuerePosition = value;
                OnPropertyChanged(nameof(OfentuerePosition));
            }
        }

        #endregion OfentuerePosition

        #region Color P1

        public void FarbeP1(bool val)
        {
            if (val) ColorP1 = "LawnGreen"; else ColorP1 = "White";
        }

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

        public void FarbeQ1(bool val)
        {
            if (val) ColorQ1 = "LawnGreen"; else ColorQ1 = "White";
        }

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

        public void FarbeQ2(bool val)
        {
            if (val) ColorQ2 = "LawnGreen"; else ColorQ2 = "White";
        }

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
            get => _clickModeBtnS2; 
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
            get => _clickModeBtnS3; 
            set
            {
                _clickModeBtnS3 = value;
                OnPropertyChanged(nameof(ClickModeBtnS3));
            }
        }

        #endregion ClickModeBtnS3



        #region ClickModeBtnB3

        public bool ClickModeButtonB3()
        {
            if (ClickModeBtnB3 == "Press")
            {
                ClickModeBtnB3 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnB3 = "Press";
            }
            return false;
        }

        private string _clickModeBtnB3;

        public string ClickModeBtnB3
        {
            get => _clickModeBtnB3; 
            set
            {
                _clickModeBtnB3 = value;
                OnPropertyChanged(nameof(ClickModeBtnB3));
            }
        }

        #endregion ClickModeBtnB3

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

        #region Sichtbarkeit B3

        public void SichtbarkeitB3(bool val)
        {
            if (val)
            {
                VisibilityB3Ein = "Visible";
                VisibilityB3Aus = "Hidden";
            }
            else
            {
                VisibilityB3Ein = "Hidden";
                VisibilityB3Aus = "Visible";
            }
        }

        private string _visibilityB3Ein;

        public string VisibilityB3Ein
        {
            get => _visibilityB3Ein; 
            set
            {
                _visibilityB3Ein = value;
                OnPropertyChanged(nameof(VisibilityB3Ein));
            }
        }

        private string _visibilityB3Aus;

        public string VisibilityB3Aus
        {
            get => _visibilityB3Aus; 
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
            get => _visibilityKurzschluss; 
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