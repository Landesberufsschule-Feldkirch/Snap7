namespace BehaelterSteuerung.Model
{
    using System.Collections.Generic;
    using System.Threading;

    // ReSharper disable once UnusedMember.Global
    public class AlleBehaelter
    {
        public enum AutomatikModus
        {
            Modus1234 = 0,
            Modus1324,
            Modus1432,
            Modus4321
        }

        public bool AlleKnoepfeAktivieren { get; set; }
        public bool P1 { get; set; }
        public List<Behaelter> AlleMeineBehaelter { get; set; }

        private bool _automatikModusAktiv;

        public AlleBehaelter()
        {
            AlleMeineBehaelter = new List<Behaelter>
            {
                new Behaelter(0.2), new Behaelter(0.4), new Behaelter(0.6), new Behaelter(0.8)
            };


            System.Threading.Tasks.Task.Run(AlleBehaelterTask);
        }

        private void AlleBehaelterTask()
        {
            while (true)
            {

                var automatikModusNochAktiv = false;

                foreach (var beh in AlleMeineBehaelter)
                {
                    beh.PegelUeberwachen();
                    if (!_automatikModusAktiv) continue;
                    beh.VentilUnten = beh.Pegel < 0.99 && beh.Pegel > 0.01;
                    if (beh.Pegel > 0.01) automatikModusNochAktiv = true;

                }
                if (_automatikModusAktiv && !automatikModusNochAktiv)
                {
                    AlleAutomatikKnoepfeAktivieren();
                    _automatikModusAktiv = false;
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        // ReSharper disable once UnusedMember.Global
        internal void VentilQ2() => AlleMeineBehaelter[0].VentilUntenUmschalten();
        // ReSharper disable once UnusedMember.Global
        internal void VentilQ4() => AlleMeineBehaelter[1].VentilUntenUmschalten();
        // ReSharper disable once UnusedMember.Global
        internal void VentilQ6() => AlleMeineBehaelter[2].VentilUntenUmschalten();
        // ReSharper disable once UnusedMember.Global
        internal void VentilQ8() => AlleMeineBehaelter[3].VentilUntenUmschalten();

        // ReSharper disable once UnusedMember.Global
        internal void Automatik1234() => AutomatikBetriebStarten(AutomatikModus.Modus1234);
        // ReSharper disable once UnusedMember.Global
        internal void Automatik1324() => AutomatikBetriebStarten(AutomatikModus.Modus1324);

        // ReSharper disable once UnusedMember.Global
        internal void Automatik1432() => AutomatikBetriebStarten(AutomatikModus.Modus1432);
        // ReSharper disable once UnusedMember.Global
        internal void Automatik4321() => AutomatikBetriebStarten(AutomatikModus.Modus4321);


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
            _automatikModusAktiv = true;
            switch (modus)
            {
                case AutomatikModus.Modus1234:
                    AlleMeineBehaelter[0].AutomatikmodusStarten(1.2);
                    AlleMeineBehaelter[1].AutomatikmodusStarten(2.4);
                    AlleMeineBehaelter[2].AutomatikmodusStarten(3.6);
                    AlleMeineBehaelter[3].AutomatikmodusStarten(4.8);
                    break;

                case AutomatikModus.Modus1324:
                    AlleMeineBehaelter[0].AutomatikmodusStarten(1.2);
                    AlleMeineBehaelter[2].AutomatikmodusStarten(2.4);
                    AlleMeineBehaelter[1].AutomatikmodusStarten(3.6);
                    AlleMeineBehaelter[3].AutomatikmodusStarten(4.8);
                    break;

                case AutomatikModus.Modus1432:
                    AlleMeineBehaelter[0].AutomatikmodusStarten(1.2);
                    AlleMeineBehaelter[3].AutomatikmodusStarten(2.4);
                    AlleMeineBehaelter[2].AutomatikmodusStarten(3.6);
                    AlleMeineBehaelter[1].AutomatikmodusStarten(4.8);
                    break;

                case AutomatikModus.Modus4321:
                    AlleMeineBehaelter[3].AutomatikmodusStarten(1.2);
                    AlleMeineBehaelter[2].AutomatikmodusStarten(2.4);
                    AlleMeineBehaelter[1].AutomatikmodusStarten(3.6);
                    AlleMeineBehaelter[0].AutomatikmodusStarten(4.8);
                    break;

                default:
                    AlleMeineBehaelter[0].AutomatikmodusStarten(0);
                    AlleMeineBehaelter[1].AutomatikmodusStarten(0);
                    AlleMeineBehaelter[2].AutomatikmodusStarten(0);
                    AlleMeineBehaelter[3].AutomatikmodusStarten(0);
                    break;

            }

        }
    }
}
