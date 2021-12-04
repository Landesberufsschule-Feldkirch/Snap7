using _AlleManConfigTesten.Model;

namespace _AlleManConfigTesten;

public partial class MainWindow
{
    public MainWindow()
    {

        var viewModel = new ViewModel.ViewModel();
        var alleManConfigTesten = new ManConfigTesten(viewModel);

        InitializeComponent();
        DataContext = viewModel;

        alleManConfigTesten.AlleConfigEinlesen();

        DataGridAa.ItemsSource = viewModel.ViAnz.AaAlleDaten;
        DataGridAi.ItemsSource = viewModel.ViAnz.AiAlleDaten;
        DataGridDa.ItemsSource = viewModel.ViAnz.DaAlleDaten;
        DataGridDi.ItemsSource = viewModel.ViAnz.DiAlleDaten;
    }
}