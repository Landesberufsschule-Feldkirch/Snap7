using System.Diagnostics;
using System.Threading;
using PlcDatenTypen;
using TestAutomat.AutoTester.Config;

namespace TestAutomat.AutoTester.Model
{
    internal partial class AlleTestBefehle
    {
        internal static void TestBefehlInit(AutoTesterWindow autoTesterWindow, TestsEinstellungen befehlsZeile, Kommunikation.Datenstruktur datenstruktur)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            // Little und Bit Endian verwirrt irgendwann jeden
            datenstruktur.DigInput[0] = Simatic.Simatic_Digital_GetLowByte((uint)befehlsZeile.EingaengeBitmuster.GetDec());
            datenstruktur.DigInput[1] = Simatic.Simatic_Digital_GetHighByte((uint)befehlsZeile.EingaengeBitmuster.GetDec());

            do
            {
                autoTesterWindow.UpdateDataGrid(new TestAusgabe(
                befehlsZeile.LaufendeNr,
                ZeitDauer.ConvertLong2Ms(stopwatch.ElapsedMilliseconds),
                TestErgebnis.Aktiv,
                befehlsZeile.Befehl,
                befehlsZeile.EingaengeBitmuster,
                new Uint(Simatic.Simatic_Digital_CombineTwoByte(datenstruktur.DigOutput[0], datenstruktur.DigOutput[1]).ToString()),
                befehlsZeile.Kommentar));

                Thread.Sleep(10);
            } while (stopwatch.ElapsedMilliseconds < befehlsZeile.Dauer.GetZeitDauerMs());

            autoTesterWindow.UpdateDataGrid(new TestAusgabe(
                befehlsZeile.LaufendeNr,
                ZeitDauer.ConvertLong2Ms(stopwatch.ElapsedMilliseconds),
                TestErgebnis.Erfolgreich,
                befehlsZeile.Befehl,
                befehlsZeile.EingaengeBitmuster,
                new Uint(Simatic.Simatic_Digital_CombineTwoByte(datenstruktur.DigOutput[0], datenstruktur.DigOutput[1]).ToString()),
                befehlsZeile.Kommentar));
        }
    }
}