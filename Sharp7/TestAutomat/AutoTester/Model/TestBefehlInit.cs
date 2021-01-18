using System.Threading;
using PlcDatenTypen;
using TestAutomat.AutoTester.Config;
using Utilities;

namespace TestAutomat.AutoTester.Model
{
    internal partial class AlleTestBefehle
    {
        internal static TestAusgabe TestBefehlInit(TestsEinstellungen befehlsZeile, Kommunikation.Datenstruktur datenstruktur)
        {

            datenstruktur.DigInput[0] = Simatic.Simatic_Digital_GetLowByte((uint)befehlsZeile.EingaengeBitmuster.GetDec());
            datenstruktur.DigInput[1] = Simatic.Simatic_Digital_GetHighByte((uint)befehlsZeile.EingaengeBitmuster.GetDec());

            Thread.Sleep((int)befehlsZeile.Dauer.GetZeitDauerMs());

            var ergebnis = new TestAusgabe(
                befehlsZeile.LaufendeNr,
                TestErgebnis.Erfolgreich,
                befehlsZeile.Befehl,
                befehlsZeile.EingaengeBitmuster,
                new PlcUint( Simatic.Simatic_Digital_CombineTwoByte( datenstruktur.DigOutput[0], datenstruktur.DigOutput[1]).ToString()), 
                befehlsZeile.Kommentar);

            return ergebnis;
        }
    }
}