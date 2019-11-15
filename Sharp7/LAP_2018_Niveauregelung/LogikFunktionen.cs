using System.Threading.Tasks;

namespace LAP_2018_Niveauregelung
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
