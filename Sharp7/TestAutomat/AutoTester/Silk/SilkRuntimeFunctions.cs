using SoftCircuits.Silk;
using System.Diagnostics;
using System.Threading;
using TestAutomat.AutoTester.Model;

namespace TestAutomat.AutoTester.Silk
{
    public partial class Silk
    {
        public static void Runtime_Function(object sender, FunctionEventArgs e)
        {
            switch (e.Name)
            {
                case "Sleep":
                    var sleepTime = e.Parameters[0].ToInteger();
                    Sleep(sleepTime);
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
            var di = e.Parameters[0].ToString();
            var digitalInput = new PlcDatenTypen.Uint(di);
            Datenstruktur.DigInput[0] = PlcDatenTypen.Simatic.Digital_GetLowByte((uint)digitalInput.GetDec());
            Datenstruktur.DigInput[1] = PlcDatenTypen.Simatic.Digital_GetHighByte((uint)digitalInput.GetDec());
            Thread.Sleep(100);
        }
        public static void Sleep(int sleepTime) => Thread.Sleep(sleepTime);
        private static void IncrementDataGridId() => AutoTesterWindow.DataGridId++;
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
            var maxLaufzeit = e.Parameters[2].ToInteger();
            var kommentar = e.Parameters[3].ToString();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.ElapsedMilliseconds < maxLaufzeit)
            {
                var digitalOutput = PlcDatenTypen.Simatic.Digital_CombineTwoByte(Datenstruktur.DigOutput[0], Datenstruktur.DigOutput[1]);

                if ((digitalOutput & (short)bitMaske) == (short)bitMuster)
                {
                    DataGridAnzeigeUpdaten(Model.AutoTester.TestErgebnis.Erfolgreich, kommentar);
                    return;
                }

                DataGridAnzeigeUpdaten(Model.AutoTester.TestErgebnis.Aktiv, kommentar);

                Thread.Sleep(10);
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
                dInput.GetHex16Bit() + "  " + dInput.GetBin16Bit(),
                dOutput.GetHex16Bit() + "  " + dOutput.GetBin16Bit(),
                silkKommentar));
        }
        private static void Plc2Dec(FunctionEventArgs e)
        {
            var zahl = e.Parameters[0].ToString();
            var plcZahl = new PlcDatenTypen.Uint(zahl);
            e.ReturnValue.SetValue((int)plcZahl.GetDec());
        }
    }
}