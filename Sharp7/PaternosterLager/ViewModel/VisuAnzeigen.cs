namespace PaternosterLager.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Paternosterlager paternosterlager;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Paternosterlager pa)
        {
            mainWindow = mw;
            paternosterlager = pa;

            for (int i = 0; i < 100; i++) ClickModeBtn.Add("Press");

            ClickModeBtnAuf = "Press";
            ClickModeBtnAb = "Press";

            IstPosition = "00";
            SollPosition = "00";

            VisibilityB1Ein = "hidden";
            VisibilityB1Aus = "visible";

            VisibilityB2Ein = "Visible";
            VisibilityB2Aus = "Hidden";

            SpsStatus = "-";
            SpsColor = "LightBlue";

            AlleKettengliedRegale = new ObservableCollection<KettengliedRegal>();
            for (var i = 0; i < 20; i++) AlleKettengliedRegale.Add(new KettengliedRegal(i));

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }

        private void VisuAnzeigenTask()
        {
            paternosterlager.GesamtLaenge = AlleKettengliedRegale[0].GetGesamtLaenge();

            while (true)
            {
                IstPosition = paternosterlager.IstPos.ToString("D2");
                SollPosition = paternosterlager.SollPos.ToString("D2");

                SichtbarkeitB1(paternosterlager.B1);
                SichtbarkeitB2(paternosterlager.B2);

                if (mainWindow.FensterAktiv)
                {
                    mainWindow.Dispatcher.Invoke(() =>
                               {
                                   if (mainWindow.FensterAktiv)
                                   {
                                       mainWindow.ZeichenFlaeche.Children.Clear();
                                   }
                                   foreach (var kettengliedRegal in AlleKettengliedRegale) kettengliedRegal.Zeichnen(mainWindow, paternosterlager.Position);
                               });
                }

                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(100);
            }
        }

        internal void Buchstabe(object buchstabe)
        {
            if (buchstabe is string ascii)
            {
                var asciiCode = ascii[0];
                if (ClickModeButton(asciiCode)) paternosterlager.Zeichen = asciiCode; else paternosterlager.Zeichen = ' ';
            }
        }

        internal void TasterAuf() => paternosterlager.ManualAuf = ClickModeButtonAuf();
        internal void TasterAb() => paternosterlager.ManualAb = ClickModeButtonAb();

        #region SPS Status und Farbe

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

        #endregion SPS Status und Farbe

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

        public bool ClickModeButton(int AsciiCode)
        {
            if (ClickModeBtn[AsciiCode] == "Press")
            {
                ClickModeBtn[AsciiCode] = "Release";
                return true;
            }
            else
            {
                ClickModeBtn[AsciiCode] = "Press";
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