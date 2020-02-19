using Kommunikation;
using System.Windows;

namespace Nadeltelegraph
{
    public partial class MainWindow : Window
    {
        private DatenRangieren datenRangieren;
        private ViewModel.NadeltelegraphViewModel nadeltelegraphViewModel;
        public S7_1200 s7_1200;

        public MainWindow()
        {
            nadeltelegraphViewModel = new ViewModel.NadeltelegraphViewModel(this);
            datenRangieren = new DatenRangieren(this, nadeltelegraphViewModel);

            InitializeComponent();
            DataContext = nadeltelegraphViewModel;

             s7_1200 = new S7_1200(1, 2, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}
