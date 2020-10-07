using AutomatischesLagersystem.DreiD;
using Kommunikation;
using System.Windows;
using SharpDX.Text;

namespace AutomatischesLagersystem
{
    public partial class MainWindow
    {
        public IPlc Plc { get; set; }
        public DreiDElemente[] BediengeraetStartpositionen { get; set; }
        public DreiDElemente[] KistenStartPositionen { get; set; }
        public DreiDKisten[] KistenAktuellePositionen { get; set; }
        public SetManual.SetManualWindow SetManualWindow { get; set; }
        public bool KisteLiegtAufDemRegalbediengeraet { get; set; }
        public Model.RegalBedienGeraet RegalBedienGeraet { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public IPlc S71200 { get; set; }
        public bool FensterAktiv { get; set; }
        public DreiDErstellen DreiD { get; set; }
        public int[] DreiDModelleIds { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }

        public Datenstruktur Datenstruktur { get; set; }

        private readonly DatenRangieren _datenRangieren;
        private readonly ViewModel.ViewModel _viewModel;
        private const int AnzByteDigInput = 2;
        private const int AnzByteDigOutput = 2;
        private const int AnzByteAnalogInput = 2;
        private const int AnzByteAnalogOutput = 2;

        public MainWindow()
        {
            const string versionText = "3D Automatisches Lagersystem";
            VersionNummer = "V2.0";
           VersionInfo = versionText + " - " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInput = Encoding.ASCII.GetBytes(VersionInfo)
            };


           
            

            FensterAktiv = true;
            KisteLiegtAufDemRegalbediengeraet = false;
            BediengeraetStartpositionen = new DreiDElemente[4];
            KistenStartPositionen = new DreiDElemente[100];
            KistenAktuellePositionen = new DreiDKisten[100];
            DreiDModelleIds = new int[ViewModel.IdEintraege.AnzahlEintraege];

            RegalBedienGeraet = new Model.RegalBedienGeraet();

            _viewModel = new ViewModel.ViewModel(this);
            var datenRangieren = new DatenRangieren(this, _viewModel);

            InitializeComponent();

            DataContext = _viewModel;
            Plc = new S7_1200(Datenstruktur, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);

            DreiD = new DreiDErstellen(this);

            btnDebugWindow.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManualWindow(_viewModel);
            SetManualWindow.Show();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e) => FensterAktiv = false;
    }
}