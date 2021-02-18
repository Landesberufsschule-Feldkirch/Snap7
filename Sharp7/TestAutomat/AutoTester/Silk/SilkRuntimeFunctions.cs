using SoftCircuits.Silk;
using System.Diagnostics;
using System.Threading;
using Kommunikation;
using TestAutomat.AutoTester.Model;

namespace TestAutomat.AutoTester.Silk
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
                case "Sleep":
                    Sleep(e);
                    break;

                case "GetDigitaleAusgaenge":
                    GetDigitaleAusgaenge(e);
                    break;

                case "SetDigitaleEingaenge":
                    SetDigitaleEingaenge(e);
                    break;

                case "UpdateAnzeige":
                    UpdateAnzeige(e);
                    break;

                case "IncrementDataGridId":
                    IncrementDataGridId();
                    break;

                case "ResetStopwatch":
                    ResetStopwatch();
                    break;

                case "BitmusterTesten":
                    BitmusterTesten(e);
                    break;

                case "Plc2Dec":
                    Plc2Dec(e);
                    break;

                case "SetDataGridBitAnzahl":
                    SetDataGridBitAnzahl(e);
                    break;

                case "BitmusterBlinktTesten":
                    BitmusterBlinktTesten(e);
                    break;
            }
        }
        private static void GetDigitaleAusgaenge(FunctionEventArgs e)
        {
            //
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
            Model.AutoTester.TestErgebnis ergebnis;

            // ReSharper disable once ConvertSwitchStatementToSwitchExpression
            switch (silkTestergebnis)
            {
                case "Aktiv": ergebnis = Model.AutoTester.TestErgebnis.Aktiv; break;
                case "Init": ergebnis = Model.AutoTester.TestErgebnis.Init; break;
                case "Erfolgreich": ergebnis = Model.AutoTester.TestErgebnis.Erfolgreich; break;
                case "Timeout": ergebnis = Model.AutoTester.TestErgebnis.Timeout; break;
                case "Fehler": ergebnis = Model.AutoTester.TestErgebnis.Fehler; break;

                default: ergebnis = Model.AutoTester.TestErgebnis.UnbekanntesErgebnis; break;
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
                    DataGridAnzeigeUpdaten(Model.AutoTester.TestErgebnis.Erfolgreich, kommentar);
                    return;
                }

                DataGridAnzeigeUpdaten(Model.AutoTester.TestErgebnis.Aktiv, kommentar);
            }

            DataGridAnzeigeUpdaten(Model.AutoTester.TestErgebnis.Timeout, kommentar);
        }
        private static void DataGridAnzeigeUpdaten(Model.AutoTester.TestErgebnis testErgebnis, string silkKommentar)
        {
            var digitalInput = PlcDatenTypen.Simatic.Digital_CombineTwoByte(Datenstruktur.DigInput[0], Datenstruktur.DigInput[1]);
            var digitalOutput = PlcDatenTypen.Simatic.Digital_CombineTwoByte(Datenstruktur.DigOutput[0], Datenstruktur.DigOutput[1]);

            var dInput = new PlcDatenTypen.Uint(digitalInput.ToString());
            var dOutput = new PlcDatenTypen.Uint(digitalOutput.ToString());

            AutoTesterWindow.UpdateDataGrid(new TestAusgabe(
                AutoTesterWindow.DataGridId,
                $"{SilkStopwatch.ElapsedMilliseconds}ms",
                testErgebnis,
                dInput.GetHexBit(_anzahlBitEingaenge) + "  " + dInput.GetBinBit(_anzahlBitEingaenge),
                dOutput.GetHexBit(_anzahlBitAusgaenge) + "  " + dOutput.GetBinBit(_anzahlBitAusgaenge),
                silkKommentar));
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

            var aktuellePeriodenDauer = 0.0;
            var messungAktiv = false;
            var tastverhaeltnis = 0.0;
            var periodenAnzahl = 0;
            var aktuelleEinZeit = 0.0;
            var schritte = SchritteBlinken.AufNegFlankeWarten;
            var periodenDauerMessen = new Stopwatch();
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.ElapsedMilliseconds < timeout.GetZeitDauerMs())
            {
                Thread.Sleep(10);

                var digitalOutput = PlcDatenTypen.Simatic.Digital_CombineTwoByte(Datenstruktur.DigOutput[0], Datenstruktur.DigOutput[1]);

                switch (schritte)
                {
                    case SchritteBlinken.AufPosFlankeWarten:
                        if ((digitalOutput & (short)bitMaske) == (short)bitMuster)
                        {
                            if (messungAktiv)
                            {
                                aktuellePeriodenDauer = periodenDauerMessen.ElapsedMilliseconds;
                                tastverhaeltnis = aktuelleEinZeit / aktuellePeriodenDauer;
                                if (aktuellePeriodenDauer > periodenDauerMax || aktuellePeriodenDauer < periodenDauerMin)
                                {
                                    DataGridAnzeigeUpdaten(Model.AutoTester.TestErgebnis.Fehler, $"{kommentar}: Falsche Periodendauer: {aktuellePeriodenDauer}ms");
                                    return;
                                }

                                if (tastverhaeltnis > tastVerhaeltnisMax || tastverhaeltnis < tastVerhaeltnisMin)
                                {
                                    DataGridAnzeigeUpdaten(Model.AutoTester.TestErgebnis.Fehler, $"{kommentar}: Falsches Tastverhältnis: {tastverhaeltnis:F2}ms");
                                    return;
                                }

                                periodenAnzahl++;
                                if (periodenAnzahl > anzahlPerioden)
                                {
                                    DataGridAnzeigeUpdaten(Model.AutoTester.TestErgebnis.Erfolgreich, $"{kommentar}: T: {aktuellePeriodenDauer}ms / {100 * tastverhaeltnis:F1}%");
                                    return;
                                }
                            }
                            messungAktiv = true;
                            schritte = SchritteBlinken.AufNegFlankeWarten;
                            periodenDauerMessen.Reset();
                            periodenDauerMessen.Restart();
                        }
                        break;

                    case SchritteBlinken.AufNegFlankeWarten:
                        if ((digitalOutput & (short)bitMaske) == 0)
                        {
                            if (messungAktiv) aktuelleEinZeit = periodenDauerMessen.ElapsedMilliseconds;
                            schritte = SchritteBlinken.AufPosFlankeWarten;
                        }
                        break;
                }

                DataGridAnzeigeUpdaten(Model.AutoTester.TestErgebnis.Aktiv, $"{kommentar}: T: {aktuellePeriodenDauer}ms / {100 * tastverhaeltnis:F1}%");
            }

            DataGridAnzeigeUpdaten(Model.AutoTester.TestErgebnis.Timeout, kommentar);
        }
    }
}