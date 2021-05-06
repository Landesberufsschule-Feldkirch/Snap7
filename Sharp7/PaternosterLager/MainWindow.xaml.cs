using Kommunikation;
using System;

namespace PaternosterLager
{
    public partial class MainWindow
    {
        public ConfigPlc.Plc ConfigPlc { get; set; }
        public IPlc Plc { get; set; }
        public bool FensterAktiv { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public const double AnzahlKisten = 16;
        public Datenstruktur Datenstruktur { get; set; }

        public DatenRangieren DatenRangieren { get; set; }
        private readonly ViewModel.ViewModel _viewModel;
        private const int AnzByteDigInput = 2;
        private const int AnzByteDigOutput = 2;
        private const int AnzByteAnalogInput = 2;
        private const int AnzByteAnalogOutput = 2;

        public MainWindow()
        {
            const string versionText = "Paternosterlager";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);

            FensterAktiv = true;
            _viewModel = new ViewModel.ViewModel(this, AnzahlKisten);
            DatenRangieren = new DatenRangieren(_viewModel);

            InitializeComponent();
            DataContext = _viewModel;

            var befehlszeile = Environment.GetCommandLineArgs();
            if (befehlszeile.Length == 2 && befehlszeile[1].Contains("CX9020")) Plc = new Cx9020(Datenstruktur, DatenRangieren.Rangieren);
            else Plc = new S71200(Datenstruktur, DatenRangieren.Rangieren);

            DatenRangieren.ReferenzUebergeben(Plc);

            Title = Plc.GetPlcBezeichnung() + ": " + versionText + " " + VersionNummer;

            ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");
        }

        private void PolygonAufPressed(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S1 = true;
        private void PolygonAufReleased(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S1 = false;
        private void PolygonAbPressed(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S2 = true;
        private void PolygonAbReleased(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S2 = false;
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e) => FensterAktiv = false;
    }
}