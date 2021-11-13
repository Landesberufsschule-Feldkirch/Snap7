using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace Tiefgarage.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using Utilities;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.AlleFahrzeugePersonen _alleFahrzeugePersonen;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.AlleFahrzeugePersonen aFp)
        {
            _mainWindow = mw;
            _alleFahrzeugePersonen = aFp;

            SpsSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            for (var i = 0; i < 100; i++)
            {
                Farbe.Add(Brushes.White);
                Enable.Add(false);
                PosLinks.Add(0);
                PosOben.Add(0);
            }

            AnzahlAutos = "Autos in der Tiefgarage: 123";
            AnzahlPersonen = "Personen in der Tiefgarage: 123";

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }
        private void VisuAnzeigenTask()
        {
            while (true)
            {
                AnzahlAutosInDerTiefgarage(_alleFahrzeugePersonen.AnzahlAutos);
                AnzahlPersonenInDerTiefgarage(_alleFahrzeugePersonen.AnzahlPersonen);

                FarbeUmschalten(_alleFahrzeugePersonen.B1, 1, Brushes.Red, Brushes.LightGray);
                FarbeUmschalten(_alleFahrzeugePersonen.B2, 2, Brushes.Red, Brushes.LightGray);

                for (var i = 0; i < 4; i++)
                {
                    PositionSetzen(_alleFahrzeugePersonen.AllePkwPersonen[i].AktuellePosition, i + 11);
                    PositionSetzen(_alleFahrzeugePersonen.AllePkwPersonen[i + 4].AktuellePosition, i + 21);
                    Enable[11 + i] = _alleFahrzeugePersonen.AllesInParkposition;
                    Enable[21 + i] = _alleFahrzeugePersonen.AllesInParkposition;
                }

 if ( _mainWindow.PlcDaemon != null &&  _mainWindow.PlcDaemon.Plc != null)
                {
                    SpsVersionLokal = _mainWindow.VersionInfoLokal;
                    SpsVersionEntfernt = _mainWindow.PlcDaemon.Plc.GetVersion();
                    SpsSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    SpsColor = _mainWindow.PlcDaemon.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.PlcDaemon.Plc?.GetSpsStatus();

                    FensterTitel = _mainWindow.PlcDaemon.Plc.GetPlcBezeichnung() + ": " + _mainWindow.VersionInfoLokal;
                }         

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
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
                case 11:
                case 12:
                case 13:
                case 14: _alleFahrzeugePersonen.AllePkwPersonen[tasterId - 11].Losfahren(); break;

                case 21:
                case 22:
                case 23:
                case 24: _alleFahrzeugePersonen.AllePkwPersonen[tasterId - 17].Losfahren(); break;

                case 51: _alleFahrzeugePersonen.DraussenParken(); break;
                case 52: _alleFahrzeugePersonen.DrinnenParken(); break;

                default: throw new ArgumentOutOfRangeException(nameof(id));
            }
        }

        #region SPS Version, Status und Farbe
        private string fensterTitel;
        public string FensterTitel
        {
            get => fensterTitel;
            set
            {
                fensterTitel = value;
                OnPropertyChanged(nameof(FensterTitel));
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

        #region Anzahl Autos / Personen
        public void AnzahlAutosInDerTiefgarage(int val) => AnzahlAutos = "Autos in der Tiefgarage: " + val;
        private string _anzahlAutos;
        public string AnzahlAutos
        {
            get => _anzahlAutos;
            set
            {
                _anzahlAutos = value;
                OnPropertyChanged(nameof(AnzahlAutos));
            }
        }

        public void AnzahlPersonenInDerTiefgarage(int val) => AnzahlPersonen = "Personen in der Tiefgarage: " + val;
        private string _anzahlPersonen;
        public string AnzahlPersonen
        {
            get => _anzahlPersonen;
            set
            {
                _anzahlPersonen = value;
                OnPropertyChanged(nameof(AnzahlPersonen));
            }
        }
        #endregion Anzahl Autos / Personen

        #region Enable
        private ObservableCollection<bool> _enable = new();
        public ObservableCollection<bool> Enable
        {
            get => _enable;
            set
            {
                _enable = value;
                OnPropertyChanged(nameof(Enable));
            }
        }
        #endregion Enable

        #region Positionen
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