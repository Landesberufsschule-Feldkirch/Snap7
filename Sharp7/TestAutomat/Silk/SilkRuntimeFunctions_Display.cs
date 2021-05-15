using Kommunikation;
using PlcDatenTypen;
using SoftCircuits.Silk;
using System.Text;
using System.Threading;
using TestAutomat.Model;

namespace TestAutomat.Silk
{
    public partial class Silk
    {
        private static int _anzahlBitEingaenge = 16;
        private static int _anzahlBitAusgaenge = 16;
        private static void IncrementDataGridId()
        {
            AutoTesterWindow.DataGridId++;

            if (Datenstruktur.BetriebsartTestablauf == BetriebsartTestablauf.Automatik) return;

            while (!Datenstruktur.NaechstenSchrittGehen)
            {
                Thread.Sleep(10);
            }

            Datenstruktur.NaechstenSchrittGehen = false;
        }
        private static void UpdateAnzeige(FunctionEventArgs e)
        {
            var silkTestergebnis = e.Parameters[0].ToString();
            var silkKommentar = e.Parameters[1].ToString();
            AutoTester.TestErgebnis ergebnis;

            // ReSharper disable once ConvertSwitchStatementToSwitchExpression
            switch (silkTestergebnis)
            {
                case "Kommentar": ergebnis = AutoTester.TestErgebnis.Kommentar; break;
                case "Aktiv": ergebnis = AutoTester.TestErgebnis.Aktiv; break;
                case "Init": ergebnis = AutoTester.TestErgebnis.Init; break;
                case "Erfolgreich": ergebnis = AutoTester.TestErgebnis.Erfolgreich; break;
                case "Timeout": ergebnis = AutoTester.TestErgebnis.Timeout; break;
                case "Fehler": ergebnis = AutoTester.TestErgebnis.Fehler; break;

                default: ergebnis = AutoTester.TestErgebnis.UnbekanntesErgebnis; break;
            }

            DataGridAnzeigeUpdaten(ergebnis, 0, silkKommentar);
        }
        private static void DataGridAnzeigeUpdaten(AutoTester.TestErgebnis testErgebnis, uint digOutSoll, string silkKommentar)
        {
            var digitalInput = GetDigtalInputWord();
            var digitalOutput = GetDigitalOutputWord();

            var dInput = new Uint(digitalInput.ToString());
            var dOutputIst = new Uint(digitalOutput.ToString());
            var dOutputSoll = new Uint(digOutSoll.ToString());
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (testErgebnis)
            {
                case AutoTester.TestErgebnis.Kommentar:
                case AutoTester.TestErgebnis.Version:
                    AutoTesterWindow.UpdateDataGrid(new TestAusgabe(
                                   AutoTesterWindow.DataGridId,
                                   " ",
                                   testErgebnis,
                                   silkKommentar,   // DigInput 
                                   " ",             // DigOutput Soll
                                   " ",             // DigOutput Ist
                                   " "));
                    break;
                default:
                    AutoTesterWindow.UpdateDataGrid(new TestAusgabe(
                                   AutoTesterWindow.DataGridId,
                                   $"{SilkStopwatch.ElapsedMilliseconds}ms",
                                   testErgebnis,
                                   dInput.GetHexBit(_anzahlBitEingaenge) + "  " + dInput.GetBinBit(_anzahlBitEingaenge),            // DigInput 
                                   dOutputSoll.GetHexBit(_anzahlBitAusgaenge) + "  " + dOutputSoll.GetBinBit(_anzahlBitAusgaenge),  // DigOutput Soll                                                                                   // DigOutput Soll
                                   dOutputIst.GetHexBit(_anzahlBitAusgaenge) + "  " + dOutputIst.GetBinBit(_anzahlBitAusgaenge),    // DigOutput Ist
                                   silkKommentar));
                    break;
            }
        }
        private static void KommentarAnzeigen(FunctionEventArgs e)
        {
            var kommentar = e.Parameters[0].ToString();
            AutoTesterWindow.DataGridId++;
            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Kommentar, 0, kommentar);
        }
        private static void VersionAnzeigen()
        {
            var textLaenge = Datenstruktur.VersionInputSps[1];
            var enc = new ASCIIEncoding();
            var version = enc.GetString(Datenstruktur.VersionInputSps, 2, textLaenge);

            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Version, 0, version);
            AutoTesterWindow.DataGridId++;
        }
    }
}