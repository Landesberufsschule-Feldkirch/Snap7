using System.Windows;
using System.Windows.Controls;

namespace Tiefgarage
{
    public partial class MainWindow : Window
    {
        public bool TaskAktiv;
        public bool DatenRangierenAktiv = true;
        public bool FensterAktiv = true;

        public int FahrzeugPersonGeklickt = -1;

        Logikfunktionen logikfunktionen;
        DatenRangieren datenRangieren;

        public MainWindow()
        {
            logikfunktionen = new Logikfunktionen();
            datenRangieren = new DatenRangieren(logikfunktionen);

            InitializeComponent();

            S7_1200 s7_1200 = new S7_1200(10, 0, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            EinAusgabeFelderInitialisieren();
            AlleFahrzeugePersonenInitialisieren();

            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
            System.Threading.Tasks.Task.Run(() => Logikfunktionen_Task());
            System.Threading.Tasks.Task.Run(() => Display_Task());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var fp = btn.Tag as FahrzeugPerson;
            fp.Losfahren();
        }

        private void AlleDraussenParken_Click(object sender, RoutedEventArgs e)
        {
            AlleDraussenParken();
        }

        private void AlleDrinnenParken_Click(object sender, RoutedEventArgs e)
        {
            AlleDrinnenParken();
        }
    }
}
