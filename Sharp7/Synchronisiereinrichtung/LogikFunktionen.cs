using System.Threading;
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

                Thread.Sleep(100);
            }
        }



    }
}
