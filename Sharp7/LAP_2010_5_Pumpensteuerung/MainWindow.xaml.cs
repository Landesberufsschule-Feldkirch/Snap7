using Kommunikation;
using System.Windows;

namespace LAP_2010_5_Pumpensteuerung
{
    public partial class MainWindow
    {
        public S7_1200 S71200 { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public LAP_2010_5_Pumpensteuerung.SetManual.SetManual SetManualWindow { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }

        public readonly ViewModel.ViewModel ViewModel;
        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            var versionText = "LAP 2010/5 Pumpensteuerung";
            VersionNummer = "V2.0";
            VersionInfo = versionText + " - " + VersionNummer;

            ViewModel = new ViewModel.ViewModel(this);

            var datenRangieren = new DatenRangieren(this, ViewModel);

            InitializeComponent();
            DataContext = ViewModel;

            S71200 = new S7_1200(VersionInfo.Length, AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            btnDebugWindow.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new LAP_2010_5_Pumpensteuerung.SetManual.SetManual(ViewModel);
            SetManualWindow.Show();
        }
    }
}