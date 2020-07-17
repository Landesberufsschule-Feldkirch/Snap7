using Kommunikation;
using System.Windows;

namespace LAP_2018_3_Hydraulikaggregat
{
    public partial class MainWindow : Window
    {
        public LAP_2018_3_Hydraulikaggregat.SetManual.SetManualWindow SetManualWindow { get; set; }
        public bool DebugWindowAktiv { get; set; }

        public S7_1200 S7_1200 { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly LAP_2018_3_Hydraulikaggregat.ViewModel.ViewModel viewModel;
        public string Versionsinfo { get; set; }
        private readonly int anzByteVersion;
        private const int anzByteDigInput = 2;
        private const int anzByteDigOutput = 2;
        private const int anzByteAnalogInput = 2;
        private const int anzByteAnalogOutput = 2;
        public MainWindow()
        {
            Versionsinfo = "LAP 2018/3 Hydraulikaggregat V1.00";
            anzByteVersion = Versionsinfo.Length;
            
            viewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(this, viewModel);

            InitializeComponent();
            DataContext = viewModel;

            S7_1200 = new S7_1200(anzByteVersion, anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

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