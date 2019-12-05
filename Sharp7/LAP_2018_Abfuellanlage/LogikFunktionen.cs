using System.Threading.Tasks;

namespace LAP_2018_Abfuellanlage
{
    public partial class MainWindow
    {

        double Pegel = 0.95;
        double FuellGeschwindigkeit = 0.000008;
        double LeerGeschwindigkeit = 0.00001;

        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                S1 = TasterSimulation(ref S1_Zaehler);
                S2 = !TasterSimulation(ref S2_Zaehler);
                S3 = TasterSimulation(ref S3_Zaehler);


                if (M1) Pegel += FuellGeschwindigkeit;
                if (M2) Pegel += FuellGeschwindigkeit;
                if (Y1) Pegel -= LeerGeschwindigkeit;

                if (Pegel > 1) Pegel = 1;
                if (Pegel < 0) Pegel = 0;

                B1 = (Pegel > 0.1);
                B2 = (Pegel > 0.5);
                B3 = (Pegel > 0.9);

                AnzeigeAktualisieren();

                foreach (Flaschen flasche in gAlleFlaschen) { flasche.FlascheBewegen(); }

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
