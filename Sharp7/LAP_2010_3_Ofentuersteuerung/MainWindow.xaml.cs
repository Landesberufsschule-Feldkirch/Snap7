using Kommunikation;
using System.Windows;

namespace LAP_2010_3_Ofentuersteuerung
{
    public partial class MainWindow
    {
        public S7_1200 S71200 { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public SetManual.SetManual SetManualWindow { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }

        private readonly ViewModel.ViewModel _viewModel;
        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "LAP 2010/3 Ofenstuersteuerung";
            VersionNummer = "V2.0";
            VersionInfo = versionText + " - " + VersionNummer;

            _viewModel = new ViewModel.ViewModel(this);
            var datenRangieren = new DatenRangieren(_viewModel);

            InitializeComponent();
            DataContext = _viewModel;

            S71200 = new S7_1200(VersionInfo.Length, AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            BtnDebugWindow.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManual(_viewModel);
            SetManualWindow.Show();
        }
    }
}