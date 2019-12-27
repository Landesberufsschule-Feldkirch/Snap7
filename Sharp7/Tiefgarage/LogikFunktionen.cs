using System.Threading;

namespace Tiefgarage
{
    public class Logikfunktionen
    {
        public Logikfunktionen() { }
        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                Thread.Sleep(10);
            }
        }
    }
}
