using Kommunikation;
using System.Windows;

namespace Tiefgarage
{
    public partial class MainWindow : Window
    {
        private ViewModel.TiefgarageViewModel tiefgarageViewModel;
        private DatenRangieren datenRangieren;
        public S7_1200 s7_1200;

        public MainWindow()
        {
            tiefgarageViewModel = new ViewModel.TiefgarageViewModel(this);
            datenRangieren = new DatenRangieren(this, tiefgarageViewModel);

            InitializeComponent();
            DataContext = tiefgarageViewModel;

            s7_1200 = new S7_1200(1, 2, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}