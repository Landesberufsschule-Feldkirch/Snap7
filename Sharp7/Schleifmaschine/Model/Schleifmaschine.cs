using System.Threading;

namespace Schleifmaschine.Model
{
    public class Schleifmaschine
    {
        public bool B1 { get; set; }    // Störung "Motor Übersynchron"
        public bool F1 { get; set; }    // Thermorelais langsame Drehzahl
        public bool F2 { get; set; }    // Thermorelais schnelle Drehzahl
        public bool S0 { get; set; }    // Taster ( ⓪ ) 
        public bool S1 { get; set; }    // Taster ( Ⅰ )  
        public bool S2 { get; set; }    // Taster ( Ⅱ )  
        public bool S3 { get; set; }    // Not-Halt
        public bool S4 { get; set; }    // Störung quittieren
        public bool P1 { get; set; }    // Meldeleuchte "Taster langsam"
        public bool P2 { get; set; }    // Meldeleuchte "Taster schnell"
        public bool P3 { get; set; }    // Meldeleuchte "Störung"
        public bool Q1 { get; set; }    // Getriebemotor Langsam
        public bool Q2 { get; set; }    // Getriebemotor Schnell

        public double WinkelSchleifmaschine { get; set; }
        public double DrehzahlSchleifmaschine { get; set; }

        private const double SynchrondrehzahlLangsam = 1000;
        private const double SynchrondrehzahlSchnell = 3000;
        private const double BeschleunigungLangsam = 0.01;
        private const double BeschleunigungSchnell = 0.01;
        private const double VerzoegerungDrehzahl = 0.999;

        private const double DrehzahlWinkelFaktor = 0.002;


        public Schleifmaschine()
        {
            F1 = true;
            F2 = true;
            S0 = true;
            S3 = true;

            System.Threading.Tasks.Task.Run(SchleifmaschineTask);
        }
        private void SchleifmaschineTask()
        {
            while (true)
            {
                if (Q1) DrehzahlSchleifmaschine += (SynchrondrehzahlLangsam - DrehzahlSchleifmaschine) * BeschleunigungLangsam;
                if (Q2) DrehzahlSchleifmaschine += (SynchrondrehzahlSchnell - DrehzahlSchleifmaschine) * BeschleunigungSchnell;

                DrehzahlSchleifmaschine *= VerzoegerungDrehzahl;

                WinkelSchleifmaschine += DrehzahlSchleifmaschine * DrehzahlWinkelFaktor;

                if (WinkelSchleifmaschine > 360) WinkelSchleifmaschine -= 360;
                if (WinkelSchleifmaschine < 0) WinkelSchleifmaschine += 360;

                if (Q1 & DrehzahlSchleifmaschine > SynchrondrehzahlLangsam) B1 = true;

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }
        internal void ThermorelaisF1() => F1 = !F1;
        internal void ThermorelaisF2() => F2 = !F2;
        internal void TasterS3() => S3 = !S3;
    }
}