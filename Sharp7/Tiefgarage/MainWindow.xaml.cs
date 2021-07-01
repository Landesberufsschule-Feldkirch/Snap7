using Kommunikation;
using System;

namespace Tiefgarage
{
    public partial class MainWindow
    {
        public IPlc Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public DatenRangieren DatenRangieren { get; set; }

        private readonly ViewModel.ViewModel _viewModel;

        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 2;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "Tiefgarage";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);

            _viewModel = new ViewModel.ViewModel(this);
            DatenRangieren = new DatenRangieren(_viewModel);
            
            InitializeComponent();
            DataContext = _viewModel;

            var befehlszeile = Environment.GetCommandLineArgs();
            Plc = befehlszeile.Length == 2 && befehlszeile[1].Contains("CX9020")
                ? new Cx9020(Datenstruktur, DatenRangieren.Rangieren)
                : new S71200(Datenstruktur, DatenRangieren.Rangieren);

            DatenRangieren.ReferenzUebergeben(Plc);

            Title = Plc.GetPlcBezeichnung() + ": " + versionText + " " + VersionNummer;
        }
    }
}