namespace BehaelterSteuerung.Model
{
    using System;
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

        public VisuAnzeigen ViAnzeige { get; set; }
        private readonly List<Behaelter> alleBehaelter = new List<Behaelter>();
        private bool AutomatikModusAktiv;

        public AlleBehaelter()
        {
            ViAnzeige = new VisuAnzeigen();

            alleBehaelter.Add(new Behaelter());
            alleBehaelter.Add(new Behaelter());
            alleBehaelter.Add(new Behaelter());
            alleBehaelter.Add(new Behaelter());

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

                    if ((beh.InternerPegel > 0.01)) AutomatikModusNochAktiv = true;
                }
                if (AutomatikModusAktiv && !AutomatikModusNochAktiv)
                {
                    AlleAutomatikKnoepfeAktivieren();
                    AutomatikModusAktiv = false;
                }


                AnzeigeAktualisieren();

                Thread.Sleep(10);
            }
        }

        private void AnzeigeAktualisieren()
        {
            ViAnzeige.VisibilityVentilQ1(alleBehaelter[0].VentilObenEingeschaltet());
            ViAnzeige.VisibilityVentilQ3(alleBehaelter[1].VentilObenEingeschaltet());
            ViAnzeige.VisibilityVentilQ5(alleBehaelter[2].VentilObenEingeschaltet());
            ViAnzeige.VisibilityVentilQ7(alleBehaelter[3].VentilObenEingeschaltet());




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
            ViAnzeige.EnableAutomatik1234 = "false";
            ViAnzeige.EnableAutomatik1324 = "false";
            ViAnzeige.EnableAutomatik1432 = "false";
            ViAnzeige.EnableAutomatik4321 = "false";
        }

        private void AlleAutomatikKnoepfeAktivieren()
        {
            ViAnzeige.EnableAutomatik1234 = "true";
            ViAnzeige.EnableAutomatik1324 = "true";
            ViAnzeige.EnableAutomatik1432 = "true";
            ViAnzeige.EnableAutomatik4321 = "true";
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
                default:
                    alleBehaelter[3].AutomatikmodusStarten(0);
                    alleBehaelter[2].AutomatikmodusStarten(0);
                    alleBehaelter[1].AutomatikmodusStarten(0);
                    alleBehaelter[0].AutomatikmodusStarten(0);
                    break;

            }

        }



    }
}
