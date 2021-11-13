using Silk.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using TestAutomat.Model;

namespace TestAutomat.Silk
{
    public partial class Silk
    {
        private static void TestAblauf(FunctionEventArgs e)
        {
            var listeDigEingaenge = new List<DigEingaengeSetzen>();
            var listeDigAusgaenge = new List<DigAusgaengeTesten>();

            for (var i = 0; i < e.Parameters[0].ListCount; i++)
            {
                listeDigEingaenge.Add(new DigEingaengeSetzen(
                    (ulong)e.Parameters[0][i][0].ToInteger(),   // BitMuster
                    e.Parameters[0][i][1].ToString(),              // ZeitDauer
                    e.Parameters[0][i][2].ToString()));         // Kommentar
            }
            DigEingaengeSetzen.SetAktuellerSchritt(0);

            for (var i = 0; i < e.Parameters[1].ListCount; i++)
            {
                listeDigAusgaenge.Add(new DigAusgaengeTesten(
                    (ulong)e.Parameters[1][i][0].ToInteger(),   // Bitmuster
                    (ulong)e.Parameters[1][i][1].ToInteger(),    // Bitmaske
                    e.Parameters[1][i][2].ToString(),              // Dauer
                    e.Parameters[1][i][3].ToFloat(),             // Toleranz
                    e.Parameters[1][i][4].ToString(),            // TimeOut
                    e.Parameters[1][i][5].ToString()));         // Kommentar
            }
            DigAusgaengeTesten.SetAktuellerSchritt(0);

            var gesamteTimeOutZeit = listeDigAusgaenge.Sum(test => test.GetTimeoutMs());

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            while (stopwatch.ElapsedMilliseconds < gesamteTimeOutZeit)
            {
                Thread.Sleep(10);

                var testAblaufDigEingaengeFertig = FunktionDigEingaenge(listeDigEingaenge, stopwatch);
                var testAblaufDigAusgaengeFertig = FunktionDigAusgaenge(listeDigAusgaenge, stopwatch);

                if (testAblaufDigEingaengeFertig && testAblaufDigAusgaengeFertig) return;
            }
            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Timeout, 0, "uups");
        }
        private static bool FunktionDigEingaenge(IReadOnlyList<DigEingaengeSetzen> listeDi, Stopwatch aktuelleZeit)
        {
            var schritt = DigEingaengeSetzen.GetAktuellerSchritt();
            var aufgabe = listeDi[schritt];

            switch (aufgabe.GetAktuellerStatus())
            {
                case DigEingaengeSetzen.StatusDigEingaenge.Init:
                    if (aufgabe.GetDauer().DauerMs == 0)
                    {
                        aufgabe.SetAktuellerStatus(DigEingaengeSetzen.StatusDigEingaenge.SchrittAbgeschlossen);
                        return true;
                    }

                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Erfolgreich, 0, "DI[" + schritt + "]: " + aufgabe.GetKommentar());
                    aufgabe.SetStartzeit(aktuelleZeit.ElapsedMilliseconds);
                    aufgabe.SetAktuellerStatus(DigEingaengeSetzen.StatusDigEingaenge.SchrittAktiv);
                    SetDigitaleEingaengeWord(aufgabe.GetBitmuster());
                    return false;

                case DigEingaengeSetzen.StatusDigEingaenge.SchrittAktiv:
                    SetDigitaleEingaengeWord(aufgabe.GetBitmuster());

                    if (aktuelleZeit.ElapsedMilliseconds <= aufgabe.GetEndZeit()) return false;

                    aufgabe.SetAktuellerStatus(DigEingaengeSetzen.StatusDigEingaenge.SchrittAbgeschlossen);
                    DigEingaengeSetzen.SetNaechsterSchritt();
                    return false;

                case DigEingaengeSetzen.StatusDigEingaenge.SchrittAbgeschlossen:
                    SetDigitaleEingaengeWord(aufgabe.GetBitmuster());
                    break;
                default: throw new ArgumentOutOfRangeException(aufgabe.GetAktuellerStatus().ToString());
            }
            return false;
        }
        private static bool FunktionDigAusgaenge(IReadOnlyList<DigAusgaengeTesten> listeDa, Stopwatch aktuelleZeit)
        {
            var schritt = DigAusgaengeTesten.GetAktuellerSchritt();
            if (schritt >= listeDa.Count) return true;
            var aufgabe = listeDa[schritt];

            var digBitmaske = aufgabe.GetBitMaske().GetDec();
            var digBitmuster = aufgabe.GetBitMuster().GetDec();
            var digOutputIst = GetDigitalOutputWord();

            switch (aufgabe.GetAktuellerStatus())
            {
                case DigAusgaengeTesten.StatusDigAusgaenge.Init:
                    aufgabe.SetStartzeit(aktuelleZeit.ElapsedMilliseconds);
                    aufgabe.SetAktuellerStatus(DigAusgaengeTesten.StatusDigAusgaenge.AufBitmusterWarten);
                    AutoTesterWindow.DataGridId++;
                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Aktiv, (uint)digBitmuster, "DA[" + schritt + "]: " + aufgabe.GetKommentar());
                    return false;

                case DigAusgaengeTesten.StatusDigAusgaenge.AufBitmusterWarten:
                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.AufBitmusterWarten, (uint)digBitmuster, "DA[" + schritt + "]: " + aufgabe.GetKommentar());
                    if ((digOutputIst & digBitmaske) == digBitmuster) aufgabe.SetAktuellerStatus(DigAusgaengeTesten.StatusDigAusgaenge.BitmusterLiegtAn);
                    if (aktuelleZeit.ElapsedMilliseconds > aufgabe.GetTimeoutMs())
                    {
                        DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Timeout, (uint)digBitmuster, "DA[" + schritt + "]: " + aufgabe.GetKommentar());
                        aufgabe.SetAktuellerStatus(DigAusgaengeTesten.StatusDigAusgaenge.Timeout);
                        DigAusgaengeTesten.SetNaechsterSchritt();
                        return false;
                    }
                    break;

                case DigAusgaengeTesten.StatusDigAusgaenge.BitmusterLiegtAn:
                    if ((digOutputIst & digBitmaske) != digBitmuster)
                    {
                        if (aktuelleZeit.ElapsedMilliseconds < aufgabe.GetZeitdauerMin())
                        {
                            aufgabe.SetAktuellerStatus(DigAusgaengeTesten.StatusDigAusgaenge.SchrittAbgeschlossen);
                            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.ImpulsWarZuKurz, (uint)digBitmuster, "DA[" + schritt + "]: " + aufgabe.GetKommentar());
                            DigAusgaengeTesten.SetNaechsterSchritt();
                        }

                        if (aktuelleZeit.ElapsedMilliseconds < aufgabe.GetZeitdauerMax())
                        {
                            aufgabe.SetAktuellerStatus(DigAusgaengeTesten.StatusDigAusgaenge.SchrittAbgeschlossen);
                            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Erfolgreich, (uint)digBitmuster, "DA[" + schritt + "]: " + aufgabe.GetKommentar());
                            DigAusgaengeTesten.SetNaechsterSchritt();
                        }
                    }

                    if (aktuelleZeit.ElapsedMilliseconds <= aufgabe.GetZeitdauerMax()) return false;

                    aufgabe.SetAktuellerStatus(DigAusgaengeTesten.StatusDigAusgaenge.SchrittAbgeschlossen);
                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.ImpulsWarZuLang, (uint)digBitmuster, "DA[" + schritt + "]: " + aufgabe.GetKommentar());
                    DigAusgaengeTesten.SetNaechsterSchritt();
                    return false;

                case DigAusgaengeTesten.StatusDigAusgaenge.SchrittAbgeschlossen:
                case DigAusgaengeTesten.StatusDigAusgaenge.Timeout:
                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Fehler, (uint)digBitmuster, "DA[" + schritt + "]: " + "Status:" + aufgabe.GetAktuellerStatus());
                    return false;
                default: throw new ArgumentOutOfRangeException(aufgabe.GetAktuellerStatus().ToString());
            }
            return false;
        }
    }
}