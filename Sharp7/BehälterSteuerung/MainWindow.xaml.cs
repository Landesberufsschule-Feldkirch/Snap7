using Kommunikation;
using System.Windows;

namespace BehaelterSteuerung
{
    public partial class MainWindow : Window
    {
        private DatenRangieren datenRangieren;
        private BehaelterSteuerung.ViewModel.BehaelterViewModel behaelterViewModel;
        public S7_1200 s7_1200;

        public MainWindow()
        {
            behaelterViewModel = new BehaelterSteuerung.ViewModel.BehaelterViewModel(this);
            datenRangieren = new DatenRangieren(behaelterViewModel);

            InitializeComponent();
            DataContext = behaelterViewModel;

            s7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}