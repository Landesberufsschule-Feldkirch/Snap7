using System.Threading;

namespace VoltmeterMitSiebenSegmentAnzeige.Model
{
    public class Voltmeter
    {

        public byte[] AlleVoltmeter { get; set; }

        public Voltmeter()
        {
            AlleVoltmeter = new byte[10];
            System.Threading.Tasks.Task.Run(VoltmeterTask);
        }

        private static void VoltmeterTask()
        {

            while (true)
            {
                //
                
                Thread.Sleep(200);
            }
            // ReSharper disable once FunctionNeverReturns
        }


    }
}