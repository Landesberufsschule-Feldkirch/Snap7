using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LAP_2010_1_Kompressoranlage.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Kompressoranlage _kompressoranlage;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Kompressoranlage ka)
        {
            _mainWindow = mw;
            _kompressoranlage = ka;

            AktuellerDruck = 2;

            ClickModeBtnS1 = ClickMode.Press;
            ClickModeBtnS2 = ClickMode.Press;

            ColorF1 = Colors.LawnGreen;
            ColorP1 = Colors.LawnGreen;
            ColorP2 = Colors.LawnGreen;
            ColorQ1 = Colors.LawnGreen;
            ColorQ2 = Colors.LawnGreen;
            ColorQ3 = Colors.LawnGreen;
            ColorB1 = Colors.LawnGreen;

            VisibilityB1Ein =  Visibility.Hidden;
            VisibilityB1Aus = Visibility.Visible;

            VisibilityB2Ein = Visibility.Visible;
            VisibilityB2Aus =  Visibility.Hidden;

            VisibilityKurzschluss =  Visibility.Hidden;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Colors.LightBlue;

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeF1(_kompressoranlage.F1);
                FarbeP1(_kompressoranlage.P1);
                FarbeP2(_kompressoranlage.P2);
                FarbeQ1(_kompressoranlage.Q1);
                FarbeQ2(_kompressoranlage.Q2);
                FarbeQ3(_kompressoranlage.Q3);
                FarbeB1(_kompressoranlage.B1);

                SichtbarkeitB1(_kompressoranlage.B1);
                SichtbarkeitB2(_kompressoranlage.B2);

                if (_kompressoranlage.Q2 && _kompressoranlage.Q3) VisibilityKurzschluss = Visibility.Visible; else VisibilityKurzschluss =  Visibility.Hidden;

                AktuellerDruck = _kompressoranlage.Druck;

                if (_mainWindow.Plc != null)
                {
                    if (_mainWindow.Plc.GetPlcModus() == "S7-1200")
                    {
                        VersionNr = _mainWindow.VersionNummer;
                        SpsVersionLokal = _mainWindow.VersionInfoLokal;
                        SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                        SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    }

                    SpsColor = _mainWindow.Plc.GetSpsError() ? Colors.Red : Colors.LightGray;
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void SetS1() => _kompressoranlage.S1 = !ClickModeButtonS1();
        internal void BtnS2() => _kompressoranlage.S2 = ClickModeButtonS2();

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

        private Color _spsColor;

        public Color SpsColor
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
            if (ClickModeBtnS1 ==  ClickMode.Press)
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
            if (ClickModeBtnS2 ==  ClickMode.Press)
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

        #region Color F1

        public void FarbeF1(bool val)
        {
            ColorF1 = val ? Colors.LawnGreen : Colors.Red;
        }

        private Color _colorF1;

        public Color ColorF1
        {
            get => _colorF1;
            set
            {
                _colorF1 = value;
                OnPropertyChanged(nameof(ColorF1));
            }
        }

        #endregion Color F1

        #region Color P1

        public void FarbeP1(bool val)
        {
            ColorP1 = val ? Colors.Red : Colors.White;
        }

        private Color _colorP1;

        public Color ColorP1
        {
            get => _colorP1;
            set
            {
                _colorP1 = value;
                OnPropertyChanged(nameof(ColorP1));
            }
        }

        #endregion Color P1

        #region Color P2

        public void FarbeP2(bool val)
        {
            ColorP2 = val ? Colors.LawnGreen : Colors.White;
        }

        private Color _colorP2;

        public Color ColorP2
        {
            get => _colorP2;
            set
            {
                _colorP2 = value;
                OnPropertyChanged(nameof(ColorP2));
            }
        }

        #endregion Color P2

        #region Color Q1

        public void FarbeQ1(bool val)
        {
            ColorQ1 = val ? Colors.LawnGreen : Colors.White;
        }

        private Color _colorQ1;

        public Color ColorQ1
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
            ColorQ2 = val ? Colors.LawnGreen : Colors.White;
        }

        private Color _colorQ2;

        public Color ColorQ2
        {
            get => _colorQ2;
            set
            {
                _colorQ2 = value;
                OnPropertyChanged(nameof(ColorQ2));
            }
        }

        #endregion Color Q2

        #region Color Q3

        public void FarbeQ3(bool val)
        {
            ColorQ3 = val ? Colors.LawnGreen : Colors.White;
        }

        private Color _colorQ3;

        public Color ColorQ3
        {
            get => _colorQ3;
            set
            {
                _colorQ3 = value;
                OnPropertyChanged(nameof(ColorQ3));
            }
        }

        #endregion Color Q3

        #region Color S7

        public void FarbeB1(bool val)
        {
            ColorB1 = val ? Colors.LawnGreen : Colors.Red;
        }

        private Color _colorB1;

        public Color ColorB1
        {
            get => _colorB1;
            set
            {
                _colorB1 = value;
                OnPropertyChanged(nameof(ColorB1));
            }
        }

        #endregion Color S7

        #region VisibilityKurzschluss

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

        #endregion VisibilityKurzschluss

        #region Sichtbarkeit B1

        public void SichtbarkeitB1(bool val)
        {
            if (val)
            {
                VisibilityB1Ein = Visibility.Visible;
                VisibilityB1Aus =  Visibility.Hidden;
            }
            else
            {
                VisibilityB1Ein =  Visibility.Hidden;
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

        #endregion Sichtbarkeit B1

        #region Sichtbarkeit B2

        public void SichtbarkeitB2(bool val)
        {
            if (val)
            {
                VisibilityB2Ein = Visibility.Visible;
                VisibilityB2Aus =  Visibility.Hidden;
            }
            else
            {
                VisibilityB2Ein =  Visibility.Hidden;
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

        #endregion Sichtbarkeit B2


        private double _aktuellerDruck;
        public double AktuellerDruck
        {
            get => _aktuellerDruck;
            set
            {
                _aktuellerDruck = value;
                OnPropertyChanged(nameof(AktuellerDruck));
            }
        }

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}