using System.Threading;

namespace AutomatischesLagersystem.Model
{
    public class AutomatischesLagersystem
    {
        readonly MainWindow mainWindow;

        
        public bool K1 { get; set; } // Regalbediengerät hinein
        public bool K2 { get; set; } // Regalbediengerät heraus
        public bool K3 { get; set; } // Regalbediengerät links
        public bool K4 { get; set; } // Regalbediengerät rechts
        public bool K5 { get; set; } // Regalbediengerät aufwärts
        public bool K6 { get; set; } // Regalbediengerät abwärts

        private readonly double Geschwindigkeit = 0.001;


        public AutomatischesLagersystem(MainWindow mw)
        {
            mainWindow = mw;

            System.Threading.Tasks.Task.Run(() => AutomatischesLagersystemTask());
        }

        private void AutomatischesLagersystemTask()
        {
            while (true)
            {
                if (K1) mainWindow.RegalBedienGeraet.FahreX(Geschwindigkeit);
                if (K2) mainWindow.RegalBedienGeraet.FahreX(-Geschwindigkeit);     
                if (K3) mainWindow.RegalBedienGeraet.FahreY(-Geschwindigkeit);
                if (K4) mainWindow.RegalBedienGeraet.FahreY(Geschwindigkeit);    
                if (K5) mainWindow.RegalBedienGeraet.FahreZ(Geschwindigkeit);
                if (K6) mainWindow.RegalBedienGeraet.FahreZ(-Geschwindigkeit);

                Thread.Sleep(10);
            }
        }

        public char Zeichen { get; internal set; }

      
    }
}
