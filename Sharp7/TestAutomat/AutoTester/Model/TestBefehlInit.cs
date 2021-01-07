using System.Threading;
using TestAutomat.AutoTester.Config;
using TestAutomat.AutoTester.ViewModel;

namespace TestAutomat.AutoTester.Model
{
    internal partial class AlleTestBefehle
    {
        internal static void TestBefehlInit(TestsEinstellungen befehlsZeile, AutoTesterViewModel autoTesterViewModel)
        {
            //
            Thread.Sleep((int)befehlsZeile.Dauer.GetZeitDauerMs());

            var ergebnis = new TestAusgabe(
                befehlsZeile.LaufendeNr,
                befehlsZeile.Befehl,
                befehlsZeile.EingaengeBitmuster.GetDec(),
                befehlsZeile.AusgaengeBitmuster.GetDec(), 
                befehlsZeile.Kommentar);
          
            autoTesterViewModel.AutoTesterAnzeige.AddEinzelneZeileAnzeigen(ergebnis);
        }
    }
}