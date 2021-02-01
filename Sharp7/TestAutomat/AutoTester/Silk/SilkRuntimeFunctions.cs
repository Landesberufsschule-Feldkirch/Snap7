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




        private static void GetDigitaleAusgaenge(FunctionEventArgs functionEventArgs)
        {
            //
        }

        private static void SetDigitaleEingaenge(FunctionEventArgs functionEventArgs)
        {
            //
        }


        public static void Sleep(int sleepTime) => Thread.Sleep(sleepTime);
        private static void IncrementDataGridId() => AutoTesterWindow.DataGridId++;
        private static void ResetStopwatch() => SilkStopwatch.Restart();

        private static void UpdateAnzeige(FunctionEventArgs functionEventArgs)
        {


            AutoTesterWindow.UpdateDataGrid(new TestAusgabe(
                AutoTesterWindow.DataGridId,
                $"{SilkStopwatch.ElapsedMilliseconds}ms",
                Model.AutoTester.TestErgebnis.Timeout,
                "12",
                "new PlcDatenTypen.Uint(\"3\")",
                "new PlcDatenTypen.Uint(\"2\")",
                "abc"));

        }

    }
}