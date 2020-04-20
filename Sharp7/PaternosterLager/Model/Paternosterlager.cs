using PaternosterLager.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace PaternosterLager.Model
{
    public class Paternosterlager
    {
        public bool ManualAuf { get; set; }
        public bool ManualAb { get; set; }
        public double Geschwindigkeit { get; set; }
        public ObservableCollection<KettengliedRegal> AlleKettengliedRegale { get; set; }
        public char Zeichen { get; internal set; }
        public bool S1 { get; internal set; }
        public bool S2 { get; internal set; }
        public bool B1 { get; set; }
        public bool B2 { get; set; }
        public bool Q1 { get; set; }
        public bool Q2 { get; set; }
        public int IstPos { get; set; }
        public int SollPos { get; set; }
        public double Position { get; set; }
        public double GesamtLaenge { get; set; }

        private readonly MainWindow mainWindow;

        public Paternosterlager(MainWindow mw)
        {
            mainWindow = mw;
            System.Threading.Tasks.Task.Run(() => PaternosterLagerTask());
        }

        private void PaternosterLagerTask()
        {
            const int bereichInitiator = 10;
            const double geschwindigkeit = 1;

            while (true)
            {
                int abstandRegale = Convert.ToInt32(GesamtLaenge / 20);
                int pos = Convert.ToInt32(Position);

                if (mainWindow.DebugWindowAktiv)
                {
                    if (ManualAuf) Position += geschwindigkeit;
                    if (ManualAb) Position -= geschwindigkeit;
                }
                else
                {
                    if (S1) Position += geschwindigkeit;
                    if (S2) Position -= geschwindigkeit;
                }


                if (abstandRegale > 0)
                {
                    if (pos > GesamtLaenge) Position -= GesamtLaenge;
                    if (pos < 0) Position += GesamtLaenge;

                    if ((pos >= 0) && (pos < bereichInitiator)) B1 = true; else B1 = false;
                    if (((pos % abstandRegale) >= 0) && ((pos % abstandRegale) < bereichInitiator)) B2 = true; else B2 = false;
                }

                Thread.Sleep(10);
            }
        }
    }
}