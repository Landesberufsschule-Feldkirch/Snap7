using System.Threading.Tasks;

namespace BehälterSteuerung
{
    public partial class MainWindow
    {
        public const double SinkGeschwindigkeit = 0.00005;
        public const double FuellGeschwindigkeit = 0.2 * SinkGeschwindigkeit; // damit drei Behälter gleichzeit leer sein können

        public double PegelOffset_1;
        public double PegelOffset_2;
        public double PegelOffset_3;

        public const double HoeheFuellBalken = 200.0;

        public bool Tank_1_AutomatischEntleeren;
        public bool Tank_2_AutomatischEntleeren;
        public bool Tank_3_AutomatischEntleeren;

        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                if (AutomatikModusAktiv) AutomatikFunktionen();
                else ManuelleFunktionen();

                LimitsUeberwachen();
                AnzeigeAktualisieren();

                Task.Delay(100);
            }
        }

        public void AutomatikFunktionen()
        {
            switch (AutomatikSchrittschaltwerk)
            {

                case "Init":
                    Pegel_1 = 1;
                    Pegel_2 = 1;
                    Pegel_3 = 1;

                    if (Automatik_123_Aktiv)
                    {
                        PegelOffset_1 = 1.2;
                        PegelOffset_2 = 2.4;
                        PegelOffset_3 = 3.6;
                    }

                    if (Automatik_132_Aktiv)
                    {
                        PegelOffset_1 = 1.2;
                        PegelOffset_3 = 2.4;
                        PegelOffset_2 = 3.6;
                    }

                    if (Automatik_321_Aktiv)
                    {
                        PegelOffset_3 = 1.2;
                        PegelOffset_2 = 2.4;
                        PegelOffset_1 = 3.6;
                    }

                    Tank_1_AutomatischEntleeren = true;
                    Tank_2_AutomatischEntleeren = true;
                    Tank_3_AutomatischEntleeren = true;

                    AutomatikSchrittschaltwerk = "Entleeren";
                    break;

                case "Entleeren":
                    PegelOffset_1 -= SinkGeschwindigkeit;
                    PegelOffset_2 -= SinkGeschwindigkeit;
                    PegelOffset_3 -= SinkGeschwindigkeit;

                    if ((PegelOffset_1 < 1) && (PegelOffset_1 > 0.05)) Ventil_Q2 = true; else Ventil_Q2 = false;
                    if ((PegelOffset_2 < 1) && (PegelOffset_2 > 0.05)) Ventil_Q4 = true; else Ventil_Q4 = false;
                    if ((PegelOffset_3 < 1) && (PegelOffset_3 > 0.05)) Ventil_Q6 = true; else Ventil_Q6 = false;

                    if (Tank_1_AutomatischEntleeren) Pegel_1 = PegelOffset_1;
                    if (Tank_2_AutomatischEntleeren) Pegel_2 = PegelOffset_2;
                    if (Tank_3_AutomatischEntleeren) Pegel_3 = PegelOffset_3;

                    if (PegelOffset_1 < 0.05) Tank_1_AutomatischEntleeren = false;
                    if (PegelOffset_2 < 0.05) Tank_2_AutomatischEntleeren = false;
                    if (PegelOffset_3 < 0.05) Tank_3_AutomatischEntleeren = false;

                    if ((Tank_1_AutomatischEntleeren || Tank_2_AutomatischEntleeren || Tank_3_AutomatischEntleeren) == false)
                    {
                        AutomatikModusAktiv = false;
                        AutomatikAktivieren();
                    }
                    break;

                default:
                    AutomatikSchrittschaltwerk = "Init";
                    break;
            }
            TankFuellen();
        }
        
        public void ManuelleFunktionen()
        {
            if (Ventil_Q2) Pegel_1 -= SinkGeschwindigkeit;
            if (Ventil_Q4) Pegel_2 -= SinkGeschwindigkeit;
            if (Ventil_Q6) Pegel_3 -= SinkGeschwindigkeit;

            TankFuellen();
        }

        public void TankFuellen()
        {
            if (Ventil_Q1) Pegel_1 += FuellGeschwindigkeit;
            if (Ventil_Q3) Pegel_2 += FuellGeschwindigkeit;
            if (Ventil_Q5) Pegel_3 += FuellGeschwindigkeit;
        }

        public void LimitsUeberwachen()
        {
            if (Pegel_1 > 0.95) Pegel_B1 = true; else Pegel_B1 = false;
            if (Pegel_2 > 0.95) Pegel_B3 = true; else Pegel_B3 = false;
            if (Pegel_3 > 0.95) Pegel_B5 = true; else Pegel_B5 = false;

            if (Pegel_1 > 0.05) Pegel_B2 = true; else Pegel_B2 = false;
            if (Pegel_2 > 0.05) Pegel_B4 = true; else Pegel_B4 = false;
            if (Pegel_3 > 0.05) Pegel_B6 = true; else Pegel_B6 = false;


            if (Pegel_1 < 0) Pegel_1 = 0;
            if (Pegel_1 > 1) Pegel_1 = 1;

            if (Pegel_2 < 0) Pegel_2 = 0;
            if (Pegel_2 > 1) Pegel_2 = 1;

            if (Pegel_3 < 0) Pegel_3 = 0;
            if (Pegel_3 > 1) Pegel_3 = 1;
        }
        
    }
}
