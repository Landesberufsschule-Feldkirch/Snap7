using System.Threading;

namespace VoltmeterMitSiebenSegmentAnzeige.Model
{
    public class Voltmeter
    {

        public byte[] AlleVoltmeter { get; set; } = new byte[10];

        public Voltmeter()
        {

            System.Threading.Tasks.Task.Run(ZeitenTask);
        }

        private void ZeitenTask()
        {

            while (true)
            {


                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }


    }
}