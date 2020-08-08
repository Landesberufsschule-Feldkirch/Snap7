using Kommunikation;
using System.Windows;

namespace ElektronischesZahlenschloss
{
    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.ViewModel viewModel;
        private const int anzByteDigInput = 2;
        private const int anzByteDigOutput = 2;
        private const int anzByteAnalogInput = 2;
        private const int anzByteAnalogOutput = 2;

        public MainWindow()
        {
            const string versionText = "Elektronisches Zahlenschloss";
            VersionNummer = "V2.0";
            VersionInfo = versionText + " - " + VersionNummer;

            viewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;
            S7_1200 = new S7_1200(VersionInfo.Length, anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}