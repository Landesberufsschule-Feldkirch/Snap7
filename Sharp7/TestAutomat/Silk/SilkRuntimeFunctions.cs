using Kommunikation;
using SoftCircuits.Silk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using TestAutomat.Model;

namespace TestAutomat.Silk
{
    public partial class Silk
    {
        private enum SchritteBlinken
        {
            AufPosFlankeWarten = 0,
            AufNegFlankeWarten
        }

        private static int _anzahlBitEingaenge = 16;
        private static int _anzahlBitAusgaenge = 16;

        public static void Runtime_Function(object sender, FunctionEventArgs e)
        {
            switch (e.Name)
            {
                case "Sleep": Sleep(e); break;
                case "GetDigitaleAusgaenge": GetDigitaleAusgaenge(e); break;
                case "SetDigitaleEingaenge": SetDigitaleEingaenge(e); break;
                case "UpdateAnzeige": UpdateAnzeige(e); break;
                case "IncrementDataGridId": IncrementDataGridId(); break;
                case "ResetStopwatch": ResetStopwatch(); break;
                case "BitmusterTesten": BitmusterTesten(e); break;
                case "Plc2Dec": Plc2Dec(e); break;
                case "SetDataGridBitAnzahl": SetDataGridBitAnzahl(e); break;
                case "BitmusterBlinktTesten": BitmusterBlinktTesten(e); break;
                case "KommentarAnzeigen": KommentarAnzeigen(e); break;
                case "VersionAnzeigen": VersionAnzeigen(); break;
                case "TestAblauf": TestAblauf(e); break;
            }
        }
        private static void GetDigitaleAusgaenge(FunctionEventArgs e)
        {
            var digitalOutput = PlcDatenTypen.Simatic.Digital_CombineTwoByte(Datenstruktur.DigOutput[0], Datenstruktur.DigOutput[1]);
            e.ReturnValue.SetValue((int)digitalOutput);
        }
        private static void SetDigitaleEingaenge(FunctionEventArgs e)
        {
            var digitalInput = new PlcDatenTypen.Uint((ulong)e.Parameters[0].ToInteger());
            Datenstruktur.DigInput[0] = PlcDatenTypen.Simatic.Digital_GetLowByte((uint)digitalInput.GetDec());
            Datenstruktur.DigInput[1] = PlcDatenTypen.Simatic.Digital_GetHighByte((uint)digitalInput.GetDec());
            Thread.Sleep(100);
        }
        public static void Sleep(FunctionEventArgs e)
        {
            var sleepTime = new PlcDatenTypen.ZeitDauer(e.Parameters[0].ToString());
            Thread.Sleep((int)sleepTime.GetZeitDauerMs());
        }
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
        private static void ResetStopwatch() => SilkStopwatch.Restart();
        private static void UpdateAnzeige(FunctionEventArgs e)
        {
            var silkTestergebnis = e.Parameters[0].ToString();
            var silkKommentar = e.Parameters[0].ToString();
            AutoTester.TestErgebnis ergebnis;

            // ReSharper disable once ConvertSwitchStatementToSwitchExpression
            switch (silkTestergebnis)
            {
                case "Aktiv": ergebnis = AutoTester.TestErgebnis.Aktiv; break;
                case "Init": ergebnis = AutoTester.TestErgebnis.Init; break;
                case "Erfolgreich": ergebnis = AutoTester.TestErgebnis.Erfolgreich; break;
                case "Timeout": ergebnis = AutoTester.TestErgebnis.Timeout; break;
                case "Fehler": ergebnis = AutoTester.TestErgebnis.Fehler; break;

                default: ergebnis = AutoTester.TestErgebnis.UnbekanntesErgebnis; break;
            }

            DataGridAnzeigeUpdaten(ergebnis, silkKommentar);
        }
        private static void BitmusterTesten(FunctionEventArgs e)
        {
            var bitMuster = e.Parameters[0].ToInteger();
            var bitMaske = e.Parameters[1].ToInteger();
            var timeout = new PlcDatenTypen.ZeitDauer(e.Parameters[2].ToString());
            var kommentar = e.Parameters[3].ToString();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.ElapsedMilliseconds < timeout.GetZeitDauerMs())
            {

                Thread.Sleep(10);

                var digitalOutput = PlcDatenTypen.Simatic.Digital_CombineTwoByte(Datenstruktur.DigOutput[0], Datenstruktur.DigOutput[1]);

                if ((digitalOutput & (short)bitMaske) == (short)bitMuster)
                {
                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Erfolgreich, kommentar);
                    return;
                }

                DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Aktiv, kommentar);
            }

            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Timeout, kommentar);
        }
        private static void DataGridAnzeigeUpdaten(AutoTester.TestErgebnis testErgebnis, string silkKommentar)
        {
            var digitalInput = PlcDatenTypen.Simatic.Digital_CombineTwoByte(Datenstruktur.DigInput[0], Datenstruktur.DigInput[1]);
            var digitalOutput = PlcDatenTypen.Simatic.Digital_CombineTwoByte(Datenstruktur.DigOutput[0], Datenstruktur.DigOutput[1]);

            var dInput = new PlcDatenTypen.Uint(digitalInput.ToString());
            var dOutput = new PlcDatenTypen.Uint(digitalOutput.ToString());

            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (testErgebnis)
            {
                case AutoTester.TestErgebnis.Kommentar:
                case AutoTester.TestErgebnis.Version:
                    AutoTesterWindow.UpdateDataGrid(new TestAusgabe(
                                   AutoTesterWindow.DataGridId,
                                   " ",
                                   testErgebnis,
                                   silkKommentar,
                                   " ",
                                   " "));
                    break;
                default:
                    AutoTesterWindow.UpdateDataGrid(new TestAusgabe(
                                   AutoTesterWindow.DataGridId,
                                   $"{SilkStopwatch.ElapsedMilliseconds}ms",
                                   testErgebnis,
                                   dInput.GetHexBit(_anzahlBitEingaenge) + "  " + dInput.GetBinBit(_anzahlBitEingaenge),
                                   dOutput.GetHexBit(_anzahlBitAusgaenge) + "  " + dOutput.GetBinBit(_anzahlBitAusgaenge),
                                   silkKommentar));
                    break;
            }
        }
        private static void Plc2Dec(FunctionEventArgs e)
        {
            var zahl = e.Parameters[0].ToString();
            var plcZahl = new PlcDatenTypen.Uint(zahl);
            e.ReturnValue.SetValue((int)plcZahl.GetDec());
        }
        private static void SetDataGridBitAnzahl(FunctionEventArgs e)
        {
            _anzahlBitEingaenge = e.Parameters[0].ToInteger();
            _anzahlBitAusgaenge = e.Parameters[1].ToInteger();
        }
        private static void BitmusterBlinktTesten(FunctionEventArgs e)
        {
            var bitMuster = e.Parameters[0].ToInteger();
            var bitMaske = e.Parameters[1].ToInteger();
            var periodenDauer = new PlcDatenTypen.ZeitDauer(e.Parameters[2].ToString());
            var tastVerhaeltnis = e.Parameters[3].ToFloat();
            var anzahlPerioden = e.Parameters[4].ToInteger();
            var toleranz = e.Parameters[5].ToFloat();
            var timeout = new PlcDatenTypen.ZeitDauer(e.Parameters[6].ToString());
            var kommentar = e.Parameters[7].ToString();

            var periodenDauerMax = periodenDauer.GetZeitDauerMs() * (1 + toleranz);
            var periodenDauerMin = periodenDauer.GetZeitDauerMs() * (1 - toleranz);

            var tastVerhaeltnisMax = tastVerhaeltnis * (1 + toleranz);
            var tastVerhaeltnisMin = tastVerhaeltnis * (1 - toleranz);

            var messungAktiv = false;
            var tastverhaeltnis = 0.0;
            var periodenAnzahl = 0;
            var zeitImpuls = 0.0;
            var zeitPause = 0.0;
            var schritte = SchritteBlinken.AufNegFlankeWarten;
            var periodenDauerMessen = new Stopwatch();
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.ElapsedMilliseconds < timeout.GetZeitDauerMs())
            {
                Thread.Sleep(10);

                var digitalOutput = PlcDatenTypen.Simatic.Digital_CombineTwoByte(Datenstruktur.DigOutput[0], Datenstruktur.DigOutput[1]);

                var aktuellePeriodenDauer = zeitImpuls + zeitPause;
                if (zeitImpuls > 0) tastverhaeltnis = zeitImpuls / aktuellePeriodenDauer;

                switch (schritte)
                {
                    case SchritteBlinken.AufPosFlankeWarten:
                        zeitPause = periodenDauerMessen.ElapsedMilliseconds;

                        if ((digitalOutput & (short)bitMaske) == (short)bitMuster)
                        {
                            if (messungAktiv)
                            {
                                if (aktuellePeriodenDauer > periodenDauerMax || aktuellePeriodenDauer < periodenDauerMin)
                                {
                                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Fehler, $"{kommentar}: Falsche Periodendauer: {aktuellePeriodenDauer}ms");
                                    return;
                                }

                                if (tastverhaeltnis > tastVerhaeltnisMax || tastverhaeltnis < tastVerhaeltnisMin)
                                {
                                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Fehler, $"{kommentar}: Falsches Tastverhältnis: {tastverhaeltnis:F2}ms");
                                    return;
                                }

                                periodenAnzahl++;
                                if (periodenAnzahl > anzahlPerioden)
                                {
                                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Erfolgreich, $"{kommentar}: E:{zeitImpuls}ms A: {zeitPause}ms → {100 * tastverhaeltnis:F1}%");
                                    return;
                                }
                            }
                            messungAktiv = true;
                            schritte = SchritteBlinken.AufNegFlankeWarten;
                            periodenDauerMessen.Restart();
                        }
                        break;

                    case SchritteBlinken.AufNegFlankeWarten:
                        zeitImpuls = periodenDauerMessen.ElapsedMilliseconds;
                        if ((digitalOutput & (short)bitMaske) == 0)
                        {
                            if (messungAktiv) periodenDauerMessen.Restart();
                            schritte = SchritteBlinken.AufPosFlankeWarten;
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Aktiv, $"{kommentar}: I:{zeitImpuls}ms A: {zeitPause}ms → {100 * tastverhaeltnis:F1}%");
            }

            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Timeout, kommentar);
        }
        private static void KommentarAnzeigen(FunctionEventArgs e)
        {
            var kommentar = e.Parameters[0].ToString();
            AutoTesterWindow.DataGridId++;
            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Kommentar, kommentar);
        }
        private static void VersionAnzeigen()
        {
            var textLaenge = Datenstruktur.VersionInputSps[1];
            var enc = new ASCIIEncoding();
            var version = enc.GetString(Datenstruktur.VersionInputSps, 2, textLaenge);

            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Version, version);
            AutoTesterWindow.DataGridId++;
        }
        private static void TestAblauf(FunctionEventArgs e)
        {
            var listeAblaufSet = new List<AblaufSet>();
            var listeAblaufTests = new List<AblaufTest>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < e.Parameters[0].ListCount; i++)
            {
                listeAblaufSet.Add(new AblaufSet(
                    (ulong)e.Parameters[0][i][0].ToInteger(),
                    e.Parameters[0][i][1].ToString(),
                    e.Parameters[0][i][2].ToString()));
            }

            for (var i = 0; i < e.Parameters[1].ListCount; i++)
            {
                listeAblaufTests.Add(new AblaufTest(
                    (ulong)e.Parameters[1][i][0].ToInteger(),
                    (ulong)e.Parameters[1][i][1].ToInteger(),
                    e.Parameters[1][i][2].ToString(),
                    e.Parameters[1][i][3].ToString(),
                    e.Parameters[1][i][4].ToString()));
            }

            var timeOut = listeAblaufTests.Sum(test => test.GetTimeoutMs());

            bool testAblaufFertig = false;
            var setAktuellerSchritt = 0;
            long setAbgelaufeneZeit = 0;
            var testAktuellerschritt = 0;
            long testAbgelaufeneZeit = 0;

            while (stopwatch.ElapsedMilliseconds < timeOut)
            {
                Thread.Sleep(10);

                (testAblaufFertig, setAktuellerSchritt, setAbgelaufeneZeit) = AblaufSetFunktion(listeAblaufSet, setAktuellerSchritt, setAbgelaufeneZeit, stopwatch, testAblaufFertig);
                (testAblaufFertig, testAktuellerschritt, testAbgelaufeneZeit) = AblaufTestFunktion(listeAblaufTests, testAktuellerschritt, testAbgelaufeneZeit, stopwatch, testAblaufFertig);
                if (testAblaufFertig) return;
            }
        }
        private static (bool fertig, int schritt, long zeit) AblaufSetFunktion(IReadOnlyList<AblaufSet> listeAblauf, int schritt, long zeit, Stopwatch stopwatch, bool fertig)
        {
            var setBitmuster = listeAblauf[schritt].GetBitmuster();
            Datenstruktur.DigInput[0] = PlcDatenTypen.Simatic.Digital_GetLowByte((uint)setBitmuster.GetDec());
            Datenstruktur.DigInput[1] = PlcDatenTypen.Simatic.Digital_GetHighByte((uint)setBitmuster.GetDec());

            if (listeAblauf[schritt].GetDauer().GetZeitDauerMs() == 0) return (fertig, schritt, zeit);
            if (stopwatch.ElapsedMilliseconds <= listeAblauf[schritt].GetDauer().GetZeitDauerMs() + zeit) return (fertig, schritt, zeit);

            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Erfolgreich, listeAblauf[schritt].GetKommentar());
            AutoTesterWindow.DataGridId++;
            zeit += listeAblauf[schritt].GetDauer().GetZeitDauerMs();
            schritt++;

            return (fertig, schritt, zeit);
        }
        private static (bool fertig, int schritt, long zeit) AblaufTestFunktion(IReadOnlyList<AblaufTest> listeAblauf, int schritt, long zeit, Stopwatch stopwatch, bool fertig)
        {
            var digitalOutput = PlcDatenTypen.Simatic.Digital_CombineTwoByte(Datenstruktur.DigOutput[0], Datenstruktur.DigOutput[1]);

            if ((digitalOutput & (short)listeAblauf[schritt].GetBitMaske().GetDec()) == (short)listeAblauf[schritt].GetBitMuster().GetDec())
            {
                listeAblauf[schritt].SetSchrittAktiv(true);
                DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Aktiv, listeAblauf[schritt].GetKommentar());

            }
            else
            {
                if (listeAblauf[schritt].GetSchrittAktiv())
                {
                    zeit = stopwatch.ElapsedMilliseconds;
                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Erfolgreich, listeAblauf[schritt].GetKommentar());
                    AutoTesterWindow.DataGridId++;
                    schritt++;
                    if (schritt >= listeAblauf.Count) return (true, schritt, zeit);
                }
                else
                {
                    //
                }
                //listeAblauf[schritt].SetLaufzeit(stopwatch.ElapsedMilliseconds - zeit);                             
                //  if (schritt >= listeAblauf.Count) schritt = listeAblauf.Count;
            }

            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Aktiv, listeAblauf[schritt].GetKommentar());

            //    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Aktiv, kommentar);


            if (stopwatch.ElapsedMilliseconds > zeit + listeAblauf[schritt].GetTimeoutMs())
            {
                DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Timeout, listeAblauf[schritt].GetKommentar());
                AutoTesterWindow.DataGridId++;
                zeit = stopwatch.ElapsedMilliseconds;
                schritt++;
                if (schritt >= listeAblauf.Count) return (true, schritt, zeit);
            }

            return (fertig, schritt, zeit);
        }
    }
}