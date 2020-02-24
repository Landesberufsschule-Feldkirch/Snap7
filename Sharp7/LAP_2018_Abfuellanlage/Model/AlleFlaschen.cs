using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;

namespace LAP_2018_Abfuellanlage.Model
{
    public class AlleFlaschen
    {
        private readonly MainWindow mainWindow;

        public readonly List<Flaschen> alleFlaschen = new List<Flaschen>();

        public bool B1 { get; set; }
        public bool F5 { get; set; }
        public bool K1 { get; set; }
        public bool K2 { get; set; }
        public bool M1 { get; set; }
        public bool P1 { get; set; }
        public bool P2 { get; set; }
        public bool S1 { get; set; }
        public bool S2 { get; set; }
        public bool S3 { get; set; }
        public bool S4 { get; set; }
        public double Pegel { get; set; }

        private readonly int AnzahlFlaschen;
        private int AktuelleFlasche;

        public AlleFlaschen(MainWindow mw)
        {
            mainWindow = mw;

            alleFlaschen.Add(new Flaschen(AnzahlFlaschen++));
            alleFlaschen.Add(new Flaschen(AnzahlFlaschen++));
            alleFlaschen.Add(new Flaschen(AnzahlFlaschen++));
            alleFlaschen.Add(new Flaschen(AnzahlFlaschen++));
            alleFlaschen.Add(new Flaschen(AnzahlFlaschen++));
            alleFlaschen.Add(new Flaschen(AnzahlFlaschen++));

            S2 = false;
            F5 = true;
            Pegel = 0.4;

            System.Threading.Tasks.Task.Run(() => AlleFlaschenTask());
        }

        internal void TasterNachfuellen() { Pegel = 1; }
        internal void TasterF5() { if (F5) F5 = false; else F5 = true; }
        internal void TasterS1() { S1 = ButtonFunktionPressReleaseAendern(mainWindow.btnS1); }
        internal void TasterS2() { S2 = !ButtonFunktionPressReleaseAendern(mainWindow.btnS2); }
        internal void TasterS3() { S3 = ButtonFunktionPressReleaseAendern(mainWindow.btnS3); }
        internal void TasterS4() { S4 = ButtonFunktionPressReleaseAendern(mainWindow.btnS4); }
        internal void SetManualK1() { if (mainWindow.DebugWindowAktiv) { K1 = ButtonFunktionPressReleaseAendern(mainWindow.setManualWindow.SetK1); } }
        internal void SetManualK2() { if (mainWindow.DebugWindowAktiv) { K2 = ButtonFunktionPressReleaseAendern(mainWindow.setManualWindow.SetK2); } }
        internal void SetManualM1() { if (mainWindow.DebugWindowAktiv) { M1 = ButtonFunktionPressReleaseAendern(mainWindow.setManualWindow.SetM1); } }

        private void AlleFlaschenTask()
        {
            double LeerGeschwindigkeit = 0.001;

            while (true)
            {
                if (K1) Pegel -= LeerGeschwindigkeit;
                if (Pegel < 0) Pegel = 0;

                if (K2) alleFlaschen[AktuelleFlasche].FlascheVereinzeln();

                B1 = false;
                foreach (Flaschen flasche in alleFlaschen)
                {
                    var lichtschranke = false;
                    (lichtschranke, AktuelleFlasche) = flasche.FlascheBewegen(M1, AnzahlFlaschen, AktuelleFlasche);
                    B1 |= lichtschranke;
                }

                Thread.Sleep(10);
            }
        }


        private bool ButtonFunktionPressReleaseAendern(Button knopf)
        {
            if (knopf.ClickMode == System.Windows.Controls.ClickMode.Press)
            {
                knopf.ClickMode = System.Windows.Controls.ClickMode.Release;
                return true;
            }
            else
            {
                knopf.ClickMode = System.Windows.Controls.ClickMode.Press;
                return false;
            }
        }
    }
}
