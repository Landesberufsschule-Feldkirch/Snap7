using Kommunikation;

namespace Nadeltelegraph
{
    public partial class MainWindow
    {
        public bool DebugWindowAktiv { get; set; }
        public SetManual.SetManual SetManualWindow { get; set; }
        public Kommunikation.IPlc Plc { get; set; }
        //public S7_1200 S71200 { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }

        private readonly ViewModel.ViewModel _viewModel;
        private DatenRangieren _datenRangieren;

        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 2;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "Nadeltelegraph";
            VersionNummer = "V2.0";
            VersionInfo = versionText + " - " + VersionNummer;

            _viewModel = new ViewModel.ViewModel(this);
            _datenRangieren = new DatenRangieren(_viewModel);

            InitializeComponent();
            DataContext = _viewModel;

            Plc = new S7_1200(VersionInfo.Length, AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);

        }

        private void DebugWindowOeffnen(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Plc.GetModel() == "S7-1200")
            {
                Plc.SetTaskRunning(false);

                Plc = new Kommunikation.Manual(VersionInfo.Length, AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);
            }

            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManual(_viewModel);
            SetManualWindow.Show();
            SetManualWindow.Closing += SetManualWindow_Closing;
        }

        private void SetManualWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}