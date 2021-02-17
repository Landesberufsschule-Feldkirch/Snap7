using SoftCircuits.Silk;
using TestAutomat.AutoTester.Model;

namespace TestAutomat.AutoTester.Silk
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
                Model.AutoTester.TestErgebnis.TestStart,
                "", "", ""));
        }

        private static void Runtime_End(object sender, EndEventArgs e)
        {
            AutoTesterWindow.UpdateDataGrid(new TestAusgabe(
                AutoTesterWindow.DataGridId++,
                $"{SilkStopwatch.ElapsedMilliseconds}ms",
                Model.AutoTester.TestErgebnis.TestEnde,
                "", "", ""));
        }
    }
}