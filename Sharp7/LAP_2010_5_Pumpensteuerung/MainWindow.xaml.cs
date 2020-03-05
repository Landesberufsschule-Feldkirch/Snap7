using Kommunikation;
using System.Windows;

namespace LAP_2010_5_Pumpensteuerung
{
    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public LAP_2010_5_Pumpensteuerung.SetManual.SetManual SetManualWindow { get; set; }
        public ViewModel.PumpensteuerungViewModel PumpensteuerungViewModel { get; set; }

        private readonly DatenRangieren datenRangieren;

        public MainWindow()
        {
            PumpensteuerungViewModel = new ViewModel.PumpensteuerungViewModel(this);

            datenRangieren = new DatenRangieren(this, PumpensteuerungViewModel);

            InitializeComponent();
            DataContext = PumpensteuerungViewModel;

            S7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            if (System.Diagnostics.Debugger.IsAttached) btnDebugWindow.Visibility = System.Windows.Visibility.Visible;
            else btnDebugWindow.Visibility = System.Windows.Visibility.Hidden;
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new LAP_2010_5_Pumpensteuerung.SetManual.SetManual(PumpensteuerungViewModel);
            SetManualWindow.Show();
        }
    }
}