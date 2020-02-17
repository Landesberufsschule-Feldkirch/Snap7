using System.Windows;

namespace Nadeltelegraph
{
    public partial class MainWindow : Window
    {


        private DatenRangieren datenRangieren;
        private ViewModel.NadeltelegraphViewModel nadeltelegraphViewModel;

        public MainWindow()
        {

            nadeltelegraphViewModel = new ViewModel.NadeltelegraphViewModel(this);
            datenRangieren = new DatenRangieren(this, nadeltelegraphViewModel);

            InitializeComponent();
            DataContext = nadeltelegraphViewModel;
        }
    }
}
