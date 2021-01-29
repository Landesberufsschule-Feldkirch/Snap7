using System;
using SoftCircuits.Silk;
using TestAutomat.AutoTester.Model;

namespace TestAutomat.AutoTester.Silk
{
    public partial class Silk
    {
        public static void Sleep(int sleepTime)
        {
            var now = DateTime.Now;
            do
            {
                // Application.DoEvents();
            } while (now.AddSeconds(5) > DateTime.Now);
        }

        private static void GetDigitaleAusgaenge(FunctionEventArgs functionEventArgs)
        {
            //
        }

        private static void SetDigitaleEingaenge(FunctionEventArgs functionEventArgs)
        {
            //
        }

        private static void UpdateAnzeige(FunctionEventArgs functionEventArgs)
        {

            AutoTesterWindow.UpdateDataGrid(new TestAusgabe(
                1,
                "0",
                Model.AutoTester.TestErgebnis.Timeout,
                "12",
                new PlcDatenTypen.Uint("3"),
                new PlcDatenTypen.Uint("2"),
                "abc"));
           
        }

    }
}