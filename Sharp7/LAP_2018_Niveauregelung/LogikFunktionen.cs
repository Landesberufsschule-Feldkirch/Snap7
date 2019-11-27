using System.Threading.Tasks;

namespace LAP_2018_Niveauregelung
{
    public partial class MainWindow
    {

        double Pegel = 0.5;
        double FuellGeschwindigkeit = 0.000008;
        double LeerGeschwindigkeit = 0.00001;

        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                if (M1) Pegel += FuellGeschwindigkeit;
                if (M2) Pegel += FuellGeschwindigkeit;
                if (Y1) Pegel -= LeerGeschwindigkeit;

                if (Pegel > 1) Pegel = 1;
                if (Pegel < 0) Pegel = 0;

                B1 = (Pegel > 0.2);
                B2 = (Pegel > 0.7);
                B3 = (Pegel > 0.9);

                AnzeigeAktualisieren();

                Task.Delay(100);
            }
        }
    }
}
