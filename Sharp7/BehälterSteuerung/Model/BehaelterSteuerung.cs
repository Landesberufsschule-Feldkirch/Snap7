namespace BehaelterSteuerung.Model
{
    using System.Collections.Generic;
    using System.Threading;

    public class BehaelterSteuerung
    {
        public enum AutomatikModus
        {
            Modus_1234 = 0,
            Modus_1324,
            Modus_1432,
            Modus_4321
        }

        public bool AlleKnoepfeAktivieren { get; set; }
        public bool P1 { get; set; }
        public List<Behaelter> AlleBehaelter { get; set; }

        public bool AutomatikModusAktiv { get; set; }

        public BehaelterSteuerung()
        {
            AlleBehaelter = new List<Behaelter>
            {
                new Behaelter(0.2),
                new Behaelter(0.4),
                new Behaelter(0.6),
                new Behaelter(0.8)
            };

            System.Threading.Tasks.Task.Run(() => AlleBehaelterTask());
        }

        private void AlleBehaelterTask()
        {
            bool AutomatikModusNochAktiv;

            while (true)
            {

                AutomatikModusNochAktiv = false;

                foreach (Behaelter beh in AlleBehaelter)
                {
                    beh.PegelUeberwachen();
                    if (AutomatikModusAktiv)
                    {
                        if (beh.AutomatikModus()) AutomatikModusNochAktiv = true;
                    }

                }
                if (AutomatikModusAktiv && !AutomatikModusNochAktiv)
                {
                    AutomatikModusAktiv = false;
                }

                Thread.Sleep(10);
            }
        }

        internal void VentilQ2() { AlleBehaelter[0].VentilUntenUmschalten(); }
        internal void VentilQ4() { AlleBehaelter[1].VentilUntenUmschalten(); }
        internal void VentilQ6() { AlleBehaelter[2].VentilUntenUmschalten(); }
        internal void VentilQ8() { AlleBehaelter[3].VentilUntenUmschalten(); }

        internal void Automatik1234() { AutomatikBetriebStarten(AutomatikModus.Modus_1234); }
        internal void Automatik1324() { AutomatikBetriebStarten(AutomatikModus.Modus_1324); }
        internal void Automatik1432() { AutomatikBetriebStarten(AutomatikModus.Modus_1432); }
        internal void Automatik4321() { AutomatikBetriebStarten(AutomatikModus.Modus_4321); }


        private void AutomatikBetriebStarten(AutomatikModus modus)
        {
            AutomatikModusAktiv = true;
            switch (modus)
            {
                case AutomatikModus.Modus_1234:
                    AlleBehaelter[0].AutomatikmodusStarten(1.2);
                    AlleBehaelter[1].AutomatikmodusStarten(2.4);
                    AlleBehaelter[2].AutomatikmodusStarten(3.6);
                    AlleBehaelter[3].AutomatikmodusStarten(4.8);
                    break;

                case AutomatikModus.Modus_1324:
                    AlleBehaelter[0].AutomatikmodusStarten(1.2);
                    AlleBehaelter[2].AutomatikmodusStarten(2.4);
                    AlleBehaelter[1].AutomatikmodusStarten(3.6);
                    AlleBehaelter[3].AutomatikmodusStarten(4.8);
                    break;

                case AutomatikModus.Modus_1432:
                    AlleBehaelter[0].AutomatikmodusStarten(1.2);
                    AlleBehaelter[3].AutomatikmodusStarten(2.4);
                    AlleBehaelter[2].AutomatikmodusStarten(3.6);
                    AlleBehaelter[1].AutomatikmodusStarten(4.8);
                    break;

                case AutomatikModus.Modus_4321:
                    AlleBehaelter[3].AutomatikmodusStarten(1.2);
                    AlleBehaelter[2].AutomatikmodusStarten(2.4);
                    AlleBehaelter[1].AutomatikmodusStarten(3.6);
                    AlleBehaelter[0].AutomatikmodusStarten(4.8);
                    break;

                default:
                    AlleBehaelter[0].AutomatikmodusStarten(0);
                    AlleBehaelter[1].AutomatikmodusStarten(0);
                    AlleBehaelter[2].AutomatikmodusStarten(0);
                    AlleBehaelter[3].AutomatikmodusStarten(0);
                    break;
            }
        }
    }
}
