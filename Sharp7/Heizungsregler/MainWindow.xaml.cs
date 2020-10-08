using Heizungsregler.Model;
using Heizungsregler.SetManual;
using Kommunikation;
using System.Text;
using System.Windows;

namespace Heizungsregler
{
    public partial class MainWindow
    {
        public bool DebugWindowAktiv { get; set; }
        public IPlc Plc { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }
        public WohnHaus WohnHaus { get; set; }

        private SetManualWindow _setManualWindow;
        private RealTimeGraphWindow _realTimeGraphWindow;
        private readonly Heizungsregler.ViewModel.ViewModel _viewModel;
        public Datenstruktur Datenstruktur { get; set; }

        private readonly DatenRangieren _datenRangieren;

        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 20;
        private const int AnzByteAnalogOutput = 4;

        public MainWindow()
        {
            const string versionText = "Heizungsregler";
            VersionNummer = "V2.0";
            VersionInfo = versionText + " - " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInput = Encoding.ASCII.GetBytes(VersionInfo)
            };

            _viewModel = new ViewModel.ViewModel(this);

            InitializeComponent();
            DataContext = _viewModel;

            WohnHaus = new WohnHaus();

            _datenRangieren = new DatenRangieren(this, _viewModel);

            Plc = new S7_1200(Datenstruktur, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);

            BtnDebugWindow.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            _setManualWindow = new SetManualWindow(_viewModel);
            _setManualWindow.Show();
        }

        private void GraphWindow_Click(object sender, RoutedEventArgs e)
        {
            _realTimeGraphWindow = new RealTimeGraphWindow(_viewModel);
            _realTimeGraphWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => Application.Current.Shutdown();

    }
}