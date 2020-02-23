using Kommunikation;
using System.Windows;

namespace LAP_2018_Abfuellanlage
{
    public partial class MainWindow : Window
    {

        public bool DebugWindowAktiv;

        public SetManual.SetManual setManualWindow;
        private DatenRangieren datenRangieren;
        private ViewModel.AbfuellanlageViewModel abfuellanlageViewModel;
        public S7_1200 S7_1200 { get; set; } 

        public MainWindow()
        {
            abfuellanlageViewModel = new ViewModel.AbfuellanlageViewModel(this);
            datenRangieren = new DatenRangieren(this, abfuellanlageViewModel);

            InitializeComponent();
            DataContext = abfuellanlageViewModel;

            S7_1200 = new S7_1200(1, 1, 4, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            setManualWindow = new SetManual.SetManual(abfuellanlageViewModel);
            setManualWindow.Show();
        }
    }
}