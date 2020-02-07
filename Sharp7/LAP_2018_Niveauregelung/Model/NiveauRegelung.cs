using System.Threading;
using System.Windows.Controls;

namespace LAP_2018_Niveauregelung.Model
{
    public class NiveauRegelung
    {
        private readonly MainWindow mainWindow;
        public VisuAnzeigen ViAnzeige { get; set; }

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


        public NiveauRegelung(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            ViAnzeige = new VisuAnzeigen();

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

                AnzeigeAktualisieren();
                Thread.Sleep(10);
            }
        }

        private void AnzeigeAktualisieren()
        {
            ViAnzeige.FarbeTherorelais_F1(F1);
            ViAnzeige.FarbeTherorelais_F2(F2);            
            ViAnzeige.FarbeCircle_P1(P1);
            ViAnzeige.FarbeCircle_P2(P2);
            ViAnzeige.FarbeCircle_P3(P3);

            ViAnzeige.FarbeZuleitungLinksWaagrecht(M1);
            ViAnzeige.FarbeZuleitungLinksSenkrecht(M1);
            ViAnzeige.FarbeZuleitungRechtsWaagrecht(M2);
            ViAnzeige.FarbeZuleitungRechtsSenkrecht(M2);
            ViAnzeige.FarbeAbleitungOben(Pegel > 0.01);
            ViAnzeige.FarbeAbleitungUnten(Y1 && (Pegel > 0.01));

            ViAnzeige.VisibilitySensorB1(B1);
            ViAnzeige.VisibilitySensorB2(B2);
            ViAnzeige.VisibilitySensorB3(B3);
            ViAnzeige.VisibilityMotorM1(M1);
            ViAnzeige.VisibilityMotorM2(M2);
            ViAnzeige.VisibilityVentilY1(Y1);

            ViAnzeige.Margin_1(Pegel);
        }

        internal void TasterS1() { S1 = ButtonFunktionPressReleaseAendern(mainWindow.btnS1); }
        internal void TasterS2() { S2 = ButtonFunktionPressReleaseAendern(mainWindow.btnS2); }
        internal void TasterS3() { S3 = ButtonFunktionPressReleaseAendern(mainWindow.btnS3); }
        internal void ThermorelaisF1() { F1 = !F1; }
        internal void ThermorelaisF2() { F2 = !F2; }
        internal void SetManualM1() { if (mainWindow.DebugWindowAktiv) { M1 = !M1; } }
        internal void SetManualM2() { if (mainWindow.DebugWindowAktiv) { M2 = !M2; } }
        internal void VentilY1() { Y1 = !Y1; }

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
