using Kommunikation;
using System.Windows;

namespace LAP_2018_4_Niveauregelung
{
    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public SetManual.SetManualWindow SetManualWindow { get; set; }
        public ViewModel.NiveauRegelungViewModel NiveauRegelungViewModel { get; set; }

        private readonly DatenRangieren datenRangieren;

        public MainWindow()
        {
            NiveauRegelungViewModel = new ViewModel.NiveauRegelungViewModel(this);

            datenRangieren = new DatenRangieren(this, NiveauRegelungViewModel);

            InitializeComponent();
            DataContext = NiveauRegelungViewModel;

            S7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManualWindow(NiveauRegelungViewModel);
            SetManualWindow.Show();
        }
    }
}