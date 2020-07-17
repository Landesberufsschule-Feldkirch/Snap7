using Kommunikation;
using System.Windows;

namespace BehaelterSteuerung
{
    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly BehaelterSteuerung.ViewModel.ViewModel viewModel;
        public string Versionsinfo { get; set; }
        private readonly int anzByteVersion;
        private const int anzByteDigInput = 1;
        private const int anzByteDigOutput = 1;
        private const int anzByteAnalogInput = 0;
        private const int anzByteAnalogOutput = 0;
        public MainWindow()
        {
            Versionsinfo = "Behaeltersteuerung V1.00";
            anzByteVersion = Versionsinfo.Length;

            viewModel = new BehaelterSteuerung.ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            S7_1200 = new S7_1200(anzByteVersion, anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput,datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}