using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            VerbindungErstellen();
        }

        private void ButtonDisconnect_Click(object sender, RoutedEventArgs e)
        {
            VerbindungTrennen();
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

