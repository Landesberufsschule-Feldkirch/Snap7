using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LAP_2018_Abfuellanlage
{
    public partial class MainWindow
    {

        public void AnzeigeAktualisieren()
        {

            this.Dispatcher.Invoke(() =>
                       {
                           if (FensterAktiv)
                           {


                           }
                       });
        }




    }
}
