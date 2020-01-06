using Kommunikation;
using System.Windows;

namespace LAP_2019_Foerderanlage
{



    public partial class MainWindow : Window
    {

        public bool FensterAktiv = true;

        Logikfunktionen logikfunktionen;
        DatenRangieren datenRangieren;
        public MainWindow()
        {
            logikfunktionen = new Logikfunktionen(this);
            datenRangieren = new DatenRangieren(this);

            InitializeComponent();

            S7_1200 s7_1200 = new S7_1200(10, 0, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            System.Threading.Tasks.Task.Run(() => logikfunktionen.Logikfunktionen_Task());
            System.Threading.Tasks.Task.Run(() => Display_Task());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }

        private void WagenNachLinks_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WagenNachRechts_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
