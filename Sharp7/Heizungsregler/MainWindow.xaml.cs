using Kommunikation;
using System.Windows;

namespace Heizungsregler
{
    public partial class MainWindow
    {
        public bool DebugWindowAktiv { get; set; }

        public S7_1200 S71200 { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }

        public  Model.WohnHaus Heizungsregler { get; set; }
        



        private RealTimeGraphWindow _realTimeGraphWindow;
        private readonly Heizungsregler.ViewModel.ViewModel _viewModel;
        private SetManualWindow _setManualWindow;
        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 20;
        private const int AnzByteAnalogOutput = 4;

        public MainWindow()
        {
            const string versionText = "Heizungsregler";
            VersionNummer = "V2.0";
            VersionInfo = versionText + " - " + VersionNummer;

            _viewModel = new Heizungsregler.ViewModel.ViewModel(this);

            InitializeComponent();
            DataContext = _viewModel;

            Heizungsregler = new Model.WohnHaus(this);


            var datenRangieren = new DatenRangieren(this, _viewModel);

            S71200 = new S7_1200(VersionInfo.Length, AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            btnDebugWindow.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
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
    }
}