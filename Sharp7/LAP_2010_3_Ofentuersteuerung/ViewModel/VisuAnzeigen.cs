namespace LAP_2010_3_Ofentuersteuerung.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.OfentuerSteuerung _ofentuerSteuerung;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.OfentuerSteuerung oSt)
        {
            _mainWindow = mw;
            _ofentuerSteuerung = oSt;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            ZahnradWinkel = 0;
            ZahnstangePosition = _ofentuerSteuerung.PositionZahnstange;
            OfentuerePosition = _ofentuerSteuerung.PositionOfentuere;

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

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                OfentuerePosition = _ofentuerSteuerung.PositionOfentuere;
                ZahnstangePosition = _ofentuerSteuerung.PositionZahnstange;
                ZahnradWinkel = _ofentuerSteuerung.WinkelZahnrad;

                SichtbarkeitB1(_ofentuerSteuerung.B1);
                SichtbarkeitB2(_ofentuerSteuerung.B2);
                SichtbarkeitB3(_ofentuerSteuerung.B3);

                FarbeP1(_ofentuerSteuerung.P1);
                FarbeQ1(_ofentuerSteuerung.Q1);
                FarbeQ2(_ofentuerSteuerung.Q2);

                if (_ofentuerSteuerung.Q1 && _ofentuerSteuerung.Q2) VisibilityKurzschluss = "Visible"; else VisibilityKurzschluss = "Hidden";

                if (_mainWindow.S71200 != null)
                {
                    VersionNr = _mainWindow.VersionNummer;
                    SpsVersionLokal = _mainWindow.VersionInfo;
                    SpsVersionEntfernt = _mainWindow.S71200.GetVersion();                  
                    SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? "hidden" : "visible";

                    SpsColor = _mainWindow.S71200.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = _mainWindow.S71200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void SetManualQ1() => _ofentuerSteuerung.Q1 = ClickModeButtonQ1();

        internal void SetManualQ2() => _ofentuerSteuerung.Q2 = ClickModeButtonQ2();

        internal void SetS1() => _ofentuerSteuerung.S1 = ClickModeButtonS1();

        internal void SetS2() => _ofentuerSteuerung.S2 = ClickModeButtonS2();

        internal void SetS3() => _ofentuerSteuerung.S3 = ClickModeButtonS3();

        internal void SetB3() => _ofentuerSteuerung.B3 = !ClickModeButtonB3();

        #region SPS Version, Status und Farbe

        private string _versionNr;
        public string VersionNr
        {
            get => _versionNr;
            set
            {
                _versionNr = value;
                OnPropertyChanged(nameof(VersionNr));
            }
        }

        private string _spsVersionLokal;
        public string SpsVersionLokal
        {
            get => _spsVersionLokal;
            set
            {
                _spsVersionLokal = value;
                OnPropertyChanged(nameof(SpsVersionLokal));
            }
        }

        private string _spsVersionEntfernt;
        public string SpsVersionEntfernt
        {
            get => _spsVersionEntfernt;
            set
            {
                _spsVersionEntfernt = value;
                OnPropertyChanged(nameof(SpsVersionEntfernt));
            }
        }

        private string _spsVersionsInfoSichtbar;
        public string SpsVersionsInfoSichtbar
        {
            get => _spsVersionsInfoSichtbar;
            set
            {
                _spsVersionsInfoSichtbar = value;
                OnPropertyChanged(nameof(SpsVersionsInfoSichtbar));
            }
        }

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

        // ReSharper disable once UnusedMember.Global
        internal void BtnS3()
        {
            throw new NotImplementedException();
        }

        #endregion SPS Versionsinfo, Status und Farbe

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

        public void FarbeP1(bool val) => ColorP1 = val ? "LawnGreen" : "White";

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

        public void FarbeQ1(bool val) => ColorQ1 = val ? "LawnGreen" : "White";

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

        public void FarbeQ2(bool val) => ColorQ2 = val ? "LawnGreen" : "White";

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

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}