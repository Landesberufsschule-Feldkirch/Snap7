using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;

namespace LAP_2018_Abfuellanlage.Model
{
    public class AlleFlaschen
    {
        private readonly MainWindow mainWindow;

        public VisuAnzeigen ViAnzeige { get; set; }

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


        public AlleFlaschen(MainWindow mw)
        {
            mainWindow = mw;

            ViAnzeige = new VisuAnzeigen();

            alleFlaschen.Add(new Flaschen());
            alleFlaschen.Add(new Flaschen());
            alleFlaschen.Add(new Flaschen());
            alleFlaschen.Add(new Flaschen());
            alleFlaschen.Add(new Flaschen());
            alleFlaschen.Add(new Flaschen());

            F5 = true;
            Pegel = 0.4;

            System.Threading.Tasks.Task.Run(() => AlleFlaschenTask());
        }

        internal void TasterF5() { if (F5) F5 = false; else F5 = true; }
        internal void TasterS1() { S1 = ButtonFunktionPressReleaseAendern(mainWindow.btnS1); }
        internal void TasterS2() { S2 = ButtonFunktionPressReleaseAendern(mainWindow.btnS2); }
        internal void TasterS3() { S3 = ButtonFunktionPressReleaseAendern(mainWindow.btnS3); }
        internal void TasterS4() { S4 = ButtonFunktionPressReleaseAendern(mainWindow.btnS4); }

        private void AlleFlaschenTask()
        {
            double LeerGeschwindigkeit = 0.000_01;

            while (true)
            {
                if (K1) Pegel -= LeerGeschwindigkeit;
                if (Pegel < 0) Pegel = 0;
                
                if (K2)
                {
                    int AktFlasche = alleFlaschen[0].GetAktuelleFlasche();
                    alleFlaschen[AktFlasche].FlascheVereinzeln();
                }

                B1 = false;
                foreach (Flaschen flasche in alleFlaschen) { B1 |= flasche.FlascheBewegen(M1); }
                
                AnzeigeAktualisieren();

                Thread.Sleep(10);
            }
        }


        private void AnzeigeAktualisieren()
        {
            ViAnzeige.PositionImage_1(alleFlaschen[0].AktuellePosition());
            ViAnzeige.PositionImage_2(alleFlaschen[1].AktuellePosition());
            ViAnzeige.PositionImage_3(alleFlaschen[2].AktuellePosition());
            ViAnzeige.PositionImage_4(alleFlaschen[3].AktuellePosition());
            ViAnzeige.PositionImage_5(alleFlaschen[4].AktuellePosition());
            ViAnzeige.PositionImage_6(alleFlaschen[5].AktuellePosition());

            ViAnzeige.VisibilityFlasche1(alleFlaschen[0].FlascheSichtbar());
            ViAnzeige.VisibilityFlasche2(alleFlaschen[1].FlascheSichtbar());
            ViAnzeige.VisibilityFlasche3(alleFlaschen[2].FlascheSichtbar());
            ViAnzeige.VisibilityFlasche4(alleFlaschen[3].FlascheSichtbar());
            ViAnzeige.VisibilityFlasche5(alleFlaschen[4].FlascheSichtbar());
            ViAnzeige.VisibilityFlasche6(alleFlaschen[5].FlascheSichtbar());


            ViAnzeige.VisibilitySensorB1(B1);
            ViAnzeige.VisibilityVentilK1(K1);
            ViAnzeige.VisibilityVentilK2(K2);
            ViAnzeige.VisibilityAbleitung(K1 && (Pegel > 0.01));

            ViAnzeige.FarbeCircle_F5(!F5);
            ViAnzeige.FarbeCircle_M1(M1);
            ViAnzeige.FarbeCircle_P1(P1);
            ViAnzeige.FarbeCircle_P2(P2);

            ViAnzeige.FarbeRectangleZuleitung(Pegel > 0.01);

            ViAnzeige.Margin_1(Pegel);
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
