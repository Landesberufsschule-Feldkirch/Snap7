using SoftCircuits.Silk;
using TestAutomat.Model;

namespace TestAutomat.Silk
{
    public partial class Silk
    {
        public static void Runtime_Begin(object sender, BeginEventArgs e)
        {
            const string data = "";
            e.UserData = data;

            AutoTesterWindow.UpdateDataGrid(new TestAusgabe(
                AutoTesterWindow.DataGridId++,
                $"{SilkStopwatch.ElapsedMilliseconds}ms",
                AutoTester.TestErgebnis.TestStart,
                " ",        // DigInput 
                " ",        // DigOutput Soll
                " ",        // DigOutput Ist
                " "));
        }

        private static void Runtime_End(object sender, EndEventArgs e)
        {
            AutoTesterWindow.UpdateDataGrid(new TestAusgabe(
                AutoTesterWindow.DataGridId++,
                $"{SilkStopwatch.ElapsedMilliseconds}ms",
                AutoTester.TestErgebnis.TestEnde,
                " ",        // DigInput 
                " ",        // DigOutput Soll
                " ",        // DigOutput Ist
                " "));
        }
    }
}