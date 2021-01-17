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
        public AutoTester(AutoTesterWindow autoTesterWindow, FileSystemInfo aktuellesProjekt)
        {
            AutoTesterWindow = autoTesterWindow;
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
                    case TestBefehle.Init: AutoTesterWindow.UpdateForReal(AlleTestBefehle.TestBefehlInit(einzelneZeile)); break;
                    case TestBefehle.EingaengeTesten: AutoTesterWindow.UpdateForReal(AlleTestBefehle.TestBefehlEingaengeTesten(einzelneZeile)); break;
                    case TestBefehle.Pause: AutoTesterWindow.UpdateForReal(AlleTestBefehle.TestBefehlPause(einzelneZeile)); break;

                    case TestBefehle.Default: break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}