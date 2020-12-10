using _AlleManConfigTesten.Model;

namespace _AlleManConfigTesten
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            
            var viewModel = new ViewModel.ViewModel(this);
var alleManConfigTesten = new AlleManConfigTesten(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            alleManConfigTesten.AlleConfigEinlesen();
        }
    }
}
