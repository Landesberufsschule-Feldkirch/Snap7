using StiegenhausBeleuchtung.ViewModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace StiegenhausBeleuchtung.Model
{
    public class StiegenhausBeleuchtung
    {
        public bool JobAktiv { get; set; }

        private VisuAnzeigen visuAnzeigen;
        private readonly Dictionary<string, (int raum, int stock)> topologie;
        private (int raum, int stock) ortZiel;
        private (int raum, int stock) ortAktuell;
        private const long zeitProBewegungsmelder = 1000;
        private readonly Stopwatch stopwatch;
        private readonly bool[] alleBewegungsmelder = new bool[100];
        private readonly bool[] alleLampen = new bool[100];

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

        internal void ProblemLoesen(VisuAnzeigen viAnzeige) { visuAnzeigen = viAnzeige; }
        public bool GetBewegungsmelder(int index) => alleBewegungsmelder[index];
        public void SetBewegungsmelder(int index, bool val) { alleBewegungsmelder[index] = val; }
        public void SetLampen(int index, bool val) { alleLampen[index] = val; }
        public bool GetLampen(int index) => alleLampen[index];

        private void StiegenhausBeleuchtungTask()
        {
            while (true)
            {
                if (JobAktiv) JobAusfuehren();
                Thread.Sleep(10);
            }
        }

        internal void BtnStart(object _)
        {
            if (visuAnzeigen.ReiseStart != "-" && visuAnzeigen.ReiseZiel != "-" && visuAnzeigen.ReiseStart != visuAnzeigen.ReiseZiel)
            {
                ortAktuell = topologie[visuAnzeigen.ReiseStart];
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
            for (int i = 0; i < 100; i++) alleBewegungsmelder[i] = false;

            if (aktuell.raum != -2 && aktuell.stock != -2)
            {
                var Bewegungsmelder = 2 + aktuell.raum + 10 * aktuell.stock;
                alleBewegungsmelder[Bewegungsmelder] = true;
            }
        }
    }
}
