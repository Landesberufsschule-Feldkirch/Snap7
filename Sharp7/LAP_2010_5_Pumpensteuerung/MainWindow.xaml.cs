using Kommunikation;
using System.Windows;

namespace LAP_2010_5_Pumpensteuerung
{
    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public LAP_2010_5_Pumpensteuerung.SetManual.SetManual SetManualWindow { get; set; }

        public readonly ViewModel.ViewModel viewModel;
        private readonly DatenRangieren datenRangieren;

        public MainWindow()
        {
            viewModel = new ViewModel.ViewModel(this);

            datenRangieren = new DatenRangieren(this, viewModel);

            InitializeComponent();
            DataContext = viewModel;

            S7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            if (System.Diagnostics.Debugger.IsAttached) btnDebugWindow.Visibility = System.Windows.Visibility.Visible;
            else btnDebugWindow.Visibility = System.Windows.Visibility.Hidden;
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new LAP_2010_5_Pumpensteuerung.SetManual.SetManual(viewModel);
            SetManualWindow.Show();
        }
    }
}