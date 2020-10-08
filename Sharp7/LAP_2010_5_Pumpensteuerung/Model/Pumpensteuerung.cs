using System.Threading;

namespace LAP_2010_5_Pumpensteuerung.Model
{
    public class Pumpensteuerung
    {
        public bool S1 { get; set; } // Wahlschalter Hand
        public bool S2 { get; set; } // Wahlschalter Automatik
        public bool S3 { get; set; } // Störung quittieren
        public bool F1 { get; set; } // Thermorelais
        public bool B1 { get; set; } // Schwimmerschalter oben
        public bool B2 { get; set; } // Schwimmerschalter unten
        public bool Q1 { get; set; } // Motor Pumpe
        public bool P1 { get; set; } // "Pumpe Ein"
        public bool P2 { get; set; } // "Störung"
        public bool Y1 { get; set; } // Entleerungsventil
        public double Pegel { get; set; }

        public Pumpensteuerung()
        {
            S3 = true;
            F1 = true;
            Pegel = 0.95;

            System.Threading.Tasks.Task.Run(PumpensteuerungTask);
        }

        private void PumpensteuerungTask()
        {
            const double fuellGeschwindigkeit = 0.002;
            const double leerGeschwindigkeit = 0.001;

            while (true)
            {
                if (Q1) Pegel += fuellGeschwindigkeit;
                if (Y1) Pegel -= leerGeschwindigkeit;

                if (Pegel > 1) Pegel = 1;
                if (Pegel < 0) Pegel = 0;

                B1 = Pegel > 0.9;
                B2 = Pegel > 0.1;

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void ThermorelaisF1() => F1 = !F1;

        internal void SetManualQ1() => Q1 = !Q1;

        internal void VentilY1() => Y1 = !Y1;

        internal void TasterHand()
        {
            S1 = true;
            S2 = false;
        }

        internal void TasterAus()
        {
            S1 = false;
            S2 = false;
        }

        internal void TasterAutomatik()
        {
            S1 = false;
            S2 = true;
        }
    }
}