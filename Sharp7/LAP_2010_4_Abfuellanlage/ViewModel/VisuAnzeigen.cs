using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

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

            ColorP1 = Brushes.White;
            ColorQ1 = Brushes.LawnGreen;
            ColorRectangleZuleitung = Brushes.Coral;

            ClickModeBtnS1 = ClickMode.Press;
            ClickModeBtnS2 = ClickMode.Press;

            ClickModeBtnQ1 = ClickMode.Press;
            ClickModeBtnK1 = ClickMode.Press;
            ClickModeBtnK2 = ClickMode.Press;

            Margin1 = new Thickness(0, 30, 0, 0);

            VisibilityRectangleAbleitung = Visibility.Visible;

            for (var i = 0; i < 10; i++) VisDose.Add(Visibility.Visible);
            for (var i = 0; i < 10; i++) TopDose.Add(10 + i * 10);
            for (var i = 0; i < 10; i++) LeftDose.Add(10 + i * 10);

            VisibilityK1Ein = Visibility.Hidden;
            VisibilityK2Ein = Visibility.Hidden;

            VisibilityK1Aus = Visibility.Visible;
            VisibilityK2Aus = Visibility.Visible;

            VisibilityB1Ein = Visibility.Visible;
            VisibilityB1Aus = Visibility.Hidden;
            VisibilityB2Ein = Visibility.Visible;
            VisibilityB2Aus = Visibility.Hidden;

            VersionNr = "V0.0";
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
                FarbeP1(_abfuellAnlage.P1);
                FarbeQ1(_abfuellAnlage.Q1);

                FarbeRectangleZuleitung(_abfuellAnlage.Pegel > 0.01);

                Margin_1(_abfuellAnlage.Pegel);

                for (var i = 0; i < 5; i++)
                {
                    VisibilityDose1(_abfuellAnlage.AlleDosen[i].Sichtbar, i);
                    PositionDose(_abfuellAnlage.AlleDosen[i].EineDose.GetPosition(), i);
                }


                VisibilityAbleitung(_abfuellAnlage.K2 && _abfuellAnlage.Pegel > 0.01);

                SichtbarkeitK1(_abfuellAnlage.K1);
                SichtbarkeitK2(_abfuellAnlage.K2);

                SichtbarkeitB1(_abfuellAnlage.B1);
                SichtbarkeitB2(_abfuellAnlage.B2);

                if (_mainWindow.Plc != null)
                {
                    VersionNr = _mainWindow.VersionNummer;
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

        internal void SetQ1() => _abfuellAnlage.Q1 = ClickModeButtonQ1();
        internal void SetK1() => _abfuellAnlage.K1 = ClickModeButtonK1();
        internal void SetK2() => _abfuellAnlage.K2 = ClickModeButtonK2();
        internal void SetS1() => _abfuellAnlage.S1 = ClickModeButtonS1();
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


        #region EineDose Dosen

        private void VisibilityDose1(bool v, int id) => VisDose[id] = v ? Visibility.Visible : Visibility.Hidden;

        private void PositionDose(Punkt p, int id)
        {
            LeftDose[id] = (int)p.X;
            TopDose[id] = (int)p.Y;
        }



        private ObservableCollection<int> _topDose = new();
        public ObservableCollection<int> TopDose
        {
            get => _topDose;
            set
            {
                _topDose = value;
                OnPropertyChanged(nameof(TopDose));
            }
        }

        private ObservableCollection<int> _leftDose = new();
        public ObservableCollection<int> LeftDose
        {
            get => _leftDose;
            set
            {
                _leftDose = value;
                OnPropertyChanged(nameof(LeftDose));
            }
        }

        private ObservableCollection<Visibility> _visDose = new();
        public ObservableCollection<Visibility> VisDose
        {
            get => _visDose;
            set
            {
                _visDose = value;
                OnPropertyChanged(nameof(VisDose));
            }
        }

        #endregion

        #region Color P1

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

        #endregion Color P1

        #region Color Q1

        public void FarbeQ1(bool val) => ColorQ1 = val ? Brushes.LawnGreen : Brushes.LightGray;

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

        #endregion Color Q1

        #region Color Zuleitung

        public void FarbeRectangleZuleitung(bool val) => ColorRectangleZuleitung = val ? Brushes.Coral : Brushes.LightCoral;

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

        #region Sichtbarkeit K1

        public void SichtbarkeitK1(bool val)
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

        #endregion Sichtbarkeit K1

        #region Sichtbarkeit K2

        public void SichtbarkeitK2(bool val)
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

        #endregion Sichtbarkeit K2

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

        #endregion Visibility Ableitung

        #region Sichtbarkeit B1

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

        #endregion Sichtbarkeit B1

        #region Sichtbarkeit B2

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