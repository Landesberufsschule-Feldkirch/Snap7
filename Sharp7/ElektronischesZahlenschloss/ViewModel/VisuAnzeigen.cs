using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ElektronischesZahlenschloss.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.Zahlenschloss _zahlenschloss;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Zahlenschloss zs)
        {
            _mainWindow = mw;
            _zahlenschloss = zs;

            for (var i = 0; i < 100; i++) ClickModeBtn.Add(ClickMode.Press);

            CodeAnzeige = "00000";

            ColorP1 = Colors.White;
            ColorP2 = Colors.White;

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
                FarbeP1(_zahlenschloss.P1);
                FarbeP2(_zahlenschloss.P2);

                CodeAnzeige = _zahlenschloss.CodeAnzeige.ToString("D5");

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

                Thread.Sleep(100);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void Buchstabe(object buchstabe)
        {
            if (!(buchstabe is string ascii)) return;
            var asciiCode = int.Parse(ascii);
            _zahlenschloss.Zeichen = ClickModeButton(asciiCode) ? (char)asciiCode : ' ';
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

        #region Color P1

        public void FarbeP1(bool val) => ColorP1 = val ? Colors.Red : Colors.White;

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

        public void FarbeP2(bool val) => ColorP2 = val ? Colors.LawnGreen : Colors.White;

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

        #region ClickModeAlleButtons

        public bool ClickModeButton(int asciiCode)
        {
            if (ClickModeBtn[asciiCode] ==  ClickMode.Press)
            {
                ClickModeBtn[asciiCode] = ClickMode.Release;
                return true;
            }

            ClickModeBtn[asciiCode] = ClickMode.Press;
            return false;
        }

        private ObservableCollection<System.Windows.Controls.ClickMode> _clickModeBtn = new ObservableCollection<System.Windows.Controls.ClickMode>();
        public ObservableCollection<System.Windows.Controls.ClickMode> ClickModeBtn
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