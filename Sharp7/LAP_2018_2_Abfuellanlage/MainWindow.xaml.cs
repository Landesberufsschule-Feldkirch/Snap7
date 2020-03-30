using Kommunikation;
using System.Windows;

namespace LAP_2018_2_Abfuellanlage
{
    public partial class MainWindow : Window
    {
        public bool DebugWindowAktiv { get; set; }
        public SetManual.SetManual SetManualWindow { get; set; }
        public S7_1200 S7_1200 { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.ViewModel viewModel;

        public MainWindow()
        {
            viewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(this, viewModel);

            InitializeComponent();
            DataContext = viewModel;

            S7_1200 = new S7_1200(1, 1, 4, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManual(viewModel);
            SetManualWindow.Show();
        }
    }
}