using System.Threading;

namespace Ampel_Verbania.Model
{
    public class AmpelVerbania
    {
        public bool S1 { get; set; }        // Taster "Fußgänger" 
        public bool P11 { get; set; }       // Autoampel - rot 
        public bool P12 { get; set; }       // Autoampel - gelb 
        public bool P13 { get; set; }       // Autoampel - grün
        public bool P21 { get; set; }       // Fußgängerampel - rot 
        public bool P22 { get; set; }       // Fußgängerampel - gelb 
        public bool P23 { get; set; }       // Fußgängerampel - grün 
        public bool P31 { get; set; }       // Fußgängerampel - Anzeige rot
        public bool P32 { get; set; }       // Fußgängerampel - Anzeige gelb 
        public bool P33 { get; set; }       // Fußgängerampel - Anzeige grün 
        public int AmpelWert { get; set; }

        public AmpelVerbania()
        {

            System.Threading.Tasks.Task.Run(KataTask);
        }
        private static void KataTask()
        {
            while (true)
            {
                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}