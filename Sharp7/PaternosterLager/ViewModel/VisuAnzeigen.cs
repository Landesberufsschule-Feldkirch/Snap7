namespace PaternosterLager.ViewModel
{
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {

        private readonly PaternosterLager.Model.Paternosterlager paternosterlager;
        private readonly MainWindow mainWindow;

        private const double geschwindigkeit = 0.01;

        public VisuAnzeigen(MainWindow mw, PaternosterLager.Model.Paternosterlager pa)
        {
            mainWindow = mw;
            paternosterlager = pa;

            ClickModeBtnAuf = "Press";
            ClickModeBtnAb = "Press";

            SpsStatus = "-";
            SpsColor = "LightBlue";

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }
        private void VisuAnzeigenTask()
        {
            while (true)
            {
                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }

        public void SetGeschwindigkeit(double geschwindigkeit)
        {
            foreach (var kettengliedRegal in paternosterlager.AlleKettengliedRegale) kettengliedRegal.SetGeschwindigkeit(geschwindigkeit);
        }


        internal void TasterAuf()
        {
            paternosterlager.RichtungAuf = ClickModeButtonAuf();
            SetGeschwindigkeit(geschwindigkeit);
        }
        internal void TasterAb()
        {
            paternosterlager.RichtungAb = ClickModeButtonAb();
            SetGeschwindigkeit(-geschwindigkeit);
        }






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




        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members

    }
}
