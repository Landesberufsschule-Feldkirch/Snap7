using System.Windows.Controls;

namespace _TestProjekt
{
    public partial class MainWindow
    {
        private readonly ViewModel.ViewModel _viewModel;
        public MainWindow()
        {
            _viewModel = new ViewModel.ViewModel();

            InitializeComponent();
            DataContext = _viewModel;

            TextBox.TextChanged += TextBox_TextChanged;
            Slider.ValueChanged += Slider_ValueChanged;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox text) _viewModel.ViAnz.TextFeld = text.Text;
        }

        private void Slider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e) => _viewModel.ViAnz.SliderValue = e.NewValue;

        private void TabAutomatikManualChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            //
        }

        private void BtnNaechsterSchritt(object sender, System.Windows.RoutedEventArgs e)
        {
            //
        }
    }
}