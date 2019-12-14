using System.Threading.Tasks;

namespace LAP_2018_Abfuellanlage
{
    public partial class MainWindow
    {

        double Pegel = 1;
       private readonly double LeerGeschwindigkeit = 0.00001;

        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {           
                if (K1) Pegel -= LeerGeschwindigkeit;
                if (Pegel < 0) Pegel = 0;

                PegelByte = (byte)(100 * Pegel);
                PegelInt = (ushort)(27648 * Pegel);

                AnzeigeAktualisieren();

                if (K2)
                {
                    int AktFlasche = gAlleFlaschen[0].getAktuelleFlasche();
                    gAlleFlaschen[AktFlasche].FlascheVereinzeln();
                }                

                B1 = false;
                foreach (Flaschen flasche in gAlleFlaschen) { B1 |= flasche.FlascheBewegen(M1); }

                Task.Delay(100);
            }
        }
    }
}