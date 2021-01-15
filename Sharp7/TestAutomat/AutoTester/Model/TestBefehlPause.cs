using System.Threading;
using TestAutomat.AutoTester.Config;

namespace TestAutomat.AutoTester.Model
{
    internal partial class AlleTestBefehle
    {
        internal static TestAusgabe TestBefehlPause(TestsEinstellungen befehlsZeile)
        {
            //
            Thread.Sleep((int)befehlsZeile.Dauer.GetZeitDauerMs());

            var ergebnis = new TestAusgabe(
                befehlsZeile.LaufendeNr,
                befehlsZeile.Befehl,
                befehlsZeile.EingaengeBitmuster,
                befehlsZeile.AusgaengeBitmuster,
                befehlsZeile.Kommentar);

            return ergebnis;
        }
    }
}