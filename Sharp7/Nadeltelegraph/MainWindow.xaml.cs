using Kommunikation;
using System.Windows;

namespace Nadeltelegraph
{
    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.ViewModel nadeltelegraphViewModel;

        public MainWindow()
        {
            nadeltelegraphViewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(nadeltelegraphViewModel);

            InitializeComponent();
            DataContext = nadeltelegraphViewModel;

            S7_1200 = new S7_1200(1, 2, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}