using System.Threading;

namespace LAP_2018_Abfuellanlage
{
    public partial class MainWindow
    {
        double Pegel = 1;
        private readonly double LeerGeschwindigkeit = 0.00001;
        private readonly int SiemensAnalogwertMax = 27648;
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