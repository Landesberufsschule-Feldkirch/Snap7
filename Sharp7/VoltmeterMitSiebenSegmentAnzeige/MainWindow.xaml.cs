using BeschriftungPlc;
using Kommunikation;

namespace VoltmeterMitSiebenSegmentAnzeige
{
    public partial class MainWindow
    {
        public PlcDaemon PlcDaemon { get; set; }
        public string VersionInfoLokal { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public ConfigPlc.Plc ConfigPlc { get; set; }
        public BeschriftungenPlc BeschriftungenPlc { get; set; }
        public DatenRangieren DatenRangieren { get; set; }



        public MainWindow()
        {
            const string versionText = "VoltmeterMitSiebenSegmentAnzeige";
            const string versionNummer = "V2.0";

            const int anzByteDigInput = 0;
            const int anzByteDigOutput = 6;
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

            /*
            DisplayPlc = new DisplayPlc.DisplayPlc(Datenstruktur, ConfigPlc, BeschriftungenPlc);

            TestAutomat = new TestAutomat.TestAutomat(Datenstruktur, DisplayPlc.EventBeschriftungAktualisieren, BeschriftungenPlc, PlcDaemon.Plc);
            TestAutomat.SetTestConfig("./ConfigTests/");
            TestAutomat.TabItemFuellen(TabItemAutomatischerSoftwareTest, DisplayPlc);
            */
        }
    }
}