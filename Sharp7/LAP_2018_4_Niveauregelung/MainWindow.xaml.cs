using Kommunikation;
using System.Windows;

namespace LAP_2018_4_Niveauregelung
{
    public partial class MainWindow
    {
        public S7_1200 S71200 { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public SetManual.SetManualWindow SetManualWindow { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }

        public readonly ViewModel.ViewModel ViewModel;
        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "LAP 2018/4 Niveauregelung";
            VersionNummer = "V2.0";
            VersionInfo = versionText + " - " + VersionNummer;

            ViewModel = new ViewModel.ViewModel(this);

            var datenRangieren = new DatenRangieren(this, ViewModel);

            InitializeComponent();
            DataContext = ViewModel;

            S71200 = new S7_1200(VersionInfo.Length, AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            BtnDebugWindow.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManualWindow(ViewModel);
            SetManualWindow.Show();
        }
    }
}