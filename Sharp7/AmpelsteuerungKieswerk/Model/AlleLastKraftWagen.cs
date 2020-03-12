namespace AmpelsteuerungKieswerk.Model
{
    using System.Collections.Generic;
    using System.Threading;

    public class AlleLastKraftWagen
    {
        private readonly MainWindow mainWindow;
        public readonly List<LastKraftWagen> AlleLkw = new List<LastKraftWagen>();
        private readonly int AnzahlLkw;

        public bool B1 { get; set; }
        public bool B2 { get; set; }
        public bool B3 { get; set; }
        public bool B4 { get; set; }


        public AlleLastKraftWagen(MainWindow mw)
        {
            mainWindow = mw;

            AlleLkw.Add(new LastKraftWagen(AnzahlLkw++));
            AlleLkw.Add(new LastKraftWagen(AnzahlLkw++));
            AlleLkw.Add(new LastKraftWagen(AnzahlLkw++));
            AlleLkw.Add(new LastKraftWagen(AnzahlLkw++));
            AlleLkw.Add(new LastKraftWagen(AnzahlLkw++));

            System.Threading.Tasks.Task.Run(() => AlleLastKraftWagenTask());
        }

        private void AlleLastKraftWagenTask()
        {

            while (true)
            {
                B1 = false;
                B2 = false;
                B3 = false;
                B4 = false;

                foreach (LastKraftWagen lkw in AlleLkw)
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
        }

        private bool KollisionErkennen(LastKraftWagen laster)
        {
            bool stop = false;
            var (lx, ly) = laster.GetRichtung();

            foreach (LastKraftWagen lkw in AlleLkw)
            {
                if (laster.ID != lkw.ID)
                {
                    var (hx, hy) = lkw.GetRichtung();
                    if (hx != Utilities.Rechteck.RichtungX.steht || hy != Utilities.Rechteck.RichtungY.steht)
                    {
                        stop |= Utilities.Rechteck.Ausgebremst(laster.Position, lkw.Position, lx, ly);
                    }
                }
            }

            return stop;
        }

        internal void TasterLkw1() { AlleLkw[0].Losfahren(); }
        internal void TasterLkw2() { AlleLkw[1].Losfahren(); }
        internal void TasterLkw3() { AlleLkw[2].Losfahren(); }
        internal void TasterLkw4() { AlleLkw[3].Losfahren(); }
        internal void TasterLkw5() { AlleLkw[4].Losfahren(); }
        internal void TasterLinksParken() { foreach (LastKraftWagen lkw in AlleLkw) lkw.LKW_Position = LastKraftWagen.LkwPositionen.LinksGeparkt; }
        internal void TasterRechtsParken() { foreach (LastKraftWagen lkw in AlleLkw) lkw.LKW_Position = LastKraftWagen.LkwPositionen.RechtsGeparkt; }
    }
}