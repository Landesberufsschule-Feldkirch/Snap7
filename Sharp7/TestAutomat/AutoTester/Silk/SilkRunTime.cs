using System;
using System.Linq;
using SoftCircuits.Silk;

namespace TestAutomat.AutoTester.Silk
{
    public static partial class Silk
    {
        public static void Runtime_Begin(object sender, BeginEventArgs e)
        {
            const string data = "";
            e.UserData = data;
        }
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
            }
        }
        private static void Runtime_End(object sender, EndEventArgs e) { /* nichts zu tun */ }
        public static void Sleep(int sleepTime)
        {
            var now = DateTime.Now;
            do
            {
                // Application.DoEvents();
            } while (now.AddSeconds(5) > DateTime.Now);
        }
    }
}