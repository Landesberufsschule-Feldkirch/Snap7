namespace AmpelsteuerungKieswerk.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using Utilities;
    using static Model.LastKraftWagen;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.AlleLastKraftWagen _alleLastKraftWagen;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.AlleLastKraftWagen alleLkw)
        {
            _mainWindow = mw;
            _alleLastKraftWagen = alleLkw;
            DatenRangieren_AmpelChangedEvent(null, new Model.AmpelZustandEventArgs(Model.AmpelZustand.Aus, Model.AmpelZustand.Aus));

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            ColorB1 = "LightGray";
            ColorB2 = "LightGray";
            ColorB3 = "LightGray";
            ColorB4 = "LightGray";

            ColorLinksRot = "White";
            ColorLinksGelb = "White";
            ColorLinksGruen = "White";
            ColorRechtsRot = "White";
            ColorRechtsGelb = "White";
            ColorRechtsGruen = "White";

            PosLkw1Left = 10;
            PosLkw1Top = 10;
            PosLkw2Left = 10;
            PosLkw2Top = 50;
            PosLkw3Left = 10;
            PosLkw3Top = 80;
            PosLkw4Left = 10;
            PosLkw4Top = 100;
            PosLkw5Left = 10;
            PosLkw5Top = 130;

            DirectionLkw1 = 1;
            DirectionLkw2 = 1;
            DirectionLkw3 = 1;
            DirectionLkw4 = 1;
            DirectionLkw5 = 1;

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeB1(_alleLastKraftWagen.B1);
                FarbeB2(_alleLastKraftWagen.B2);
                FarbeB3(_alleLastKraftWagen.B3);
                FarbeB4(_alleLastKraftWagen.B4);

                PositionLkw1(_alleLastKraftWagen.GetPositionLkw(0));
                PositionLkw2(_alleLastKraftWagen.GetPositionLkw(1));
                PositionLkw3(_alleLastKraftWagen.GetPositionLkw(2));
                PositionLkw4(_alleLastKraftWagen.GetPositionLkw(3));
                PositionLkw5(_alleLastKraftWagen.GetPositionLkw(4));

                RichtungLkw1(_alleLastKraftWagen.GetRichtungLkw(0));
                RichtungLkw2(_alleLastKraftWagen.GetRichtungLkw(1));
                RichtungLkw3(_alleLastKraftWagen.GetRichtungLkw(2));
                RichtungLkw4(_alleLastKraftWagen.GetRichtungLkw(3));
                RichtungLkw5(_alleLastKraftWagen.GetRichtungLkw(4));

                VersionNr = _mainWindow.VersionNummer;

                if (_mainWindow.S71200 != null)
                {
                    VersionNr = _mainWindow.VersionNummer;
                    SpsVersionLokal = _mainWindow.VersionInfo;
                    SpsVersionEntfernt = _mainWindow.S71200.GetVersion();                  
                    SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? "hidden" : "visible";

                    SpsColor = _mainWindow.S71200.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = _mainWindow.S71200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        public void DatenRangieren_AmpelChangedEvent(object sender, Model.AmpelZustandEventArgs e)
        {
            FarbeLinksRot(false);
            FarbeLinksGelb(false);
            FarbeLinksGruen(false);

            FarbeRechtsRot(false);
            FarbeRechtsGelb(false);
            FarbeRechtsGruen(false);

            switch (e.AmpelZustandLinks)
            {
                case Model.AmpelZustand.Rot:
                    FarbeLinksRot(true);
                    break;

                case Model.AmpelZustand.RotUndGelb:
                    FarbeLinksRot(true);
                    FarbeLinksGelb(true);
                    break;

                case Model.AmpelZustand.Gelb:
                    FarbeLinksGelb(true);
                    break;

                case Model.AmpelZustand.Gruen:
                    FarbeLinksGruen(true);
                    break;
            }

            switch (e.AmpelZustandRechts)
            {
                case Model.AmpelZustand.Rot:
                    FarbeRechtsRot(true);
                    break;

                case Model.AmpelZustand.RotUndGelb:
                    FarbeRechtsRot(true);
                    FarbeRechtsGelb(true);
                    break;

                case Model.AmpelZustand.Gelb:
                    FarbeRechtsGelb(true);
                    break;

                case Model.AmpelZustand.Gruen:
                    FarbeRechtsGruen(true);
                    break;
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

        #region Color B1

        public void FarbeB1(bool val) => ColorB1 = val ? "Red" : "LightGray";

        private string _colorB1;

        public string ColorB1
        {
            get => _colorB1;
            set
            {
                _colorB1 = value;
                OnPropertyChanged(nameof(ColorB1));
            }
        }

        #endregion Color B1

        #region Color B2

        public void FarbeB2(bool val) => ColorB2 = val ? "Red" : "LightGray";

        private string _colorB2;

        public string ColorB2
        {
            get => _colorB2;
            set
            {
                _colorB2 = value;
                OnPropertyChanged(nameof(ColorB2));
            }
        }

        #endregion Color B2

        #region Color B3

        public void FarbeB3(bool val) => ColorB3 = val ? "Red" : "LightGray";

        private string _colorB3;

        public string ColorB3
        {
            get => _colorB3;
            set
            {
                _colorB3 = value;
                OnPropertyChanged(nameof(ColorB3));
            }
        }

        #endregion Color B3

        #region Color B4

        public void FarbeB4(bool val) => ColorB4 = val ? "Red" : "LightGray";

        private string _colorB4;

        public string ColorB4
        {
            get => _colorB4;
            set
            {
                _colorB4 = value;
                OnPropertyChanged(nameof(ColorB4));
            }
        }

        #endregion Color B4

        #region ColorLinksRot

        public void FarbeLinksRot(bool val) => ColorLinksRot = val ? "Red" : "White";

        private string _colorLinksRot;

        public string ColorLinksRot
        {
            get => _colorLinksRot;
            set
            {
                _colorLinksRot = value;
                OnPropertyChanged(nameof(ColorLinksRot));
            }
        }

        #endregion ColorLinksRot

        #region ColorLinksGelb

        public void FarbeLinksGelb(bool val) => ColorLinksGelb = val ? "Yellow" : "White";

        private string _colorLinksGelb;

        public string ColorLinksGelb
        {
            get => _colorLinksGelb;
            set
            {
                _colorLinksGelb = value;
                OnPropertyChanged(nameof(ColorLinksGelb));
            }
        }

        #endregion ColorLinksGelb

        #region ColorLinksGruen

        public void FarbeLinksGruen(bool val) => ColorLinksGruen = val ? "Green" : "White";

        private string _colorLinksGruen;

        public string ColorLinksGruen
        {
            get => _colorLinksGruen;
            set
            {
                _colorLinksGruen = value;
                OnPropertyChanged(nameof(ColorLinksGruen));
            }
        }

        #endregion ColorLinksGruen

        #region ColorRechtsRot

        public void FarbeRechtsRot(bool val) => ColorRechtsRot = val ? "Red" : "White";

        private string _colorRechtsRot;

        public string ColorRechtsRot
        {
            get => _colorRechtsRot;
            set
            {
                _colorRechtsRot = value;
                OnPropertyChanged(nameof(ColorRechtsRot));
            }
        }

        #endregion ColorRechtsRot

        #region ColorRechtsGelb

        public void FarbeRechtsGelb(bool val) => ColorRechtsGelb = val ? "Yellow" : "White";

        private string _colorRechtsGelb;

        public string ColorRechtsGelb
        {
            get => _colorRechtsGelb;
            set
            {
                _colorRechtsGelb = value;
                OnPropertyChanged(nameof(ColorRechtsGelb));
            }
        }

        #endregion ColorRechtsGelb

        #region ColorRechtsGruen

        public void FarbeRechtsGruen(bool val) => ColorRechtsGruen = val ? "Green" : "White";

        private string _colorRechtsGruen;

        public string ColorRechtsGruen
        {
            get => _colorRechtsGruen;
            set
            {
                _colorRechtsGruen = value;
                OnPropertyChanged(nameof(ColorRechtsGruen));
            }
        }

        #endregion ColorRechtsGruen

        #region RichtungLkw1

        public void RichtungLkw1(LkwRichtungen val)
        {
            if (val == LkwRichtungen.NachRechts) DirectionLkw1 = 1; else DirectionLkw1 = -1;
        }

        private int _directionLkw1;

        public int DirectionLkw1
        {
            get => _directionLkw1;
            set
            {
                _directionLkw1 = value;
                OnPropertyChanged(nameof(DirectionLkw1));
            }
        }

        #endregion RichtungLkw1

        #region RichtungLkw2

        public void RichtungLkw2(LkwRichtungen val)
        {
            if (val == LkwRichtungen.NachRechts) DirectionLkw2 = 1; else DirectionLkw2 = -1;
        }

        private int _directionLkw2;

        public int DirectionLkw2
        {
            get => _directionLkw2;
            set
            {
                _directionLkw2 = value;
                OnPropertyChanged(nameof(DirectionLkw2));
            }
        }

        #endregion RichtungLkw2

        #region RichtungLkw3

        public void RichtungLkw3(LkwRichtungen val)
        {
            if (val == LkwRichtungen.NachRechts) DirectionLkw3 = 1; else DirectionLkw3 = -1;
        }

        private int _directionLkw3;

        public int DirectionLkw3
        {
            get => _directionLkw3;
            set
            {
                _directionLkw3 = value;
                OnPropertyChanged(nameof(DirectionLkw3));
            }
        }

        #endregion RichtungLkw3

        #region RichtungLkw4

        public void RichtungLkw4(LkwRichtungen val)
        {
            if (val == LkwRichtungen.NachRechts) DirectionLkw4 = 1; else DirectionLkw4 = -1;
        }

        private int _directionLkw4;

        public int DirectionLkw4
        {
            get => _directionLkw4;
            set
            {
                _directionLkw4 = value;
                OnPropertyChanged(nameof(DirectionLkw4));
            }
        }

        #endregion RichtungLkw4

        #region RichtungLkw5

        public void RichtungLkw5(LkwRichtungen val)
        {
            if (val == LkwRichtungen.NachRechts) DirectionLkw5 = 1; else DirectionLkw5 = -1;
        }

        private int _directionLkw5;

        public int DirectionLkw5
        {
            get => _directionLkw5;
            set
            {
                _directionLkw5 = value;
                OnPropertyChanged(nameof(DirectionLkw5));
            }
        }

        #endregion RichtungLkw5

        #region PositionLkw1

        public void PositionLkw1(Punkt pos)
        {
            PosLkw1Left = pos.X;
            PosLkw1Top = pos.Y;
        }

        private double _posLkw1Left;

        public double PosLkw1Left
        {
            get => _posLkw1Left;
            set
            {
                _posLkw1Left = value;
                OnPropertyChanged(nameof(PosLkw1Left));
            }
        }

        private double _posLkw1Top;

        public double PosLkw1Top
        {
            get => _posLkw1Top;
            set
            {
                _posLkw1Top = value;
                OnPropertyChanged(nameof(PosLkw1Top));
            }
        }

        #endregion PositionLkw1

        #region PositionLkw2

        public void PositionLkw2(Punkt pos)
        {
            PosLkw2Left = pos.X;
            PosLkw2Top = pos.Y;
        }

        private double _posLkw2Left;

        public double PosLkw2Left
        {
            get => _posLkw2Left;
            set
            {
                _posLkw2Left = value;
                OnPropertyChanged(nameof(PosLkw2Left));
            }
        }

        private double _posLkw2Top;

        public double PosLkw2Top
        {
            get => _posLkw2Top;
            set
            {
                _posLkw2Top = value;
                OnPropertyChanged(nameof(PosLkw2Top));
            }
        }

        #endregion PositionLkw2

        #region PositionLkw3

        public void PositionLkw3(Punkt pos)
        {
            PosLkw3Left = pos.X;
            PosLkw3Top = pos.Y;
        }

        private double _posLkw3Left;

        public double PosLkw3Left
        {
            get => _posLkw3Left;
            set
            {
                _posLkw3Left = value;
                OnPropertyChanged(nameof(PosLkw3Left));
            }
        }

        private double _posLkw3Top;

        public double PosLkw3Top
        {
            get => _posLkw3Top;
            set
            {
                _posLkw3Top = value;
                OnPropertyChanged(nameof(PosLkw3Top));
            }
        }

        #endregion PositionLkw3

        #region PositionLkw4

        public void PositionLkw4(Punkt pos)
        {
            PosLkw4Left = pos.X;
            PosLkw4Top = pos.Y;
        }

        private double _posLkw4Left;

        public double PosLkw4Left
        {
            get => _posLkw4Left;
            set
            {
                _posLkw4Left = value;
                OnPropertyChanged(nameof(PosLkw4Left));
            }
        }

        private double _posLkw4Top;

        public double PosLkw4Top
        {
            get => _posLkw4Top;
            set
            {
                _posLkw4Top = value;
                OnPropertyChanged(nameof(PosLkw4Top));
            }
        }

        #endregion PositionLkw4

        #region PositionLkw5

        public void PositionLkw5(Punkt pos)
        {
            PosLkw5Left = pos.X;
            PosLkw5Top = pos.Y;
        }

        private double _posLkw5Left;

        public double PosLkw5Left
        {
            get => _posLkw5Left;
            set
            {
                _posLkw5Left = value;
                OnPropertyChanged(nameof(PosLkw5Left));
            }
        }

        private double _posLkw5Top;

        public double PosLkw5Top
        {
            get => _posLkw5Top;
            set
            {
                _posLkw5Top = value;
                OnPropertyChanged(nameof(PosLkw5Top));
            }
        }

        #endregion PositionLkw5

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}