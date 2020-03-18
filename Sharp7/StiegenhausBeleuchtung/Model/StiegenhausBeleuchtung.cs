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
        private readonly Dictionary<string, Tuple<int, int>> topologie;
        int stockAktuell;
        int raumAktuell;
        private Tuple<int, int> ortStart;
        private Tuple<int, int> ortZiel;
        private const long zeitProBewegungsmelder = 1000;
        private readonly Stopwatch stopwatch;

        public StiegenhausBeleuchtung()
        {
            JobAktiv = false;
            stopwatch = new Stopwatch();
            ortStart = new Tuple<int, int>(0, 0);
            ortZiel = new Tuple<int, int>(0, 0);

            topologie = new Dictionary<string, Tuple<int, int>>
            {
                { "EG", new Tuple<int, int>(0, 0) },
                { "Top 0.1", new Tuple<int, int>(-1, 0) },
                { "Top 0.2", new Tuple<int, int>(1, 0) },
                { "OG 1", new Tuple<int, int>(0, 1) },
                { "Top 1.1", new Tuple<int, int>(-1, 1) },
                { "Top 1.2", new Tuple<int, int>(1, 1) },
                { "OG 2", new Tuple<int, int>(0, 2) },
                { "Top 2.1", new Tuple<int, int>(-1, 2) },
                { "Top 2.2", new Tuple<int, int>(1, 2) },
                { "OG 3", new Tuple<int, int>(0, 3) },
                { "Top 3.1", new Tuple<int, int>(-1, 3) },
                { "Top 3.2", new Tuple<int, int>(1, 3) },
                { "OG 4", new Tuple<int, int>(0, 4) },
                { "Top 4.1", new Tuple<int, int>(-1, 4) },
                { "Top 4.2", new Tuple<int, int>(1, 4) }
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

        internal void BtnStart()
        {
            if (visuAnzeigen.ReiseStart != "-" && visuAnzeigen.ReiseZiel != "-" && visuAnzeigen.ReiseStart != visuAnzeigen.ReiseZiel)
            {
                raumAktuell = topologie[visuAnzeigen.ReiseStart].Item1;
                stockAktuell = topologie[visuAnzeigen.ReiseStart].Item2;
                ortStart = topologie[visuAnzeigen.ReiseStart];
                ortZiel = topologie[visuAnzeigen.ReiseZiel];
                stopwatch.Restart();
                JobAktiv = true;
            }
        }

        private void JobAusfuehren()
        {
            var elapsed = stopwatch.ElapsedMilliseconds;
            if (elapsed > zeitProBewegungsmelder)
            {
                stopwatch.Restart();

                if (raumAktuell == ortZiel.Item1 && stockAktuell == ortZiel.Item2)
                {
                    JobAktiv = false;
                    raumAktuell = -2; // unbekannter Ort --> alles deaktivieren
                    stockAktuell = -2;
                }
                else
                {
                    if (stockAktuell != ortZiel.Item2)// unterschiedlicher Stock?
                    {
                        if (raumAktuell == 0)// im Stiegenhaus
                        {
                            if (stockAktuell < ortZiel.Item2) stockAktuell++; else stockAktuell--;
                        }
                        else
                        {
                            raumAktuell = 0;//Stiegenhaus
                        }
                    }
                    else
                    {
                        if (raumAktuell < ortZiel.Item1) raumAktuell++; else raumAktuell--;
                    }
                }
            }
            BewegungsmelderAktivieren(raumAktuell, stockAktuell);
        }

        internal void BewegungsmelderAktivieren(int raum, int stock)
        {
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
        }
    }
}
