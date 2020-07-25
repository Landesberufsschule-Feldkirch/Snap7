namespace StiegenhausBeleuchtung.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.StiegenhausBeleuchtung stiegenhausBeleuchtung;
        private readonly MainWindow mainWindow;
        private bool BewegungAktiv;

        public VisuAnzeigen(MainWindow mw, Model.StiegenhausBeleuchtung stiegenhaus)
        {
            mainWindow = mw;
            stiegenhausBeleuchtung = stiegenhaus;

            ReiseStart = "sadf:-";
            ReiseZiel = "sadf:-";

            for (int i = 0; i < 100; i++) ClickModeBtn.Add("Press");
            for (int i = 0; i < 100; i++) ColorLampe.Add("Yellow");

            SpsVersionsInfo = true;
            SpsStatus = "x";
            SpsColor = "LightBlue";

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                for (int i = 0; i < 100; i++) FarbeAlleLampen(i, stiegenhausBeleuchtung.GetLampen(i));

                if (BewegungAktiv)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        if (stiegenhausBeleuchtung.GetBewegungsmelder(i)) ClickModeBtn[i] = "Release"; else ClickModeBtn[i] = "Press";
                    }
                }

                BewegungAktiv = stiegenhausBeleuchtung.JobAktiv;

                if (mainWindow.S7_1200 != null)
                {
                    string vInfo = mainWindow.S7_1200.GetVersion();
                    SpsVersionsInfo = mainWindow.Versionsinfo == vInfo;

                    SpsColor = mainWindow.S7_1200.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }

         #region SPS Versionsinfo, Status und Farbe

        private bool _spsVersionsInfo;
        public bool SpsVersionsInfo
        {
            get => _spsVersionsInfo;
            set
            {
                _spsVersionsInfo = value;
                OnPropertyChanged(nameof(SpsVersionsInfo));
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

        #endregion SPS Status und Farbe

        #region ReiseStart

        private string _reiseStart;

        public string ReiseStart
        {
            get => _reiseStart;
            set
            {
                var v = value.Split(':');
                _reiseStart = v[1].Trim();
                OnPropertyChanged(nameof(ReiseStart));
            }
        }

        #endregion ReiseStart

        #region ReiseZiel

        private string _reiseZiel;

        public string ReiseZiel
        {
            get => _reiseZiel;
            set
            {
                var v = value.Split(':');
                _reiseZiel = v[1].Trim();
                OnPropertyChanged(nameof(ReiseZiel));
            }
        }

        #endregion ReiseZiel

        #region FarbeAlleLampen

        public void FarbeAlleLampen(int lampe, bool val) { if (val) ColorLampe[lampe] = "Yellow"; else ColorLampe[lampe] = "White"; }

        private ObservableCollection<string> _colorLampe = new ObservableCollection<string>();

        public ObservableCollection<string> ColorLampe
        {
            get => _colorLampe;
            set
            {
                _colorLampe = value;
                OnPropertyChanged(nameof(ColorLampe));
            }
        }

        #endregion FarbeAlleLampen

        #region ClickModeAlleButtons

        public bool ClickModeButton(int Bewegungsmelder)
        {
            if (ClickModeBtn[Bewegungsmelder] == "Press")
            {
                ClickModeBtn[Bewegungsmelder] = "Release";
                return true;
            }
            else
            {
                ClickModeBtn[Bewegungsmelder] = "Press";
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

        #region BtnBewegungsmelder

        internal void BtnBewegungsmelder(object buttonName)
        {
            if (buttonName is int Bewegungsmelder)
            {
                stiegenhausBeleuchtung.SetBewegungsmelder(Bewegungsmelder, ClickModeButton(Bewegungsmelder));
            }
        }

        #endregion BtnBewegungsmelder

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}