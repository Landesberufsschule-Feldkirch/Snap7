using System.Threading;

namespace Utilities
{
    public class Pt1Zeitglied
    {
        private readonly int _zeitkonstante;
        private readonly double _faktor;
        private readonly double _limitMin;
        private readonly double _limitMax;

        private double _neuerSollwert;
        private double _aktuell;

        public Pt1Zeitglied(int zeitkonstante, double faktor, double limitMin, double limitMax)
        {
            _zeitkonstante = zeitkonstante;
            _faktor = faktor;
            _limitMin = limitMin;
            _limitMax = limitMax;

            System.Threading.Tasks.Task.Run(ZeitgliedTask);
        }

        private void ZeitgliedTask()
        {
            while (true)
            {
                var akt = _aktuell + _faktor * (_neuerSollwert - _aktuell);
                _aktuell = Simatic.Clamp(akt, _limitMin, _limitMax);

                Thread.Sleep(_zeitkonstante);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        public void SetNeuerEingangswert(double neu) => _neuerSollwert = neu;

        public double GetAktuellerWert() => _aktuell;




    }
}
