using System.Threading;
using TestAutomat.AutoTester.Config;

namespace TestAutomat.AutoTester.Model
{
    internal partial class AlleTestBefehle
    {
        internal static void TestBefehlEingaengeTesten(TestsEinstellungen befehlsZeile)
        {
            //
            Thread.Sleep((int)befehlsZeile.Dauer.GetZeitDauerMs());
        }
    }
}