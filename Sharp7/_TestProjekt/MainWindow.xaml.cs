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
            if (sender is TextBox text) _viewModel.ViAnzeige.TextFeld = text.Text;
        }

        private void Slider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e) => _viewModel.ViAnzeige.SliderValue = e.NewValue;
    }
}