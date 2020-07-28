using Kommunikation;
using System.Windows;

namespace LAP_2018_4_Niveauregelung
{
    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public SetManual.SetManualWindow SetManualWindow { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }

        private readonly string VersionText;
        public readonly ViewModel.ViewModel viewModel;
        private readonly DatenRangieren datenRangieren;
        private const int anzByteDigInput = 1;
        private const int anzByteDigOutput = 1;
        private const int anzByteAnalogInput = 0;
        private const int anzByteAnalogOutput = 0;

        public MainWindow()
        {
            VersionText = "LAP 2018/4 Niveauregelung";
            VersionNummer = "V2.0";
            VersionInfo = VersionText + " - " + VersionNummer;

            viewModel = new ViewModel.ViewModel(this);

            datenRangieren = new DatenRangieren(this, viewModel);

            InitializeComponent();
            DataContext = viewModel;

            S7_1200 = new S7_1200(VersionInfo.Length, anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            if (System.Diagnostics.Debugger.IsAttached) btnDebugWindow.Visibility = System.Windows.Visibility.Visible;
            else btnDebugWindow.Visibility = System.Windows.Visibility.Hidden;
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManualWindow(viewModel);
            SetManualWindow.Show();
        }
    }
}