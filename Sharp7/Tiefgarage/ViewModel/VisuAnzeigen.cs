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

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            ColorB1 = Brushes.LightGray;
            ColorB2 = Brushes.LightGray;

            EnableAuto1 = true;
            EnableAuto2 = true;
            EnableAuto3 = true;
            EnableAuto4 = true;
            EnablePerson1 = true;
            EnablePerson2 = true;
            EnablePerson3 = true;
            EnablePerson4 = true;

            PosAuto1Left = 0;
            PosAuto1Top = 0;
            PosAuto2Left = 0;
            PosAuto2Top = 0;
            PosAuto3Left = 0;
            PosAuto3Top = 0;
            PosAuto4Left = 0;
            PosAuto4Top = 0;

            PosPerson1Left = 0;
            PosPerson1Top = 0;
            PosPerson2Left = 0;
            PosPerson2Top = 0;
            PosPerson3Left = 0;
            PosPerson3Top = 0;
            PosPerson4Left = 0;
            PosPerson4Top = 0;

            AnzahlAutos = "Autos in der Tiefgarage: 123";
            AnzahlPersonen = "Personen in der Tiefgarage: 123";

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeB1(_alleFahrzeugePersonen.B1);
                FarbeB2(_alleFahrzeugePersonen.B2);

                AnzahlAutosInDerTiefgarage(_alleFahrzeugePersonen.AnzahlAutos);
                AnzahlPersonenInDerTiefgarage(_alleFahrzeugePersonen.AnzahlPersonen);

                PositionAuto1(_alleFahrzeugePersonen.AllePkwPersonen[0].AktuellePosition);
                PositionAuto2(_alleFahrzeugePersonen.AllePkwPersonen[1].AktuellePosition);
                PositionAuto3(_alleFahrzeugePersonen.AllePkwPersonen[2].AktuellePosition);
                PositionAuto4(_alleFahrzeugePersonen.AllePkwPersonen[3].AktuellePosition);
                PositionPerson1(_alleFahrzeugePersonen.AllePkwPersonen[4].AktuellePosition);
                PositionPerson2(_alleFahrzeugePersonen.AllePkwPersonen[5].AktuellePosition);
                PositionPerson3(_alleFahrzeugePersonen.AllePkwPersonen[6].AktuellePosition);
                PositionPerson4(_alleFahrzeugePersonen.AllePkwPersonen[7].AktuellePosition);

                EnableAuto1 = _alleFahrzeugePersonen.AllesInParkposition;
                EnableAuto2 = _alleFahrzeugePersonen.AllesInParkposition;
                EnableAuto3 = _alleFahrzeugePersonen.AllesInParkposition;
                EnableAuto4 = _alleFahrzeugePersonen.AllesInParkposition;
                EnablePerson1 = _alleFahrzeugePersonen.AllesInParkposition;
                EnablePerson2 = _alleFahrzeugePersonen.AllesInParkposition;
                EnablePerson3 = _alleFahrzeugePersonen.AllesInParkposition;
                EnablePerson4 = _alleFahrzeugePersonen.AllesInParkposition;

                if (_mainWindow.Plc != null)
                {
                    VersionNr = _mainWindow.VersionNummer;
                    SpsVersionLokal = _mainWindow.VersionInfoLokal;
                    SpsVersionEntfernt = _mainWindow.Plc.GetVersion();
                    SpsVersionsInfoSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                    SpsColor = _mainWindow.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                    SpsStatus = _mainWindow.Plc?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
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

        #region Color B1

        public void FarbeB1(bool val) => ColorB1 = val ? Brushes.Red : Brushes.LightGray;

        private Brush _colorB1;

        public Brush ColorB1
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

        public void FarbeB2(bool val) => ColorB2 = val ? Brushes.Red : Brushes.LightGray;

        private Brush _colorB2;

        public Brush ColorB2
        {
            get => _colorB2;
            set
            {
                _colorB2 = value;
                OnPropertyChanged(nameof(ColorB2));
            }
        }

        #endregion Color B2

        #region EnableAuto1

        private bool _enableAuto1;

        public bool EnableAuto1
        {
            get => _enableAuto1;
            set
            {
                _enableAuto1 = value;
                OnPropertyChanged(nameof(EnableAuto1));
            }
        }

        #endregion EnableAuto1

        #region EnableAuto2

        private bool _enableAuto2;

        public bool EnableAuto2
        {
            get => _enableAuto2;
            set
            {
                _enableAuto2 = value;
                OnPropertyChanged(nameof(EnableAuto2));
            }
        }

        #endregion EnableAuto2

        #region EnableAuto3

        private bool _enableAuto3;

        public bool EnableAuto3
        {
            get => _enableAuto3;
            set
            {
                _enableAuto3 = value;
                OnPropertyChanged(nameof(EnableAuto3));
            }
        }

        #endregion EnableAuto3

        #region EnableAuto4

        private bool _enableAuto4;

        public bool EnableAuto4
        {
            get => _enableAuto4;
            set
            {
                _enableAuto4 = value;
                OnPropertyChanged(nameof(EnableAuto4));
            }
        }

        #endregion EnableAuto4

        #region EnablePerson1

        private bool _enablePerson1;

        public bool EnablePerson1
        {
            get => _enablePerson1;
            set
            {
                _enablePerson1 = value;
                OnPropertyChanged(nameof(EnablePerson1));
            }
        }

        #endregion EnablePerson1

        #region EnablePerson2

        private bool _enablePerson2;

        public bool EnablePerson2
        {
            get => _enablePerson2;
            set
            {
                _enablePerson2 = value;
                OnPropertyChanged(nameof(EnablePerson2));
            }
        }

        #endregion EnablePerson2

        #region EnablePerson3

        private bool _enablePerson3;

        public bool EnablePerson3
        {
            get => _enablePerson3;
            set
            {
                _enablePerson3 = value;
                OnPropertyChanged(nameof(EnablePerson3));
            }
        }

        #endregion EnablePerson3

        #region EnablePerson4

        private bool _enablePerson4;

        public bool EnablePerson4
        {
            get => _enablePerson4;
            set
            {
                _enablePerson4 = value;
                OnPropertyChanged(nameof(EnablePerson4));
            }
        }

        #endregion EnablePerson4

        #region PositionAuto1

        public void PositionAuto1(Punkt pos)
        {
            PosAuto1Left = pos.X;
            PosAuto1Top = pos.Y;
        }

        private double _posAuto1Left;

        public double PosAuto1Left
        {
            get => _posAuto1Left;
            set
            {
                _posAuto1Left = value;
                OnPropertyChanged(nameof(PosAuto1Left));
            }
        }

        private double _posAuto1Top;

        public double PosAuto1Top
        {
            get => _posAuto1Top;
            set
            {
                _posAuto1Top = value;
                OnPropertyChanged(nameof(PosAuto1Top));
            }
        }

        #endregion PositionAuto1

        #region PositionAuto2

        public void PositionAuto2(Punkt pos)
        {
            PosAuto2Left = pos.X;
            PosAuto2Top = pos.Y;
        }

        private double _posAuto2Left;

        public double PosAuto2Left
        {
            get => _posAuto2Left;
            set
            {
                _posAuto2Left = value;
                OnPropertyChanged(nameof(PosAuto2Left));
            }
        }

        private double _posAuto2Top;

        public double PosAuto2Top
        {
            get => _posAuto2Top;
            set
            {
                _posAuto2Top = value;
                OnPropertyChanged(nameof(PosAuto2Top));
            }
        }

        #endregion PositionAuto2

        #region PositionAuto3

        public void PositionAuto3(Punkt pos)
        {
            PosAuto3Left = pos.X;
            PosAuto3Top = pos.Y;
        }

        private double _posAuto3Left;

        public double PosAuto3Left
        {
            get => _posAuto3Left;
            set
            {
                _posAuto3Left = value;
                OnPropertyChanged(nameof(PosAuto3Left));
            }
        }

        private double _posAuto3Top;

        public double PosAuto3Top
        {
            get => _posAuto3Top;
            set
            {
                _posAuto3Top = value;
                OnPropertyChanged(nameof(PosAuto3Top));
            }
        }

        #endregion PositionAuto3

        #region PositionAuto4

        public void PositionAuto4(Punkt pos)
        {
            PosAuto4Left = pos.X;
            PosAuto4Top = pos.Y;
        }

        private double _posAuto4Left;

        public double PosAuto4Left
        {
            get => _posAuto4Left;
            set
            {
                _posAuto4Left = value;
                OnPropertyChanged(nameof(PosAuto4Left));
            }
        }

        private double _posAuto4Top;

        public double PosAuto4Top
        {
            get => _posAuto4Top;
            set
            {
                _posAuto4Top = value;
                OnPropertyChanged(nameof(PosAuto4Top));
            }
        }

        #endregion PositionAuto4

        #region PositionPerson1

        public void PositionPerson1(Punkt pos)
        {
            PosPerson1Left = pos.X;
            PosPerson1Top = pos.Y;
        }

        private double _posPerson1Left;

        public double PosPerson1Left
        {
            get => _posPerson1Left;
            set
            {
                _posPerson1Left = value;
                OnPropertyChanged(nameof(PosPerson1Left));
            }
        }

        private double _posPerson1Top;

        public double PosPerson1Top
        {
            get => _posPerson1Top;
            set
            {
                _posPerson1Top = value;
                OnPropertyChanged(nameof(PosPerson1Top));
            }
        }

        #endregion PositionPerson1

        #region PositionPerson2

        public void PositionPerson2(Punkt pos)
        {
            PosPerson2Left = pos.X;
            PosPerson2Top = pos.Y;
        }

        private double _posPerson2Left;

        public double PosPerson2Left
        {
            get => _posPerson2Left;
            set
            {
                _posPerson2Left = value;
                OnPropertyChanged(nameof(PosPerson2Left));
            }
        }

        private double _posPerson2Top;

        public double PosPerson2Top
        {
            get => _posPerson2Top;
            set
            {
                _posPerson2Top = value;
                OnPropertyChanged(nameof(PosPerson2Top));
            }
        }

        #endregion PositionPerson2

        #region PositionPerson3

        public void PositionPerson3(Punkt pos)
        {
            PosPerson3Left = pos.X;
            PosPerson3Top = pos.Y;
        }

        private double _posPerson3Left;

        public double PosPerson3Left
        {
            get => _posPerson3Left;
            set
            {
                _posPerson3Left = value;
                OnPropertyChanged(nameof(PosPerson3Left));
            }
        }

        private double _posPerson3Top;

        public double PosPerson3Top
        {
            get => _posPerson3Top;
            set
            {
                _posPerson3Top = value;
                OnPropertyChanged(nameof(PosPerson3Top));
            }
        }

        #endregion PositionPerson3

        #region PositionPerson4

        public void PositionPerson4(Punkt pos)
        {
            PosPerson4Left = pos.X;
            PosPerson4Top = pos.Y;
        }

        private double _posPerson4Left;

        public double PosPerson4Left
        {
            get => _posPerson4Left;
            set
            {
                _posPerson4Left = value;
                OnPropertyChanged(nameof(PosPerson4Left));
            }
        }

        private double _posPerson4Top;

        public double PosPerson4Top
        {
            get => _posPerson4Top;
            set
            {
                _posPerson4Top = value;
                OnPropertyChanged(nameof(PosPerson4Top));
            }
        }

        #endregion PositionPerson4

        #region AnzahlAutos

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

        #endregion AnzahlAutos

        #region AnzahlPersonen

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

        #endregion AnzahlPersonen

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}