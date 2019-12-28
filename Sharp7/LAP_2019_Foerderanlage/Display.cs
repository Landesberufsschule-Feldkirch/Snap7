using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LAP_2019_Foerderanlage
{
    public partial class MainWindow
    {

        public void Display_Task()
        {
            while (FensterAktiv)
            {
                this.Dispatcher.Invoke(() =>
                {
                    if (FensterAktiv)
                    {
                        AnzeigeAktualisieren();
                    }
                });
                Thread.Sleep(10);
            }
        }
        public void AnzeigeAktualisieren()
        {
           
        }

      
    }
}
