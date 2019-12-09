using System.Threading.Tasks;

namespace LAP_2018_Abfuellanlage
{
    public partial class MainWindow
    {

        double Pegel = 1;
        double LeerGeschwindigkeit = 0.00001;

        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {     
                S1 = TasterSimulation(ref S1_Zaehler);
                S2 = !TasterSimulation(ref S2_Zaehler);
                S3 = TasterSimulation(ref S3_Zaehler);
                S4 = TasterSimulation(ref S4_Zaehler);

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

        public bool TasterSimulation(ref int Zaehler)
        {
            if (Zaehler > 0)
            {
                Zaehler--;
                return true;
            }
            else return false;
        }
    }
}
