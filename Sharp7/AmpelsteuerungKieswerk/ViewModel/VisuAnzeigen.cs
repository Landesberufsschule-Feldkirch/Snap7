using AmpelsteuerungKieswerk.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using Utilities;

namespace AmpelsteuerungKieswerk.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly AlleLastKraftWagen _alleLastKraftWagen;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, AlleLastKraftWagen alleLkw)
        {
            _mainWindow = mw;
            _alleLastKraftWagen = alleLkw;

            SpsSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            for (var i = 0; i < 100; i++)
            {
                Farbe.Add(Brushes.White);
                PosLinks.Add(0);
                PosOben.Add(0);
                Richtung.Add(1);
            }

            DatenRangieren_AmpelChangedEvent(null, new AmpelZustandEventArgs(AmpelZustand.Aus, AmpelZustand.Aus));

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }
        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeUmschalten(_alleLastKraftWagen.B1, 1, Brushes.Red, Brushes.LightGray);
                FarbeUmschalten(_alleLastKraftWagen.B2, 2, Brushes.Red, Brushes.LightGray);
                FarbeUmschalten(_alleLastKraftWagen.B3, 3, Brushes.Red, Brushes.LightGray);
                FarbeUmschalten(_alleLastKraftWagen.B4, 4, Brushes.Red, Brushes.LightGray);

                for (var i = 0; i < 5; i++)
                {
                    PositionSetzen(_alleLastKraftWagen.GetPositionLkw(i), 21 + i);
                    RichtungSetzen(_alleLastKraftWagen.GetRichtungLkw(i), 21 + i);
                }

                if (_mainWindow.Plc != null)
                {
                    SpsVersionLokal = _mainWindow.VersionInfoLokal;
                    SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                    SpsSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    SpsColor = _mainWindow.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }
        public void DatenRangieren_AmpelChangedEvent(object sender, AmpelZustandEventArgs e)
        {
            if (_mainWindow == null) return;

            FarbeUmschalten(_alleLastKraftWagen.P7, 17, Brushes.Orange, Brushes.White);
            FarbeUmschalten(_alleLastKraftWagen.P8, 18, Brushes.Orange, Brushes.White);

            switch (e.AmpelZustandLinks)
            {
                case AmpelZustand.Rot:
                    FarbeUmschalten(true, 11, Brushes.Red, Brushes.White);
                    FarbeUmschalten(false, 12, Brushes.Yellow, Brushes.White);
                    FarbeUmschalten(false, 13, Brushes.Green, Brushes.White);
                    break;

                case AmpelZustand.RotUndGelb:
                    FarbeUmschalten(true, 11, Brushes.Red, Brushes.White);
                    FarbeUmschalten(true, 12, Brushes.Yellow, Brushes.White);
                    FarbeUmschalten(false, 13, Brushes.Green, Brushes.White);
                    break;

                case AmpelZustand.Gelb:
                    FarbeUmschalten(false, 11, Brushes.Red, Brushes.White);
                    FarbeUmschalten(true, 12, Brushes.Yellow, Brushes.White);
                    FarbeUmschalten(false, 13, Brushes.Green, Brushes.White);
                    break;

                case AmpelZustand.Gruen:
                    FarbeUmschalten(false, 11, Brushes.Red, Brushes.White);
                    FarbeUmschalten(false, 12, Brushes.Yellow, Brushes.White);
                    FarbeUmschalten(true, 13, Brushes.Green, Brushes.White);
                    break;
                case AmpelZustand.Aus:
                    FarbeUmschalten(false, 11, Brushes.Red, Brushes.White);
                    FarbeUmschalten(false, 12, Brushes.Yellow, Brushes.White);
                    FarbeUmschalten(false, 13, Brushes.Green, Brushes.White);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(e.AmpelZustandLinks.ToString());
            }

            switch (e.AmpelZustandRechts)
            {
                case AmpelZustand.Rot:
                    FarbeUmschalten(true, 14, Brushes.Red, Brushes.White);
                    FarbeUmschalten(false, 15, Brushes.Yellow, Brushes.White);
                    FarbeUmschalten(false, 16, Brushes.Green, Brushes.White);
                    break;

                case AmpelZustand.RotUndGelb:
                    FarbeUmschalten(true, 14, Brushes.Red, Brushes.White);
                    FarbeUmschalten(true, 15, Brushes.Yellow, Brushes.White);
                    FarbeUmschalten(false, 16, Brushes.Green, Brushes.White);
                    break;

                case AmpelZustand.Gelb:
                    FarbeUmschalten(false, 14, Brushes.Red, Brushes.White);
                    FarbeUmschalten(true, 15, Brushes.Yellow, Brushes.White);
                    FarbeUmschalten(false, 16, Brushes.Green, Brushes.White);
                    break;

                case AmpelZustand.Gruen:
                    FarbeUmschalten(false, 14, Brushes.Red, Brushes.White);
                    FarbeUmschalten(false, 15, Brushes.Yellow, Brushes.White);
                    FarbeUmschalten(true, 16, Brushes.Green, Brushes.White);
                    break;
                case AmpelZustand.Aus:
                    FarbeUmschalten(false, 14, Brushes.Red, Brushes.White);
                    FarbeUmschalten(false, 15, Brushes.Yellow, Brushes.White);
                    FarbeUmschalten(false, 16, Brushes.Green, Brushes.White);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(e.AmpelZustandRechts.ToString());
            }
        }

        #region SPS Version, Status und Farbe
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

        private Visibility _spsSichtbar;
        public Visibility SpsSichtbar
        {
            get => _spsSichtbar;
            set
            {
                _spsSichtbar = value;
                OnPropertyChanged(nameof(SpsSichtbar));
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

        internal void RichtungSetzen(LastKraftWagen.LkwRichtungen r, int i)
        {
            Richtung[i] = (int)r;
        }
        internal void PositionSetzen(Punkt pos, int i)
        {
            PosLinks[i] = pos.X;
            PosOben[i] = pos.Y;
        }
        internal void FarbeUmschalten(bool val, int i, Brush farbe1, Brush farbe2) => Farbe[i] = val ? farbe1 : farbe2;
        internal void Taster(object id)
        {
            if (id is not string ascii) return;
            var tasterId = short.Parse(ascii);

            switch (tasterId)
            {
                case 21:
                case 22:
                case 23:
                case 24:
                case 25: _alleLastKraftWagen.LkwLosfahren(tasterId - 21); break;

                case 50: _alleLastKraftWagen.LinksParken(); break;
                case 51: _alleLastKraftWagen.RechtsParken(); break;

                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
        }

        #region Positionen / Richtung
        private ObservableCollection<double> _posLinks = new();
        public ObservableCollection<double> PosLinks
        {
            get => _posLinks;
            set
            {
                _posLinks = value;
                OnPropertyChanged(nameof(PosLinks));
            }
        }

        private ObservableCollection<double> _posOben = new();
        public ObservableCollection<double> PosOben
        {
            get => _posOben;
            set
            {
                _posOben = value;
                OnPropertyChanged(nameof(PosOben));
            }
        }

        private ObservableCollection<int> _richtung = new();
        public ObservableCollection<int> Richtung
        {
            get => _richtung;
            set
            {
                _richtung = value;
                OnPropertyChanged(nameof(Richtung));
            }
        }
        #endregion

        #region Farbe
        private ObservableCollection<Brush> _farbe = new();
        public ObservableCollection<Brush> Farbe
        {
            get => _farbe;
            set
            {
                _farbe = value;
                OnPropertyChanged(nameof(Farbe));
            }
        }
        #endregion

        #region iNotifyPeropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion iNotifyPeropertyChanged Members
    }
}