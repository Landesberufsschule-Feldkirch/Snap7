namespace LAP_2010_4_Abfuellanlage.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using Utilities;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.AbfuellAnlage _abfuellAnlage;
        private readonly MainWindow _mainWindow;

        private const double HoeheFuellBalken = 9 * 35;

        public VisuAnzeigen(MainWindow mw, Model.AbfuellAnlage aa)
        {
            _mainWindow = mw;
            _abfuellAnlage = aa;

            ColorP1 = "White";
            ColorQ1 = "LawnGreen";
            ColorRectangleZuleitung = "Coral";

            ClickModeBtnS1 = "Press";
            ClickModeBtnS2 = "Press";

            ClickModeBtnQ1 = "Press";
            ClickModeBtnK1 = "Press";
            ClickModeBtnK2 = "Press";

            ImageTop1 = 10;
            ImageTop2 = 20;
            ImageTop3 = 30;
            ImageTop4 = 40;

            ImageLeft1 = 10;
            ImageLeft2 = 20;
            ImageLeft3 = 30;
            ImageLeft4 = 10;

            Margin1 = new Thickness(0, 30, 0, 0);

            VisibilityRectangleAbleitung = "visible";

            VisibilityImage1 = "visible";
            VisibilityImage2 = "visible";
            VisibilityImage3 = "visible";
            VisibilityImage4 = "visible";

            VisibilityK1Ein = "hidden";
            VisibilityK2Ein = "hidden";

            VisibilityK1Aus = "visible";
            VisibilityK2Aus = "visible";

            VisibilityB1Ein = "Visible";
            VisibilityB1Aus = "Hidden";
            VisibilityB2Ein = "Visible";
            VisibilityB2Aus = "Hidden";

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeP1(_abfuellAnlage.P1);
                FarbeQ1(_abfuellAnlage.Q1);

                FarbeRectangleZuleitung(_abfuellAnlage.Pegel > 0.01);

                Margin_1(_abfuellAnlage.Pegel);

                PositionImage_1(_abfuellAnlage.AlleDosen[0].Position.Punkt);
                PositionImage_2(_abfuellAnlage.AlleDosen[1].Position.Punkt);
                PositionImage_3(_abfuellAnlage.AlleDosen[2].Position.Punkt);
                PositionImage_4(_abfuellAnlage.AlleDosen[3].Position.Punkt);

                VisibilityDose1(_abfuellAnlage.AlleDosen[0].Sichtbar);
                VisibilityDose2(_abfuellAnlage.AlleDosen[1].Sichtbar);
                VisibilityDose3(_abfuellAnlage.AlleDosen[2].Sichtbar);
                VisibilityDose4(_abfuellAnlage.AlleDosen[3].Sichtbar);

                VisibilityAbleitung(_abfuellAnlage.K2 && _abfuellAnlage.Pegel > 0.01);

                SichtbarkeitK1(_abfuellAnlage.K1);
                SichtbarkeitK2(_abfuellAnlage.K2);

                SichtbarkeitB1(_abfuellAnlage.B1);
                SichtbarkeitB2(_abfuellAnlage.B2);

                if (_mainWindow.Plc != null)
                {
                    VersionNr = _mainWindow.VersionNummer;
                    SpsVersionLokal = _mainWindow.VersionInfo;
                    SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                    SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? "hidden" : "visible";

                    SpsColor = _mainWindow.Plc.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void SetQ1() => _abfuellAnlage.Q1 = ClickModeButtonQ1();

        internal void SetK1() => _abfuellAnlage.K1 = ClickModeButtonK1();

        internal void SetK2() => _abfuellAnlage.K2 = ClickModeButtonK2();

        internal void SetS1() => _abfuellAnlage.S1 = !ClickModeButtonS1();

        internal void BtnS2() => _abfuellAnlage.S2 = ClickModeButtonS2();

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

        #region Color P1

        public void FarbeP1(bool val) => ColorP1 = val ? "Red" : "White";

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

        public void FarbeQ1(bool val) => ColorQ1 = val ? "LawnGreen" : "LightGray";

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

        #region Color Zuleitung

        public void FarbeRectangleZuleitung(bool val) => ColorRectangleZuleitung = val ? "Coral" : "LightCoral";

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

        #region ClickModeBtnS1

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

        #endregion ClickModeBtnS1

        #region ClickModeBtnS2

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

        #endregion ClickModeBtnS2

        #region ClickModeBtnQ1

        public bool ClickModeButtonQ1()
        {
            if (ClickModeBtnQ1 == "Press")
            {
                ClickModeBtnQ1 = "Release";
                return true;
            }

            ClickModeBtnQ1 = "Press";
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

        #region ClickModeBtnK1

        public bool ClickModeButtonK1()
        {
            if (ClickModeBtnK1 == "Press")
            {
                ClickModeBtnK1 = "Release";
                return true;
            }

            ClickModeBtnK1 = "Press";
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

            ClickModeBtnK2 = "Press";
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

        #region Sichtbarkeit K1

        public void SichtbarkeitK1(bool val)
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

        #endregion Sichtbarkeit K1

        #region Sichtbarkeit K2

        public void SichtbarkeitK2(bool val)
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

        #endregion Sichtbarkeit K2

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

        #region Visibility Dose 1

        public void VisibilityDose1(bool val) => VisibilityImage1 = val ? "visible" : "hidden";

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

        #endregion Visibility Dose 1

        #region Visibility Dose 2

        public void VisibilityDose2(bool val) => VisibilityImage2 = val ? "visible" : "hidden";

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

        #endregion Visibility Dose 2

        #region Visibility Dose 3

        public void VisibilityDose3(bool val) => VisibilityImage3 = val ? "visible" : "hidden";

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

        #endregion Visibility Dose 3

        #region Visibility Dose 4

        public void VisibilityDose4(bool val) => VisibilityImage4 = val ? "visible" : "hidden";

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

        #endregion Visibility Dose 4

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