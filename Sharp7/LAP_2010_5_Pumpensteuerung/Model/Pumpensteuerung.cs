using System;
using System.Threading;

namespace LAP_2010_5_Pumpensteuerung.Model
{
    public class Pumpensteuerung
    {
        public bool S1 { get; set; } // Wahlschalter Hand
        public bool S2 { get; set; } // Wahlschalter Automatik
        public bool S3 { get; set; } // Störung quittieren
        public bool F5 { get; set; } // Thermorelais
        public bool S7 { get; set; } // Schwimmerschalter oben
        public bool S8 { get; set; } // Schwimmerschalter unten

        public bool K1 { get; set; } // Motor Pumpe
        public bool H1 { get; set; } // "Pumpe Ein"
        public bool H2 { get; set; } // "Störung"
        public bool Y1 { get; set; } // Entleerungsventil
        public double Pegel { get; set; }


        public Pumpensteuerung()
        {
            F5 = true;
            Pegel = 0.95;

            System.Threading.Tasks.Task.Run(() => PumpensteuerungTask());
        }

        private void PumpensteuerungTask()
        {
            double FuellGeschwindigkeit = 0.002;
            double LeerGeschwindigkeit = 0.001;

            while (true)
            {
                if (K1) Pegel += FuellGeschwindigkeit;
                if (Y1) Pegel -= LeerGeschwindigkeit;

                if (Pegel > 1) Pegel = 1;
                if (Pegel < 0) Pegel = 0;

                S7 = (Pegel > 0.1);
                S8 = (Pegel > 0.9);

                Thread.Sleep(10);
            }
        }

        internal void ThermorelaisF5() { F5 = !F5; }
        internal void SetManualK1() { K1 = !K1; }
        internal void VentilY1() { Y1 = !Y1; }


        internal void TasterS1()
        {
            S1 = true;
            S2 = false;
        }

        internal void TasterS2()
        {
            S1 = false;
            S2 = false;
        }

        internal void TasterS3()
        {
            S1 = false;
            S2 = true;
        }

    }
}
