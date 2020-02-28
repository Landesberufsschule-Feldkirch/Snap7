using Kommunikation;
using System.Windows;

namespace LAP_2010_2_Transportwagen
{

    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public SetManual.SetManual SetManualWindow { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.TransportwagenViewModel transportwagenViewModel;

        public MainWindow()
        {
            transportwagenViewModel = new ViewModel.TransportwagenViewModel(this);
            datenRangieren = new DatenRangieren(transportwagenViewModel);
            
            InitializeComponent();
            DataContext = transportwagenViewModel;

            S7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManual(transportwagenViewModel);
            SetManualWindow.Show();
        }
    }
}
