using System.Threading;

namespace Synchronisiereinrichtung
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

                    }
                });
                Thread.Sleep(10);
            }
        }

    }
}
