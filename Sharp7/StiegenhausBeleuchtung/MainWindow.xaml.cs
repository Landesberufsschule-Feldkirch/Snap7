using BeschriftungPlc;
using Kommunikation;
using System;
using System.Windows;

namespace StiegenhausBeleuchtung
{
    public partial class MainWindow
    {
        public IPlc Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public ConfigPlc.Plc ConfigPlc { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public DisplayPlc.DisplayPlc DisplayPlc { get; set; }
        public BeschriftungenPlc BeschriftungenPlc { get; set; }
        public DatenRangieren DatenRangieren { get; set; }

        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "Stiegenhausbeleuchtung";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);

            var viewModel = new ViewModel.ViewModel(this);
            DatenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");
            BeschriftungenPlc = new BeschriftungenPlc();

            var befehlszeile = Environment.GetCommandLineArgs();
            Plc = befehlszeile.Length == 2 && befehlszeile[1].Contains("CX9020")
                ? new Cx9020(Datenstruktur, DatenRangieren.Rangieren)
                : new S71200(Datenstruktur, DatenRangieren.Rangieren);

            DatenRangieren.ReferenzUebergeben(Plc);

            Title = Plc.GetPlcBezeichnung() + ": " + versionText + " " + VersionNummer;

            DisplayPlc = new DisplayPlc.DisplayPlc(Datenstruktur, ConfigPlc, BeschriftungenPlc);
        }
        private void PlcButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DisplayPlc.FensterAktiv) DisplayPlc.Schliessen();
            else DisplayPlc.Oeffnen();
        }
    }
}