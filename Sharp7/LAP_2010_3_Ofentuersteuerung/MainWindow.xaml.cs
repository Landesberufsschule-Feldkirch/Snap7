using Kommunikation;
using System.Windows;

namespace LAP_2010_3_Ofentuersteuerung
{
    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public SetManual.SetManual SetManualWindow { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.OfensteuerungViewModel ofensteuerungViewModel;

        public MainWindow()
        {
            ofensteuerungViewModel = new ViewModel.OfensteuerungViewModel(this);
            datenRangieren = new DatenRangieren(ofensteuerungViewModel);

            InitializeComponent();
            DataContext = ofensteuerungViewModel;

            S7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            if (System.Diagnostics.Debugger.IsAttached) btnDebugWindow.Visibility = System.Windows.Visibility.Visible;
            else btnDebugWindow.Visibility = System.Windows.Visibility.Hidden;
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManual(ofensteuerungViewModel);
            SetManualWindow.Show();
        }
    }
}
