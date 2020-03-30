using Kommunikation;
using System.Windows;

namespace StiegenhausBeleuchtung
{
    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.ViewModel viewModel;

        public MainWindow()
        {
            viewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            S7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}