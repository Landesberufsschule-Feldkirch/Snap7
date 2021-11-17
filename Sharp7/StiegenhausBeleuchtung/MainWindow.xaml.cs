using BeschriftungPlc;
using Kommunikation;
using System.Windows;

namespace StiegenhausBeleuchtung
{
    public partial class MainWindow
    {
        public PlcDaemon PlcDaemon { get; set; }
        public string VersionInfoLokal { get; set; }
        public ConfigPlc.Plc ConfigPlc { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public DisplayPlc.DisplayPlc DisplayPlc { get; set; }
        public BeschriftungenPlc BeschriftungenPlc { get; set; }
        public DatenRangieren DatenRangieren { get; set; }



        public MainWindow()
        {
            const string versionText = "Stiegenhausbeleuchtung";
            const string versionNummer = "V2.0";

            const int anzByteDigInput = 1;
            const int anzByteDigOutput = 1;
            const int anzByteAnalogInput = 0;
            const int anzByteAnalogOutput = 0;

            VersionInfoLokal = versionText + " " + versionNummer;

            Datenstruktur = new Datenstruktur(anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput);
            ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");
            BeschriftungenPlc = new BeschriftungenPlc();

            var viewModel = new ViewModel.ViewModel(this);
            InitializeComponent();
            DataContext = viewModel;

            DatenRangieren = new DatenRangieren(viewModel);
            PlcDaemon = new PlcDaemon(Datenstruktur, DatenRangieren.Rangieren);
            DatenRangieren.ReferenzUebergeben(PlcDaemon.Plc);

            DisplayPlc = new DisplayPlc.DisplayPlc(Datenstruktur, ConfigPlc, BeschriftungenPlc);
        }
        private void PlcButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DisplayPlc.FensterAktiv) DisplayPlc.Schliessen();
            else DisplayPlc.Oeffnen();
        }
    }
}