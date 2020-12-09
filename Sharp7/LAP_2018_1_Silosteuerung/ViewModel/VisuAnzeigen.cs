using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            SpsVersionsInfoSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            ColorF1 = Brushes.LawnGreen;
            ColorF2 = Brushes.LawnGreen;
            ColorP1 = Brushes.White;
            ColorP2 = Brushes.White;
            ColorQ1 = Brushes.LawnGreen;

            ClickModeBtnS0 = ClickMode.Press;
            ClickModeBtnS1 = ClickMode.Press;
            ClickModeBtnS2 = ClickMode.Press;
            ClickModeBtnS3 = ClickMode.Press;

            Margin1 = new Thickness(0, MaterialSiloHoehe * 0.1, 0, 0);

            VisibilityB1Ein = Visibility.Hidden;
            VisibilityB1Aus = Visibility.Visible;

            VisibilityB2Ein = Visibility.Visible;
            VisibilityB2Aus = Visibility.Hidden;

            VisibilityQ1Ein = Visibility.Visible;
            VisibilityQ1Aus = Visibility.Hidden;

            VisibilityXfuEin = Visibility.Visible;

            VisibilityY1Ein = Visibility.Hidden;
            VisibilityY1Aus = Visibility.Visible;

            VisibilityMaterialOben = Visibility.Hidden;
            VisibilityMaterialUnten = Visibility.Hidden;

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

        private Visibility _spsVersionsInfoSichtbar;
        public Visibility SpsVersionsInfoSichtbar
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

         private Brush _spsColor;

        public Brush SpsColor
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

        public void SichtbarkeitMaterialOben(bool val) => VisibilityMaterialOben = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityMaterialOben;

        public Visibility VisibilityMaterialOben
        {
            get => _visibilityMaterialOben;
            set
            {
                _visibilityMaterialOben = value;
                OnPropertyChanged(nameof(VisibilityMaterialOben));
            }
        }

        public void SichtbarkeitMaterialUnten(bool val) => VisibilityMaterialUnten = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityMaterialUnten;

        public Visibility VisibilityMaterialUnten
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
                VisibilityB1Ein = Visibility.Visible;
                VisibilityB1Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityB1Ein = Visibility.Hidden;
                VisibilityB1Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityB1Ein;

        public Visibility VisibilityB1Ein
        {
            get => _visibilityB1Ein;
            set
            {
                _visibilityB1Ein = value;
                OnPropertyChanged(nameof(VisibilityB1Ein));
            }
        }

        private Visibility _visibilityB1Aus;

        public Visibility VisibilityB1Aus
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
                VisibilityB2Ein = Visibility.Visible;
                VisibilityB2Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityB2Ein = Visibility.Hidden;
                VisibilityB2Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityB2Ein;

        public Visibility VisibilityB2Ein
        {
            get => _visibilityB2Ein;
            set
            {
                _visibilityB2Ein = value;
                OnPropertyChanged(nameof(VisibilityB2Ein));
            }
        }

        private Visibility _visibilityB2Aus;

        public Visibility VisibilityB2Aus
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
                VisibilityQ1Ein = Visibility.Visible;
                VisibilityQ1Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityQ1Ein = Visibility.Hidden;
                VisibilityQ1Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityQ1Ein;

        public Visibility VisibilityQ1Ein
        {
            get => _visibilityQ1Ein;
            set
            {
                _visibilityQ1Ein = value;
                OnPropertyChanged(nameof(VisibilityQ1Ein));
            }
        }

        private Visibility _visibilityQ1Aus;

        public Visibility VisibilityQ1Aus
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
                VisibilityXfuEin = Visibility.Visible;
                VisibilityXfuAus = Visibility.Hidden;
            }
            else
            {
                VisibilityXfuEin = Visibility.Hidden;
                VisibilityXfuAus = Visibility.Visible;
            }
        }

        private Visibility _visibilityXfuEin;

        public Visibility VisibilityXfuEin
        {
            get => _visibilityXfuEin;
            set
            {
                _visibilityXfuEin = value;
                OnPropertyChanged(nameof(VisibilityXfuEin));
            }
        }

        private Visibility _visibilityXfuAus;

        public Visibility VisibilityXfuAus
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
                VisibilityY1Ein = Visibility.Visible;
                VisibilityY1Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityY1Ein = Visibility.Hidden;
                VisibilityY1Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityY1Ein;

        public Visibility VisibilityY1Ein
        {
            get => _visibilityY1Ein;
            set
            {
                _visibilityY1Ein = value;
                OnPropertyChanged(nameof(VisibilityY1Ein));
            }
        }

        private Visibility _visibilityY1Aus;

        public Visibility VisibilityY1Aus
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
            ColorF1 = val ? Brushes.LawnGreen : Brushes.Red;
        }

        private Brush _colorF1;

        public Brush ColorF1
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
            ColorF2 = val ? Brushes.LawnGreen : Brushes.Red;
        }

        private Brush _colorF2;

        public Brush ColorF2
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
            ColorP1 = val ? Brushes.LawnGreen : Brushes.White;
        }

        private Brush _colorP1;

        public Brush ColorP1
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
            ColorP2 = val ? Brushes.Red : Brushes.White;
        }

        private Brush _colorP2;

        public Brush ColorP2
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
            ColorQ1 = val ? Brushes.LawnGreen : Brushes.White;
        }

        private Brush _colorQ1;

        public Brush ColorQ1
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
            if (ClickModeBtnS0 == ClickMode.Press)
            {
                ClickModeBtnS0 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS0 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS0;

        public ClickMode ClickModeBtnS0
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
            if (ClickModeBtnS1 == ClickMode.Press)
            {
                ClickModeBtnS1 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS1 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS1;

        public ClickMode ClickModeBtnS1
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
            if (ClickModeBtnS2 == ClickMode.Press)
            {
                ClickModeBtnS2 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS2 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS2;

        public ClickMode ClickModeBtnS2
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
            if (ClickModeBtnS3 == ClickMode.Press)
            {
                ClickModeBtnS3 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS3 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS3;

        public ClickMode ClickModeBtnS3
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