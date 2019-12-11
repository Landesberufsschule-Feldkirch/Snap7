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
            AlleAmpelnInitialisieren();
            EinAusgabeFelderInitialisieren();
            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
            System.Threading.Tasks.Task.Run(() => Logikfunktionen_Task());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            switch (btn.Name)
            {


                default:
                    break;
            }


        }

        private void AlleLinksParken_Click(object sender, RoutedEventArgs e)
        {
            foreach (LKW lkw in gAlleLKW) { lkw.linksParken(); }
        }

        private void AlleRechtsParken_Click(object sender, RoutedEventArgs e)
        {
            foreach (LKW lkw in gAlleLKW) { lkw.rechtsParken(); }
        }
    }
}

