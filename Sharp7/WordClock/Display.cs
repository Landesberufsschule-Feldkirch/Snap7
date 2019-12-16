using System.Threading;

namespace WordClock
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
            secondHand.Angle = Time.Seconds * 6;
            minuteHand.Angle = Time.Minutes * 6;
        }

    }
}
