using Kommunikation;
using System.Windows;

namespace LAP_2010_1_Kompressoranlage
{
    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }

        private readonly string VersionText;
        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.ViewModel viewModel;
        private const int anzByteDigInput = 1;
        private const int anzByteDigOutput = 1;
        private const int anzByteAnalogInput = 0;
        private const int anzByteAnalogOutput = 0;

        public MainWindow()
        {
            VersionText = "LAP 2010/1 Kompressoranlage";
            VersionNummer = "V2.0";
            VersionInfo = VersionText + " - " + VersionNummer;

            viewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            GaugeDruck.DataContext = viewModel;
            GaugeDruck.ApplyTemplate();

            S7_1200 = new S7_1200(VersionInfo.Length, anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}