using System.Threading;
using DigitalerZwillingMitAutoTests;

namespace Kata.Model;

public class Kata
{
    public bool S1 { get; set; }
    public bool S2 { get; set; }
    public bool S3 { get; set; }
    public bool S4 { get; set; }
    public bool S5 { get; set; }
    public bool S6 { get; set; }
    public bool S7 { get; set; }
    public bool S8 { get; set; }

    public bool P1 { get; set; }
    public bool P2 { get; set; }
    public bool P3 { get; set; }
    public bool P4 { get; set; }
    public bool P5 { get; set; }
    public bool P6 { get; set; }
    public bool P7 { get; set; }
    public bool P8 { get; set; }

    private static DatenRangieren _datenRangieren;
    private DtAutoTests _dtAutoTests;

    private static void KataTask()
    {
        while (true)
        {
            _datenRangieren.Rangieren();

            Thread.Sleep(10);
        }
        // ReSharper disable once FunctionNeverReturns
    }
    internal void SetDtAutoTests(DtAutoTests dtAutoTests)
    {
        _dtAutoTests = dtAutoTests;
        _datenRangieren = new DatenRangieren(this, _dtAutoTests);

        S3 = true;
        S4 = true;
        S7 = true;
        S8 = true;

        System.Threading.Tasks.Task.Run(KataTask);
    }
}