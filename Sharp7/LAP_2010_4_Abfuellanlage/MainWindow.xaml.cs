using System.Text;
using Kommunikation;
using System.Windows;

namespace LAP_2010_4_Abfuellanlage
{
    public partial class MainWindow
    {
        public bool DebugWindowAktiv { get; set; }
        public SetManual.SetManual SetManualWindow { get; set; }
        public IPlc Plc { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        private readonly DatenRangieren _datenRangieren;
        private readonly ViewModel.ViewModel _viewModel;
        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 4;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "LAP 2010/4 Abfuellanlage";
            VersionNummer = "V2.0";
           VersionInfo = versionText + " - " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInput = Encoding.ASCII.GetBytes(VersionInfo)
            };

            _viewModel = new ViewModel.ViewModel(this);
            _datenRangieren = new DatenRangieren(this, _viewModel);

            InitializeComponent();
            DataContext = _viewModel;

            Plc = new S7_1200(Datenstruktur, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);

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