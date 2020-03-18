﻿using System.Threading;

namespace LAP_2018_Niveauregelung.Model
{
    public class NiveauRegelung
    {
        public bool B1 { get; set; }
        public bool B2 { get; set; }
        public bool B3 { get; set; }
        public bool F1 { get; set; }
        public bool F2 { get; set; }
        public bool S1 { get; set; }
        public bool S2 { get; set; }
        public bool S3 { get; set; }
        public bool M1 { get; set; }
        public bool M2 { get; set; }
        public bool P1 { get; set; }
        public bool P2 { get; set; }
        public bool P3 { get; set; }
        public bool Y1 { get; set; }
        public double Pegel { get; set; }


        public NiveauRegelung()
        {
            S2 = true;
            F1 = true;
            F2 = true;
            Pegel = 0.95;

            System.Threading.Tasks.Task.Run(() => NiveauRegelungTask());
        }

        private void NiveauRegelungTask()
        {
            double FuellGeschwindigkeit = 0.0008;
            double LeerGeschwindigkeit = 0.001;

            while (true)
            {
                if (M1) Pegel += FuellGeschwindigkeit;
                if (M2) Pegel += FuellGeschwindigkeit;
                if (Y1) Pegel -= LeerGeschwindigkeit;

                if (Pegel > 1) Pegel = 1;
                if (Pegel < 0) Pegel = 0;

                B1 = (Pegel > 0.1);
                B2 = (Pegel > 0.5);
                B3 = (Pegel > 0.9);

                Thread.Sleep(10);
            }
        }



        internal void ThermorelaisF1() { F1 = !F1; }
        internal void ThermorelaisF2() { F2 = !F2; }
        internal void SetManualM1() { M1 = !M1; }
        internal void SetManualM2() { M2 = !M2; }
        internal void VentilY1() { Y1 = !Y1; }
    }
}