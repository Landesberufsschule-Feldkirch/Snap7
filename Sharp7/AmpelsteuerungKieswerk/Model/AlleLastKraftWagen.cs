using System.Linq;

namespace AmpelsteuerungKieswerk.Model
{
    using System.Collections.Generic;
    using System.Threading;
    using Utilities;

    public class AlleLastKraftWagen
    {
        public bool B1 { get; set; }
        public bool B2 { get; set; }
        public bool B3 { get; set; }
        public bool B4 { get; set; }

        private readonly List<LastKraftWagen> _alleLkw = new List<LastKraftWagen>();
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

        public Punkt GetPositionLkw(int index) => _alleLkw[index].Position.Punkt;

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
                    stop |= Rechteck.Ausgebremst(laster.Position, lkw.Position, lx, ly);
                }
            }

            return stop;
        }

        internal void TasterLkw1() => _alleLkw[0].Losfahren();

        internal void TasterLkw2() => _alleLkw[1].Losfahren();

        internal void TasterLkw3() => _alleLkw[2].Losfahren();

        internal void TasterLkw4() => _alleLkw[3].Losfahren();

        internal void TasterLkw5() => _alleLkw[4].Losfahren();

        internal void TasterLinksParken()
        {
            foreach (var lkw in _alleLkw) lkw.LkwPosition = LastKraftWagen.LkwPositionen.LinksGeparkt;
        }

        internal void TasterRechtsParken()
        {
            foreach (var lkw in _alleLkw) lkw.LkwPosition = LastKraftWagen.LkwPositionen.RechtsGeparkt;
        }
    }
}