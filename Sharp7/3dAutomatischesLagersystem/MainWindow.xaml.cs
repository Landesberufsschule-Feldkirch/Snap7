using AutomatischesLagersystem._3D;
using Kommunikation;
using System.Windows;

namespace AutomatischesLagersystem
{
    public partial class MainWindow : Window
    {

        public SetManual.SetManualWindow SetManualWindow { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public S7_1200 S7_1200 { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.ViewModel viewModel;

        public DreiDerstellen DreiD { get; set; }
     
        public MainWindow()
        {
            viewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(this, viewModel);

            InitializeComponent();

            DataContext = viewModel;
            S7_1200 = new S7_1200(2, 2, 2, 2, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            DreiD = new DreiDerstellen(viewPort3d);
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManualWindow(viewModel);
            SetManualWindow.Show();
        }

       
      
    }
}
