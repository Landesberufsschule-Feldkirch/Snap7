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


namespace LAP_2018_Niveauregelung
{
    public partial class MainWindow : Window
    {

        public bool DatenRangierenAktiv = true;
        public bool FensterAktiv = true;


        public MainWindow()
        {
            InitializeComponent();
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


        private void VentilClick(object sender, RoutedEventArgs e)
        {
            Y1 = !Y1;
        }


        private void BtnF1_Click(object sender, RoutedEventArgs e)
        {
            F1 = !F1;
        }

        private void BtnF2_Click(object sender, RoutedEventArgs e)
        {
            F2 = !F2;
        }
    }
}
