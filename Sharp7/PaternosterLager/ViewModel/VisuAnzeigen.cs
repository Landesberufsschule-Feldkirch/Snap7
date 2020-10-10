namespace PaternosterLager.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Paternosterlager _paternosterlager;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Paternosterlager pa, double anzahlKisten)
        {
            _mainWindow = mw;
            _paternosterlager = pa;

            for (int i = 0; i < 100; i++) ClickModeBtn.Add("Press");

            ClickModeBtnAuf = "Press";
            ClickModeBtnAb = "Press";

            IstPosition = "00";
            SollPosition = "00";

            VisibilityB1Ein = "hidden";
            VisibilityB1Aus = "visible";

            VisibilityB2Ein = "Visible";
            VisibilityB2Aus = "Hidden";

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            AlleKettengliedRegale = new ObservableCollection<KettengliedRegal>();
            for (var i = 0; i < anzahlKisten; i++) AlleKettengliedRegale.Add(new KettengliedRegal(i, anzahlKisten));

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            _paternosterlager.GesamtLaenge = AlleKettengliedRegale[0].GetGesamtLaenge();

            while (true)
            {
                IstPosition = _paternosterlager.IstPos.ToString("D2");
                SollPosition = _paternosterlager.SollPos.ToString("D2");

                SichtbarkeitB1(_paternosterlager.B1);
                SichtbarkeitB2(_paternosterlager.B2);

                if (_mainWindow.FensterAktiv)
                {
                    _mainWindow.Dispatcher.Invoke(() =>
                               {
                                   if (_mainWindow.FensterAktiv) _mainWindow.ZeichenFlaeche.Children.Clear();
                                   foreach (var kettengliedRegal in AlleKettengliedRegale) kettengliedRegal.Zeichnen(_mainWindow, _paternosterlager.Position);
                               });
                }

                if (_mainWindow.Plc != null)
                {
                    if (_mainWindow.Plc.GetPlcModus() == "S7-1200")
                    {
                        VersionNr = _mainWindow.VersionNummer;
                        SpsVersionLokal = _mainWindow.VersionInfo;
                        SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                        SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? "hidden" : "visible";
                    }

                    SpsColor = _mainWindow.Plc.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(100);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void Buchstabe(object buchstabe)
        {
            if (buchstabe is string ascii)
            {
                var asciiCode = ascii[0];
                _paternosterlager.Zeichen = ClickModeButton(asciiCode) ? asciiCode : ' ';
            }
        }

        internal void TasterAuf() => _paternosterlager.ManualAuf = ClickModeButtonAuf();

        internal void TasterAb() => _paternosterlager.ManualAb = ClickModeButtonAb();

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

        #region KettengliederRegale

        private ObservableCollection<KettengliedRegal> _alleKettengliedRegale = new ObservableCollection<KettengliedRegal>();

        public ObservableCollection<KettengliedRegal> AlleKettengliedRegale
        {
            get => _alleKettengliedRegale;
            set
            {
                _alleKettengliedRegale = value;
                OnPropertyChanged(nameof(AlleKettengliedRegale));
            }
        }

        #endregion KettengliederRegale

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

        #region ClickModeBtnAuf

        public bool ClickModeButtonAuf()
        {
            if (ClickModeBtnAuf == "Press")
            {
                ClickModeBtnAuf = "Release";
                return true;
            }
            else
            {
                ClickModeBtnAuf = "Press";
            }
            return false;
        }

        private string _clickModeBtnAuf;

        public string ClickModeBtnAuf
        {
            get => _clickModeBtnAuf;
            set
            {
                _clickModeBtnAuf = value;
                OnPropertyChanged(nameof(ClickModeBtnAuf));
            }
        }

        #endregion ClickModeBtnAuf

        #region ClickModeBtnAb

        public bool ClickModeButtonAb()
        {
            if (ClickModeBtnAb == "Press")
            {
                ClickModeBtnAb = "Release";
                return true;
            }
            else
            {
                ClickModeBtnAb = "Press";
            }
            return false;
        }

        private string _clickModeBtnAb;

        public string ClickModeBtnAb
        {
            get => _clickModeBtnAb;
            set
            {
                _clickModeBtnAb = value;
                OnPropertyChanged(nameof(ClickModeBtnAb));
            }
        }

        #endregion ClickModeBtnAb

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

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}