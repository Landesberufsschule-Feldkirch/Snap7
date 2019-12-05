using System.Threading.Tasks;

namespace BehälterSteuerung
{
    public partial class MainWindow
    {
        public enum AutomatikModus
        {
            Modus_1234 = 0,
            Modus_1324,
            Modus_1432,
            Modus_4321
        }

        public const double SinkGeschwindigkeit = 0.00003;
        public const double FuellGeschwindigkeit = 0.15 * SinkGeschwindigkeit; // damit alle Behälter gleichzeit leer sein können

        public double PegelOffset_1;
        public double PegelOffset_2;
        public double PegelOffset_3;
        public double PegelOffset_4;

        public const double HoeheFuellBalken = 200.0;

        public bool Tank_1_AutomatischEntleeren;
        public bool Tank_2_AutomatischEntleeren;
        public bool Tank_3_AutomatischEntleeren;
        public bool Tank_4_AutomatischEntleeren;

        public bool AutomatikModusNochAktiv = false;

        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                AutomatikModusNochAktiv = false;

                foreach (Behaelter beh in gAlleBehaelter)
                {
                    beh.PegelUeberwachen();
                    if ((beh.InternerPegel > 0.01)) AutomatikModusNochAktiv = true;
                }

                if (!AutomatikModusNochAktiv) AutomatikKnoepfeAktivieren();

                this.Dispatcher.Invoke(() =>
                                   {
                                       foreach (Behaelter beh in gAlleBehaelter) beh.AnzeigeAktualisieren(FensterAktiv);
                                       AbleitungenAnzeigen(FensterAktiv);
                                   });

                Task.Delay(100);
            }
        }

        public void AutomatikBetriebStarten(AutomatikModus Modus)
        {
            switch (Modus)
            {
                case AutomatikModus.Modus_1234:
                    gAlleBehaelter[0].AutomatikmodusStarten(1.2);
                    gAlleBehaelter[1].AutomatikmodusStarten(2.4);
                    gAlleBehaelter[2].AutomatikmodusStarten(3.6);
                    gAlleBehaelter[3].AutomatikmodusStarten(4.8);
                    break;

                case AutomatikModus.Modus_1324:
                    gAlleBehaelter[0].AutomatikmodusStarten(1.2);
                    gAlleBehaelter[2].AutomatikmodusStarten(2.4);
                    gAlleBehaelter[1].AutomatikmodusStarten(3.6);
                    gAlleBehaelter[3].AutomatikmodusStarten(4.8);
                    break;

                case AutomatikModus.Modus_1432:
                    gAlleBehaelter[0].AutomatikmodusStarten(1.2);
                    gAlleBehaelter[3].AutomatikmodusStarten(2.4);
                    gAlleBehaelter[2].AutomatikmodusStarten(3.6);
                    gAlleBehaelter[1].AutomatikmodusStarten(4.8);
                    break;

                case AutomatikModus.Modus_4321:
                    gAlleBehaelter[3].AutomatikmodusStarten(1.2);
                    gAlleBehaelter[2].AutomatikmodusStarten(2.4);
                    gAlleBehaelter[1].AutomatikmodusStarten(3.6);
                    gAlleBehaelter[0].AutomatikmodusStarten(4.8);
                    break;

                default:
                    gAlleBehaelter[3].AutomatikmodusStarten(0);
                    gAlleBehaelter[2].AutomatikmodusStarten(0);
                    gAlleBehaelter[1].AutomatikmodusStarten(0);
                    gAlleBehaelter[0].AutomatikmodusStarten(0);
                    break;
            }
        }

    }
}
