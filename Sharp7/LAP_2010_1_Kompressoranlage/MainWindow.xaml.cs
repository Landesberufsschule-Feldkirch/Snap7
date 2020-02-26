using Kommunikation;
using System.Windows;

namespace LAP_2010_1_Kompressoranlage
{

    public partial class MainWindow : Window
    {

        public S7_1200 S7_1200 { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.KompressoranlageViewModel kompressoranlageViewModel;

        public MainWindow()
        {
            kompressoranlageViewModel = new ViewModel.KompressoranlageViewModel(this);
            datenRangieren = new DatenRangieren(this, kompressoranlageViewModel);

            InitializeComponent();
            DataContext = kompressoranlageViewModel;

            GaugeDruck.DataContext = kompressoranlageViewModel;
            GaugeDruck.ApplyTemplate();


            S7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}
