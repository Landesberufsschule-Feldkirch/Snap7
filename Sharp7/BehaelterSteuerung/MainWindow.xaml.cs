using BehaelterSteuerung.Model;
using Kommunikation;

namespace BehaelterSteuerung
{
    public partial class MainWindow
    {
        public S71200.BetriebsartProjekt BetriebsartProjekt { get; set; }
        public IPlc Plc { get; set; }
        public string VersionInfoLokalLokal { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            BetriebsartProjekt = S71200.BetriebsartProjekt.LaborPlatte;
            
            var johnsonTrotter = new JohnsonTrotter();

            const string versionText = "Behaeltersteuerung";
            VersionNummer = "V2.0";
            VersionInfoLokalLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);

            var viewModel = new ViewModel.ViewModel(this);
            var datenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            foreach (var p in johnsonTrotter.GetPermutations())
            {
                ComboBoxPermutationen.Items.Add(p.GetText());
            }

            Plc = new S71200(Datenstruktur, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}