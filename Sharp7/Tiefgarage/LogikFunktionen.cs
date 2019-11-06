using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tiefgarage
{
    public partial class MainWindow
    {
        public const int BitPos_B1 = 0x0001;
        public const int BitPos_B2 = 0x0002;


        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {


                Task.Delay(100);
            }
        }





        public void AnzeigeAktualisieren()
        {
            this.Dispatcher.Invoke(() =>
            {

                lbl_FahrzeugZaehler.Content = "Autos in der Garage: " + DigOutput[0].ToString();
                lbl_PersonenZaehler.Content = "Personen in der Garage: " + DigOutput[1].ToString();

            });
        }
    }
}
