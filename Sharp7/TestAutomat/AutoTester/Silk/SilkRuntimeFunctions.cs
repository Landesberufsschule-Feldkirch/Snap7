using SoftCircuits.Silk;
using System;
using System.Linq;
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
                case "Print":
                    var resultText = string.Join("", e.Parameters.Select(p => p.ToString()));
                    // KatanaForm.form3.programOutputText.AppendText(resultText);
                    break;
                case "Debug":
                    var consoleText = string.Join("", e.Parameters.Select(p => p.ToString()));
                    Console.WriteLine(consoleText);
                    break;
                case "println":
                    var resultText1 = string.Join("", e.Parameters.Select(p => p.ToString()));
                    // KatanaForm.form3.programOutputText.AppendText(resultText1 + "\n");
                    break;
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
            var digitalInput = (uint)e.Parameters[0].ToInteger();
            Datenstruktur.DigInput[0] = PlcDatenTypen.Simatic.Digital_GetLowByte(digitalInput);
            Datenstruktur.DigInput[1] = PlcDatenTypen.Simatic.Digital_GetHighByte(digitalInput);
        }
        public static void Sleep(int sleepTime) => Thread.Sleep(sleepTime);
        private static void IncrementDataGridId() => AutoTesterWindow.DataGridId++;
        private static void ResetStopwatch() => SilkStopwatch.Restart();
        private static void UpdateAnzeige(FunctionEventArgs e)
        {

            var silkTestergebnis = e.Parameters[0].ToString();
            var silkKommentar = e.Parameters[0].ToString();
            Model.AutoTester.TestErgebnis ergebnis;

            switch (silkTestergebnis)
            {
                case "Aktiv": ergebnis = Model.AutoTester.TestErgebnis.Aktiv; break;
                case "Init": ergebnis = Model.AutoTester.TestErgebnis.Init; break;
                case "Erfolgreich": ergebnis = Model.AutoTester.TestErgebnis.Erfolgreich; break;
                case "Timeout": ergebnis = Model.AutoTester.TestErgebnis.Timeout; break;
                case "Fehler": ergebnis = Model.AutoTester.TestErgebnis.Fehler; break;

                default: ergebnis = Model.AutoTester.TestErgebnis.UnbekanntesErgebnis; break;
            }

            var digitalInput = PlcDatenTypen.Simatic.Digital_CombineTwoByte(Datenstruktur.DigInput[0], Datenstruktur.DigInput[1]);
            var digitalOutput = PlcDatenTypen.Simatic.Digital_CombineTwoByte(Datenstruktur.DigOutput[0], Datenstruktur.DigOutput[1]);

            var dInput = new PlcDatenTypen.Uint(digitalInput.ToString());
            var dOutput = new PlcDatenTypen.Uint(digitalOutput.ToString());

            AutoTesterWindow.UpdateDataGrid(new TestAusgabe(
                AutoTesterWindow.DataGridId,
                $"{SilkStopwatch.ElapsedMilliseconds}ms",
                ergebnis,
              dInput.GetHex16Bit() + "  " + dInput.GetBin16Bit(),
             dOutput.GetHex16Bit() + "  " + dOutput.GetBin16Bit(),
                silkKommentar));
        }

    }
}