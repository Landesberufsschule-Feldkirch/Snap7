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

        public MainWindow()
        {
            InitializeComponent();
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
            /*
            switch (btn.Name)
            {
                case "btn_auto_1": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Auto_1; break;
                case "btn_auto_2": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Auto_2; break;
                case "btn_auto_3": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Auto_3; break;
                case "btn_auto_4": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Auto_4; break;

                case "btn_person_1": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Person_1; break;
                case "btn_person_2": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Person_2; break;
                case "btn_person_3": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Person_3; break;
                case "btn_person_4": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Person_4; break;

                default:
                    break;
            }
            */
            AlleBtnDeaktivieren();
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
