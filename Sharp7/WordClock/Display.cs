using Kommunikation;
using System.Threading;

namespace WordClock
{
    public partial class MainWindow
    {
        public void Display_Task(S7_1200 s7_1200, DatenRangieren datenRangieren)
        {
            while (FensterAktiv)
            {
                this.Dispatcher.Invoke(() =>
                {
                    if (FensterAktiv)
                    {
                        AnzeigeAktualisieren(s7_1200, datenRangieren);
                    }
                });
                Thread.Sleep(10);
            }
        }

        public void AnzeigeAktualisieren(S7_1200 s7_1200, DatenRangieren datenRangieren)
        {
            lbl_PlcPing.Content = s7_1200.GetSpsStatus();

            secondHand.Angle = datenRangieren.Sekunde * 6;
            minuteHand.Angle = datenRangieren.Minute * 6;
            hourHand.Angle = (datenRangieren.Stunde % 12) * 30;
        }
    }
}
