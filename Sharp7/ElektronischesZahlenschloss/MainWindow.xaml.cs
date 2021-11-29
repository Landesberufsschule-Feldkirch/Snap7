using BeschriftungPlc;
using Kommunikation;

namespace ElektronischesZahlenschloss
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
            const string versionText = "Elektronisches Zahlenschloss";
            const string versionNummer = "V2.0";


            const int anzByteDigInput = 0;
            const int anzByteDigOutput = 2;
            const int anzByteAnalogInput = 1;
            const int anzByteAnalogOutput = 2;

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