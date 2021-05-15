using Kommunikation;
using PlcDatenTypen;
using SoftCircuits.Silk;
using System;
using System.Diagnostics;
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

        internal static uint GetDigtalInputWord() => Simatic.Digital_CombineTwoByte(Datenstruktur.DigInput[0], Datenstruktur.DigInput[1]);
        internal static uint GetDigitalOutputWord() => Simatic.Digital_CombineTwoByte(Datenstruktur.DigOutput[0], Datenstruktur.DigOutput[1]);
        private static void GetDigitaleAusgaenge(FunctionEventArgs e)
        {
            var digitalOutput = GetDigitalOutputWord();
            e.ReturnValue.SetValue((int)digitalOutput);
        }
        private static void BitmusterTesten(FunctionEventArgs e)
        {
            var bitMuster = e.Parameters[0].ToInteger();
            var bitMaske = e.Parameters[1].ToInteger();
            var timeout = new ZeitDauer(e.Parameters[2].ToString());
            var kommentar = e.Parameters[3].ToString();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.ElapsedMilliseconds < timeout.GetZeitDauerMs())
            {

                Thread.Sleep(10);

                var digitalOutput = GetDigitalOutputWord();

                if ((digitalOutput & (short)bitMaske) == (short)bitMuster)
                {
                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Erfolgreich, (uint)bitMuster, kommentar);
                    return;
                }

                DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Aktiv, (uint)bitMuster, kommentar);
            }

            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Timeout, (uint)bitMuster, kommentar);
        }
        private static void BitmusterBlinktTesten(FunctionEventArgs e)
        {
            var bitMuster = e.Parameters[0].ToInteger();
            var bitMaske = e.Parameters[1].ToInteger();
            var periodenDauer = new ZeitDauer(e.Parameters[2].ToString());
            var tastVerhaeltnis = e.Parameters[3].ToFloat();
            var anzahlPerioden = e.Parameters[4].ToInteger();
            var toleranz = e.Parameters[5].ToFloat();
            var timeout = new ZeitDauer(e.Parameters[6].ToString());
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

                var digitalOutput = GetDigitalOutputWord();

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
                                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Fehler, (uint)bitMuster, $"{kommentar}: Falsche Periodendauer: {aktuellePeriodenDauer}ms");
                                    return;
                                }

                                if (tastverhaeltnis > tastVerhaeltnisMax || tastverhaeltnis < tastVerhaeltnisMin)
                                {
                                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Fehler, (uint)bitMuster, $"{kommentar}: Falsches Tastverhältnis: {tastverhaeltnis:F2}ms");
                                    return;
                                }

                                periodenAnzahl++;
                                if (periodenAnzahl > anzahlPerioden)
                                {
                                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Erfolgreich, (uint)bitMuster, $"{kommentar}: E:{zeitImpuls}ms A: {zeitPause}ms → {100 * tastverhaeltnis:F1}%");
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

                DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Aktiv, (uint)bitMuster, $"{kommentar}: I:{zeitImpuls}ms A: {zeitPause}ms → {100 * tastverhaeltnis:F1}%");
            }

            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Timeout, (uint)bitMuster, kommentar);
        }
    }
}