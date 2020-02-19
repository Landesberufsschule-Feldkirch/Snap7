using Kommunikation;
using System.Windows;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow : Window
    {

        private ViewModel.AmpelsteuerungKieswerkViewModel ampelsteuerungKieswerkViewModel;
        private DatenRangieren datenRangieren;
        public S7_1200 s7_1200;

        public MainWindow()
        {
            ampelsteuerungKieswerkViewModel = new ViewModel.AmpelsteuerungKieswerkViewModel(this);
            datenRangieren = new DatenRangieren(this, ampelsteuerungKieswerkViewModel);

            InitializeComponent();
            DataContext = ampelsteuerungKieswerkViewModel;

            s7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }   
    }
}