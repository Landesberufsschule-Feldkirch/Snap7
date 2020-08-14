namespace LAP_2018_2_Abfuellanlage.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using Utilities;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Abfuellanlage _alleFlaschen;
        private readonly MainWindow _mainWindow;
        private readonly double _hoeheFuellBalken = 9 * 35;

        public VisuAnzeigen(MainWindow mw, Model.Abfuellanlage af)
        {
            _mainWindow = mw;
            _alleFlaschen = af;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            ClickModeBtnS1 = "Press";
            ClickModeBtnS2 = "Press";
            ClickModeBtnS3 = "Press";
            ClickModeBtnS4 = "Press";
            ClickModeBtnK1 = "Press";
            ClickModeBtnK2 = "Press";
            ClickModeBtnQ1 = "Press";

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

            VisibilityImage1 = "visible";
            VisibilityImage2 = "visible";
            VisibilityImage3 = "visible";
            VisibilityImage4 = "visible";
            VisibilityImage5 = "visible";
            VisibilityImage6 = "visible";

            VisibilityB1Ein = "hidden";
            VisibilityK1Ein = "hidden";
            VisibilityK2Ein = "hidden";

            VisibilityB1Aus = "visible";
            VisibilityK1Aus = "visible";
            VisibilityK2Aus = "visible";

            VisibilityRectangleAbleitung = "hidden";

            ColorCircleF1 = "LawnGreen";
            ColorCircleQ1 = "LightGray";
            ColorCircleP1 = "LightGray";
            ColorCircleP2 = "LightGray";

            ColorRectangleZuleitung = "LightBlue";

            Margin1 = new Thickness(0, 30, 0, 0);

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                PositionImage_1(_alleFlaschen.AlleFlaschen[0].Position.Punkt);
                PositionImage_2(_alleFlaschen.AlleFlaschen[1].Position.Punkt);
                PositionImage_3(_alleFlaschen.AlleFlaschen[2].Position.Punkt);
                PositionImage_4(_alleFlaschen.AlleFlaschen[3].Position.Punkt);
                PositionImage_5(_alleFlaschen.AlleFlaschen[4].Position.Punkt);
                PositionImage_6(_alleFlaschen.AlleFlaschen[5].Position.Punkt);

                VisibilityFlasche1(_alleFlaschen.AlleFlaschen[0].Sichtbar);
                VisibilityFlasche2(_alleFlaschen.AlleFlaschen[1].Sichtbar);
                VisibilityFlasche3(_alleFlaschen.AlleFlaschen[2].Sichtbar);
                VisibilityFlasche4(_alleFlaschen.AlleFlaschen[3].Sichtbar);
                VisibilityFlasche5(_alleFlaschen.AlleFlaschen[4].Sichtbar);
                VisibilityFlasche6(_alleFlaschen.AlleFlaschen[5].Sichtbar);

                VisibilitySensorB1(_alleFlaschen.B1);
                VisibilityVentilK1(_alleFlaschen.K1);
                VisibilityVentilK2(_alleFlaschen.K2);
                VisibilityAbleitung(_alleFlaschen.K1 && (_alleFlaschen.Pegel > 0.01));

                FarbeCircle_F1(_alleFlaschen.F1);
                FarbeCircle_Q1(_alleFlaschen.Q1);
                FarbeCircle_P1(_alleFlaschen.P1);
                FarbeCircle_P2(_alleFlaschen.P2);

                FarbeRectangleZuleitung(_alleFlaschen.Pegel > 0.01);

                Margin_1(_alleFlaschen.Pegel);

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

        internal void TasterS1() => _alleFlaschen.S1 = ClickModeButtonS1();

        internal void TasterS2() => _alleFlaschen.S2 = !ClickModeButtonS2();

        internal void TasterS3() => _alleFlaschen.S3 = ClickModeButtonS3();

        internal void TasterS4() => _alleFlaschen.S4 = ClickModeButtonS4();

        internal void SetManualK1() => _alleFlaschen.K1 = ClickModeButtonK1();

        internal void SetManualK2() => _alleFlaschen.K2 = ClickModeButtonK2();

        internal void SetManualQ1() => _alleFlaschen.Q1 = ClickModeButtonQ1();

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

        #region ClickModeBtnS4

        public bool ClickModeButtonS4()
        {
            if (ClickModeBtnS4 == "Press")
            {
                ClickModeBtnS4 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnS4 = "Press";
            }
            return false;
        }

        private string _clickModeBtnS4;

        public string ClickModeBtnS4
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

        #region Visibility Flasche 1

        public void VisibilityFlasche1(bool val) => VisibilityImage1 = val ? "visible" : "hidden";

        private string _visibilityImage1;

        public string VisibilityImage1
        {
            get => _visibilityImage1;
            set
            {
                _visibilityImage1 = value;
                OnPropertyChanged(nameof(VisibilityImage1));
            }
        }

        #endregion Visibility Flasche 1

        #region Visibility Flasche 2

        public void VisibilityFlasche2(bool val) => VisibilityImage2 = val ? "visible" : "hidden";

        private string _visibilityImage2;

        public string VisibilityImage2
        {
            get => _visibilityImage2;
            set
            {
                _visibilityImage2 = value;
                OnPropertyChanged(nameof(VisibilityImage2));
            }
        }

        #endregion Visibility Flasche 2

        #region Visibility Flasche 3

        public void VisibilityFlasche3(bool val) => VisibilityImage3 = val ? "visible" : "hidden";

        private string _visibilityImage3;

        public string VisibilityImage3
        {
            get => _visibilityImage3;
            set
            {
                _visibilityImage3 = value;
                OnPropertyChanged(nameof(VisibilityImage3));
            }
        }

        #endregion Visibility Flasche 3

        #region Visibility Flasche 4

        public void VisibilityFlasche4(bool val) => VisibilityImage4 = val ? "visible" : "hidden";

        private string _visibilityImage4;

        public string VisibilityImage4
        {
            get => _visibilityImage4;
            set
            {
                _visibilityImage4 = value;
                OnPropertyChanged(nameof(VisibilityImage4));
            }
        }

        #endregion Visibility Flasche 4

        #region Visibility Flasche 5

        public void VisibilityFlasche5(bool val) => VisibilityImage5 = val ? "visible" : "hidden";

        private string _visibilityImage5;

        public string VisibilityImage5
        {
            get => _visibilityImage5;
            set
            {
                _visibilityImage5 = value;
                OnPropertyChanged(nameof(VisibilityImage5));
            }
        }

        #endregion Visibility Flasche 5

        #region Visibility Flasche 6

        public void VisibilityFlasche6(bool val) => VisibilityImage6 = val ? "visible" : "hidden";

        private string _visibilityImage6;

        public string VisibilityImage6
        {
            get => _visibilityImage6;
            set
            {
                _visibilityImage6 = value;
                OnPropertyChanged(nameof(VisibilityImage6));
            }
        }

        #endregion Visibility Flasche 6

        #region Visibility Sensor B1

        public void VisibilitySensorB1(bool val)
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

        #endregion Visibility Sensor B1

        #region Visibility Ventil K1

        public void VisibilityVentilK1(bool val)
        {
            if (val)
            {
                VisibilityK1Ein = "visible";
                VisibilityK1Aus = "hidden";
            }
            else
            {
                VisibilityK1Ein = "hidden";
                VisibilityK1Aus = "visible";
            }
        }

        private string _visibilityK1Ein;

        public string VisibilityK1Ein
        {
            get => _visibilityK1Ein;
            set
            {
                _visibilityK1Ein = value;
                OnPropertyChanged(nameof(VisibilityK1Ein));
            }
        }

        private string _visibilityK1Aus;

        public string VisibilityK1Aus
        {
            get => _visibilityK1Aus;
            set
            {
                _visibilityK1Aus = value;
                OnPropertyChanged(nameof(VisibilityK1Aus));
            }
        }

        #endregion Visibility Ventil K1

        #region Visibility Ventil K2

        public void VisibilityVentilK2(bool val)
        {
            if (val)
            {
                VisibilityK2Ein = "visible";
                VisibilityK2Aus = "hidden";
            }
            else
            {
                VisibilityK2Ein = "hidden";
                VisibilityK2Aus = "visible";
            }
        }

        private string _visibilityK2Ein;

        public string VisibilityK2Ein
        {
            get => _visibilityK2Ein;
            set
            {
                _visibilityK2Ein = value;
                OnPropertyChanged(nameof(VisibilityK2Ein));
            }
        }

        private string _visibilityK2Aus;

        public string VisibilityK2Aus
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

        public void VisibilityAbleitung(bool val) => VisibilityRectangleAbleitung = val ? "visible" : "hidden";

        private string _visibilityRectangleAbleitung;

        public string VisibilityRectangleAbleitung
        {
            get => _visibilityRectangleAbleitung;
            set
            {
                _visibilityRectangleAbleitung = value;
                OnPropertyChanged(nameof(VisibilityRectangleAbleitung));
            }
        }

        #endregion Visibility Ableitung

        #region Color F1

        public void FarbeCircle_F1(bool val) => ColorCircleF1 = val ? "LawnGreen" : "Red";

        private string _colorCircleF1;

        public string ColorCircleF1
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

        public void FarbeCircle_Q1(bool val) => ColorCircleQ1 = val ? "lawngreen" : "LightGray";

        private string _colorCircleQ1;

        public string ColorCircleQ1
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

        public void FarbeCircle_P1(bool val) => ColorCircleP1 = val ? "lawngreen" : "LightGray";

        private string _colorCircleP1;

        public string ColorCircleP1
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

        public void FarbeCircle_P2(bool val) => ColorCircleP2 = val ? "red" : "LightGray";

        private string _colorCircleP2;

        public string ColorCircleP2
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

        public void FarbeRectangleZuleitung(bool val) => ColorRectangleZuleitung = val ? "blue" : "lightblue";

        private string _colorRectangleZuleitung;

        public string ColorRectangleZuleitung
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
            Margin1 = new Thickness(0, _hoeheFuellBalken * (1 - pegel), 0, 0);
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