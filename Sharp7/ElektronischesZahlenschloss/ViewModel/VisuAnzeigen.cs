namespace ElektronischesZahlenschloss.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Zahlenschloss zahlenschloss;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Zahlenschloss zs)
        {
            mainWindow = mw;
            zahlenschloss = zs;

            for (int i = 0; i < 100; i++) ClickModeBtn.Add("Press");

            CodeAnzeige = "00000";

            ColorP1 = "White";
            ColorP2 = "White";

            SpsVersionsInfo = true;
            SpsStatus = "x";
            SpsColor = "LightBlue";


            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeP1(zahlenschloss.P1);
                FarbeP2(zahlenschloss.P2);

                CodeAnzeige = zahlenschloss.CodeAnzeige.ToString("D5");

                if (mainWindow.S7_1200 != null)
                {
                    string vInfo = mainWindow.S7_1200.GetVersion();
                    SpsVersionsInfo = mainWindow.Versionsinfo == vInfo;

                    SpsColor = mainWindow.S7_1200.GetSpsError() ? "Red" : "LightGray";
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
                if (ClickModeButton(asciiCode)) zahlenschloss.Zeichen = asciiCode; else zahlenschloss.Zeichen = ' ';
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


        #region Color P1
        public void FarbeP1(bool val) { if (val) ColorP1 = "Red"; else ColorP1 = "White"; }

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

        #region Color P2

        public void FarbeP2(bool val) { if (val) ColorP2 = "LawnGreen"; else ColorP2 = "White"; }

        private string _colorP2;

        public string ColorP2
        {
            get => _colorP2;
            set
            {
                _colorP2 = value;
                OnPropertyChanged(nameof(ColorP2));
            }
        }

        #endregion Color P2


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


        #region CodeAnzeige

        private string _codeAnzeige;

        public string CodeAnzeige
        {
            get => _codeAnzeige;
            set
            {
                _codeAnzeige = value;
                OnPropertyChanged(nameof(CodeAnzeige));
            }
        }

        #endregion CodeAnzeige


        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}