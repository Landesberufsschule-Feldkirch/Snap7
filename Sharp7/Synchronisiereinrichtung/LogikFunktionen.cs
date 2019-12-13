using System.Threading.Tasks;

namespace Synchronisiereinrichtung
{
    public partial class MainWindow
    {

        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {



                this.Dispatcher.Invoke(() =>
                                   {

                                   });

                Task.Delay(100);
            }
        }



    }
}
