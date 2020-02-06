using Kommunikation;
using System.Windows;

namespace LAP_2018_Abfuellanlage
{
    public partial class MainWindow : Window
    {

        public bool DebugWindowAktiv;

        private SetManual.SetManual setManualWindow;
        private DatenRangieren datenRangieren;
        private ViewModel.AbfuellanlageViewModel abfuellanlageViewModel;

        public MainWindow()
        {
            abfuellanlageViewModel = new ViewModel.AbfuellanlageViewModel(this);
            datenRangieren = new DatenRangieren(this, abfuellanlageViewModel);

            InitializeComponent();
            DataContext = abfuellanlageViewModel;

            S7_1200 s7_1200 = new S7_1200(2, 2, 4, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            setManualWindow = new SetManual.SetManual(abfuellanlageViewModel);
            setManualWindow.Show();
        }
    }
}