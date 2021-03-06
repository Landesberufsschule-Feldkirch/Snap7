﻿using System.Windows.Controls;
using System.Windows.Media;
using LAP_2018_2_Abfuellanlage.Model;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using Utilities;

namespace LAP_2018_2_Abfuellanlage.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Abfuellanlage _alleFlaschen;
        private readonly MainWindow _mainWindow;
        private const double HoeheFuellBalken = 9 * 35;

        public VisuAnzeigen(MainWindow mw, Abfuellanlage af)
        {
            _mainWindow = mw;
            _alleFlaschen = af;

            SpsVersionsInfoSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            ClickModeBtnS1 = ClickMode.Press;
            ClickModeBtnS2 = ClickMode.Press;
            ClickModeBtnS3 = ClickMode.Press;
            ClickModeBtnS4 = ClickMode.Press;
            ClickModeBtnK1 = ClickMode.Press;
            ClickModeBtnK2 = ClickMode.Press;
            ClickModeBtnQ1 = ClickMode.Press;

            ImageTop1 = 10;
            ImageTop2 = 20;
            ImageTop3 = 30;
            ImageTop4 = 40;
            ImageTop5 = 50;
            ImageTop6 = 60;

            ImageLeft1 = 10;
            ImageLeft2 = 20;
            ImageLeft3 = 30;
            ImageLeft4 = 10;
            ImageLeft5 = 50;
            ImageLeft6 = 60;

            VisibilityImage1 = Visibility.Visible;
            VisibilityImage2 = Visibility.Visible;
            VisibilityImage3 = Visibility.Visible;
            VisibilityImage4 = Visibility.Visible;
            VisibilityImage5 = Visibility.Visible;
            VisibilityImage6 = Visibility.Visible;

            VisibilityB1Ein = Visibility.Hidden;
            VisibilityK1Ein = Visibility.Hidden;
            VisibilityK2Ein = Visibility.Hidden;

            VisibilityB1Aus = Visibility.Visible;
            VisibilityK1Aus = Visibility.Visible;
            VisibilityK2Aus = Visibility.Visible;

            VisibilityRectangleAbleitung = Visibility.Hidden;

            VisibilityFohrenburger0 = Visibility.Visible;
            VisibilityFohrenburger1 = Visibility.Hidden;
            VisibilityFohrenburger2 = Visibility.Hidden;
            VisibilityFohrenburger3 = Visibility.Hidden;
            VisibilityFohrenburger4 = Visibility.Hidden;
            VisibilityFohrenburger5 = Visibility.Hidden;
            VisibilityFohrenburger6 = Visibility.Hidden;

            VisibilityMohren0 = Visibility.Hidden;
            VisibilityMohren1 = Visibility.Hidden;
            VisibilityMohren2 = Visibility.Hidden;
            VisibilityMohren3 = Visibility.Hidden;
            VisibilityMohren4 = Visibility.Hidden;
            VisibilityMohren5 = Visibility.Hidden;
            VisibilityMohren6 = Visibility.Hidden;

            ColorCircleF1 = Brushes.LawnGreen;
            ColorCircleQ1 = Brushes.LightGray;
            ColorCircleP1 = Brushes.LightGray;
            ColorCircleP2 = Brushes.LightGray;

            ColorRectangleZuleitung = Brushes.LightBlue;

            Margin1 = new Thickness(0, 30, 0, 0);

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                PositionImage_1(_alleFlaschen.AlleFlaschen[0].Flasche.GetPosition());
                PositionImage_2(_alleFlaschen.AlleFlaschen[1].Flasche.GetPosition());
                PositionImage_3(_alleFlaschen.AlleFlaschen[2].Flasche.GetPosition());
                PositionImage_4(_alleFlaschen.AlleFlaschen[3].Flasche.GetPosition());
                PositionImage_5(_alleFlaschen.AlleFlaschen[4].Flasche.GetPosition());
                PositionImage_6(_alleFlaschen.AlleFlaschen[5].Flasche.GetPosition());

                VisibilityFlasche1(_alleFlaschen.AlleFlaschen[0].Sichtbar);
                VisibilityFlasche2(_alleFlaschen.AlleFlaschen[1].Sichtbar);
                VisibilityFlasche3(_alleFlaschen.AlleFlaschen[2].Sichtbar);
                VisibilityFlasche4(_alleFlaschen.AlleFlaschen[3].Sichtbar);
                VisibilityFlasche5(_alleFlaschen.AlleFlaschen[4].Sichtbar);
                VisibilityFlasche6(_alleFlaschen.AlleFlaschen[5].Sichtbar);

                VisibilitySensorB1(_alleFlaschen.B1);
                VisibilityVentilK1(_alleFlaschen.K1);
                VisibilityVentilK2(_alleFlaschen.K2);
                VisibilityAbleitung(_alleFlaschen.K1 && _alleFlaschen.Pegel > 0.01);

                KisteAnzeigen(_alleFlaschen.FlaschenInDerKiste);

                FarbeCircle_F1(_alleFlaschen.F1);
                FarbeCircle_Q1(_alleFlaschen.Q1);
                FarbeCircle_P1(_alleFlaschen.P1);
                FarbeCircle_P2(_alleFlaschen.P2);

                FarbeRectangleZuleitung(_alleFlaschen.Pegel > 0.01);

                Margin_1(_alleFlaschen.Pegel);

                FuellstandProzent = (100 * _alleFlaschen.Pegel).ToString("F0") + "%";

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

        private void KisteAnzeigen(int anzahlFlaschen)
        {

            var alleFohrenburgerKisten = new Visibility[10];
            var alleMohrenKisten = new Visibility[10];

            for (var i = 0; i < 10; i++)
            {
                alleFohrenburgerKisten[i] = Visibility.Hidden;
                alleMohrenKisten[i] = Visibility.Hidden;
            }


            if (_alleFlaschen.AktuellesBier == Abfuellanlage.Bier.Fohrenburger)
            {
                alleFohrenburgerKisten[anzahlFlaschen] = Visibility.Visible;
            }
            else
            {
                alleMohrenKisten[anzahlFlaschen] = Visibility.Visible;
            }


            VisibilityFohrenburger0 = alleFohrenburgerKisten[0];
            VisibilityFohrenburger1 = alleFohrenburgerKisten[1];
            VisibilityFohrenburger2 = alleFohrenburgerKisten[2];
            VisibilityFohrenburger3 = alleFohrenburgerKisten[3];
            VisibilityFohrenburger4 = alleFohrenburgerKisten[4];
            VisibilityFohrenburger5 = alleFohrenburgerKisten[5];
            VisibilityFohrenburger6 = alleFohrenburgerKisten[6];

            VisibilityMohren0 = alleMohrenKisten[0];
            VisibilityMohren1 = alleMohrenKisten[1];
            VisibilityMohren2 = alleMohrenKisten[2];
            VisibilityMohren3 = alleMohrenKisten[3];
            VisibilityMohren4 = alleMohrenKisten[4];
            VisibilityMohren5 = alleMohrenKisten[5];
            VisibilityMohren6 = alleMohrenKisten[6];
        }

        internal void TasterS1() => _alleFlaschen.S1 = ClickModeButtonS1();

        internal void TasterS2() => _alleFlaschen.S2 = !ClickModeButtonS2();

        internal void TasterS3() => _alleFlaschen.S3 = ClickModeButtonS3();

        internal void TasterS4() => _alleFlaschen.S4 = ClickModeButtonS4();

        internal void SetManualK1() => _alleFlaschen.K1 = ClickModeButtonK1();

        internal void SetManualK2() => _alleFlaschen.K2 = ClickModeButtonK2();

        internal void SetManualQ1() => _alleFlaschen.Q1 = ClickModeButtonQ1();

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


        private string _fuellstandProzent;
        public string FuellstandProzent
        {
            get => _fuellstandProzent;
            set
            {
                _fuellstandProzent = value;
                OnPropertyChanged(nameof(FuellstandProzent));
            }
        }

        #region ClickModeBtnS1

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

        #endregion ClickModeBtnS1

        #region ClickModeBtnS2

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

        #endregion ClickModeBtnS2

        #region ClickModeBtnS3

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

        #endregion ClickModeBtnS3

        #region ClickModeBtnS4

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

        #endregion ClickModeBtnS4

        #region ClickModeBtnK1

        public bool ClickModeButtonK1()
        {
            if (ClickModeBtnK1 == ClickMode.Press)
            {
                ClickModeBtnK1 = ClickMode.Release;
                return true;
            }

            ClickModeBtnK1 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnK1;

        public ClickMode ClickModeBtnK1
        {
            get => _clickModeBtnK1;
            set
            {
                _clickModeBtnK1 = value;
                OnPropertyChanged(nameof(ClickModeBtnK1));
            }
        }

        #endregion ClickModeBtnK1

        #region ClickModeBtnK2

        public bool ClickModeButtonK2()
        {
            if (ClickModeBtnK2 == ClickMode.Press)
            {
                ClickModeBtnK2 = ClickMode.Release;
                return true;
            }

            ClickModeBtnK2 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnK2;

        public ClickMode ClickModeBtnK2
        {
            get => _clickModeBtnK2;
            set
            {
                _clickModeBtnK2 = value;
                OnPropertyChanged(nameof(ClickModeBtnK2));
            }
        }

        #endregion ClickModeBtnK2

        #region ClickModeBtnQ1

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

        #endregion ClickModeBtnQ1

        #region Image1

        public void PositionImage_1(Punkt pos)
        {
            ImageLeft1 = pos.X;
            ImageTop1 = pos.Y;
        }

        private double _imageTop1;

        public double ImageTop1
        {
            get => _imageTop1;
            set
            {
                _imageTop1 = value;
                OnPropertyChanged(nameof(ImageTop1));
            }
        }

        private double _imageLeft1;

        public double ImageLeft1
        {
            get => _imageLeft1;
            set
            {
                _imageLeft1 = value;
                OnPropertyChanged(nameof(ImageLeft1));
            }
        }

        #endregion Image1

        #region Image2

        public void PositionImage_2(Punkt pos)
        {
            ImageLeft2 = pos.X;
            ImageTop2 = pos.Y;
        }

        private double _imageTop2;

        public double ImageTop2
        {
            get => _imageTop2;
            set
            {
                _imageTop2 = value;
                OnPropertyChanged(nameof(ImageTop2));
            }
        }

        private double _imageLeft2;

        public double ImageLeft2
        {
            get => _imageLeft2;
            set
            {
                _imageLeft2 = value;
                OnPropertyChanged(nameof(ImageLeft2));
            }
        }

        #endregion Image2

        #region Image3

        public void PositionImage_3(Punkt pos)
        {
            ImageLeft3 = pos.X;
            ImageTop3 = pos.Y;
        }

        private double _imageTop3;

        public double ImageTop3
        {
            get => _imageTop3;
            set
            {
                _imageTop3 = value;
                OnPropertyChanged(nameof(ImageTop3));
            }
        }

        private double _imageLeft3;

        public double ImageLeft3
        {
            get => _imageLeft3;
            set
            {
                _imageLeft3 = value;
                OnPropertyChanged(nameof(ImageLeft3));
            }
        }

        #endregion Image3

        #region Image4

        public void PositionImage_4(Punkt pos)
        {
            ImageLeft4 = pos.X;
            ImageTop4 = pos.Y;
        }

        private double _imageTop4;

        public double ImageTop4
        {
            get => _imageTop4;
            set
            {
                _imageTop4 = value;
                OnPropertyChanged(nameof(ImageTop4));
            }
        }

        private double _imageLeft4;

        public double ImageLeft4
        {
            get => _imageLeft4;
            set
            {
                _imageLeft4 = value;
                OnPropertyChanged(nameof(ImageLeft4));
            }
        }

        #endregion Image4

        #region Image5

        public void PositionImage_5(Punkt pos)
        {
            ImageLeft5 = pos.X;
            ImageTop5 = pos.Y;
        }

        private double _imageTop5;

        public double ImageTop5
        {
            get => _imageTop5;
            set
            {
                _imageTop5 = value;
                OnPropertyChanged(nameof(ImageTop5));
            }
        }

        private double _imageLeft5;

        public double ImageLeft5
        {
            get => _imageLeft5;
            set
            {
                _imageLeft5 = value;
                OnPropertyChanged(nameof(ImageLeft5));
            }
        }

        #endregion Image5

        #region Image6

        public void PositionImage_6(Punkt pos)
        {
            ImageLeft6 = pos.X;
            ImageTop6 = pos.Y;
        }

        private double _imageTop6;

        public double ImageTop6
        {
            get => _imageTop6;
            set
            {
                _imageTop6 = value;
                OnPropertyChanged(nameof(ImageTop6));
            }
        }

        private double _imageLeft6;

        public double ImageLeft6
        {
            get => _imageLeft6;
            set
            {
                _imageLeft6 = value;
                OnPropertyChanged(nameof(ImageLeft6));
            }
        }

        #endregion Image6

        #region Visibility 

        public void VisibilityFlasche1(bool val) => VisibilityImage1 = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityImage1;

        public Visibility VisibilityImage1
        {
            get => _visibilityImage1;
            set
            {
                _visibilityImage1 = value;
                OnPropertyChanged(nameof(VisibilityImage1));
            }
        }


        public void VisibilityFlasche2(bool val) => VisibilityImage2 = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityImage2;

        public Visibility VisibilityImage2
        {
            get => _visibilityImage2;
            set
            {
                _visibilityImage2 = value;
                OnPropertyChanged(nameof(VisibilityImage2));
            }
        }


        public void VisibilityFlasche3(bool val) => VisibilityImage3 = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityImage3;

        public Visibility VisibilityImage3
        {
            get => _visibilityImage3;
            set
            {
                _visibilityImage3 = value;
                OnPropertyChanged(nameof(VisibilityImage3));
            }
        }

        public void VisibilityFlasche4(bool val) => VisibilityImage4 = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityImage4;

        public Visibility VisibilityImage4
        {
            get => _visibilityImage4;
            set
            {
                _visibilityImage4 = value;
                OnPropertyChanged(nameof(VisibilityImage4));
            }
        }

        public void VisibilityFlasche5(bool val) => VisibilityImage5 = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityImage5;

        public Visibility VisibilityImage5
        {
            get => _visibilityImage5;
            set
            {
                _visibilityImage5 = value;
                OnPropertyChanged(nameof(VisibilityImage5));
            }
        }


        public void VisibilityFlasche6(bool val) => VisibilityImage6 = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityImage6;

        public Visibility VisibilityImage6
        {
            get => _visibilityImage6;
            set
            {
                _visibilityImage6 = value;
                OnPropertyChanged(nameof(VisibilityImage6));
            }
        }

        public void VisibilitySensorB1(bool val)
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


        public void VisibilityVentilK1(bool val)
        {
            if (val)
            {
                VisibilityK1Ein = Visibility.Visible;
                VisibilityK1Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityK1Ein = Visibility.Hidden;
                VisibilityK1Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityK1Ein;

        public Visibility VisibilityK1Ein
        {
            get => _visibilityK1Ein;
            set
            {
                _visibilityK1Ein = value;
                OnPropertyChanged(nameof(VisibilityK1Ein));
            }
        }

        private Visibility _visibilityK1Aus;

        public Visibility VisibilityK1Aus
        {
            get => _visibilityK1Aus;
            set
            {
                _visibilityK1Aus = value;
                OnPropertyChanged(nameof(VisibilityK1Aus));
            }
        }

        public void VisibilityVentilK2(bool val)
        {
            if (val)
            {
                VisibilityK2Ein = Visibility.Visible;
                VisibilityK2Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityK2Ein = Visibility.Hidden;
                VisibilityK2Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityK2Ein;

        public Visibility VisibilityK2Ein
        {
            get => _visibilityK2Ein;
            set
            {
                _visibilityK2Ein = value;
                OnPropertyChanged(nameof(VisibilityK2Ein));
            }
        }

        private Visibility _visibilityK2Aus;

        public Visibility VisibilityK2Aus
        {
            get => _visibilityK2Aus;
            set
            {
                _visibilityK2Aus = value;
                OnPropertyChanged(nameof(VisibilityK2Aus));
            }
        }

        #endregion Visibility Ventil K2

        #region Visibility Ableitung

        public void VisibilityAbleitung(bool val) => VisibilityRectangleAbleitung = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityRectangleAbleitung;

        public Visibility VisibilityRectangleAbleitung
        {
            get => _visibilityRectangleAbleitung;
            set
            {
                _visibilityRectangleAbleitung = value;
                OnPropertyChanged(nameof(VisibilityRectangleAbleitung));
            }
        }




        private Visibility _visibilityFohrenburger0;
        public Visibility VisibilityFohrenburger0
        {
            get => _visibilityFohrenburger0;
            set
            {
                _visibilityFohrenburger0 = value;
                OnPropertyChanged(nameof(VisibilityFohrenburger0));
            }
        }


        private Visibility _visibilityFohrenburger1;
        public Visibility VisibilityFohrenburger1
        {
            get => _visibilityFohrenburger1;
            set
            {
                _visibilityFohrenburger1 = value;
                OnPropertyChanged(nameof(VisibilityFohrenburger1));
            }
        }

        private Visibility _visibilityFohrenburger2;
        public Visibility VisibilityFohrenburger2
        {
            get => _visibilityFohrenburger2;
            set
            {
                _visibilityFohrenburger2 = value;
                OnPropertyChanged(nameof(VisibilityFohrenburger2));
            }
        }

        private Visibility _visibilityFohrenburger3;
        public Visibility VisibilityFohrenburger3
        {
            get => _visibilityFohrenburger3;
            set
            {
                _visibilityFohrenburger3 = value;
                OnPropertyChanged(nameof(VisibilityFohrenburger3));
            }
        }

        private Visibility _visibilityFohrenburger4;
        public Visibility VisibilityFohrenburger4
        {
            get => _visibilityFohrenburger4;
            set
            {
                _visibilityFohrenburger4 = value;
                OnPropertyChanged(nameof(VisibilityFohrenburger4));
            }
        }

        private Visibility _visibilityFohrenburger5;
        public Visibility VisibilityFohrenburger5
        {
            get => _visibilityFohrenburger5;
            set
            {
                _visibilityFohrenburger5 = value;
                OnPropertyChanged(nameof(VisibilityFohrenburger5));
            }
        }

        private Visibility _visibilityFohrenburger6;
        public Visibility VisibilityFohrenburger6
        {
            get => _visibilityFohrenburger6;
            set
            {
                _visibilityFohrenburger6 = value;
                OnPropertyChanged(nameof(VisibilityFohrenburger6));
            }
        }



        private Visibility _visibilityMohren0;
        public Visibility VisibilityMohren0
        {
            get => _visibilityMohren0;
            set
            {
                _visibilityMohren0 = value;
                OnPropertyChanged(nameof(VisibilityMohren0));
            }
        }

        private Visibility _visibilityMohren1;
        public Visibility VisibilityMohren1
        {
            get => _visibilityMohren1;
            set
            {
                _visibilityMohren1 = value;
                OnPropertyChanged(nameof(VisibilityMohren1));
            }
        }

        private Visibility _visibilityMohren2;
        public Visibility VisibilityMohren2
        {
            get => _visibilityMohren2;
            set
            {
                _visibilityMohren2 = value;
                OnPropertyChanged(nameof(VisibilityMohren2));
            }
        }

        private Visibility _visibilityMohren3;
        public Visibility VisibilityMohren3
        {
            get => _visibilityMohren3;
            set
            {
                _visibilityMohren3 = value;
                OnPropertyChanged(nameof(VisibilityMohren3));
            }
        }

        private Visibility _visibilityMohren4;
        public Visibility VisibilityMohren4
        {
            get => _visibilityMohren4;
            set
            {
                _visibilityMohren4 = value;
                OnPropertyChanged(nameof(VisibilityMohren4));
            }
        }

        private Visibility _visibilityMohren5;
        public Visibility VisibilityMohren5
        {
            get => _visibilityMohren5;
            set
            {
                _visibilityMohren5 = value;
                OnPropertyChanged(nameof(VisibilityMohren5));
            }
        }

        private Visibility _visibilityMohren6;
        public Visibility VisibilityMohren6
        {
            get => _visibilityMohren6;
            set
            {
                _visibilityMohren6 = value;
                OnPropertyChanged(nameof(VisibilityMohren6));
            }
        }


        #endregion Visibility 

        #region Color F1

        public void FarbeCircle_F1(bool val) => ColorCircleF1 = val ? Brushes.LawnGreen : Brushes.Red;

        private Brush _colorCircleF1;

        public Brush ColorCircleF1
        {
            get => _colorCircleF1;
            set
            {
                _colorCircleF1 = value;
                OnPropertyChanged(nameof(ColorCircleF1));
            }
        }

        #endregion Color F1

        #region Color Q1

        public void FarbeCircle_Q1(bool val) => ColorCircleQ1 = val ? Brushes.LawnGreen : Brushes.LightGray;

        private Brush _colorCircleQ1;

        public Brush ColorCircleQ1
        {
            get => _colorCircleQ1;
            set
            {
                _colorCircleQ1 = value;
                OnPropertyChanged(nameof(ColorCircleQ1));
            }
        }

        #endregion Color Q1

        #region Color P1

        public void FarbeCircle_P1(bool val) => ColorCircleP1 = val ? Brushes.LawnGreen : Brushes.LightGray;

        private Brush _colorCircleP1;

        public Brush ColorCircleP1
        {
            get => _colorCircleP1;
            set
            {
                _colorCircleP1 = value;
                OnPropertyChanged(nameof(ColorCircleP1));
            }
        }

        #endregion Color P1

        #region Color P2

        public void FarbeCircle_P2(bool val) => ColorCircleP2 = val ? Brushes.Red : Brushes.LightGray;

        private Brush _colorCircleP2;

        public Brush ColorCircleP2
        {
            get => _colorCircleP2;
            set
            {
                _colorCircleP2 = value;
                OnPropertyChanged(nameof(ColorCircleP2));
            }
        }

        #endregion Color P2

        #region Color Zuleitung

        public void FarbeRectangleZuleitung(bool val) => ColorRectangleZuleitung = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorRectangleZuleitung;

        public Brush ColorRectangleZuleitung
        {
            get => _colorRectangleZuleitung;
            set
            {
                _colorRectangleZuleitung = value;
                OnPropertyChanged(nameof(ColorRectangleZuleitung));
            }
        }

        #endregion Color Zuleitung

        #region Margin1

        public void Margin_1(double pegel)
        {
            Margin1 = new Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);
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

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}