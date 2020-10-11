using System.Threading;

namespace Blinker.Model
{
    public class Blinker    
    {
        public bool P1 { get; set; }
        public bool S1 { get; set; }
        public bool S2 { get; set; }
        public bool S3 { get; set; }
        public bool S4 { get; set; }
        public bool S5{ get; set; }

 

        public Blinker()
        {
         

            System.Threading.Tasks.Task.Run(BlinkerTask);
        }

        private static void BlinkerTask()
        {
           
            while (true)
            {

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}