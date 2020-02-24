namespace BehaelterSteuerung.Model
{
    using System.Collections.Generic;
    using System.Threading;

    public class AlleBehaelter
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
        public  List<Behaelter> alleBehaelter { get; set; }

        private bool AutomatikModusAktiv;

        public AlleBehaelter()
        {
            alleBehaelter = new List<Behaelter>();

            alleBehaelter.Add(new Behaelter(0.2));
            alleBehaelter.Add(new Behaelter(0.4));
            alleBehaelter.Add(new Behaelter(0.6));
            alleBehaelter.Add(new Behaelter(0.8));

            System.Threading.Tasks.Task.Run(() => AlleBehaelterTask());
        }

        private void AlleBehaelterTask()
        {
            bool AutomatikModusNochAktiv;

            while (true)
            {

                AutomatikModusNochAktiv = false;

                foreach (Behaelter beh in alleBehaelter)
                {
                    beh.PegelUeberwachen();
                    if (AutomatikModusAktiv)
                    {
                        if (beh.Pegel < 0.99 && beh.Pegel > 0.01) beh.VentilUnten = true; else beh.VentilUnten = false;
                        if ((beh.Pegel > 0.01)) AutomatikModusNochAktiv = true;
                    }

                }
                if (AutomatikModusAktiv && !AutomatikModusNochAktiv)
                {
                    AlleAutomatikKnoepfeAktivieren();
                    AutomatikModusAktiv = false;
                }
                
                Thread.Sleep(10);
            }
        }

        internal void VentilQ2() { alleBehaelter[0].VentilUntenUmschalten(); }
        internal void VentilQ4() { alleBehaelter[1].VentilUntenUmschalten(); }
        internal void VentilQ6() { alleBehaelter[2].VentilUntenUmschalten(); }
        internal void VentilQ8() { alleBehaelter[3].VentilUntenUmschalten(); }

        internal void Automatik1234() { AutomatikBetriebStarten(AutomatikModus.Modus_1234); }
        internal void Automatik1324() { AutomatikBetriebStarten(AutomatikModus.Modus_1324); }
        internal void Automatik1432() { AutomatikBetriebStarten(AutomatikModus.Modus_1432); }
        internal void Automatik4321() { AutomatikBetriebStarten(AutomatikModus.Modus_4321); }


        private void AlleAutomatikKnoepfeDeaktivieren()
        {
            AlleKnoepfeAktivieren = false;
        }
        private void AlleAutomatikKnoepfeAktivieren()
        {
            AlleKnoepfeAktivieren = true;
        }
        private void AutomatikBetriebStarten(AutomatikModus modus)
        {
            AlleAutomatikKnoepfeDeaktivieren();
            AutomatikModusAktiv = true;
            switch (modus)
            {
                case AutomatikModus.Modus_1234:
                    alleBehaelter[0].AutomatikmodusStarten(1.2);
                    alleBehaelter[1].AutomatikmodusStarten(2.4);
                    alleBehaelter[2].AutomatikmodusStarten(3.6);
                    alleBehaelter[3].AutomatikmodusStarten(4.8);
                    break;

                case AutomatikModus.Modus_1324:
                    alleBehaelter[0].AutomatikmodusStarten(1.2);
                    alleBehaelter[2].AutomatikmodusStarten(2.4);
                    alleBehaelter[1].AutomatikmodusStarten(3.6);
                    alleBehaelter[3].AutomatikmodusStarten(4.8);
                    break;

                case AutomatikModus.Modus_1432:
                    alleBehaelter[0].AutomatikmodusStarten(1.2);
                    alleBehaelter[3].AutomatikmodusStarten(2.4);
                    alleBehaelter[2].AutomatikmodusStarten(3.6);
                    alleBehaelter[1].AutomatikmodusStarten(4.8);
                    break;

                case AutomatikModus.Modus_4321:
                    alleBehaelter[3].AutomatikmodusStarten(1.2);
                    alleBehaelter[2].AutomatikmodusStarten(2.4);
                    alleBehaelter[1].AutomatikmodusStarten(3.6);
                    alleBehaelter[0].AutomatikmodusStarten(4.8);
                    break;

                default:
                    alleBehaelter[0].AutomatikmodusStarten(0);
                    alleBehaelter[1].AutomatikmodusStarten(0);
                    alleBehaelter[2].AutomatikmodusStarten(0);
                    alleBehaelter[3].AutomatikmodusStarten(0);
                    break;

            }

        }
    }
}
