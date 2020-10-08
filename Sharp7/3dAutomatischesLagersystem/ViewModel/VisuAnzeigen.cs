namespace AutomatischesLagersystem.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;

    public static class IdEintraege
    {
        public const int System = 0;
        public const int Bodenplatte = 1;
        public const int Streben = 2;
        public const int Regalbediengeraet = 3;
        public const int Kisten = 4;
        public const int AnzahlEintraege = 5;
    };

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.AutomatischesLagersystem _automatischesLagersystem;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.AutomatischesLagersystem al)
        {
            _mainWindow = mw;
            _automatischesLagersystem = al;

            for (var i = 0; i < 100; i++) ClickModeBtn.Add("Press");

            ClickModeBtnK1 = "Press";       // für SetManual
            ClickModeBtnK2 = "Press";
            ClickModeBtnK3 = "Press";
            ClickModeBtnK4 = "Press";
            ClickModeBtnK5 = "Press";
            ClickModeBtnK6 = "Press";

            ColorKollisionRegalMitSchlitten = "Lawngreen";

            VisibilityButtonsAktiv = "hidden";
            VisibilitySlidersAktiv = "visible";

            IstPosition = "00";
            SollPosition = "00";

            XPosition = "0";
            YPosition = "0";
            ZPosition = "0";

            XPosSlider = 0;
            YPosSlider = 0;
            ZPosSlider = 0;

            VisibilityB1Ein = "hidden";
            VisibilityB1Aus = "visible";

            VisibilityB2Ein = "visible";
            VisibilityB2Aus = "hidden";

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        internal void AllesReset()
        {
            XPosSlider = 0;
            YPosSlider = 0;
            ZPosSlider = 0;
            _mainWindow.DreiD.EineEinzigeKisteAufDemRegalbediengeraet();
            _mainWindow.viewPort3d.Children[_mainWindow.DreiDModelleIds[IdEintraege.Regalbediengeraet]].Transform = _mainWindow.KistenStartPositionen[0].Transform(-1750, 1400, -100);
        }

        internal void SetButtonsAktiv()
        {
            if (VisibilityButtonsAktiv == "hidden")
            {
                VisibilityButtonsAktiv = "visible";
                VisibilitySlidersAktiv = "hidden";
            }
            else
            {
                VisibilityButtonsAktiv = "hidden";
                VisibilitySlidersAktiv = "visible";
            }
        }

        internal void AllesAusraeumen() => _mainWindow.DreiD.AlleKistenEntfernen();

        internal void AllesEinraeumen() => _mainWindow.DreiD.AlleKistenHinzufeugen();

        internal void SetK1() => _automatischesLagersystem.K1 = ClickModeButtonK1();

        internal void SetK2() => _automatischesLagersystem.K2 = ClickModeButtonK2();

        internal void SetK3() => _automatischesLagersystem.K3 = ClickModeButtonK3();

        internal void SetK4() => _automatischesLagersystem.K4 = ClickModeButtonK4();

        internal void SetK5() => _automatischesLagersystem.K5 = ClickModeButtonK5();

        internal void SetK6() => _automatischesLagersystem.K6 = ClickModeButtonK6();

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                if (_mainWindow.DebugWindowAktiv && (VisibilitySlidersAktiv == "visible"))
                {
                    _mainWindow.RegalBedienGeraet.SetX(XPosSlider);  // Zahlenbereich 0 .. 1
                    _mainWindow.RegalBedienGeraet.SetY(YPosSlider);  // Zahlenbereich -1 .. 1
                    _mainWindow.RegalBedienGeraet.SetZ(ZPosSlider);  // Zahlenbereich 0 .. 1
                }

                FarbeKollisionRegalMitSchlitten(_automatischesLagersystem.KollisionRegal.GetKollisionRegalMitSchlitten());

                if (_mainWindow.viewPort3d != null)
                {
                    _mainWindow.Dispatcher.Invoke(() =>
                   {
                       if (_mainWindow.FensterAktiv)
                       {
                           if (_mainWindow.DreiDModelleIds[IdEintraege.Regalbediengeraet] == 201)
                           {
                               //Bediengerät
                               _mainWindow.viewPort3d.Children[197].Transform = _mainWindow.BediengeraetStartpositionen[0].Transform(_mainWindow.RegalBedienGeraet.GetXPosition(), 0, 0);

                               // Schlitten senkrecht
                               _mainWindow.viewPort3d.Children[198].Transform = _mainWindow.BediengeraetStartpositionen[1].Transform(_mainWindow.RegalBedienGeraet.GetXPosition(), 0, _mainWindow.RegalBedienGeraet.GetZPosition());

                               // Schlitten waagrecht Zwischenteil
                               _mainWindow.viewPort3d.Children[199].Transform = _mainWindow.BediengeraetStartpositionen[2].Transform(_mainWindow.RegalBedienGeraet.GetXPosition(), (-1) * _mainWindow.RegalBedienGeraet.GetYPosition() / 2, _mainWindow.RegalBedienGeraet.GetZPosition());

                               // Schlitten waagrecht
                               _mainWindow.viewPort3d.Children[200].Transform = _mainWindow.BediengeraetStartpositionen[3].Transform(_mainWindow.RegalBedienGeraet.GetXPosition(), (-1) * _mainWindow.RegalBedienGeraet.GetYPosition(), _mainWindow.RegalBedienGeraet.GetZPosition());

                               XPosition = (_mainWindow.BediengeraetStartpositionen[3].GetX() + _mainWindow.RegalBedienGeraet.GetXPosition()).ToString();
                               YPosition = (_mainWindow.BediengeraetStartpositionen[3].GetY() + _mainWindow.RegalBedienGeraet.GetYPosition()).ToString();
                               ZPosition = (_mainWindow.BediengeraetStartpositionen[3].GetZ() + _mainWindow.RegalBedienGeraet.GetZPosition()).ToString();
                           }
                           else
                           {
                               MessageBox.Show("Es hat sich die Anzahl der 3D Objekte geändert!!!");
                           }
                       }
                   });
                }

                if (_mainWindow.Plc != null)
                {
                    SpsVersionLokal = _mainWindow.VersionInfo;
                    SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                    SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? "hidden" : "visible";

                    SpsColor = _mainWindow.Plc.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(100);
            }
        }

        internal void Buchstabe(object buchstabe)
        {
            if (buchstabe is string ascii)
            {
                var asciiCode = ascii[0];
                _automatischesLagersystem.Zeichen = ClickModeButton(asciiCode) ? asciiCode : ' ';
            }
        }

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

        #region Sichtbarkeit B1

        // ReSharper disable once UnusedMember.Global
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

        #endregion Sichtbarkeit B1

        #region Sichtbarkeit B2

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

        #endregion Sichtbarkeit B2

        #region ColorKollisionRegalMitSchlitten

        public void FarbeKollisionRegalMitSchlitten(bool val) => ColorKollisionRegalMitSchlitten = val ? "Red" : "Lawngreen";

        private string _colorKollisionRegalMitSchlitten;

        public string ColorKollisionRegalMitSchlitten
        {
            get => _colorKollisionRegalMitSchlitten;
            set
            {
                _colorKollisionRegalMitSchlitten = value;
                OnPropertyChanged(nameof(ColorKollisionRegalMitSchlitten));
            }
        }

        #endregion ColorKollisionRegalMitSchlitten

        #region VisibilityButtonsAktiv

        private string _visibilityButtonsAktiv;

        public string VisibilityButtonsAktiv
        {
            get => _visibilityButtonsAktiv;
            set
            {
                _visibilityButtonsAktiv = value;
                OnPropertyChanged(nameof(VisibilityButtonsAktiv));
            }
        }

        #endregion VisibilityButtonsAktiv

        #region VisibilitySlidersAktiv

        private string _visibilitySlidersAktiv;

        public string VisibilitySlidersAktiv
        {
            get => _visibilitySlidersAktiv;
            set
            {
                _visibilitySlidersAktiv = value;
                OnPropertyChanged(nameof(VisibilitySlidersAktiv));
            }
        }

        #endregion VisibilitySlidersAktiv

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

        #region ClickModeBtnK3

        public bool ClickModeButtonK3()
        {
            if (ClickModeBtnK3 == "Press")
            {
                ClickModeBtnK3 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnK3 = "Press";
            }
            return false;
        }

        private string _clickModeBtnK3;

        public string ClickModeBtnK3
        {
            get => _clickModeBtnK3;
            set
            {
                _clickModeBtnK3 = value;
                OnPropertyChanged(nameof(ClickModeBtnK3));
            }
        }

        #endregion ClickModeBtnK3

        #region ClickModeBtnK4

        public bool ClickModeButtonK4()
        {
            if (ClickModeBtnK4 == "Press")
            {
                ClickModeBtnK4 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnK4 = "Press";
            }
            return false;
        }

        private string _clickModeBtnK4;

        public string ClickModeBtnK4
        {
            get => _clickModeBtnK4;
            set
            {
                _clickModeBtnK4 = value;
                OnPropertyChanged(nameof(ClickModeBtnK4));
            }
        }

        #endregion ClickModeBtnK4

        #region ClickModeBtnK5

        public bool ClickModeButtonK5()
        {
            if (ClickModeBtnK5 == "Press")
            {
                ClickModeBtnK5 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnK5 = "Press";
            }
            return false;
        }

        private string _clickModeBtnK5;

        public string ClickModeBtnK5
        {
            get => _clickModeBtnK5;
            set
            {
                _clickModeBtnK5 = value;
                OnPropertyChanged(nameof(ClickModeBtnK5));
            }
        }

        #endregion ClickModeBtnK5

        #region ClickModeBtnK6

        public bool ClickModeButtonK6()
        {
            if (ClickModeBtnK6 == "Press")
            {
                ClickModeBtnK6 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnK6 = "Press";
            }
            return false;
        }

        private string _clickModeBtnK6;

        public string ClickModeBtnK6
        {
            get => _clickModeBtnK6;
            set
            {
                _clickModeBtnK6 = value;
                OnPropertyChanged(nameof(ClickModeBtnK6));
            }
        }

        #endregion ClickModeBtnK6

        #region ClickModeAlleButtons

        public bool ClickModeButton(int asciiCode)
        {
            if (ClickModeBtn[asciiCode] == "Press")
            {
                ClickModeBtn[asciiCode] = "Release";
                return true;
            }
            else
            {
                ClickModeBtn[asciiCode] = "Press";
            }
            return false;
        }

        private ObservableCollection<string> _clickModeBtn = new ObservableCollection<string>();

        public ObservableCollection<string> ClickModeBtn
        {
            get => _clickModeBtn;
            set
            {
                _clickModeBtn = value;
                OnPropertyChanged(nameof(ClickModeBtn));
            }
        }

        #endregion ClickModeAlleButtons

        #region XPosition

        private string _xPosition;

        public string XPosition
        {
            get => _xPosition;
            set
            {
                _xPosition = value;
                OnPropertyChanged(nameof(XPosition));
            }
        }

        #endregion XPosition

        #region YPosition

        private string _yPosition;

        public string YPosition
        {
            get => _yPosition;
            set
            {
                _yPosition = value;
                OnPropertyChanged(nameof(YPosition));
            }
        }

        #endregion YPosition

        #region ZPosition

        private string _zPosition;

        public string ZPosition
        {
            get => _zPosition;
            set
            {
                _zPosition = value;
                OnPropertyChanged(nameof(ZPosition));
            }
        }

        #endregion ZPosition

        #region IstPosition

        private string _istPosition;

        public string IstPosition
        {
            get => _istPosition;
            set
            {
                _istPosition = value;
                OnPropertyChanged(nameof(IstPosition));
            }
        }

        #endregion IstPosition

        #region SollPosition

        private string _sollPosition;

        public string SollPosition
        {
            get => _sollPosition;
            set
            {
                _sollPosition = value;
                OnPropertyChanged(nameof(SollPosition));
            }
        }

        #endregion SollPosition

        #region xPosSlider

        // ReSharper disable once UnusedMember.Global
        public double XSliderPosition() => XPosSlider;

        private double _xSliderPosition;

        public double XPosSlider
        {
            get => _xSliderPosition;
            set
            {
                _xSliderPosition = value;
                OnPropertyChanged(nameof(XPosSlider));
            }
        }

        #endregion xPosSlider

        #region yPosSlider

        // ReSharper disable once UnusedMember.Global
        public double YSliderPosition() => YPosSlider;

        private double _ySliderPosition;

        public double YPosSlider
        {
            get => _ySliderPosition;
            set
            {
                _ySliderPosition = value;
                OnPropertyChanged(nameof(YPosSlider));
            }
        }

        #endregion yPosSlider

        #region zPosSlider

        // ReSharper disable once UnusedMember.Global
        public double ZSliderPosition() => ZPosSlider;

        private double _zSliderPosition;

        public double ZPosSlider
        {
            get => _zSliderPosition;
            set
            {
                _zSliderPosition = value;
                OnPropertyChanged(nameof(ZPosSlider));
            }
        }

        #endregion zPosSlider

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}