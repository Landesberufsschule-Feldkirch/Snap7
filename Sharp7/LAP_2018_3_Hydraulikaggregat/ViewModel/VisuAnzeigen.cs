using System.Windows.Controls;
using System.Windows.Media;

namespace LAP_2018_3_Hydraulikaggregat.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Hydraulikaggregat _hydraulikaggregat;
        private readonly MainWindow _mainWindow;

        private const double FuellBalkenHoehe = 390;
        private const double FuellBalkenOben = -40;

        public VisuAnzeigen(MainWindow mw, Model.Hydraulikaggregat ha)
        {
            _mainWindow = mw;
            _hydraulikaggregat = ha;

            Druck = 1.1;

            ClickModeBtnB4 = ClickMode.Press;
            ClickModeBtnB5 = ClickMode.Press;

            ClickModeBtnQ1 = ClickMode.Press;
            ClickModeBtnQ2 = ClickMode.Press;
            ClickModeBtnQ3 = ClickMode.Press;
            ClickModeBtnQ1Q3 = ClickMode.Press;

            ClickModeBtnS1 = ClickMode.Press;
            ClickModeBtnS2 = ClickMode.Press;
            ClickModeBtnS3 = ClickMode.Press;
            ClickModeBtnS4 = ClickMode.Press;

            ColorUeberdruck = Brushes.LawnGreen;

            ColorK1 = Brushes.LawnGreen;
            ColorK2 = Brushes.LawnGreen;

            ColorP1 = Brushes.LawnGreen;
            ColorP2 = Brushes.LawnGreen;
            ColorP3 = Brushes.LawnGreen;
            ColorP4 = Brushes.LawnGreen;
            ColorP5 = Brushes.LawnGreen;
            ColorP6 = Brushes.LawnGreen;

            ColorQ1 = Brushes.LawnGreen;
            ColorQ2 = Brushes.LawnGreen;
            ColorQ3 = Brushes.LawnGreen;
            ColorQ4 = Brushes.LawnGreen;

            Margin1 = new Thickness(42, 0, 32, 0);

            VisibilityB1Ein = Visibility.Hidden;
            VisibilityB1Aus = Visibility.Visible;

            VisibilityB2Ein = Visibility.Hidden;
            VisibilityB2Aus = Visibility.Visible;

            VisibilityB3Ein = Visibility.Hidden;
            VisibilityB3Aus = Visibility.Visible;

            VisibilityKurzschluss = Visibility.Hidden;


            OelkuehlerAbgedeckt = Visibility.Visible;
            ZylinderAbgedeckt = Visibility.Visible;
            OelfilterAbgedeckt = Visibility.Visible;

            SpsVersionsInfoSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                Druck = _hydraulikaggregat.Druck;

                FarbeUeberdruck(_hydraulikaggregat.B3);

                FarbeF1(_hydraulikaggregat.F1);

                FarbeK1(_hydraulikaggregat.K1);
                FarbeK2(_hydraulikaggregat.K2);

                FarbeP1(_hydraulikaggregat.P1);
                FarbeP2(_hydraulikaggregat.P2);
                FarbeP3(_hydraulikaggregat.P3);
                FarbeP4(_hydraulikaggregat.P4);
                FarbeP5(_hydraulikaggregat.P5);
                FarbeP6(_hydraulikaggregat.P6);

                FarbeQ1(_hydraulikaggregat.Q1);
                FarbeQ2(_hydraulikaggregat.Q2);
                FarbeQ3(_hydraulikaggregat.Q3);
                FarbeQ4(_hydraulikaggregat.Q4);

                Margin_1(_hydraulikaggregat.Pegel);

                SichtbarkeitB1(_hydraulikaggregat.B1);
                SichtbarkeitB2(_hydraulikaggregat.B2);
                SichtbarkeitB3(_hydraulikaggregat.B3);

                if (_hydraulikaggregat.Q2 && _hydraulikaggregat.Q3) VisibilityKurzschluss = Visibility.Visible; else VisibilityKurzschluss = Visibility.Hidden;

                if (_mainWindow.Plc != null)
                {
                    SpsVersionLokal = _mainWindow.VersionInfoLokal;
                    SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                    SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    SpsColor = _mainWindow.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }


        internal void BtnQ1Q3()
        {
            if (ClickModeButtonQ1Q3())
            {
                _hydraulikaggregat.Q1 = true;
                _hydraulikaggregat.Q3 = true;
            }
            else
            {
                _hydraulikaggregat.Q1 = false;
                _hydraulikaggregat.Q3 = false;
            }
        }

        internal void BtnS1() => _hydraulikaggregat.S1 = ClickModeButtonS1();
        internal void BtnS2() => _hydraulikaggregat.S2 = !ClickModeButtonS2();

        internal void BtnS3()
        {
            _hydraulikaggregat.Stopwatch.Restart();
            _hydraulikaggregat.S3 = ClickModeButtonS3();
        }

        internal void BtnS4() => _hydraulikaggregat.S4 = ClickModeButtonS4();
        internal void BtnB4() => _hydraulikaggregat.B4 = ClickModeButtonB4();
        internal void BtnB5() => _hydraulikaggregat.B5 = ClickModeButtonB5();
        internal void BtnQ1() => _hydraulikaggregat.Q1 = ClickModeButtonQ1();
        internal void BtnQ2() => _hydraulikaggregat.Q2 = ClickModeButtonQ2();
        internal void BtnQ3() => _hydraulikaggregat.Q3 = ClickModeButtonQ3();





        #region SPS Version, Status und Farbe

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

        #region ClickMode


        public bool ClickModeButtonB4()
        {
            if (ClickModeBtnB4 == ClickMode.Press)
            {
                ClickModeBtnB4 = ClickMode.Release;
                return true;
            }

            ClickModeBtnB4 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnB4;

        public ClickMode ClickModeBtnB4
        {
            get => _clickModeBtnB4;
            set
            {
                _clickModeBtnB4 = value;
                OnPropertyChanged(nameof(ClickModeBtnB4));
            }
        }

        public bool ClickModeButtonB5()
        {
            if (ClickModeBtnB5 == ClickMode.Press)
            {
                ClickModeBtnB5 = ClickMode.Release;
                return true;
            }

            ClickModeBtnB5 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnB5;

        public ClickMode ClickModeBtnB5
        {
            get => _clickModeBtnB5;
            set
            {
                _clickModeBtnB5 = value;
                OnPropertyChanged(nameof(ClickModeBtnB5));
            }
        }

        public bool ClickModeButtonQ1()
        {
            if (ClickModeBtnQ1 == ClickMode.Press)
            {
                ClickModeBtnQ1 = ClickMode.Release;
                return true;
            }

            ClickModeBtnQ1 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnQ1;

        public ClickMode ClickModeBtnQ1
        {
            get => _clickModeBtnQ1;
            set
            {
                _clickModeBtnQ1 = value;
                OnPropertyChanged(nameof(ClickModeBtnQ1));
            }
        }


        public bool ClickModeButtonQ2()
        {
            if (ClickModeBtnQ2 == ClickMode.Press)
            {
                ClickModeBtnQ2 = ClickMode.Release;
                return true;
            }

            ClickModeBtnQ2 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnQ2;

        public ClickMode ClickModeBtnQ2
        {
            get => _clickModeBtnQ2;
            set
            {
                _clickModeBtnQ2 = value;
                OnPropertyChanged(nameof(ClickModeBtnQ2));
            }
        }

        public bool ClickModeButtonQ3()
        {
            if (ClickModeBtnQ3 == ClickMode.Press)
            {
                ClickModeBtnQ3 = ClickMode.Release;
                return true;
            }

            ClickModeBtnQ3 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnQ3;

        public ClickMode ClickModeBtnQ3
        {
            get => _clickModeBtnQ3;
            set
            {
                _clickModeBtnQ3 = value;
                OnPropertyChanged(nameof(ClickModeBtnQ3));
            }
        }


        public bool ClickModeButtonQ1Q3()
        {
            if (ClickModeBtnQ1Q3 == ClickMode.Press)
            {
                ClickModeBtnQ1Q3 = ClickMode.Release;
                return true;
            }

            ClickModeBtnQ1Q3 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnQ1Q3;

        public ClickMode ClickModeBtnQ1Q3
        {
            get => _clickModeBtnQ1Q3;
            set
            {
                _clickModeBtnQ1Q3 = value;
                OnPropertyChanged(nameof(ClickModeBtnQ1Q3));
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

        public bool ClickModeButtonS4()
        {
            if (ClickModeBtnS4 == ClickMode.Press)
            {
                ClickModeBtnS4 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS4 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS4;

        public ClickMode ClickModeBtnS4
        {
            get => _clickModeBtnS4;
            set
            {
                _clickModeBtnS4 = value;
                OnPropertyChanged(nameof(ClickModeBtnS4));
            }
        }

        #endregion ClickMode

        #region Color

        public void FarbeF1(bool val) => ColorF1 = val ? Brushes.LawnGreen : Brushes.Red;

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





        public void FarbeUeberdruck(bool val) => ColorUeberdruck = val ? Brushes.White : Brushes.Red;

        private Brush _colorUeberdruck;
        public Brush ColorUeberdruck
        {
            get => _colorUeberdruck;
            set
            {
                _colorUeberdruck = value;
                OnPropertyChanged(nameof(ColorUeberdruck));
            }
        }


        public void FarbeK1(bool val) => ColorK1 = val ? Brushes.Red : Brushes.White;

        private Brush _colorK1;
        public Brush ColorK1
        {
            get => _colorK1;
            set
            {
                _colorK1 = value;
                OnPropertyChanged(nameof(ColorK1));
            }
        }

        public void FarbeK2(bool val) => ColorK2 = val ? Brushes.Red : Brushes.White;

        private Brush _colorK2;

        public Brush ColorK2
        {
            get => _colorK2;
            set
            {
                _colorK2 = value;
                OnPropertyChanged(nameof(ColorK2));
            }
        }

        public void FarbeP1(bool val) => ColorP1 = val ? Brushes.Red : Brushes.White;

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


        public void FarbeP2(bool val) => ColorP2 = val ? Brushes.Red : Brushes.White;

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


        public void FarbeP3(bool val) => ColorP3 = val ? Brushes.LawnGreen : Brushes.White;

        private Brush _colorP3;

        public Brush ColorP3
        {
            get => _colorP3;
            set
            {
                _colorP3 = value;
                OnPropertyChanged(nameof(ColorP3));
            }
        }


        public void FarbeP4(bool val) => ColorP4 = val ? Brushes.Red : Brushes.White;

        private Brush _colorP4;

        public Brush ColorP4
        {
            get => _colorP4;
            set
            {
                _colorP4 = value;
                OnPropertyChanged(nameof(ColorP4));
            }
        }


        public void FarbeP5(bool val) => ColorP5 = val ? Brushes.Red : Brushes.White;

        private Brush _colorP5;

        public Brush ColorP5
        {
            get => _colorP5;
            set
            {
                _colorP5 = value;
                OnPropertyChanged(nameof(ColorP5));
            }
        }


        public void FarbeP6(bool val) => ColorP6 = val ? Brushes.Red : Brushes.White;

        private Brush _colorP6;

        public Brush ColorP6
        {
            get => _colorP6;
            set
            {
                _colorP6 = value;
                OnPropertyChanged(nameof(ColorP6));
            }
        }
        public void FarbeQ1(bool val) => ColorQ1 = val ? Brushes.LawnGreen : Brushes.White;

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


        public void FarbeQ2(bool val) => ColorQ2 = val ? Brushes.LawnGreen : Brushes.White;

        private Brush _colorQ2;

        public Brush ColorQ2
        {
            get => _colorQ2;
            set
            {
                _colorQ2 = value;
                OnPropertyChanged(nameof(ColorQ2));
            }
        }


        public void FarbeQ3(bool val) => ColorQ3 = val ? Brushes.LawnGreen : Brushes.White;

        private Brush _colorQ3;

        public Brush ColorQ3
        {
            get => _colorQ3;
            set
            {
                _colorQ3 = value;
                OnPropertyChanged(nameof(ColorQ3));
            }
        }

        public void FarbeQ4(bool val) => ColorQ4 = val ? Brushes.LawnGreen : Brushes.White;

        private Brush _colorQ4;

        public Brush ColorQ4
        {
            get => _colorQ4;
            set
            {
                _colorQ4 = value;
                OnPropertyChanged(nameof(ColorQ4));
            }
        }

        #endregion Color

        #region Druck

        private double _druck;

        public double Druck
        {
            get => _druck;
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
            Margin1 = new Thickness(41, FuellBalkenOben + FuellBalkenHoehe * (1 - pegel), 31, 0);
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

        #endregion Margin1

        #region Sichtbarkeiten

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

        public void SichtbarkeitB3(bool val)
        {
            if (val)
            {
                VisibilityB3Ein = Visibility.Visible;
                VisibilityB3Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityB3Ein = Visibility.Hidden;
                VisibilityB3Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityB3Ein;

        public Visibility VisibilityB3Ein
        {
            get => _visibilityB3Ein;
            set
            {
                _visibilityB3Ein = value;
                OnPropertyChanged(nameof(VisibilityB3Ein));
            }
        }

        private Visibility _visibilityB3Aus;

        public Visibility VisibilityB3Aus
        {
            get => _visibilityB3Aus;
            set
            {
                _visibilityB3Aus = value;
                OnPropertyChanged(nameof(VisibilityB3Aus));
            }
        }


        private Visibility _visibilityKurzschluss;

        public Visibility VisibilityKurzschluss
        {
            get => _visibilityKurzschluss;
            set
            {
                _visibilityKurzschluss = value;
                OnPropertyChanged(nameof(VisibilityKurzschluss));
            }
        }

        private Visibility _oelkuehlerAbgedeckt;
        public Visibility OelkuehlerAbgedeckt
        {
            get => _oelkuehlerAbgedeckt;
            set
            {
                _oelkuehlerAbgedeckt = value;
                OnPropertyChanged(nameof(OelkuehlerAbgedeckt));
            }
        }

        private Visibility _zylinderAbgedeckt;
        public Visibility ZylinderAbgedeckt
        {
            get => _zylinderAbgedeckt;
            set
            {
                _zylinderAbgedeckt = value;
                OnPropertyChanged(nameof(ZylinderAbgedeckt));
            }
        }

        private Visibility _oelfilterAbgedeckt;
        public Visibility OelfilterAbgedeckt
        {
            get => _oelfilterAbgedeckt;
            set
            {
                _oelfilterAbgedeckt = value;
                OnPropertyChanged(nameof(OelfilterAbgedeckt));
            }
        }

        #endregion Sichtbarkeit

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}