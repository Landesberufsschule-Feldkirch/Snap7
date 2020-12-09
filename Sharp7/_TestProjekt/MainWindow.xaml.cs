namespace _TestProjekt
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            var viewModel = new ViewModel.ViewModel();

            InitializeComponent();
            DataContext = viewModel;
        }
    }
}