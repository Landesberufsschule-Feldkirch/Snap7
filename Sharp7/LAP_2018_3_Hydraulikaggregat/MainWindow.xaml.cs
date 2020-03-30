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

        public MainWindow()
        {
            viewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(this, viewModel);

            InitializeComponent();
            DataContext = viewModel;

            S7_1200 = new S7_1200(2, 2, 2, 2, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManualWindow(viewModel);
            SetManualWindow.Show();
        }
    }
}
