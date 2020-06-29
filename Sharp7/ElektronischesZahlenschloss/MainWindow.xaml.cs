using System.Windows;

namespace ElektronischesZahlenschloss
{


    public partial class MainWindow : Window
    {

        public bool FensterAktiv { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.ViewModel viewModel;


        public MainWindow()
        {
            FensterAktiv = true;
            viewModel = new ViewModel.ViewModel(this, AnzahlKisten);
            datenRangieren = new DatenRangieren(this, viewModel);

            InitializeComponent();
            DataContext = viewModel;
            S7_1200 = new S7_1200(2, 2, 2, 2, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            if (System.Diagnostics.Debugger.IsAttached) btnDebugWindow.Visibility = System.Windows.Visibility.Visible;
            else btnDebugWindow.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
