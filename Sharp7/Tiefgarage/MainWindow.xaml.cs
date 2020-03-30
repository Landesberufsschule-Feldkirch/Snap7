using Kommunikation;
using System.Windows;

namespace Tiefgarage
{
    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }

        private readonly ViewModel.ViewModel tiefgarageViewModel;
        private readonly DatenRangieren datenRangieren;

        public MainWindow()
        {
            tiefgarageViewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(tiefgarageViewModel);

            InitializeComponent();
            DataContext = tiefgarageViewModel;

            S7_1200 = new S7_1200(1, 2, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}