using System.Threading;

namespace AutomatischesLagersystem.Model
{
    public class AutomatischesLagersystem
    {
        private readonly MainWindow _mainWindow;

        public bool K1 { get; set; } // Regalbediengerät hinein
        public bool K2 { get; set; } // Regalbediengerät heraus
        public bool K3 { get; set; } // Regalbediengerät links
        public bool K4 { get; set; } // Regalbediengerät rechts
        public bool K5 { get; set; } // Regalbediengerät aufwärts
        public bool K6 { get; set; } // Regalbediengerät abwärts

        private readonly double _geschwindigkeit = 0.0001; //0.001;

        public KollisionRegalBestimmen KollisionRegal { get; set; }

        public AutomatischesLagersystem(MainWindow mw)
        {
            _mainWindow = mw;

            KollisionRegal = new KollisionRegalBestimmen();

            System.Threading.Tasks.Task.Run(AutomatischesLagersystemTask);
        }

        private void AutomatischesLagersystemTask()
        {
            while (true)
            {
                if (K1) _mainWindow.RegalBedienGeraet.FahreX(_geschwindigkeit);
                if (K2) _mainWindow.RegalBedienGeraet.FahreX(-_geschwindigkeit);
                if (K3) _mainWindow.RegalBedienGeraet.FahreY(-_geschwindigkeit);
                if (K4) _mainWindow.RegalBedienGeraet.FahreY(_geschwindigkeit);
                if (K5) _mainWindow.RegalBedienGeraet.FahreZ(_geschwindigkeit);
                if (K6) _mainWindow.RegalBedienGeraet.FahreZ(-_geschwindigkeit);

                if (_mainWindow.BediengeraetStartpositionen[3] != null)
                {
                    KollisionRegal.SetNeuePositionSchlitten(
                         _mainWindow.BediengeraetStartpositionen[3].GetX() + _mainWindow.RegalBedienGeraet.GetXPosition(),
                         _mainWindow.BediengeraetStartpositionen[3].GetY() + _mainWindow.RegalBedienGeraet.GetYPosition(),
                         _mainWindow.BediengeraetStartpositionen[3].GetZ() + _mainWindow.RegalBedienGeraet.GetZPosition());
                }

                Thread.Sleep(10);
            }
        }

        public char Zeichen { get; internal set; }
    }
}