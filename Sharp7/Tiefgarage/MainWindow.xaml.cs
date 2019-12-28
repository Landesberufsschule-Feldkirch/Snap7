using Kommunikation;
using System.Windows;
using System.Windows.Controls;

namespace Tiefgarage
{
    public partial class MainWindow : Window
    {

        public bool FensterAktiv = true;
                public int FahrzeugPersonGeklickt = -1;


        public bool B1;
        public bool B2;

        public int AnzahlFahrzeuge = 0;
        public int AnzahlPersonen = 0;




        Logikfunktionen logikfunktionen;
        DatenRangieren datenRangieren;

        public MainWindow()
        {
            logikfunktionen = new Logikfunktionen(this);
            datenRangieren = new DatenRangieren(this);

            InitializeComponent();

            S7_1200 s7_1200 = new S7_1200(10, 0, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            AlleFahrzeugePersonenInitialisieren();

            System.Threading.Tasks.Task.Run(() => logikfunktionen.Logikfunktionen_Task());
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
