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

        public VisuAnzeigen ViAnzeige { get; set; }
        public bool P1 { get; set; }
        public readonly List<Behaelter> alleBehaelter = new List<Behaelter>();
        private bool AutomatikModusAktiv;
        private readonly MainWindow mainWindow;

        public AlleBehaelter(MainWindow mw)
        {
            mainWindow = mw;
            ViAnzeige = new VisuAnzeigen();

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

                AnzeigeAktualisieren();

                Thread.Sleep(10);
            }
        }

        private void AnzeigeAktualisieren()
        {
            bool AbleitungenUnten;
            bool AbleitungUnten1;
            bool AbleitungUnten2;
            bool AbleitungUnten3;
            bool AbleitungUnten4;

            ViAnzeige.VisibilityVentilQ1(alleBehaelter[0].VentilOben);
            ViAnzeige.VisibilityVentilQ3(alleBehaelter[1].VentilOben);
            ViAnzeige.VisibilityVentilQ5(alleBehaelter[2].VentilOben);
            ViAnzeige.VisibilityVentilQ7(alleBehaelter[3].VentilOben);

            ViAnzeige.FarbeZuleitung1b(alleBehaelter[0].VentilOben);
            ViAnzeige.FarbeZuleitung2b(alleBehaelter[1].VentilOben);
            ViAnzeige.FarbeZuleitung3b(alleBehaelter[2].VentilOben);
            ViAnzeige.FarbeZuleitung4b(alleBehaelter[3].VentilOben);


            ViAnzeige.Margin_1(alleBehaelter[0].Pegel);
            ViAnzeige.Margin_2(alleBehaelter[1].Pegel);
            ViAnzeige.Margin_3(alleBehaelter[2].Pegel);
            ViAnzeige.Margin_4(alleBehaelter[3].Pegel);


            ViAnzeige.FarbeAbleitung1a(alleBehaelter[0].Pegel > 0.01);
            ViAnzeige.FarbeAbleitung2a(alleBehaelter[1].Pegel > 0.01);
            ViAnzeige.FarbeAbleitung3a(alleBehaelter[2].Pegel > 0.01);
            ViAnzeige.FarbeAbleitung4a(alleBehaelter[3].Pegel > 0.01);

            AbleitungUnten1 = alleBehaelter[0].Pegel > 0.01 && alleBehaelter[0].VentilUnten;
            AbleitungUnten2 = alleBehaelter[1].Pegel > 0.01 && alleBehaelter[1].VentilUnten;
            AbleitungUnten3 = alleBehaelter[2].Pegel > 0.01 && alleBehaelter[2].VentilUnten;
            AbleitungUnten4 = alleBehaelter[3].Pegel > 0.01 && alleBehaelter[3].VentilUnten;
            AbleitungenUnten = AbleitungUnten1 || AbleitungUnten2 || AbleitungUnten3 || AbleitungUnten4;

            ViAnzeige.VisibilityVentilQ2(AbleitungUnten1);
            ViAnzeige.VisibilityVentilQ4(AbleitungUnten2);
            ViAnzeige.VisibilityVentilQ6(AbleitungUnten3);
            ViAnzeige.VisibilityVentilQ8(AbleitungUnten4);         

            ViAnzeige.FarbeAbleitung1b(AbleitungenUnten);
            ViAnzeige.FarbeAbleitung2b(AbleitungenUnten);
            ViAnzeige.FarbeAbleitung3b(AbleitungenUnten);
            ViAnzeige.FarbeAbleitung4b(AbleitungenUnten);
            ViAnzeige.FarbeAbleitungGesamt(AbleitungenUnten);


            ViAnzeige.FarbeLabelB1(alleBehaelter[0].SchwimmerschalterOben);
            ViAnzeige.FarbeLabelB2(alleBehaelter[0].SchwimmerschalterUnten);
            ViAnzeige.FarbeLabelB3(alleBehaelter[1].SchwimmerschalterOben);
            ViAnzeige.FarbeLabelB4(alleBehaelter[1].SchwimmerschalterUnten);
            ViAnzeige.FarbeLabelB5(alleBehaelter[2].SchwimmerschalterOben);
            ViAnzeige.FarbeLabelB6(alleBehaelter[2].SchwimmerschalterUnten);
            ViAnzeige.FarbeLabelB7(alleBehaelter[3].SchwimmerschalterOben);
            ViAnzeige.FarbeLabelB8(alleBehaelter[3].SchwimmerschalterUnten);

            ViAnzeige.FarbeCircle_P1(P1);

            if (mainWindow.s7_1200 != null)
            {
                if (mainWindow.s7_1200.GetSpsError()) ViAnzeige.SpsColor = "Red"; else ViAnzeige.SpsColor = "LightGray";
                ViAnzeige.SpsStatus = mainWindow.s7_1200?.GetSpsStatus();
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
