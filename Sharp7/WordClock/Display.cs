using Kommunikation;
using System.Threading;

namespace WordClock
{
    public partial class MainWindow
    {
        public void Display_Task(S7_1200 s7_1200, Logikfunktionen logikfunktionen)
        {
            while (FensterAktiv)
            {
                this.Dispatcher.Invoke(() =>
                {
                    if (FensterAktiv) AnzeigeAktualisieren(s7_1200, logikfunktionen);
                });
                Thread.Sleep(10);
            }
        }

        public void AnzeigeAktualisieren(S7_1200 s7_1200, Logikfunktionen logikfunktionen)
        {
            Zeiten zeiten = logikfunktionen.getZeit();
            lbl_PlcPing.Content = s7_1200.GetSpsStatus();

            secondHand.Angle = zeiten.Sekunde * 6;
            minuteHand.Angle = zeiten.Minute * 6;
            hourHand.Angle = zeiten.Stunde * 30 + zeiten.Minute * 0.5;
        }
    }
}
