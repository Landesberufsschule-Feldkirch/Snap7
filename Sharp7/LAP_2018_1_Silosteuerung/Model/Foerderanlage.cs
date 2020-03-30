using System.Threading;

namespace LAP_2018_1_Silosteuerung.Model
{
    public class Foerderanlage
    {
        public Wagen Wagen { get; set; }
        public Silo Silo { get; set; }
        public bool S0 { get; set; }        // Taster Aus
        public bool S1 { get; set; }        // Taster Ein
        public bool S2 { get; set; }        // Not-Halt
        public bool S3 { get; set; }        // Taster Start
        public bool S4 { get; set; }        // Wagen Position rechts
        public bool S5 { get; set; }        // Wagen voll
        public bool F1 { get; set; }        //  Motorschutzschalter Förderband
        public bool F2 { get; set; }        //  Motorschutzschalter Schneckenförderer

        public bool P1 { get; set; }        // Anlage Ein
        public bool P2 { get; set; }        // Sammelstörung
        public bool Q1 { get; set; }        // Förderband 
        public bool Q2 { get; set; }        // Schneckenförderer
        public bool Y1 { get; set; }        // Materialschieber Silo            
        public bool Manual_M1 { get; set; }
        public bool Manual_M2 { get; set; }
        public bool Manual_Y1 { get; set; }


        private readonly MainWindow mainWindow;


        public Foerderanlage(MainWindow mw)
        {
            mainWindow = mw;

            Wagen = new Wagen();
            Silo = new Silo();



            System.Threading.Tasks.Task.Run(() => FoerderanlageTask());
        }

        private void FoerderanlageTask()
        {
            while (true)
            {


                Thread.Sleep(10);
            }
        }


    }
}
