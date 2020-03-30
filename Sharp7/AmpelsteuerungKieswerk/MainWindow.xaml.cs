using Kommunikation;
using System.Windows;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }

        private readonly ViewModel.ViewModel ampelsteuerungKieswerkViewModel;
        private readonly DatenRangieren datenRangieren;

        public MainWindow()
        {
            ampelsteuerungKieswerkViewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(ampelsteuerungKieswerkViewModel);

            InitializeComponent();
            DataContext = ampelsteuerungKieswerkViewModel;

            S7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}