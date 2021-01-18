using System;
using System.IO;
using TestAutomat.AutoTester.Config;
using TestAutomat.PlcDisplay.Config;

namespace TestAutomat.AutoTester.Model
{
    public class AutoTester
    {
        public GetTestConfig GetTestConfig { get; set; }
        public GetPlcConfig GetPlcConfig { get; set; }
        public AutoTesterWindow AutoTesterWindow { get; set; }
        public  Kommunikation.Datenstruktur Datenstruktur { get; set; }

        public AutoTester(AutoTesterWindow autoTesterWindow, FileSystemInfo aktuellesProjekt, Kommunikation.Datenstruktur datenstruktur)
        {
            AutoTesterWindow = autoTesterWindow;
            Datenstruktur = datenstruktur;
            GetTestConfig = new GetTestConfig(aktuellesProjekt);
            GetPlcConfig = new GetPlcConfig(aktuellesProjekt);
            GetTestConfig.KonfigurationTesten();

            System.Threading.Tasks.Task.Run(TestRunnerTask);
        }
        private void TestRunnerTask()
        {
            foreach (var einzelneZeile in GetTestConfig.TestConfig.AutomatischeSoftwareTests)
            {
                switch (einzelneZeile.Befehl)
                {
                    case TestBefehle.Init: AutoTesterWindow.UpdateDataGrid(AlleTestBefehle.TestBefehlInit(einzelneZeile, Datenstruktur)); break;
                    case TestBefehle.EingaengeTesten: AutoTesterWindow.UpdateDataGrid(AlleTestBefehle.TestBefehlEingaengeTesten(einzelneZeile)); break;
                    case TestBefehle.Pause: AutoTesterWindow.UpdateDataGrid(AlleTestBefehle.TestBefehlPause(einzelneZeile)); break;

                    case TestBefehle.Default: break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}