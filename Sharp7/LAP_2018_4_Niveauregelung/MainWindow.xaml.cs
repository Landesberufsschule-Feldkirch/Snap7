using Kommunikation;
using System.Text;
using System.Windows;

namespace LAP_2018_4_Niveauregelung
{
    public partial class MainWindow
    {
        public IPlc Plc { get; set; }
        public ConfigPlc.Plc ConfigPlc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        public DatenRangieren DatenRangieren { get; set; }
        public readonly ViewModel.ViewModel ViewModel;
        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "LAP 2018/4 Niveauregelung";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);

            ViewModel = new ViewModel.ViewModel(this);

            DatenRangieren = new DatenRangieren(ViewModel);

            InitializeComponent();
            DataContext = ViewModel;

            Plc = new S71200(Datenstruktur, DatenRangieren.RangierenInput, DatenRangieren.RangierenOutput);

            ConfigPlc = new ConfigPlc.Plc(Datenstruktur, "./ManualConfig");

            Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.Simulation;
        }
    }
}