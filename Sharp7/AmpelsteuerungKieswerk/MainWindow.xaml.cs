using System.Windows;
using System.Windows.Controls;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow : Window
    {
        public bool TaskAktiv;
        public bool DatenRangierenAktiv = true;
        public bool FensterAktiv = true;

        public int FahrzeugPersonGeklickt = -1;

        public MainWindow()
        {
            InitializeComponent();
            AlleLKWInitialisieren();
            EinAusgabeFelderInitialisieren();

            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
            System.Threading.Tasks.Task.Run(() => Logikfunktionen_Task());
            System.Threading.Tasks.Task.Run(() => Display_Task());
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            var lkw = btn.Tag as LKW;
            lkw.Losfahren();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) { FensterAktiv = false; }
        private void AlleLinksParken_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button btn in gAlleButton)
            {
                var lkw = btn.Tag as LKW;
                lkw.LinksParken();
            }
        }
        private void AlleRechtsParken_Click(object sender, RoutedEventArgs e) {
            foreach (Button btn in gAlleButton)
            {
                var lkw = btn.Tag as LKW;
                lkw.RechtsParken();
            }
        }
    }
}

