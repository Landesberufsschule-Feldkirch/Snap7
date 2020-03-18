using StiegenhausBeleuchtung.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace StiegenhausBeleuchtung.Model
{
    public class StiegenhausBeleuchtung
    {
        public bool B_01 { get; set; }
        public bool B_02 { get; set; }
        public bool B_03 { get; set; }
        public bool B_11 { get; set; }
        public bool B_12 { get; set; }
        public bool B_13 { get; set; }
        public bool B_21 { get; set; }
        public bool B_22 { get; set; }
        public bool B_23 { get; set; }
        public bool B_31 { get; set; }
        public bool B_32 { get; set; }
        public bool B_33 { get; set; }
        public bool B_41 { get; set; }
        public bool B_42 { get; set; }
        public bool B_43 { get; set; }
        public bool P_01 { get; set; }
        public bool P_02 { get; set; }
        public bool P_03 { get; set; }
        public bool P_11 { get; set; }
        public bool P_12 { get; set; }
        public bool P_13 { get; set; }
        public bool P_21 { get; set; }
        public bool P_22 { get; set; }
        public bool P_23 { get; set; }
        public bool P_31 { get; set; }
        public bool P_32 { get; set; }
        public bool P_33 { get; set; }
        public bool P_41 { get; set; }
        public bool P_42 { get; set; }
        public bool P_43 { get; set; }


        private bool JobAktiv;
        private VisuAnzeigen visuAnzeigen;
        private readonly Dictionary<string, (int raum, int stock)> topologie;
        private (int raum, int stock) ortZiel;
        private (int raum, int stock) ortAktuell;
        private const long zeitProBewegungsmelder = 1000;
        private readonly Stopwatch stopwatch;

        public StiegenhausBeleuchtung()
        {
            JobAktiv = false;
            stopwatch = new Stopwatch();
            ortZiel = (0, 0);

            topologie = new Dictionary<string, (int, int)>
            {
                { "EG",(0, 0) },
                { "Top 0.1",(-1, 0) },
                { "Top 0.2", (1, 0) },
                { "OG 1", (0, 1) },
                { "Top 1.1", (-1, 1) },
                { "Top 1.2", (1, 1) },
                { "OG 2", (0, 2) },
                { "Top 2.1",(-1, 2) },
                { "Top 2.2", (1, 2) },
                { "OG 3", (0, 3) },
                { "Top 3.1", (-1, 3) },
                { "Top 3.2",(1, 3) },
                { "OG 4", (0, 4) },
                { "Top 4.1", (-1, 4) },
                { "Top 4.2", (1, 4) }
            };

            System.Threading.Tasks.Task.Run(() => StiegenhausBeleuchtungTask());
        }

        internal void ProblemLoesen(VisuAnzeigen viAnzeige)
        {
            visuAnzeigen = viAnzeige;
        }

        private void StiegenhausBeleuchtungTask()
        {
            while (true)
            {
                if (JobAktiv) JobAusfuehren();
                Thread.Sleep(10);
            }
        }

        internal void BtnStart(object buttonName)
        {
            if (buttonName is string name)
            {

                if (visuAnzeigen.ReiseStart != "-" && visuAnzeigen.ReiseZiel != "-" && visuAnzeigen.ReiseStart != visuAnzeigen.ReiseZiel)
                {
                    ortAktuell = topologie[visuAnzeigen.ReiseStart];
                    ortZiel = topologie[visuAnzeigen.ReiseZiel];
                    stopwatch.Restart();
                    JobAktiv = true;
                }
            }
        }

        private void JobAusfuehren()
        {
            var elapsed = stopwatch.ElapsedMilliseconds;
            if (elapsed > zeitProBewegungsmelder)
            {
                stopwatch.Restart();

                if (ortAktuell == ortZiel)
                {
                    JobAktiv = false;
                    ortAktuell = (-2, -2);// unbekannter Ort --> alles deaktivieren
                }
                else
                {
                    if (ortAktuell.stock != ortZiel.stock)// unterschiedlicher Stock?
                    {
                        if (ortAktuell.raum == 0)// im Stiegenhaus
                        {
                            if (ortAktuell.stock < ortZiel.stock) ortAktuell.stock++; else ortAktuell.stock--;
                        }
                        else
                        {
                            ortAktuell.raum = 0;//Stiegenhaus
                        }
                    }
                    else
                    {
                        if (ortAktuell.raum < ortZiel.raum) ortAktuell.raum++; else ortAktuell.raum--;
                    }
                }
            }
            BewegungsmelderAktivieren(ortAktuell);
        }

        internal void BewegungsmelderAktivieren((int raum, int stock) aktuell)
        {


            /*
            
            B_01 = raum == -1 && stock == 0;
            B_02 = raum == 0 && stock == 0;
            B_03 = raum == 1 && stock == 0;
            B_11 = raum == -1 && stock == 1;
            B_12 = raum == 0 && stock == 1;
            B_13 = raum == 1 && stock == 1;
            B_21 = raum == -1 && stock == 2;
            B_22 = raum == 0 && stock == 2;
            B_23 = raum == 1 && stock == 2;
            B_31 = raum == -1 && stock == 3;
            B_32 = raum == 0 && stock == 3;
            B_33 = raum == 1 && stock == 3;
            B_41 = raum == -1 && stock == 4;
            B_42 = raum == 0 && stock == 4;
            B_43 = raum == 1 && stock == 4;
            */
        }
    }
}
