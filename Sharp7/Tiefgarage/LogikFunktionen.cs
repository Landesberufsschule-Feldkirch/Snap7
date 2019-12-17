using System.Threading;

namespace Tiefgarage
{
    public partial class MainWindow
    {
        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                Thread.Sleep(10);
            }
        }
    }
}
