namespace PaternosterLager.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Paternosterlager paternosterlager;
        private readonly MainWindow mainWindow;

        private const double geschwindigkeit = 2;

        public VisuAnzeigen(MainWindow mw, Model.Paternosterlager pa)
        {
            mainWindow = mw;
            paternosterlager = pa;

            for (int i = 0; i < 100; i++) ClickModeBtn.Add("Press");

            ClickModeBtnAuf = "Press";
            ClickModeBtnAb = "Press";

            IstPosition = "00";
            SollPosition = "00";

            SpsStatus = "-";
            SpsColor = "LightBlue";

            AlleKettengliedRegale = new ObservableCollection<KettengliedRegal>();
            for (var i = 0; i < 20; i++) AlleKettengliedRegale.Add(new KettengliedRegal(i));
            //for (var i = 0; i < 15; i++) AlleKettengliedRegale.Add(new KettengliedRegal(i));

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                if (paternosterlager.RichtungAuf) SetGeschwindigkeit(geschwindigkeit);
                if (paternosterlager.RichtungAb) SetGeschwindigkeit(-geschwindigkeit);

                if (mainWindow.FensterAktiv)
                {
                    mainWindow.Dispatcher.Invoke(() =>
                               {
                                   if (mainWindow.FensterAktiv) mainWindow.ZeichenFlaeche.Children.Clear();
                                   foreach (var kettengliedRegal in AlleKettengliedRegale) kettengliedRegal.Zeichnen(mainWindow);
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

        public void SetGeschwindigkeit(double geschwindigkeit) { foreach (var kettengliedRegal in AlleKettengliedRegale) kettengliedRegal.SetGeschwindigkeit(geschwindigkeit); }
        internal void TasterAuf() => paternosterlager.RichtungAuf = ClickModeButtonAuf();
        internal void TasterAb() => paternosterlager.RichtungAb = ClickModeButtonAb();


        #region SPS Status und Farbe
        private string _spsStatus;
        public string SpsStatus
        {
            get { return _spsStatus; }
            set
            {
                _spsStatus = value;
                OnPropertyChanged(nameof(SpsStatus));
            }
        }



        private string _spsColor;
        public string SpsColor
        {
            get { return _spsColor; }
            set
            {
                _spsColor = value;
                OnPropertyChanged(nameof(SpsColor));
            }
        }
        #endregion


        #region KettengliederRegale

        private ObservableCollection<KettengliedRegal> _alleKettengliedRegale = new ObservableCollection<KettengliedRegal>();
        public ObservableCollection<KettengliedRegal> AlleKettengliedRegale
        {
            get { return _alleKettengliedRegale; }
            set
            {
                _alleKettengliedRegale = value;
                OnPropertyChanged(nameof(AlleKettengliedRegale));
            }
        }

        #endregion


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
            get { return _clickModeBtn; }
            set
            {
                _clickModeBtn = value;
                OnPropertyChanged(nameof(ClickModeBtn));
            }
        }
        #endregion

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
            get { return _clickModeBtnAuf; }
            set
            {
                _clickModeBtnAuf = value;
                OnPropertyChanged(nameof(ClickModeBtnAuf));
            }
        }
        #endregion

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
            get { return _clickModeBtnAb; }
            set
            {
                _clickModeBtnAb = value;
                OnPropertyChanged(nameof(ClickModeBtnAb));
            }
        }
        #endregion



        #region IstPosition
        private string _istPosition;
        public string IstPosition
        {
            get { return _istPosition; }
            set
            {
                _istPosition = value;
                OnPropertyChanged(nameof(IstPosition));
            }
        }
        #endregion

        #region SollPosition
        private string _sollPosition;
        public string SollPosition
        {
            get { return _sollPosition; }
            set
            {
                _sollPosition = value;
                OnPropertyChanged(nameof(SollPosition));
            }
        }
        #endregion

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members

    }
}
