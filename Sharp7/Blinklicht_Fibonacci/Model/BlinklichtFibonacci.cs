using System.Threading;

namespace Blinklicht_Fibonacci.Model
{
    public class BlinklichtFibonacci
    {
        public bool P1 { get; set; }
        public bool S1 { get; set; }

        public BlinklichtFibonacci() => System.Threading.Tasks.Task.Run(BlinklichtTask);
        private static void BlinklichtTask()
        {
            while (true)
            {
                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}