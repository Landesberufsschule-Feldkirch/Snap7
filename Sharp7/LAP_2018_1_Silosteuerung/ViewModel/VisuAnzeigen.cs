using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using Utilities;

namespace LAP_2018_1_Silosteuerung.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private const int MaterialSiloHoehe = 8 * 35;

        private readonly MainWindow _mainWindow;
        public readonly Model.Silosteuerung Silosteuerung;

        public VisuAnzeigen(MainWindow mw, Model.Silosteuerung st)
        {
            _mainWindow = mw;
            Silosteuerung = st;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            ColorF1 = "LawnGreen";
            ColorF2 = "LawnGreen";
            ColorP1 = "White";
            ColorP2 = "White";
            ColorQ1 = "LawnGreen";

            ClickModeBtnS0 = "Press";
            ClickModeBtnS1 = "Press";
            ClickModeBtnS2 = "Press";
            ClickModeBtnS3 = "Press";

            Margin1 = new Thickness(0, MaterialSiloHoehe * 0.1, 0, 0);

            VisibilityB1Ein = "hidden";
            VisibilityB1Aus = "visible";

            VisibilityB2Ein = "visible";
            VisibilityB2Aus = "hidden";

            VisibilityQ1Ein = "visible";
            VisibilityQ1Aus = "hidden";

            VisibilityXfuEin = "visible";

            VisibilityY1Ein = "hidden";
            VisibilityY1Aus = "visible";

            VisibilityMaterialOben = "Hidden";
            VisibilityMaterialUnten = "Hidden";

            PosWagenBeschriftungLeft = 74;
            PosWagenBeschriftungTop = 106;

            PosWagenLeft = 0;
            PosWagenTop = 0;

            PosWagenInhaltLeft = 12;
            PosWagenInhaltTop = 10;

            WagenFuellstand = 88;

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FuellstandSilo(Silosteuerung.Silo.GetFuellstand());

                FarbeF1(Silosteuerung.F1);
                FarbeF2(Silosteuerung.F2);

                FarbeP1(Silosteuerung.P1);
                FarbeP2(Silosteuerung.P2);
                FarbeQ1(Silosteuerung.Q1);

                SichtbarkeitB1(Silosteuerung.B1);
                SichtbarkeitB2(Silosteuerung.B2);
                SichtbarkeitQ1(Silosteuerung.Q1);
                SichtbarkeitXfu(Silosteuerung.Xfu);
                SichtbarkeitY1(Silosteuerung.Y1);

                SichtbarkeitMaterialOben(Silosteuerung.Silo.GetFuellstand() > 0.01);
                SichtbarkeitMaterialUnten(Silosteuerung.Silo.GetFuellstand() > 0.01 && Silosteuerung.Y1);

                PositionWagenBeschriftung(Silosteuerung.Wagen.GetPosition());
                PositionWagen(Silosteuerung.Wagen.GetPosition());
                PositionWagenInhalt(Silosteuerung.Wagen.GetPosition(), Silosteuerung.Wagen.GetFuellstand());
                WagenFuellstand = Math.Floor(Silosteuerung.Wagen.GetFuellstand());

                if (_mainWindow.AnimationGestartet)
                {
                    if (Silosteuerung.Xfu) _mainWindow.Controller.Play(); else _mainWindow.Controller.Pause();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void BtnS0() => Silosteuerung.S0 = !ClickModeButtonS0();
        internal void BtnS1() => Silosteuerung.S1 = ClickModeButtonS1();
        internal void BtnS2() => Silosteuerung.S2 = !ClickModeButtonS2();
        internal void BtnS3() => Silosteuerung.S3 = ClickModeButtonS3();

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

        #endregion SPS Versionsinfo, Status und Farbe

        #region Füllstand Silo
        public void FuellstandSilo(double pegel)
        {
            Margin1 = new Thickness(0, MaterialSiloHoehe * (1 - pegel), 0, 0);
        }

        private Thickness _margin1;

        public Thickness Margin1
        {
            get => _margin1;
            set
            {
                _margin1 = value;
                OnPropertyChanged(nameof(Margin1));
            }
        }
        #endregion

        #region Sichtbarkeit

        public void SichtbarkeitMaterialOben(bool val) => VisibilityMaterialOben = val ? "Visible" : "Hidden";

        private string _visibilityMaterialOben;

        public string VisibilityMaterialOben
        {
            get => _visibilityMaterialOben;
            set
            {
                _visibilityMaterialOben = value;
                OnPropertyChanged(nameof(VisibilityMaterialOben));
            }
        }

        public void SichtbarkeitMaterialUnten(bool val) => VisibilityMaterialUnten = val ? "Visible" : "Hidden";

        private string _visibilityMaterialUnten;

        public string VisibilityMaterialUnten
        {
            get => _visibilityMaterialUnten;
            set
            {
                _visibilityMaterialUnten = value;
                OnPropertyChanged(nameof(VisibilityMaterialUnten));
            }
        }

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

        // ReSharper disable once UnusedMember.Global
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



        public void SichtbarkeitQ1(bool val)
        {
            if (val)
            {
                VisibilityQ1Ein = "visible";
                VisibilityQ1Aus = "hidden";
            }
            else
            {
                VisibilityQ1Ein = "hidden";
                VisibilityQ1Aus = "visible";
            }
        }

        private string _visibilityQ1Ein;

        public string VisibilityQ1Ein
        {
            get => _visibilityQ1Ein;
            set
            {
                _visibilityQ1Ein = value;
                OnPropertyChanged(nameof(VisibilityQ1Ein));
            }
        }

        private string _visibilityQ1Aus;

        public string VisibilityQ1Aus
        {
            get => _visibilityQ1Aus;
            set
            {
                _visibilityQ1Aus = value;
                OnPropertyChanged(nameof(VisibilityQ1Aus));
            }
        }



        public void SichtbarkeitXfu(bool val)
        {
            if (val)
            {
                VisibilityXfuEin = "visible";
                VisibilityXfuAus = "hidden";
            }
            else
            {
                VisibilityXfuEin = "hidden";
                VisibilityXfuAus = "visible";
            }
        }

        private string _visibilityXfuEin;

        public string VisibilityXfuEin
        {
            get => _visibilityXfuEin;
            set
            {
                _visibilityXfuEin = value;
                OnPropertyChanged(nameof(VisibilityXfuEin));
            }
        }

        private string _visibilityXfuAus;

        public string VisibilityXfuAus
        {
            get => _visibilityXfuAus;
            set
            {
                _visibilityXfuAus = value;
                OnPropertyChanged(nameof(VisibilityXfuAus));
            }
        }


        public void SichtbarkeitY1(bool val)
        {
            if (val)
            {
                VisibilityY1Ein = "visible";
                VisibilityY1Aus = "hidden";
            }
            else
            {
                VisibilityY1Ein = "hidden";
                VisibilityY1Aus = "visible";
            }
        }

        private string _visibilityY1Ein;

        public string VisibilityY1Ein
        {
            get => _visibilityY1Ein;
            set
            {
                _visibilityY1Ein = value;
                OnPropertyChanged(nameof(VisibilityY1Ein));
            }
        }

        private string _visibilityY1Aus;

        public string VisibilityY1Aus
        {
            get => _visibilityY1Aus;
            set
            {
                _visibilityY1Aus = value;
                OnPropertyChanged(nameof(VisibilityY1Aus));
            }
        }

        #endregion

        #region Farben

        public void FarbeF1(bool val)
        {
            ColorF1 = val ? "LawnGreen" : "Red";
        }

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



        public void FarbeF2(bool val)
        {
            ColorF2 = val ? "LawnGreen" : "Red";
        }

        private string _colorF2;

        public string ColorF2
        {
            get => _colorF2;
            set
            {
                _colorF2 = value;
                OnPropertyChanged(nameof(ColorF2));
            }
        }




        public void FarbeP1(bool val)
        {
            ColorP1 = val ? "LawnGreen" : "White";
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


        public void FarbeP2(bool val)
        {
            ColorP2 = val ? "Red" : "White";
        }

        private string _colorP2;

        public string ColorP2
        {
            get => _colorP2;
            set
            {
                _colorP2 = value;
                OnPropertyChanged(nameof(ColorP2));
            }
        }

        public void FarbeQ1(bool val)
        {
            ColorQ1 = val ? "LawnGreen" : "White";
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

        #endregion

        #region ClickMode Button

        public bool ClickModeButtonS0()
        {
            if (ClickModeBtnS0 == "Press")
            {
                ClickModeBtnS0 = "Release";
                return true;
            }

            ClickModeBtnS0 = "Press";
            return false;
        }

        private string _clickModeBtnS0;

        public string ClickModeBtnS0
        {
            get => _clickModeBtnS0;
            set
            {
                _clickModeBtnS0 = value;
                OnPropertyChanged(nameof(ClickModeBtnS0));
            }
        }



        public bool ClickModeButtonS1()
        {
            if (ClickModeBtnS1 == "Press")
            {
                ClickModeBtnS1 = "Release";
                return true;
            }

            ClickModeBtnS1 = "Press";
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


        public bool ClickModeButtonS2()
        {
            if (ClickModeBtnS2 == "Press")
            {
                ClickModeBtnS2 = "Release";
                return true;
            }

            ClickModeBtnS2 = "Press";
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

        public bool ClickModeButtonS3()
        {
            if (ClickModeBtnS3 == "Press")
            {
                ClickModeBtnS3 = "Release";
                return true;
            }

            ClickModeBtnS3 = "Press";
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

        #endregion

        #region Wagen
        public void PositionWagenBeschriftung(Punkt pos)
        {
            PosWagenBeschriftungLeft = pos.X + 74;
            PosWagenBeschriftungTop = pos.Y + 106;
        }

        private double _posWagenBeschriftungLeft;

        public double PosWagenBeschriftungLeft
        {
            get => _posWagenBeschriftungLeft;
            set
            {
                _posWagenBeschriftungLeft = value;
                OnPropertyChanged(nameof(PosWagenBeschriftungLeft));
            }
        }

        private double _posWagenBeschriftungTop;

        public double PosWagenBeschriftungTop
        {
            get => _posWagenBeschriftungTop;
            set
            {
                _posWagenBeschriftungTop = value;
                OnPropertyChanged(nameof(PosWagenBeschriftungTop));
            }
        }



        public void PositionWagen(Punkt pos)
        {
            PosWagenLeft = pos.X;
            PosWagenTop = pos.Y;
        }

        private double _posWagenLeft;

        public double PosWagenLeft
        {
            get => _posWagenLeft;
            set
            {
                _posWagenLeft = value;
                OnPropertyChanged(nameof(PosWagenLeft));
            }
        }

        private double _posWagenTop;

        public double PosWagenTop
        {
            get => _posWagenTop;
            set
            {
                _posWagenTop = value;
                OnPropertyChanged(nameof(PosWagenTop));
            }
        }


        public void PositionWagenInhalt(Punkt pos, double fuellstand)
        {
            PosWagenInhaltLeft = pos.X + 12;
            PosWagenInhaltTop = pos.Y + 10 + 88 - fuellstand;
        }

        private double _posWagenInhaltLeft;

        public double PosWagenInhaltLeft
        {
            get => _posWagenInhaltLeft;
            set
            {
                _posWagenInhaltLeft = value;
                OnPropertyChanged(nameof(PosWagenInhaltLeft));
            }
        }

        private double _posWagenInhaltTop;

        public double PosWagenInhaltTop
        {
            get => _posWagenInhaltTop;
            set
            {
                _posWagenInhaltTop = value;
                OnPropertyChanged(nameof(PosWagenInhaltTop));
            }
        }



        private double _wagenFuellstand;

        public double WagenFuellstand
        {
            get => _wagenFuellstand;
            set
            {
                _wagenFuellstand = value;
                OnPropertyChanged(nameof(WagenFuellstand));
            }
        }


        #endregion



        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}