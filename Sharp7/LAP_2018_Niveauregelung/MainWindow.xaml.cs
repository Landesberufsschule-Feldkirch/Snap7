using Kommunikation;
using System.Windows;

namespace LAP_2018_Niveauregelung
{
    public partial class MainWindow : Window
    {
        public bool DebugWindowAktiv;
        public SetManual.SetManualWindow setManualWindow;
        public ViewModel.NiveauRegelungViewModel niveauRegelungViewModel;

        private DatenRangieren datenRangieren;
        public S7_1200 s7_1200;

        public MainWindow()
        {
            niveauRegelungViewModel = new ViewModel.NiveauRegelungViewModel(this);

            datenRangieren = new DatenRangieren(this, niveauRegelungViewModel);

            InitializeComponent();
            DataContext = niveauRegelungViewModel;

            s7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            setManualWindow = new SetManual.SetManualWindow(niveauRegelungViewModel);
            setManualWindow.Show();
        }
    }
}