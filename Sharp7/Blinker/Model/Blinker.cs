using System.Diagnostics;
using System.Threading;

namespace Blinker.Model;

public class Blinker
{
    public bool P1 { get; set; }
    public bool S1 { get; set; }
    public bool S2 { get; set; }
    public bool S3 { get; set; }
    public bool S4 { get; set; }
    public bool S5 { get; set; }

    public double Frequenz { get; set; }
    public double Tastverhaeltnis { get; set; }
    public double EinZeit { get; set; }
    public double AusZeit { get; set; }

    public Blinker() => System.Threading.Tasks.Task.Run(BlinkerTask);
    private void BlinkerTask()
    {
        var p1Alt = false;

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        while (true)
        {
            long zeitDauer;
            switch (P1)
            {
                // positive Flanke
                case true when !p1Alt:
                    zeitDauer = stopwatch.ElapsedMilliseconds;
                    stopwatch.Restart();
                    AusZeit = zeitDauer;
                    break;
                // negative Flanke
                case false when p1Alt:
                    zeitDauer = stopwatch.ElapsedMilliseconds;
                    stopwatch.Restart();
                    EinZeit = zeitDauer;
                    break;
            }

            p1Alt = P1;

            var periodenDauer = EinZeit + AusZeit;
            Frequenz = 1000 / periodenDauer;
            Tastverhaeltnis = 100 * EinZeit / periodenDauer;
            Thread.Sleep(10);
        }
        // ReSharper disable once FunctionNeverReturns
    }
}