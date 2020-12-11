using _AlleManConfigTesten.Model;

namespace _AlleManConfigTesten
{
    public partial class MainWindow
    {
        public MainWindow()
        {

            var viewModel = new ViewModel.ViewModel();
            var alleManConfigTesten = new ManConfigTesten(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            alleManConfigTesten.AlleConfigEinlesen();

            DataGridAa.ItemsSource = viewModel.ViAnzeige.AaAlleDaten;
            DataGridAi.ItemsSource = viewModel.ViAnzeige.AiAlleDaten;
            DataGridDa.ItemsSource = viewModel.ViAnzeige.DaAlleDaten;
            DataGridDi.ItemsSource = viewModel.ViAnzeige.DiAlleDaten;
        }
    }
}
