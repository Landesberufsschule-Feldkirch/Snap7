using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Utilities;

namespace AmpelsteuerungKieswerk.Model
{
    public class AlleLastKraftWagen
    {
        public bool B1 { get; set; }
        public bool B2 { get; set; }
        public bool B3 { get; set; }
        public bool B4 { get; set; }

        private readonly List<LastKraftWagen> _alleLkw = new();
        private readonly int _anzahlLkw;

        public AlleLastKraftWagen()
        {
            _alleLkw.Add(new LastKraftWagen(_anzahlLkw++));
            _alleLkw.Add(new LastKraftWagen(_anzahlLkw++));
            _alleLkw.Add(new LastKraftWagen(_anzahlLkw++));
            _alleLkw.Add(new LastKraftWagen(_anzahlLkw++));
            _alleLkw.Add(new LastKraftWagen(_anzahlLkw++));

            System.Threading.Tasks.Task.Run(AlleLastKraftWagenTask);
        }
        public Punkt GetPositionLkw(int index) => _alleLkw[index].Lkw.GetPosition();
        public LastKraftWagen.LkwRichtungen GetRichtungLkw(int index) => _alleLkw[index].LkwRichtung;
        private void AlleLastKraftWagenTask()
        {
            while (true)
            {
                B1 = false;
                B2 = false;
                B3 = false;
                B4 = false;

                foreach (var lkw in _alleLkw)
                {
                    var stop = KollisionErkennen(lkw);
                    var (b1, b2, b3, b4) = lkw.LastwagenFahren(stop);
                    B1 |= b1;
                    B2 |= b2;
                    B3 |= b3;
                    B4 |= b4;
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }
        private bool KollisionErkennen(LastKraftWagen laster)
        {
            var stop = false;
            var (lx, ly) = laster.GetRichtung();

            foreach (var lkw in _alleLkw.Where(lkw => laster.Id != lkw.Id))
            {
                var (hx, hy) = lkw.GetRichtung();
                if (hx != Rechteck.RichtungX.Steht || hy != Rechteck.RichtungY.Steht)
                {
                    stop |= Rechteck.Ausgebremst(laster.Lkw, lkw.Lkw, lx, ly);
                }
            }

            return stop;
        }
        public void LkwLosfahren(int i) => _alleLkw[i].Losfahren();
        internal void LinksParken()
        {
            foreach (var lkw in _alleLkw) lkw.LkwPosition = LastKraftWagen.LkwPositionen.LinksGeparkt;
        }
        internal void RechtsParken()
        {
            foreach (var lkw in _alleLkw) lkw.LkwPosition = LastKraftWagen.LkwPositionen.RechtsGeparkt;
        }
    }
}