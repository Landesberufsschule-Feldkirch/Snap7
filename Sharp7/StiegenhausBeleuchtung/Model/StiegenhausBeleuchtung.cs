using StiegenhausBeleuchtung.ViewModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace StiegenhausBeleuchtung.Model
{
    public class StiegenhausBeleuchtung
    {
        public bool JobAktiv { get; set; }

        private VisuAnzeigen _visuAnzeigen;
        private readonly Dictionary<string, (int raum, int stock)> _topologie;
        private (int raum, int stock) _ortZiel;
        private (int raum, int stock) _ortAktuell;
        private const long ZeitProBewegungsmelder = 1000;
        private readonly Stopwatch _stopwatch;
        private readonly bool[] _alleBewegungsmelder = new bool[100];
        private readonly bool[] _alleLampen = new bool[100];

        public StiegenhausBeleuchtung()
        {
            JobAktiv = false;
            _stopwatch = new Stopwatch();
            _ortZiel = (0, 0);

            _topologie = new Dictionary<string, (int, int)>
            {
                { "EG",         (0, 0) },
                { "Top 0.1",    (-2, 0) },
                { "Top 0.2",    (-1, 0) },
                { "Top 0.3",    (1, 0) },
                { "Top 0.4",    (2, 0) },
                { "OG 1",       (0, 1) },
                { "Top 1.1",    (-2, 1) },
                { "Top 1.2",    (-1, 1) },
                { "Top 1.3",    (1, 1) },
                { "Top 1.4",    (2, 1) },
                { "OG 2",       (0, 2) },
                { "Top 2.1",    (-2, 2) },
                { "Top 2.2",    (-1, 2) },
                { "Top 2.3",    (1, 2) },
                { "Top 2.4",    (2, 2) },
                { "OG 3",       (0, 3) },
                { "Top 3.1",    (-2, 3) },
                { "Top 3.2",    (-1, 3) },
                { "Top 3.3",    (1, 3) },
                { "Top 3.4",    (2, 3) },
                { "OG 4",       (0, 4) },
                { "Top 4.1",    (-2, 4) },
                { "Top 4.2",    (-1, 4) },
                { "Top 4.3",    (1, 4) },
                { "Top 4.4",    (2, 4) }
            };

            System.Threading.Tasks.Task.Run(StiegenhausBeleuchtungTask);
        }

        internal void ProblemLoesen(VisuAnzeigen viAnzeige)
        {
            _visuAnzeigen = viAnzeige;
        }

        public bool GetBewegungsmelder(int index) => _alleBewegungsmelder[index];

        public void SetBewegungsmelder(int index, bool val)
        {
            _alleBewegungsmelder[index] = val;
        }

        public void SetLampen(int index, bool val)
        {
            _alleLampen[index] = val;
        }

        public bool GetLampen(int index) => _alleLampen[index];

        private void StiegenhausBeleuchtungTask()
        {
            while (true)
            {
                if (JobAktiv) JobAusfuehren();
                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void BtnStart(object _)
        {
            if (_visuAnzeigen.ReiseStart != "-" && _visuAnzeigen.ReiseZiel != "-" && _visuAnzeigen.ReiseStart != _visuAnzeigen.ReiseZiel)
            {
                _ortAktuell = _topologie[_visuAnzeigen.ReiseStart];
                _ortZiel = _topologie[_visuAnzeigen.ReiseZiel];
                _stopwatch.Restart();
                JobAktiv = true;
            }
        }

        private void JobAusfuehren()
        {
            var elapsed = _stopwatch.ElapsedMilliseconds;
            if (elapsed > ZeitProBewegungsmelder)
            {
                _stopwatch.Restart();

                if (_ortAktuell == _ortZiel)
                {
                    JobAktiv = false;
                    _ortAktuell = (-10, -10);// unbekannter Ort --> alles deaktivieren
                }
                else
                {
                    if (_ortAktuell.stock != _ortZiel.stock)// unterschiedlicher Stock?
                    {
                        if (_ortAktuell.raum == 0)// im Stiegenhaus
                        {
                            if (_ortAktuell.stock < _ortZiel.stock) _ortAktuell.stock++; else _ortAktuell.stock--;
                        }
                        else
                        {
                            if (_ortAktuell.raum < 0) _ortAktuell.raum++; else _ortAktuell.raum--;
                        }
                    }
                    else
                    {
                        if (_ortAktuell.raum < _ortZiel.raum) _ortAktuell.raum++; else _ortAktuell.raum--;
                    }
                }
            }
            BewegungsmelderAktivieren(_ortAktuell);
        }

        internal void BewegungsmelderAktivieren((int raum, int stock) aktuell)
        {
            for (int i = 0; i < 100; i++) _alleBewegungsmelder[i] = false;

            if (aktuell.raum != -10 && aktuell.stock != -10)
            {
                var bewegungsmelder = 3 + aktuell.raum + 10 * aktuell.stock;
                _alleBewegungsmelder[bewegungsmelder] = true;
            }
        }
    }
}