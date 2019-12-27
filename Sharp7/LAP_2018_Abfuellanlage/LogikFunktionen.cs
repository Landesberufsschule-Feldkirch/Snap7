using System.Threading;

namespace LAP_2018_Abfuellanlage
{
    public class Logikfunktionen
    {
        double Pegel = 1;
        private readonly double LeerGeschwindigkeit = 0.000_01;
        private readonly int SiemensAnalogwertMax = 27_648;

        public Logikfunktionen() { }
        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                if (K1) Pegel -= LeerGeschwindigkeit;
                if (Pegel < 0) Pegel = 0;

                PegelByte = (byte)(100 * Pegel);
                PegelInt = (ushort)(SiemensAnalogwertMax * Pegel);

                if (K2)
                {
                    int AktFlasche = gAlleFlaschen[0].GetAktuelleFlasche();
                    gAlleFlaschen[AktFlasche].FlascheVereinzeln();
                }

                B1 = false;
                foreach (Flaschen flasche in gAlleFlaschen) { B1 |= flasche.FlascheBewegen(M1); }

                Thread.Sleep(100);
            }
        }
    }
}