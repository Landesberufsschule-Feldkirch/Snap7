using Kommunikation;
using System.Text;
using System.Windows;

namespace ElektronischesZahlenschloss
{
    public partial class MainWindow
    {
        public S71200 Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public ConfigPlc.Plc ConfigPlc { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        public DatenRangieren DatenRangieren { get; set; }
        private const int AnzByteDigInput = 0;
        private const int AnzByteDigOutput = 2;
        private const int AnzByteAnalogInput = 1;
        private const int AnzByteAnalogOutput = 2;

        public MainWindow()
        {
            const string versionText = "Elektronisches Zahlenschloss";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);

            var viewModel = new ViewModel.ViewModel(this);
            DatenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;
            Plc = new S71200(Datenstruktur, DatenRangieren.RangierenInput, DatenRangieren.RangierenOutput);

            ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");

            Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.Simulation;
        }
    }
}