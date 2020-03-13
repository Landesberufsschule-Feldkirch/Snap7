using Kommunikation;
using System.Windows;

namespace StiegenhausBeleuchtung
{
    public partial class MainWindow : Window
    {

        public S7_1200 S7_1200 { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.StiegenhausBeleuchtungViewModel stiegenhausBeleuchtungViewModel;

        public MainWindow()
        {
            stiegenhausBeleuchtungViewModel = new ViewModel.StiegenhausBeleuchtungViewModel(this);
            datenRangieren = new DatenRangieren(stiegenhausBeleuchtungViewModel);

            InitializeComponent();
            DataContext = stiegenhausBeleuchtungViewModel;

            S7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}
