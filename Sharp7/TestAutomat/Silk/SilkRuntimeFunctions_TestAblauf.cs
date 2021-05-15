using System;
using Kommunikation;
using PlcDatenTypen;
using SoftCircuits.Silk;
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
                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Erfolgreich, 0, aufgabe.GetKommentar());
                    if (aufgabe.GetDauer().GetZeitDauerMs() == 0)
                    {
                        aufgabe.SetAktuellerStatus(DigEingaengeSetzen.StatusDigEingaenge.SchrittAbgeschlossen);
                        return true;
                    }

                    aufgabe.SetStartzeit(aktuelleZeit.ElapsedMilliseconds);
                    aufgabe.SetAktuellerStatus(DigEingaengeSetzen.StatusDigEingaenge.SchrittAktiv);
                    SetDigitaleEingaengeWord(listeDi[schritt].GetBitmuster());
                    return false;

                case DigEingaengeSetzen.StatusDigEingaenge.SchrittAktiv:
                    SetDigitaleEingaengeWord(aufgabe.GetBitmuster());

                    if (aktuelleZeit.ElapsedMilliseconds <= aufgabe.GetEndZeit()) return false;

                    aufgabe.SetAktuellerStatus(DigEingaengeSetzen.StatusDigEingaenge.SchrittAbgeschlossen);
                    DigEingaengeSetzen.SetNaechsterSchritt();
                    return false;

                case DigEingaengeSetzen.StatusDigEingaenge.SchrittAbgeschlossen: break;
                default: throw new ArgumentOutOfRangeException(aufgabe.GetAktuellerStatus().ToString());
            }
            return false;
        }


        private static bool FunktionDigAusgaenge(IReadOnlyList<DigAusgaengeTesten> listeDa, Stopwatch aktuelleZeit)
        {
            var schritt = DigAusgaengeTesten.GetAktuellerSchritt();
            var aufgabe = listeDa[schritt];

            var digBitmaske = aufgabe.GetBitMaske().GetDec();
            var digBitmuster = aufgabe.GetBitMuster().GetDec();
            var digOutputIst = GetDigitalOutputWord();



            switch (aufgabe.GetAktuellerStatus())
            {
                case DigAusgaengeTesten.StatusDigAusgaenge.Init:
                    aufgabe.SetStartzeit(aktuelleZeit.ElapsedMilliseconds);
                    aufgabe.SetAktuellerStatus(DigAusgaengeTesten.StatusDigAusgaenge.AufBitmusterWarten);
                    return false;

                case DigAusgaengeTesten.StatusDigAusgaenge.AufBitmusterWarten:
                    if ((digOutputIst & digBitmaske) == digBitmuster) aufgabe.SetAktuellerStatus(DigAusgaengeTesten.StatusDigAusgaenge.BitmusterLiegtAn);
                    break;

                case DigAusgaengeTesten.StatusDigAusgaenge.BitmusterLiegtAn:
                    break;

                case DigAusgaengeTesten.StatusDigAusgaenge.SchrittAktiv:
                    break;

                case DigAusgaengeTesten.StatusDigAusgaenge.SchrittAbgeschlossen:
                    break;

                default: throw new ArgumentOutOfRangeException(aufgabe.GetAktuellerStatus().ToString());
            }


            if (aktuelleZeit.ElapsedMilliseconds > aufgabe.GetTimeoutMs())
            {
                aufgabe.SetAktuellerStatus(DigAusgaengeTesten.StatusDigAusgaenge.Timeout);
                if (schritt >= listeDa.Count) return true;
                DigAusgaengeTesten.SetNaechsterSchritt();
                return false;
            }


            /*


            if ((digOutputIst & (short)digBitmaske) == (short)digBitmuster)
            {
                listeAusgaenge[schritt].SetSchrittAktiv(true);
                DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Aktiv, (uint)digBitmuster, listeAusgaenge[schritt].GetKommentar());

            }
            else
            {
                if (listeAusgaenge[schritt].GetSchrittAktiv())
                {
                    zeit = gesamteZeit.ElapsedMilliseconds;
                    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Erfolgreich, (uint)digBitmuster, listeAusgaenge[schritt].GetKommentar());
                    AutoTesterWindow.DataGridId++;
                    schritt++;
                    if (schritt >= listeAusgaenge.Count) return (true, schritt, zeit);
                }
                else
                {
                    //
                }
                //listeAblauf[schritt].SetLaufzeit(stopwatch.ElapsedMilliseconds - zeit);                             
                //  if (schritt >= listeAblauf.Count) schritt = listeAblauf.Count;
            }

            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Aktiv, (uint)digBitmuster, listeAusgaenge[schritt].GetKommentar());

            //    DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Aktiv, kommentar);


            if (gesamteZeit.ElapsedMilliseconds > zeit + listeAusgaenge[schritt].GetTimeoutMs())
            {
                DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Timeout, (uint)digBitmuster, listeAusgaenge[schritt].GetKommentar());
                AutoTesterWindow.DataGridId++;
                zeit = gesamteZeit.ElapsedMilliseconds;
                schritt++;
                if (schritt >= listeAusgaenge.Count) return (true, schritt, zeit);
            }

            return (fertig, schritt, zeit);

            */
            return false;
        }
    }
}