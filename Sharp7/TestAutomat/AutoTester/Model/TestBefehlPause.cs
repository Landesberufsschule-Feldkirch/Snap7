using System.Threading;
using TestAutomat.AutoTester.Config;
using TestAutomat.AutoTester.ViewModel;

namespace TestAutomat.AutoTester.Model
{
    internal partial class AlleTestBefehle
    {
        internal static void TestBefehlPause(TestsEinstellungen befehlsZeile, AutoTesterViewModel autoTesterViewModel)
        {
            //
            Thread.Sleep((int)befehlsZeile.Dauer.GetZeitDauerMs());

            var ergebnis = new TestAusgabe(
                befehlsZeile.LaufendeNr,
                befehlsZeile.Befehl,
                befehlsZeile.EingaengeBitmuster,
                befehlsZeile.AusgaengeBitmuster,
                befehlsZeile.Kommentar);

            autoTesterViewModel.AutoTesterAnzeige.AddEinzelneZeileAnzeigen(ergebnis);
        }
    }
}