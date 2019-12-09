using System.Threading.Tasks;

namespace WordClock
{
    public partial class MainWindow
    {
        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
               

                AnzeigeAktualisieren();

                Task.Delay(100);
            }
        }
    }
}
