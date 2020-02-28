using Kommunikation;
using System.Windows;

namespace BehaelterSteuerung
{
    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly BehaelterSteuerung.ViewModel.BehaelterViewModel behaelterViewModel;

        public MainWindow()
        {
            behaelterViewModel = new BehaelterSteuerung.ViewModel.BehaelterViewModel(this);
            datenRangieren = new DatenRangieren(behaelterViewModel);

            InitializeComponent();
            DataContext = behaelterViewModel;

            S7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}